using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    private NewPlayerMovementManager m_movementManager;
    public Rigidbody2D rig2D
    {
        get
        {
            return m_movementManager.rig2D;
        }
    }
    private InputPlayer input
    {
        get
        {
            return m_movementManager.input;
        }
    }

    #region MOVEMENT PARAMETERS
    [Header("Movement")]
    [SerializeField]
    private float m_moveSpeed;
    [SerializeField]
    private float m_acceleration;
    [SerializeField]
    private float m_decceleration;
    [SerializeField]
    private float m_velocityPower;
    [SerializeField]
    private float frictionAmount;

    #endregion

    #region JUMP PARAMETERS
    [Header("Jump")]
    [SerializeField]
    private float m_jumpForce;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float m_jumpCutMultiplier;
    [SerializeField]
    private float m_jumpCoyoteTime;
    [SerializeField]
    private float m_jumpBufferTime;
    [SerializeField]
    private float m_fallGravityMultiplier;
    [SerializeField]
    private int m_maxJumpCount;

    private int m_currentJumpCount;
    private bool m_isJumping;
    private bool m_isJumpCutAction;

    public bool isJumping { get => m_isJumping; set => m_isJumping = value; }
    public int currentJumpCount
    {
        set
        {
            m_currentJumpCount = Mathf.Clamp(value, 0, m_maxJumpCount);
        }
        get
        {
            return m_currentJumpCount;
        }
    }
    public bool isJumpCutAction { get => m_isJumpCutAction; set => m_isJumpCutAction = value; }
    #endregion

    #region WALL SILDE PARAMETERS
    [Header("Wall Slide")]
    [SerializeField]
    private float m_wallSlideAccel;
    [SerializeField]
    private float m_wallSlidePower;
    [SerializeField]
    private float m_wallSlideCoyoteTime;
    [SerializeField]
    private float m_wallSlideGravity;
    //--------------------


    #endregion

    #region WALL JUMP PARAMETERS
    [Header("Wall Jump")]
    [SerializeField]
    private Vector2 m_wallJumpForce;
    [SerializeField]
    private float m_wallJumpBuffer;

    private bool m_isWallJumping;
    //----------------------------------
    private bool IsWallJumping { get => m_isWallJumping; set => m_isWallJumping = value; }
    #endregion

    #region TIMER PARAMETERS
    private float m_lastJumpTime;
    private float m_lastGroundGroundedTime;
    private float m_lastLeftWallGroundedTime;
    private float m_lastRightWallGroundedTime;
    private float m_lastWallJumpTime;
    //-------------------------------
    public float lastJumpTime
    {
        set
        {
            m_lastJumpTime = value;
        }
        get
        {
            return m_lastJumpTime;
        }
    }
    public float lastGroundGroundedTime
    {
        set
        {
            m_lastGroundGroundedTime = value;
        }
        get
        {
            return m_lastGroundGroundedTime;
        }
    }
    private float lastLeftWallGroundedTime
    {
        get => m_lastLeftWallGroundedTime;
        set => m_lastLeftWallGroundedTime = value;
    }
    private float lastRightWallGroundedTime
    {
        get => m_lastRightWallGroundedTime;
        set => m_lastRightWallGroundedTime = value;
    }
    private float lastWallJumpTime
    {
        set
        {
            m_lastWallJumpTime = value;
        }
        get
        {
            return m_lastWallJumpTime;
        }
    }
    #endregion

    #region GROUNDSENSOR PARAMETERS
    private bool m_beforeisGrounded;
    private bool beforeisGrounded { get => m_beforeisGrounded; set => m_beforeisGrounded = value; }


    #endregion

    #region DEFAULT GRAVITY
    private float m_gravityScale = 1.0f;
    #endregion


    public void Init(NewPlayerMovementManager movementManager)
    {
        m_movementManager = movementManager;
    }

    private void Update()
    {
        TimerUpdate();

        GroundCheck();
        CheckWall();
        input.ChackJumpPressed();

        #region Run
        Run();
        #endregion

        #region WALL SLIDE
        if (IsWallGrounded() && !IsWallJumping)
            WallSlide();
        #endregion

        #region JUMP

        if (CanJump())
            Jump();
        else if (CanAirJump())
            AirJump();

        if (CanWallJump())
            WallJump();

        if (CanJumpCut())
            JumpCut();
        #endregion



        Friction();

        GravitySetting();
    }

    private void TimerUpdate()
    {
        input.lastJumpPressedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;
        lastGroundGroundedTime -= Time.deltaTime;

        lastLeftWallGroundedTime -= Time.deltaTime;
        lastRightWallGroundedTime -= Time.deltaTime;

        lastWallJumpTime -= Time.deltaTime;
    }


    private void Run()
    {
        float moveDirX = input.moveDir.x;
        float targetSpeed = moveDirX * m_moveSpeed;
        float speedDif = targetSpeed - rig2D.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f ? m_acceleration : m_decceleration);

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, m_velocityPower) * Mathf.Sign(speedDif);
        movement = Mathf.Lerp(rig2D.velocity.x, movement, 0.5f);

        rig2D.AddForce(movement * Vector2.right);
    }

    private void Friction()
    {
        if (Mathf.Abs(input.moveDir.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rig2D.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rig2D.velocity.x);
            rig2D.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }




    public void Jump()
    {
        rig2D.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);

        lastGroundGroundedTime = 0.0f;
        lastJumpTime = 0.0f;
        isJumping = true;
        currentJumpCount++;
    }

    public void AirJump()
    {
        rig2D.velocity = rig2D.velocity * Vector2.right;
        rig2D.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
        lastJumpTime = 0.0f;
        currentJumpCount++;
    }

    public void JumpCut()
    {
        rig2D.AddForce(Vector2.down * rig2D.velocity.y * (1 - m_jumpCutMultiplier), ForceMode2D.Impulse);

        isJumpCutAction = true;

    }

    private void GroundCheck()
    {
        bool isGrounded = m_movementManager.isGrounded;

        if (isGrounded)
        {
            lastGroundGroundedTime = m_jumpCoyoteTime;

            if (beforeisGrounded != isGrounded)
                Randing();
        }


        beforeisGrounded = m_movementManager.isGrounded;
    }

    private void Randing()
    {
        isJumping = false;
        IsWallJumping = false;
        isJumpCutAction = false;
        currentJumpCount = 0;
    }

    private void GravitySetting()
    {
        #region Fall Gravity
        if (rig2D.velocity.y < 0f)
            rig2D.gravityScale = m_gravityScale * m_fallGravityMultiplier;
        else
            rig2D.gravityScale = m_gravityScale;
        #endregion

        #region Wall Gravity
        if (IsWallGrounded())
            rig2D.gravityScale = m_wallSlideGravity;
        #endregion
    }

    public void LastJumpTimeReset()
    {
        lastJumpTime = m_jumpBufferTime;
    }

    private bool CanJumpCut()
    {
        return currentJumpCount == 1 &&
            rig2D.velocity.y > 0.0f &&
            isJumping &&
            input.lastJumpPressedTime < 0.0f &&
            !isJumpCutAction &&
            !IsWallJumping;
    }



    private void CheckWall()
    {
        if (m_movementManager.WallSensorManager.isRightSensorGrounded)
            lastRightWallGroundedTime = m_wallSlideCoyoteTime;

        if (m_movementManager.WallSensorManager.isLeftSensorGrounded)
            lastLeftWallGroundedTime = m_wallSlideCoyoteTime;
    }

    private bool IsWallGrounded()
    {
        return lastRightWallGroundedTime > 0.0f || lastLeftWallGroundedTime > 0.0f;
    }



    private bool isMoveDirWallGrounded(float dirX)
    {
        bool isGrounded = false;

        if (dirX > 0.0f)
            isGrounded = m_movementManager.WallSensorManager.isRightSensorGrounded;
        else if (dirX < 0.0f)
            isGrounded = m_movementManager.WallSensorManager.isLeftSensorGrounded;

        return isGrounded;
    }


    private void WallSlide()
    {

        float targetSpeed = 0.0f;
        float speedDif = targetSpeed - rig2D.velocity.y;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * m_wallSlideAccel, m_wallSlidePower) * Mathf.Sign(speedDif);

        rig2D.AddForce(movement * Vector2.up, ForceMode2D.Force);
    }


    public void WallJump()
    {
        Vector2 jumpForce = m_wallJumpForce;

        if (Mathf.Sign(rig2D.velocity.x) != Mathf.Sign(jumpForce.x))
            jumpForce.x -= rig2D.velocity.x;

        if (rig2D.velocity.y < 0)
            jumpForce.y -= rig2D.velocity.y;




        rig2D.AddForce(jumpForce, ForceMode2D.Impulse);

        lastWallJumpTime = 0;
        IsWallJumping = true;

    }

    public void LastWallJumpTimeReset()
    {
        lastWallJumpTime = m_wallJumpBuffer;
    }

    private bool CanJump()
    {
        return lastGroundGroundedTime > 0.0f && lastJumpTime > 0.0f && !isJumping;
    }

    private bool CanAirJump()
    {
        return lastJumpTime > 0.0f && isJumping && currentJumpCount < m_maxJumpCount;
    }

    private bool CanWallJump()
    {
        return
            lastWallJumpTime > 0.0f &&
            (lastLeftWallGroundedTime > 0.0f || lastRightWallGroundedTime > 0.0f) &&
            !IsWallJumping;
    }

}
