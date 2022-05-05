using UnityEngine;

public class GrapplingGun : ActiveToolBase
{
    #region Option
    [Header("Option")]
    [SerializeField]
    private float m_maxDistance;
    private float maxDistance
    {
        get { return m_maxDistance; }
    }

    [SerializeField]
    private float m_hookSpeed;
    private float hookSpeed
    {
        get
        {
            return m_hookSpeed;
        }
    }

    [SerializeField]
    private float m_pullSpeed;
    private float pullSpeed
    {
        get
        {
            return m_pullSpeed;
        }
    }


    [SerializeField]
    private float m_slingshotSpeed;
    private float slingshotSpeed
    {
        get
        {
            return m_slingshotSpeed;
        }
    }

    #endregion




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
        None, Fire, Grapping, Pull
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

    #region RopeRender
    private RopeRenderer m_ropeRenderer;
    private RopeRenderer ropeRenderer
    {
        get
        {
            return m_ropeRenderer;
        }
    }
    #endregion

    #region Slingshot
    private enum SlingshotState
    {
        none, shot
    }
    private SlingshotState m_currentSlingshotState;
    private SlingshotState currentSlingshotState
    {
        set
        {
            m_currentSlingshotState = value;
        }
        get
        {
            return m_currentSlingshotState;
        }
    }

    #endregion

    #region Debug
    private Vector3 m_pullStartPos;
    private Vector3 m_hookPos;
    private Vector3 m_shlinshotPos;
    #endregion


    public override void Init(UnitPlayer player)
    {
        base.Init(player);
        ComponnetInit();
        isControl = true;
    }

    private void ComponnetInit()
    {

        m_fireTr = transform.Find("FirePoint");
        m_hook = m_fireTr.GetComponentInChildren<Hook>();
        m_ropeRenderer = transform.GetComponentInChildren<RopeRenderer>();

        hook.Init(this);
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void LeftUse()
    {

        base.LeftUse();
        if (currentState == State.None)
            Fire();

    }

    public override void LeftCancle()
    {
        base.LeftCancle();

        if (currentState == State.Fire)
            Cancle();
    }



    private void Fire()
    {
        currentState = State.Fire;

        player.animationManager.RopeAnimation();
        player.shoulder.setLookPosition(InputManager.instance.inGameMousePosition2D);

        Vector2 dir = InputManager.instance.inGameMousePosition2D - (Vector2)hook.transform.position;
        dir.Normalize();


        hook.Fire(dir, hookSpeed);

        m_pullStartPos = m_fireTr.position;

        player.inputPlayer.isMoveControl = false;
        ropeRenderer.isDraw = true;
    }

    private void Cancle()
    {
        GrappingReset();

        if (!player.movementManager.IsGrounded())
            player.movementManager.currentState = PlayerMovementManager.State.Air;
        else
            player.movementManager.currentState = PlayerMovementManager.State.Ground;

        player.inputPlayer.isMoveControl = true;
    }

    private void GrappingReset()
    {
        player.shoulder.SetMouse();
        player.animationManager.RopeToAirAnimation();

        hook.transform.parent = fireTr;
        hook.transform.localPosition = Vector3.zero;
        hook.transform.localRotation = Quaternion.identity;

        ropeRenderer.isDraw = false;

        pickType = PickType.None;
        currentState = State.None;
    }





    public void HookPick(PickType type)
    {
        currentState = State.Grapping;
        pickType = type;

        m_grappingStartDistance = GetDistance();

        if (pickType == PickType.Slingshot)
        {
            SlingshotSetting();
        }

        m_hookPos = hook.transform.position;

    }

    private float GetDistance()
    {
        return Vector2.Distance((Vector2)hook.transform.position, player.unitPos);
    }




    private void Update()
    {
        if (currentState == State.Fire)
        {
            FireUpdate();
        }

        if (currentState == State.Pull)
        {
            PullUpdate();
        }

        if (currentSlingshotState == SlingshotState.shot)
        {
            SlingshotUpdate();
        }
    }


    private void FireUpdate()
    {
        if (GetDistance() > maxDistance)
            Cancle();
    }

    private void PullUpdate()
    {
        float playerToHookDistance = GetDistance();
        if (playerToHookDistance < 0.5f)
        {
            player.rig2D.velocity = Vector2.zero;
            Cancle();
            return;
        }

        if (!isSlingshot && InputManager.instance.leftMouseState == InputManager.InputContextState.cancle)
        {
            if (playerToHookDistance > slingshotAbleStartPoint || playerToHookDistance < slingshotAbleEndPoint)
            {
                player.rig2D.velocity = Vector2.zero;
                Cancle();
            }
            else
            {
                GrappingReset();
                Slingshot();
            }
        }
    }

    private void Slingshot()
    {
        currentSlingshotState = SlingshotState.shot;
        player.rig2D.velocity = m_pullDir * slingshotSpeed;
    }


    private float m_grappingStartDistance;
    #region SlingShotDurringPoint
    private float m_slingshotAbleStartPoint;
    private float slingshotAbleStartPoint
    {
        get
        {
            return m_slingshotAbleStartPoint;
        }
    }

    private float m_slingshotAbleEndPoint;
    private float slingshotAbleEndPoint
    {
        get
        {
            return m_slingshotAbleEndPoint;
        }
    }

    private Vector2 m_shlingshotPoint;
    private Vector2 shlingshotPoint
    {
        set
        {
            m_shlingshotPoint = value;
        }
        get
        {
            return m_shlingshotPoint;
        }
    }

    private Vector2 m_pullDir;

    #endregion
    private void SlingshotSetting()
    {
        Vector2 hookPos = (Vector2)hook.transform.position;

        player.movementManager.currentState = PlayerMovementManager.State.None;
        player.rig2D.gravityScale = 0.0f;

        player.rig2D.velocity = Vector2.zero;

        m_slingshotAbleStartPoint = Mathf.Lerp(0.0f, m_grappingStartDistance, 0.8f);
        m_slingshotAbleEndPoint = Mathf.Lerp(0.0f, m_grappingStartDistance, 0.2f);

        m_pullDir = hookPos - player.unitPos;
        m_pullDir.Normalize();

        //pull
        player.rig2D.velocity = m_pullDir * pullSpeed;


        shlingshotPoint = (Vector2)hook.transform.position;
        shlingshotPoint += m_pullDir * (m_grappingStartDistance * 0.4f);

        m_shlinshotPos = shlingshotPoint;

        currentState = State.Pull;

        isSlingshot = false;
    }

    private bool isSlingshot;

    private Vector2 m_slingshotPoint;


    private void SlingshotUpdate()
    {
        if (Vector2.Distance(player.unitPos, shlingshotPoint) < 0.5f)
        {
            player.movementManager.currentState = PlayerMovementManager.State.Air;
            player.inputPlayer.isMoveControl = true;
        }
    }


    public GrapplingGun.PickType GetPickType(string tag)
    {
        GrapplingGun.PickType resultType = GrapplingGun.PickType.None;

        if (tag == "HookShlingshot")
            resultType = PickType.Slingshot;


        return resultType;
    }

    private void OnDrawGizmos()
    {
        if (currentState == State.Pull)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(m_pullStartPos, m_hookPos);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(m_hookPos, m_shlinshotPos);
        }

    }


}
