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
    private Transform fireTr
    {
        get { return m_fireTr; }
    }
    #endregion

    #region Hook
    private Hook m_hook;
    private Hook hook
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
            interaction.OnInteraction();
        }
    }


    private void Fire(Vector2 targetPos)
    {
        currentState = State.Fire;

        AllInteractionOff();

        player.movementManager.currentState = PlayerMovementManager.State.None;
        player.rig2D.gravityScale = 0.0f;
        player.rig2D.velocity = Vector2.zero;

        player.animationManager.RopeAnimation();
        player.shoulder.setLookPosition(targetPos);

        Vector2 dir = targetPos - (Vector2)hook.transform.position;
        dir.Normalize();


        hook.Fire(dir, targetPos);

        player.inputPlayer.SetControl(false);

        ropeRenderer.isDraw = true;
    }

    private void Cancle()
    {

        GrappingReset();

        if (!player.movementManager.IsGrounded())
            player.movementManager.currentState = PlayerMovementManager.State.Air;
        else
            player.movementManager.currentState = PlayerMovementManager.State.Ground;


        player.inputPlayer.SetControl(true);

        Invoke("CoolTime", data.coolTime);
    }

    private void CoolTime()
    {
        AllInteractionOn();
    }


    private void GrappingReset()
    {
        currentState = State.None;
        player.shoulder.SetMouse();
        player.animationManager.RopeToAirAnimation();


        hook.rig2D.velocity = Vector2.zero;
        hook.rig2D.bodyType = RigidbodyType2D.Kinematic;
        hook.transform.parent = fireTr;
        hook.transform.localPosition = Vector3.zero;
        hook.transform.localRotation = Quaternion.identity;


        ropeRenderer.isDraw = false;

        pickType = PickType.None;
    }





    public void HookPick(PickType type)
    {
        currentState = State.Grapping;
        pickType = type;

        if (pickType == PickType.Slingshot)
        {
            PullSetting();
        }


    }

    private float GetHookDistance()
    {
        return Vector2.Distance((Vector2)hook.transform.position, player.unitPos);
    }




    private void Update()
    {
        if (currentState == State.None)
            return;

        if (currentState == State.Fire)
        {
            FireUpdate();
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

    private Vector2 m_nextPullPos;
    private float m_pullDistance;
    private float m_nextPullDistacne;



    private void PullUpdate()
    {
        if (TargetAcceleration(
            player.rig2D,
            data.pullAcclelation,
            data.pullVelcoityPower,
            pullDir,
            data.pullMaxSpeed,
            hook.transform.position))
        {
            Cancle();
            Debug.Log("RR: " + player.rig2D.velocity);
            player.rig2D.velocity = Vector2.ClampMagnitude(player.rig2D.velocity, data.pullStopMaxLength);
        }

    }


    private Vector2 pullDir { set; get; }


    private void PullSetting()
    {
        Vector2 hookPos = (Vector2)hook.transform.position;


        pullDir = hookPos - player.unitPos;
        pullDir.Normalize();


        currentState = State.Pull;

        //Cancle();

    }




    public GrapplingGun.PickType GetPickType(string tag)
    {
        GrapplingGun.PickType resultType = GrapplingGun.PickType.None;

        if (tag == "HookShlingshot")
            resultType = PickType.Slingshot;


        return resultType;
    }

    public bool TargetAcceleration(Rigidbody2D rig, float accelRate, float velcoityPower, Vector2 targetDir, float targetSpeed, Vector2 targetPos)
    {
        Vector2 currentPos = (Vector2)rig.transform.position;
        Vector2 nextMovePos = currentPos + (rig.velocity * Time.deltaTime);

        float currentDistance = Vector2.Distance(currentPos, targetPos);
        float nextDistacen = Vector2.Distance(nextMovePos, targetPos);

        if (currentDistance < nextDistacen)
        {
            rig.transform.position = targetPos;
            return true;
        }



        Vector2 targetVelocity = targetDir * targetSpeed;

        Vector2 velocityDif;

        velocityDif = targetVelocity - rig.velocity;

        Vector2 movement;


        movement.x = Mathf.Pow(Mathf.Abs(velocityDif.x) * accelRate, velcoityPower) * Mathf.Sign(velocityDif.x);
        movement.y = Mathf.Pow(Mathf.Abs(velocityDif.y) * accelRate, velcoityPower) * Mathf.Sign(velocityDif.y);

        rig.AddForce(movement);

        return false;


    }


}
