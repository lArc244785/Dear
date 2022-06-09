using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : ActiveToolBase
{
    #region InteractionSensor
    private Transform m_interactionSensorTr;
    private Transform interactionSensorTr
    {
        get
        {
            return m_interactionSensorTr;
        }
    }
    [Header("InteractionSensor")]
    [SerializeField]
    private Vector2 m_interactionSensorOffset;
    private Vector2 interactionSensorOffset
    {
        get
        {
            return m_interactionSensorOffset;
        }
    }


    private List<InteractionGrapping> m_rangeInteractionObjectList = new List<InteractionGrapping>();
    private List<InteractionGrapping> rangeInteractionObjectList
    {
        get
        {
            return m_rangeInteractionObjectList;
        }
    }

    private CircleCollider2D m_interactionSensorCollider;
    private CircleCollider2D interactionSensorCollider
    {
        get
        {
            return m_interactionSensorCollider;
        }
    }

    [SerializeField]
    private float m_interactionSensorRadiuse;
    private float interactionSensorRadiuse
    {
        get
        {
            return m_interactionSensorRadiuse;
        }
    }
    [SerializeField]
    private LayerMask m_interactionGrappingLayerMask;
    private LayerMask interactionGrappingLayerMask
    {
        get
        {
            return m_interactionGrappingLayerMask;
        }
    }
    #endregion

    [SerializeField]
    private GrappingGunMovementData m_data;
    public GrappingGunMovementData data
    {
        get
        {
            return m_data;
        }
    }



    private RopeRenderer m_ropeRenderer;
    private RopeRenderer ropeRenderer
    {
        get
        {
            return m_ropeRenderer;
        }
    }


    #region FireTr
    private Transform m_fireTr;
    public Transform fireTr
    {
        get { return m_fireTr; }
    }
    #endregion

    #region Hook
    private Hook m_hook;
    public Hook hook
    {
        get { return m_hook; }
    }

    #endregion



    #region state
    public enum State
    {
        None, Fire, Grapping, Pull, A
    }

    private State m_currentState;
    public State currentState
    {
        set
        {
            m_currentState = value;
        }
        get { return m_currentState; }
    }
    #endregion


    #region Pick 
    public enum PickType
    {
        None, Slingshot
    }

    private PickType m_pickType;
    private PickType pickType
    {
        set
        {
            m_pickType = value;
        }
        get
        {
            return m_pickType;
        }
    }

    #endregion

    private Vector2 m_pullTargetVelcoity;
    private Vector2 pullTargetVelocity
    {
        get
        {
            return m_pullTargetVelcoity;
        }
    }


    private Vector2 m_targetPos;
    public Vector2 targetPos
    {
        get
        {
            return m_targetPos;
        }
    }


    private Vector2 m_startPos;
    public Vector2 startPos 
    {
        get
        {
            return m_startPos;
        }
    }


    public override void Init(UnitPlayer player)
    {
        base.Init(player);
        ComponnetInit();
        isControl = true;
        InteractionSensorOff();
    }

    private void ComponnetInit()
    {

        m_fireTr = transform.Find("FirePoint");
        m_hook = m_fireTr.GetComponentInChildren<Hook>();
        m_ropeRenderer = transform.GetComponentInChildren<RopeRenderer>();
        m_interactionSensorTr = transform.Find("InteractionSensor");
        m_interactionSensorCollider = m_interactionSensorTr.GetComponent<CircleCollider2D>();

        hook.Init(this);
        ropeRenderer.Init(this);
        interactionSensorCollider.radius = interactionSensorRadiuse;

    }

    #region InteractionSensor Method
    private void InteractionSensorOn()
    {
        m_interactionSensorTr.parent = player.transform;
        m_interactionSensorTr.localPosition = interactionSensorOffset;
    }

    private void InteractionSensorOff()
    {
        m_interactionSensorTr.parent = transform;
        m_interactionSensorTr.localPosition = Vector2.zero;
    }

    //반환값으로 해당 Index를 넘겨줌
    public int AddInteraction(InteractionGrapping interaction)
    {
        rangeInteractionObjectList.Add(interaction);
        return rangeInteractionObjectList.Count - 1;
    }

    public void RemoveInteraction(InteractionGrapping interaction)
    {
        rangeInteractionObjectList.Remove(interaction);
    }


    #endregion
    public override void Enter()
    {
        base.Enter();

        InteractionSensorOn();

    }

    public override void Exit()
    {
        base.Exit();
        InteractionSensorOff();
    }


    public override void LeftUse()
    {

        base.LeftUse();

        if (currentState != State.None)
            return;

        RaycastHit2D hit2D = Physics2D.Raycast(InputManager.instance.inGameMousePosition2D, Vector2.zero, Mathf.Infinity, interactionGrappingLayerMask);
        if (!hit2D)
            return;

        InteractionGrappingHandler handler = hit2D.collider.GetComponent<InteractionGrappingHandler>();
        if (!handler)
            return;

        if (!handler.interactionGrapping.isCanInteraction)
            return;

        Fire((Vector2)handler.interactionGrapping.transform.position);

    }



    public override void LeftCancle()
    {
        base.LeftCancle();

        //if (currentState == State.Fire)
        //    Cancle();
    }

    private void AllInteractionOff()
    {
        foreach (InteractionGrapping interaction in rangeInteractionObjectList)
        {
            interaction.OffInteraction();
        }
    }

    private void AllInteractionOn()
    {
        foreach (InteractionGrapping interaction in rangeInteractionObjectList)
        {
            if(IsOnInteractionRange(interaction.transform.position))
            interaction.OnInteraction();
        }
    }


    private void UpdateInteraction()
    {
        UIManager.instance.mouseCursor.SetMouseCursor(CustomMouseCursor.CursorType.Aim);


        foreach (InteractionGrapping interaction in rangeInteractionObjectList)
        {
            if (IsOnInteractionRange(interaction.transform.position))
            {
                interaction.OnInteraction();
                if(CanInteraction(interaction))
                {
                    UIManager.instance.mouseCursor.SetMouseCursor(CustomMouseCursor.CursorType.Hook);
                }
            }
            else
                interaction.OffInteraction();
        }
    }

    private bool CanInteraction(InteractionGrapping interaction)
    {
        float distance = Vector2.Distance(
            InputManager.instance.inGameMousePosition2D,
            (Vector2)interaction.transform.position);
        

        return distance < interaction.GetClickRange();
    }


    private void Fire(Vector2 targetPos)
    {
        UIManager.instance.mouseCursor.SetImageVisible(false);
        float r = Utility.GetRotaionAngleByTargetPosition(player.transform.position, targetPos, 90.0f);
        player.transform.rotation = Utility.GetRoationZ(r);
        
       
        m_startPos = (Vector2)fireTr.position;
        m_targetPos = targetPos;

        AllInteractionOff();

        player.movementManager.currentState = PlayerMovementManager.State.None;
        player.rig2D.gravityScale = 0.0f;
        player.rig2D.velocity = Vector2.zero;

        player.animationManager.TriggerRopeFire();
        

        Vector2 dir = targetPos - (Vector2)hook.transform.position;
        dir.Normalize();


        hook.Fire(startPos, targetPos);

        player.inputPlayer.SetControl(false);

        Vector2 playerTarget = targetPos - (Vector2)player.transform.position;
        if (playerTarget.x > 0)
            player.movementManager.Trun(Vector2.right);
        else
            player.movementManager.Trun(Vector2.left);


     
        ropeRenderer.DrawInit(fireTr.position, dir);



        player.sound.RopeShoot();

        currentState = State.Fire;
    }

    private void Cancle()
    {

        GrappingReset();

        player.transform.rotation = Quaternion.identity;

        if (!player.movementManager.IsGrounded())
            player.movementManager.currentState = PlayerMovementManager.State.Air;
        else
            player.movementManager.currentState = PlayerMovementManager.State.Ground;


        player.rig2D.gravityScale = 1.0f;

        player.inputPlayer.SetControl(true);

        UIManager.instance.mouseCursor.SetMouseCursor(CustomMouseCursor.CursorType.Aim);
        UIManager.instance.mouseCursor.SetImageVisible(true);


        Invoke("CoolTime", data.coolTime);
    }

    private void CoolTime()
    {
        AllInteractionOn();
    }


    private void GrappingReset()
    {
        currentState = State.None;

        player.animationManager.RopeToAirAnimation();

        hook.transform.parent = fireTr;
        hook.transform.localPosition = Vector3.zero;
        hook.transform.localRotation = Quaternion.identity;


        ropeRenderer.OffDraw();
        hook.SetGameObjectActive(false);

        pickType = PickType.None;
    }





    public void HookPick(PickType type)
    {
        currentState = State.Grapping;
        pickType = type;

        if (pickType == PickType.Slingshot)
        {
            PullSetting();
            player.animationManager.TriggerRopeMove();
            player.particleManager.RopePickEffect(hook.transform.position);
        }


    }

    private float GetHookDistance()
    {
        return Vector2.Distance((Vector2)hook.transform.position, player.unitPos);
    }




    private void Update()
    {
        if (currentState == State.None)
        {
            UpdateInteraction();
        }

        if (currentState == State.Pull)
        {
            PullUpdate();
        }

    }




    private void FireUpdate()
    {
        if (GetHookDistance() > data.maxDistance)
            Cancle();
    }



    private void PullUpdate()
    {
        Pull();
        if (isContant(player.transform.position, targetPos))
        {
            Cancle();
            player.sound.RopeJump();
            player.movementManager.currentState = PlayerMovementManager.State.RopeJump;
        }
    }




    private void PullSetting()
    {
        Vector2 hookPos = (Vector2)hook.transform.position;

        currentPullTime = 0.0f;
        player.sound.RopeMove();
        currentState = State.Pull;
    }




    public GrapplingGun.PickType GetPickType(string tag)
    {
        GrapplingGun.PickType resultType = GrapplingGun.PickType.None;

        if (tag == "HookShlingshot")
            resultType = PickType.Slingshot;


        return resultType;
    }

    private float currentPullTime { set; get; }


    private void Pull()
    {
        currentPullTime += Time.deltaTime;

        LerpMovement(player.transform, startPos, targetPos, currentPullTime, data.pullSpeed, data.pullProgessionCurve);
    }


    public void LerpMovement(Transform moveTr, Vector2 startPos, Vector2 targetPos, float currentTime, float speed, AnimationCurve progessionCurve)
    {
        float delta = currentTime * speed;
        if (delta > 1.0f)
            delta = 1.0f;
        float progession = progessionCurve.Evaluate(delta);
        Vector2 CurrentPos = Vector2.Lerp(startPos, targetPos, progession);

        moveTr.position = CurrentPos;
    }



    public bool isContant(Vector2 currentPos, Vector2 targetPos)
    {
        return Vector2.Equals(currentPos, targetPos);
    }

    public bool IsOnInteractionRange(Vector2 interactionPos)
    {
        float distance = Vector2.Distance(player.transform.position, interactionPos);

        return distance > 5.0f;
    }

    private void OnDrawGizmos()
    {
        if (player == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.transform.position, interactionSensorRadiuse);

    }


}
