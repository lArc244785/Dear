using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Reference
    [SerializeField]
    private MovementData m_movementData;
    [SerializeField]
    private Rigidbody2D m_rig2D;
    [SerializeField]
    private WallSensorManager m_wallSensorManager;
    [SerializeField]
    private NewGroundSensor m_groundSensor;
    [SerializeField]
    private InputPlayer m_input;
    [SerializeField]
    private SpringJoint2D m_springJoint2D;
    [SerializeField]
    private GrapplingShooter m_shooter;
    [SerializeField]
    private Transform m_modelTr;


    private MovementData movementData { get => m_movementData; }
    public Rigidbody2D rig2D { get => m_rig2D; }
    private InputPlayer input { get => m_input; }
    private NewGroundSensor groundSensor { get => m_groundSensor; }
    private WallSensorManager wallSensorManager { get => m_wallSensorManager; }
    private SpringJoint2D springJoint2D { get => m_springJoint2D; }
    private GrapplingShooter shooter { get => m_shooter; }
    #endregion

    #region State Parameter
    private float lastOnGroundTime { set; get; }
    private float lastOnWallTime { set; get; }
    private float lastOnWallRightTime { set; get; }
    private float lastOnWallLeftTime { set; get; }



    private bool isJump { set; get; }
    private bool isDash { set; get; }
    private bool isWallJump { set; get; }
    private bool isWallGrip { set; get; }
    public bool isRope { set; get; }
    #endregion

    #region Wall Jump Parameter
    private float wallJumpStartTime { set; get; }
    #endregion

    #region Jump Parameter
    private int m_currentJump;

    public int currentJump
    {
        set
        {
            m_currentJump = Mathf.Clamp(value, 0, movementData.maxJumpCount);
        }
        get
        {
            return m_currentJump;
        }
    }
    #endregion

    #region Input Parameter
    private float lastJumpEnterTime { set; get; }
    private float lastDashEnterTime { set; get; }

    #endregion

    #region Dush Parmeters
    private float dashCount { set; get; }
    private float dashStartTime { set; get; }
    #endregion

    #region Rope Parmeters
    private float ropeReboundTime { set; get; }

    private bool isRopeCancle { set; get; }
    private float ropeCancleStartTime { set; get; }
    #endregion

    #region Trun Parmeters
    private Vector2 oldLookDir { set; get; }  
    #endregion



    public void Init()
    {
        oldLookDir = Vector2.left;
    }

    private void Update()
    {
        TimerUpdate();

        PhysicsUpdate();

        TrunUpdate();

        GravityUpdate();

        JumpUpdate();

        DashUpdate();

        WallGripUpdate();

        RopeUpdate();
    }

    private void FixedUpdate()
    {

        ResistanceUpdate();


        RunUpdate();


        WallSlideUpdate();
    }

    #region Funtions

    private void TimerUpdate()
    {
        lastOnGroundTime -= Time.deltaTime;
        lastOnWallRightTime -= Time.deltaTime;
        lastOnWallLeftTime -= Time.deltaTime;

        lastJumpEnterTime -= Time.deltaTime;
        lastDashEnterTime -= Time.deltaTime;
    }

    #region Run
    private void RunUpdate()
    {


        if (!isDash && !isWallGrip && !isRope)
        {
            if (isWallJump)
                Run(movementData.wallJumpRunLerp);
            else
                Run(1.0f);
        }
    }
    private void Run(float lerpAmount)
    {
        float inputMoveDirX = input.moveDir.x;
        float rigVelocityX = rig2D.velocity.x;

        float targetSpeed = inputMoveDirX * movementData.runMaxSpeed;
        float SpeedDif = targetSpeed - rigVelocityX;

        #region Acceleration Rate
        float accleRate;
        if (lastOnGroundTime >= 0.0f)
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
    #endregion




    #region Resistance
    private void ResistanceUpdate()
    {
        if (isRope)
            return;

        if (isDash || lastOnGroundTime <= 0.0f)
            Resistance(movementData.resistanceInAirAmount);
        else
            Resistance(movementData.frictionAmount);

    }

    private void Resistance(float amount)
    {
        Vector2 force = rig2D.velocity.normalized * amount;
        force.x = Mathf.Min(Mathf.Abs(force.x), Mathf.Abs(rig2D.velocity.x));
        force.y = Mathf.Min(Mathf.Abs(force.y), Mathf.Abs(rig2D.velocity.y));

        force.x *= Mathf.Sign(rig2D.velocity.x);
        force.y *= Mathf.Sign(rig2D.velocity.y);

        //Debug.Log("Resistance");
        rig2D.AddForce(-force, ForceMode2D.Impulse);

    }
    #endregion

    #region Jump Funtions
    private void JumpUpdate()
    {
        

        if (rig2D.velocity.y <= 0.0f && isJump)
            isJump = false;
        if (isWallJump && Time.time - wallJumpStartTime >= movementData.wallJumpTime)
            isWallJump = false;

        if (!isDash)
        {
            if (lastJumpEnterTime > 0.0f)
            {
                if (CanFirstJump())
                {
                    isJump = true;
                    isWallJump = false;
                    Jump(movementData.jumpForce);
                }
                else if(CanAirJump())
                {
                    isJump = true;
                    isWallJump = false;
                    Jump(movementData.airJumpForce);
                }
                else if (CanWallJump())
                {
                    isWallJump = true;
                    isJump = false;
                    wallJumpStartTime = Time.time;

                    int dir;
                    if (lastOnWallLeftTime > 0.0f)
                        dir = 1;
                    else
                        dir = -1;

                    WallJump(dir);
                }

            }
        }
    }

    private void WallJump(int dir)
    {
        Debug.Log("WallJump " + dir);
        Vector2 force = movementData.wallJumpForce;

        force.x *= Mathf.Sign(dir);

        if (Mathf.Sign(rig2D.velocity.x) != Mathf.Sign(force.x))
            force.x -= rig2D.velocity.x;

        if (rig2D.velocity.y < 0.0f)
            force.y -= rig2D.velocity.y;

        Debug.Log("WallJump");
        rig2D.AddForce(force, ForceMode2D.Impulse);

        if (lastOnWallLeftTime > 0.0f)
            Trun(Vector2.right);
        else
            Trun(Vector2.left);


        lastOnGroundTime = 0.0f;
        lastOnWallLeftTime = 0.0f;
        lastOnWallRightTime = 0.0f;
        lastJumpEnterTime = 0.0f;
        lastOnWallTime = 0.0f;

    }

    private bool CanWallJump()
    {
        return lastOnWallTime > 0.0f
            && lastOnGroundTime <= 0.0f
            && !isWallJump;
    }


    private void Jump(float force)
    {
        if (rig2D.velocity.y < 0.0f)
            force -= rig2D.velocity.y;
        Debug.Log("Jump");
        rig2D.AddForce(force * Vector2.up, ForceMode2D.Impulse);

        lastJumpEnterTime = 0.0f;
        lastOnGroundTime = 0.0f;
        currentJump++;
    }

    private bool CanFirstJump()
    {
        return lastOnGroundTime > 0.0f && !isJump;
    }

    private bool CanAirJump()
    {
        return lastOnGroundTime <= 0.0f && !isJump && currentJump < movementData.maxJumpCount;
    }

    private void JumpCut()
    {
        Debug.Log("JumpCut");
        rig2D.AddForce(Vector2.down * rig2D.velocity.y * (1 - movementData.jumpCutMultiplier), ForceMode2D.Impulse);
    }

    private bool CanJumpCut()
    {
        return rig2D.velocity.y > 0.0f && isJump && currentJump == 1;
    }

    private bool CanDash()
    {
        return dashCount > 0;
    }


    private void DashUpdate()
    {
        //if (isDash && Time.time - dashStartTime >= movementData.dashEndTime)
        //{
        //    isDash = false;
        //}
        //if (isDash && lastOnWallTime > 0.0f)
        //{
        //    StopDash();
        //}
        //if (!isDash && lastOnGroundTime > 0.0f)
        //    dashCount = movementData.dashAmount;


        //if (CanDash() && lastDashEnterTime > 0.0f)
        //{
        //    StartDash(input.moveDir * Vector2.right);

        //    dashCount--;
        //}
    }

    #endregion


    private void PhysicsUpdate()
    {
        if (!isJump && !isWallJump)
        {
            if (groundSensor.IsGrounded())
            {
                lastOnGroundTime = movementData.coyoteTime;
                currentJump = 0;
            }


            if (wallSensorManager.IsLeftSensorGrounded())
                lastOnWallLeftTime = movementData.coyoteTime;
            if (wallSensorManager.IsRightSensorGrounded())
                lastOnWallRightTime = movementData.coyoteTime;

            lastOnWallTime = Mathf.Max(lastOnWallLeftTime, lastOnWallRightTime);


        }
    }

    private void GravityUpdate()
    {
        if (isDash || isWallGrip || isRope)
            return;

        if (rig2D.velocity.y < 0.0f)
            SetGravity(movementData.gravityScale * movementData.fallGravityMult);
        else
            SetGravity(movementData.gravityScale);
    }

    private void SetGravity(float gracityScale)
    {
        rig2D.gravityScale = gracityScale;
    }

    #region Wall Slide
    private void WallSlideUpdate()
    {
        if (isWallGrip)
            return;

        if (lastOnWallTime > 0.0f && !isWallJump)
            WallSlide();
    }

    private void WallSlide()
    {

        rig2D.velocity = new Vector2(rig2D.velocity.x, movementData.wallSlideVelocity);
    }
    #endregion

    #region Dash
    private void StartDash(Vector2 dir)
    {
        //SetGravity(0.0f);
        //rig2D.velocity = dir * movementData.dashSpeed;

        //isDash = true;
        //dashStartTime = Time.time;

        //lastOnGroundTime = 0.0f;
    }

    private void StopDash()
    {

        isDash = false;
    }
    #endregion

    #region WallGrip
    private void WallGripUpdate()
    {
        if (isWallGrip)
        {
            Resistance(movementData.frictionAmount);

            if (CanClimbing())
                Climbing();

        }
    }

    private void Climbing()
    {
        float targetSpeed = input.moveDir.y * movementData.runMaxSpeed;
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

    private bool CanClimbing()
    {
        float moveDirY = input.moveDir.y;

        return (moveDirY > 0.0f && wallSensorManager.UpSensorGrounded()) ||
            (moveDirY < 0.0f && wallSensorManager.DownSensorGrounded());
    }

    private bool CanWallGrip()
    {
        return lastOnWallTime > 0.0f;
    }
    private void WallGrip()
    {
        SetGravity(0.0f);
        rig2D.velocity = Vector2.zero;

        isWallGrip = true;
    }

    private void WallGripCancle()
    {
        isWallGrip = false;
    }


    private bool CanWallGripCancle()
    {
        return isWallGrip;
    }


    #endregion

    #region Trun
    private void TrunUpdate()
    {
        if (isRope || isDash|| isWallJump || isWallGrip)
            return;

        if (input.moveDir.x == 0)
            return;


        Trun(input.moveDir);



    }

    private void Trun(Vector2 lookDir)
    {
        if(lookDir.x != oldLookDir.x)
        {
            Vector3 scale = m_modelTr.localScale;
            scale.x *= -1;
            m_modelTr.localScale = scale;
        }
        oldLookDir = lookDir;
    }

    #endregion

    #region Input
    public void OnJumpUp()
    {
        if (CanJumpCut())
            JumpCut();
    }
    public void OnJumpEnter()
    {
        lastJumpEnterTime = movementData.jumpBufferTime;
    }

    public void OnDashEnter()
    {
        //lastDashEnterTime = movementData.dashBufferTime;
    }

    public void OnWallGripEnter()
    {
        if (CanWallGrip())
            WallGrip();
    }

    public void OnWallGripUp()
    {
        if (CanWallGripCancle())
            WallGripCancle();
    }


    #endregion

    #region Rope Funtion
    private void RopeUpdate()
    {
        if (!isRope)
        {
            if (shooter.isGrappling)
            {
                isRope = true;
                ropeReboundTime = 0.0f;
                ropeCancleStartTime = 0.0f;
            }
            else
                return;
        }

        if (isRopeCancle)
        {
            if (Time.time - ropeReboundTime >= 0.0f || groundSensor.IsGrounded() || lastOnWallTime > 0.0f)
            {
                isRope = false;
                isRopeCancle = false;
            }

        }
        else if (shooter.isNoneGrappling)
        {
            isRope = false;
        }


    }

    private void OnRebound(bool isRight)
    {
        //if (CanRopeRebound())
        //{
        //    ropeReboundTime = movementData.ropeReboundTime;
        //}
    }

    private bool CanRopeRebound()
    {
        return isRope && ropeReboundTime <= 0.0f;
    }



    public void Rebound(bool isRight)
    {
        //Vector2 reboundDir = Vector2.right;
        //if (!isRight)
        //    reboundDir *= -1.0f;

        //float reboundPower = movementData.ropeReboundPower;



        //Debug.Log("Rebound");
        //rig2D.AddForce(reboundDir * reboundPower, ForceMode2D.Impulse);

        //ropeReboundTime = movementData.ropeReboundTime;

    }

    public void CancleRebound()
    {
        //if (groundSensor.IsGrounded())
        //    return;



        //float xVelocity = movementData.ropeReboundPower;
        //if (rig2D.velocity.x < 0.0f)
        //    xVelocity *= -1.0f;

        //rig2D.velocity = new Vector2(xVelocity, rig2D.velocity.y);


        //isRopeCancle = true;

        //ropeCancleStartTime = Time.time;
    }


    #endregion


    #endregion

}
