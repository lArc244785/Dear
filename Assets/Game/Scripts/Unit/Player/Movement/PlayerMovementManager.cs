using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{

    #region UnitBase
    private UnitPlayer m_player;
    public UnitPlayer player
    {
        get
        {
            return m_player;
        }
    }
    #endregion

    #region MovementData
    [Header("MovementData")]
    [SerializeField]
    private MovementData m_movementData;
    public MovementData movementData
    {
        set
        {
            m_movementData = value;
        }
        get
        {
            return m_movementData;
        }
    }
    #endregion

    #region State
    public enum State
    {
        None = -1, Ground, Air, Wall, RopeJump, Hit,
        Total
    }


    private I_MovementState[] m_States;

    [SerializeField]
    private State m_currentState;

    public State currentState
    {
        set
        {
            if (m_currentState != State.None)
                m_States[(int)m_currentState].Exit(this);

            m_currentState = value;

            if (m_currentState != State.None)
                m_States[(int)m_currentState].Enter(this);
        }
        get
        {
            return m_currentState;
        }
    }


    public bool isJump { set; get; }

    public bool isWallJump { set; get; }
    public bool isWallSilde { set; get; }

    public bool isWallGrip { set; get; }
    public bool isWallGripInteraction { set; get; }

    public bool isRopeCancleRebound { set; get; }
    public bool isRopeRebound { set; get; }

    public bool isRopeReboundDirRight { set; get; }

    public bool isOnInteractionJumpObject { set; get; }
    public bool isInteractionJump { set; get; }
    #endregion


    #region Sensor

    private CircleSensor m_groundSensor;
    public CircleSensor groundSensor { get { return m_groundSensor; } }

    private WallSensor m_wallSensor;
    public WallSensor wallSensor { get { return m_wallSensor; } }
    #endregion

    #region Coyote System
    [Header("Coyote System")]
    [SerializeField]
    private CoyoteSystem m_coyoteSystem;

    public CoyoteSystem coyoteSystem { get { return m_coyoteSystem; } }
    #endregion

    #region JumpCount
    private int m_jumpCount;
    public int jumpCount
    {
        get
        {
            return m_jumpCount;
        }
        set
        {
            m_jumpCount = Mathf.Clamp(value, 0, m_movementData.maxJumpCount);

            player.animationManager.jumpCount = m_jumpCount;
        }
    }
    #endregion

    #region Trun
    private Vector2 m_lastLookDir;
    #endregion

    #region Shoulder
    public Shoulder shoulder { get { return player.shoulder; } }
    #endregion

    #region GroundPound
    private Vector3[] m_groundPoundPath;
    public Vector3[] groundPoundPath
    {
        get
        {
            return m_groundPoundPath;
        }
    }

    private CircleSensor m_groundPoundSensor;
    public CircleSensor groundPoundSensor
    {
        get
        {
            return m_groundPoundSensor;
        }
    }
    #endregion


    public void Init(UnitPlayer player)
    {
        m_player = player;

        ComponentInit();

        m_coyoteSystem = new CoyoteSystem();
        m_coyoteSystem.Init(m_movementData);

        m_lastLookDir = Vector2.left;

        m_groundPoundPath = new Vector3[movementData.groundPoundReadyPathLenth];

        StatesInit();
        currentState = State.Ground;

        isInteractionJump = false;
        isJump = false;
    }

    private void ComponentInit()
    {

        Transform sensorsTr = transform.Find("Sensors");
        m_wallSensor = sensorsTr.GetComponentInChildren<WallSensor>();
        m_groundSensor = sensorsTr.Find("GroundSensor").GetComponent<CircleSensor>();
        m_groundPoundSensor = sensorsTr.Find("GroundPoundSensor").GetComponent<CircleSensor>();

        wallSensor.Init();

    }


    private void StatesInit()
    {
        m_States = new I_MovementState[(int)State.Total];

        m_States[(int)State.Ground] = new MovementGroundState();
        m_States[(int)State.Air] = new MovementStateAir();
        m_States[(int)State.Wall] = new MovementWallState();
        m_States[(int)State.RopeJump] = new MovementRopeJumpState();
        m_States[(int)State.Hit] = new MovementStateHit();


    }





    private void Update()
    {
        if (player == null)
            return;
        if (!player.isInit)
            return;

        if (currentState == State.None)
            return;

        m_States[(int)currentState].UpdateExcute(this);

    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        if (currentState == State.None)
            return;

        m_States[(int)currentState].FixedExcute(this);
    }

    public void SetGravity(float gravitySclae)
    {
        player.rig2D.gravityScale = gravitySclae;
    }

    public bool IsGrounded()
    {
        return m_groundSensor.IsOverlap();
    }

    public bool IsWallGrouned()
    {
        return IsWallLeft() || IsWallRight();
    }

    public bool IsWallRight()
    {
        return m_wallSensor.IsRightSensorGrounded();
    }

    public bool IsWallLeft()
    {
        return m_wallSensor.IsLeftSensorGrounded();
    }

    public void Run(float lerpAmount, bool isGetInput)
    {
        if (Mathf.Abs(player.rig2D.velocity.x) > movementData.runMaxSpeed)
        {
            float maxVelocityX = movementData.runMaxSpeed * Mathf.Sign(player.rig2D.velocity.x);
            player.rig2D.velocity = new Vector2(maxVelocityX, player.rig2D.velocity.y);
        }



        float inputMoveDirX = 0.0f;
        if (isGetInput)
            inputMoveDirX = player.inputPlayer.moveDir.x;

        //Debug.Log(inputMoveDirX);

        float rigVelocityX = player.rig2D.velocity.x;

        float targetSpeed = inputMoveDirX * movementData.runMaxSpeed;
        float SpeedDif = targetSpeed - rigVelocityX;

        #region Acceleration Rate
        float accleRate;
        if (coyoteSystem.lastOnGroundTime >= 0.0f)
        {
            accleRate = movementData.runAccel;
            if (targetSpeed == Mathf.Epsilon)
                accleRate = movementData.runDeccel;
        }
        else
        {
            accleRate = movementData.runAccel * movementData.accelInAir;
            if (targetSpeed == Mathf.Epsilon)
                accleRate = movementData.runDeccel * movementData.deccelInAir;
        }

        if (Mathf.Abs(rigVelocityX) > Mathf.Abs(targetSpeed))
            accleRate = 0.0f;
        #endregion

        #region Velocity Power
        float velocityPower;
        if (inputMoveDirX == Mathf.Epsilon)
            velocityPower = movementData.stopPower;
        else if (Mathf.Abs(inputMoveDirX) > 0.0f && Mathf.Sign(targetSpeed) != Mathf.Sign(rigVelocityX))
        {
            velocityPower = movementData.turnPower;
        }
        else
            velocityPower = movementData.accelPower;
        #endregion



        float movement = Mathf.Pow(Mathf.Abs(SpeedDif) * accleRate, velocityPower) * Mathf.Sign(SpeedDif);
        movement = Mathf.Lerp(rigVelocityX, movement, lerpAmount);
        //Debug.Log("Run: " + movement);
        player.rig2D.AddForce(movement * Vector2.right);
    }

    public void Resistance(float amount)
    {
        Vector2 force = player.rig2D.velocity.normalized * amount;
        force.x = Mathf.Min(Mathf.Abs(force.x), Mathf.Abs(player.rig2D.velocity.x));
        force.y = Mathf.Min(Mathf.Abs(force.y), Mathf.Abs(player.rig2D.velocity.y));

        force.x *= Mathf.Sign(player.rig2D.velocity.x);
        force.y *= Mathf.Sign(player.rig2D.velocity.y);

        //Debug.Log("Resistance");
        player.rig2D.AddForce(-force, ForceMode2D.Impulse);

    }

    public void Jump(float force)
    {
       // Debug.Log("Jump: " + force);
        player.sound.Jump();
        player.animationManager.TriggerJump();


        if (player.rig2D.velocity.y < 0.0f)
            force -= player.rig2D.velocity.y;
        //Debug.Log("Jump " + jumpCount);
        player.rig2D.AddForce(force * Vector2.up, ForceMode2D.Impulse);

        coyoteSystem.ResetJumpEnterTime();
        coyoteSystem.ResetGroundTime();


        isJump = true;
        if (isOnInteractionJumpObject)
        {
            isInteractionJump = true;
            isOnInteractionJumpObject = false;
        }

    }

    public void JumpCut()
    {
        player.rig2D.AddForce(Vector2.down * player.rig2D.velocity.y * (1 - movementData.jumpCutMultiplier), ForceMode2D.Impulse);
        coyoteSystem.ResetJumpExitTime();
      //  Debug.Log("Jump Cut");
    }

    public void Trun(Vector2 lookDir)
    {
        if (lookDir.x != m_lastLookDir.x)
        {
            player.model.flipX = !player.model.flipX;
        }
        m_lastLookDir = lookDir;

        player.madTrackingPoint.UpdateOffset(IsLookDirRight());
    }

    public void TrunUpdate()
    {
        if (player.inputPlayer.moveDir.x == 0.0f)
            return;

        Trun(player.inputPlayer.moveDir);
    }

    public Vector2 lastLookDir
    {
        get
        {
            return m_lastLookDir;
        }
    }

    public void Climbing(float maxSpeed, float dirY)
    {
        float targetSpeed = dirY * movementData.runMaxSpeed;

        float speedDif = targetSpeed - player.rig2D.velocity.y;
        float accleRate = targetSpeed > 0.01f ? movementData.climbingAccel : movementData.climbingDeccel;
        float velocityPower = movementData.accelPower;

        if (Mathf.Abs(player.rig2D.velocity.y) > Mathf.Abs(targetSpeed))
            accleRate = 0.0f;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accleRate, velocityPower) * Mathf.Sign(speedDif);
        Debug.Log(movement);

        Debug.Log("Climbing");
        player.rig2D.AddForce(movement * Vector2.up);
    }

    private Vector2 m_hitImfectDir;
    public Vector2 hitImfectDir
    {
        private set
        {
            m_hitImfectDir = value;
        }
        get
        {
            return m_hitImfectDir;
        }
    }

    public void VectorJump(Vector2 force)
    {
        if (Mathf.Sign(player.rig2D.velocity.x) != Mathf.Sign(force.x))
            force.x -= player.rig2D.velocity.x;

        if (player.rig2D.velocity.y < 0.0f)
            force.y -= player.rig2D.velocity.y;
        player.rig2D.AddForce(force, ForceMode2D.Impulse);
    }



    public void HitMovement(Vector2 imfectDir)
    {
        hitImfectDir = imfectDir;
        currentState = State.Hit;
    }

    public void ClampJumpVelocity()
    {
        Debug.Log("A: " + player.rig2D.velocity);
        player.rig2D.velocity = Vector2.ClampMagnitude(player.rig2D.velocity, 5.0f);
        Debug.Log("B: " + player.rig2D.velocity);
    }

    public Vector3[] CalculationGroundPoundPath(float moveY)
    {
        Vector3 startPos = (Vector3)player.unitPos;

        int length = movementData.groundPoundReadyPathLenth;

        float ratio = 1.0f / length;

        for(int i = 0; i < length; i++)
        {
            Vector3 pathPos = startPos;
            float ratioY = moveY * movementData.groundPoundReadyMoveYCurve.Evaluate(ratio * i);
            pathPos.y += ratioY;
            groundPoundPath[i] = pathPos;
            Debug.Log("Path[" + i + "] " + groundPoundPath[i]);
        }

        return groundPoundPath;
    }

    public bool IsLookDirRight()
    {
        bool isRight = false;

        if (lastLookDir == Vector2.right)
            isRight = true;

        return isRight;
    }

}
