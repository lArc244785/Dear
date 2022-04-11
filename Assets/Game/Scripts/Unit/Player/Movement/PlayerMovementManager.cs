using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    #region Rigidbody
    [Header("Rigidbody")]
    [SerializeField]
    private Rigidbody2D m_rig2D;
    public Rigidbody2D rig2D { get { return m_rig2D; } }
    #endregion

    #region Input
    [Header("Input")]
    [SerializeField]
    private InputPlayer m_inputPlayer;
    public InputPlayer inputPlayer { get { return m_inputPlayer; } }
    #endregion

    #region UnitBase
    private UnitPlayer m_playerManager;
    public UnitPlayer playerManager
    {
        set
        {
            m_playerManager = value;
        }
        get
        {
            return m_playerManager;
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
        None = -1, Ground, Air, Wall, Rope,Hit,
        Total
    }

    private I_MovementState[] m_States;

    [SerializeField]
    private State m_currentState;

    public State currentState
    {
        set
        {
            if (value == State.None)
                return;

            m_States[(int)m_currentState].Exit(this);
            m_currentState = value;
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
    #endregion


    #region Sensor
    [Header("Sensor")]
    [SerializeField]
    private NewGroundSensor m_groundSensor;
    public NewGroundSensor groundSensor { get { return m_groundSensor; } }

    [SerializeField]
    private WallSensorManager m_wallSensor;
    public WallSensorManager wallSensor { get { return m_wallSensor; } }
    #endregion

    #region Coyote System
    [Header("Coyote System")]
    [SerializeField]
    private CoyoteSystem m_coyoteSystem;

    public CoyoteSystem coyoteSystem { get { return m_coyoteSystem; } }
    #endregion

    #region JumpCount
    private float m_jumpCount;
    public float jumpCount
    {
        get
        {
            return m_jumpCount;
        }
        set
        {
            m_jumpCount = Mathf.Clamp(value, 0, m_movementData.maxJumpCount);
        }
    }
    #endregion

    #region Trun
    [Header("Trun")]
    [SerializeField]
    private Transform m_modelTr;

    private Vector2 m_oldLookDir;
    #endregion



    public void Init(UnitPlayer unit)
    {
        StatesInit();

        playerManager = unit;

        m_coyoteSystem = new CoyoteSystem();
        m_coyoteSystem.Init(m_movementData);

        m_oldLookDir = Vector2.left;
    }

    private void StatesInit()
    {
        m_States = new I_MovementState[(int)State.Total];

        m_States[(int)State.Ground] = new MovementGroundState();
        m_States[(int)State.Air] = new MovementAirState();
        m_States[(int)State.Wall] = new MovementWallState();
        m_States[(int)State.Rope] = new MovementRopeState();
        m_States[(int)State.Hit] = new MovementHitState();

        currentState = State.Ground;
    }





    private void Update()
    {
        if (currentState == State.None)
            return;

        m_States[(int)currentState].UpdateExcute(this);

    }

    private void FixedUpdate()
    {
        if (currentState == State.None)
            return;

        m_States[(int)currentState].FixedExcute(this);
    }

    public void SetGravity(float gravitySclae)
    {
        m_rig2D.gravityScale = gravitySclae;
    }

    public bool IsGrounded()
    {
        return m_groundSensor.IsGrounded();
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
        float inputMoveDirX = 0.0f;
        if (isGetInput)
            inputMoveDirX = inputPlayer.moveDir.x;

        float rigVelocityX = rig2D.velocity.x;

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
        //Debug.Log("Run");
        rig2D.AddForce(movement * Vector2.right);
    }

    public void Resistance(float amount)
    {
        Vector2 force = rig2D.velocity.normalized * amount;
        force.x = Mathf.Min(Mathf.Abs(force.x), Mathf.Abs(rig2D.velocity.x));
        force.y = Mathf.Min(Mathf.Abs(force.y), Mathf.Abs(rig2D.velocity.y));

        force.x *= Mathf.Sign(rig2D.velocity.x);
        force.y *= Mathf.Sign(rig2D.velocity.y);

        //Debug.Log("Resistance");
        rig2D.AddForce(-force, ForceMode2D.Impulse);

    }

    public void Jump(float force)
    {
        if (rig2D.velocity.y < 0.0f)
            force -= rig2D.velocity.y;
        Debug.Log("Jump " + jumpCount);
        rig2D.AddForce(force * Vector2.up, ForceMode2D.Impulse);

        coyoteSystem.ResetJumpEnterTime();
        coyoteSystem.ResetGroundTime();

        isJump = true;

        jumpCount++;
    }

    public void JumpCut()
    {
        rig2D.AddForce(Vector2.down * rig2D.velocity.y * (1 - movementData.jumpCutMultiplier), ForceMode2D.Impulse);
        coyoteSystem.ResetJumpExitTime();
        Debug.Log("Jump Cut");
    }

    public void Trun(Vector2 lookDir)
    {
        if (lookDir.x != m_oldLookDir.x)
        {
            Vector3 scale = m_modelTr.localScale;
            scale.x *= -1;
            m_modelTr.localScale = scale;
        }
        m_oldLookDir = lookDir;
    }

    public void TrunUpdate()
    {
        if (inputPlayer.moveDir.x == 0.0f)
            return;

        Trun(inputPlayer.moveDir);
    }

    public Vector2 lookDir
    {
        get
        {
            return m_oldLookDir;
        }
    }

    public void Climbing(float maxSpeed, float dirY)
    {
        float targetSpeed = dirY * movementData.runMaxSpeed;

        float speedDif = targetSpeed - rig2D.velocity.y;
        float accleRate = targetSpeed > 0.01f ? movementData.climbingAccel : movementData.climbingDeccel;
        float velocityPower = movementData.accelPower;

        if (Mathf.Abs(rig2D.velocity.y) > Mathf.Abs(targetSpeed))
            accleRate = 0.0f;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accleRate, velocityPower) * Mathf.Sign(speedDif);
        Debug.Log(movement);

        Debug.Log("Climbing");
        rig2D.AddForce(movement * Vector2.up);
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
        if (Mathf.Sign(rig2D.velocity.x) != Mathf.Sign(force.x))
            force.x -= rig2D.velocity.x;

        if (rig2D.velocity.y < 0.0f)
            force.y -= rig2D.velocity.y;
        rig2D.AddForce(force, ForceMode2D.Impulse);
    }



    public void HitMovement(Vector2 imfectDir)
    {
        hitImfectDir = imfectDir;
        currentState = State.Hit;
    }
}
