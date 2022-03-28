using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Reference
    [SerializeField]
    private PlayerData m_movementData;
    [SerializeField]
    private NewPlayerMovementManager m_movementManager;

    public NewPlayerMovementManager movementManager { get => m_movementManager; }
    private PlayerData movementData { get => m_movementData; }
    private Rigidbody2D rig2D { get => m_movementManager.rig2D; }
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
    #endregion

    #region Wall Jump Parameter
    private float wallJumpStartTime { set; get; }
    #endregion


    #region Input Parameter
    private float lastJumpEnterTime { set; get; }
    private float lastDashEnterTime { set; get; }

    #endregion

    #region Dush Parmeters
    private float dashCount { set; get; }
    private float dashStartTime { set; get; }
    #endregion



    public void Init(NewPlayerMovementManager movementManager)
    {
        m_movementManager = movementManager;
    }

    private void Update()
    {
        TimerUpdate();

        PhysicsUpdate();

        GravityUpdate();

        JumpUpdate();

        DashUpdate();

        WallGripUpdate();
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
        if (!isDash && !isWallGrip)
        {
            if (isWallJump)
                Run(movementData.wallJumpRunLerp);
            else
                Run(1.0f);
        }
    }
    private void Run(float lerpAmount)
    {
        float inputMoveDirX = movementManager.input.moveDir.x;
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

        rig2D.AddForce(movement * Vector2.right);
    }
    #endregion




    #region Resistance
    private void ResistanceUpdate()
    {
        if (isDash || lastOnGroundTime <= 0.0f)
            Resistance(movementData.dragAmount);
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
                if (CanJump())
                {
                    isJump = true;
                    isWallJump = false;
                    Jump();
                }
                else if (CanWallJump())
                {
                    isWallJump = true;
                    isJump = false;
                    wallJumpStartTime = Time.time;

                    int dir;
                    if (movementManager.wallSensorManager.isLeftSensorGrounded)
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

        rig2D.AddForce(force, ForceMode2D.Impulse);

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


    private void Jump()
    {
        Debug.Log("JUmp");
        float force = movementData.jumpForce;
        if (rig2D.velocity.y < 0.0f)
            force -= rig2D.velocity.y;

        rig2D.AddForce(force * Vector2.up, ForceMode2D.Impulse);

        lastJumpEnterTime = 0.0f;
        lastOnGroundTime = 0.0f;

    }

    private bool CanJump()
    {
        return lastOnGroundTime > 0.0f && !isJump;
    }

    private void JumpCut()
    {
        rig2D.AddForce(Vector2.down * rig2D.velocity.y * (1 - movementData.jumpCutMultiplier), ForceMode2D.Impulse);
    }

    private bool CanJumpCut()
    {
        return rig2D.velocity.y > 0.0f && isJump;
    }

    private bool CanDash()
    {
        return dashCount > 0;
    }


    private void DashUpdate()
    {
        if (isDash && Time.time - dashStartTime >= movementData.dashEndTime)
        {
            isDash = false;
        }
        if (isDash && lastOnWallTime > 0.0f)
        {
            StopDash();
        }
        if (!isDash && lastOnGroundTime > 0.0f)
            dashCount = movementData.dashAmount;


        if (CanDash() && lastDashEnterTime > 0.0f)
        {
            StartDash(movementManager.input.moveDir * Vector2.right);

            dashCount--;
        }
    }

    #endregion


    private void PhysicsUpdate()
    {
        if (!isJump && !isWallJump)
        {
            if (movementManager.isGrounded)
                lastOnGroundTime = movementData.coyoteTime;

            if (movementManager.wallSensorManager.isLeftSensorGrounded)
                lastOnWallLeftTime = movementData.coyoteTime;
            if (movementManager.wallSensorManager.isRightSensorGrounded)
                lastOnWallRightTime = movementData.coyoteTime;

            lastOnWallTime = Mathf.Max(lastOnWallLeftTime, lastOnWallRightTime);


        }
    }

    private void GravityUpdate()
    {
        if (isDash || isWallGrip)
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
        SetGravity(0.0f);
        rig2D.velocity = dir * movementData.dashSpeed;

        isDash = true;
        dashStartTime = Time.time;

        lastOnGroundTime = 0.0f;
    }

    private void StopDash()
    {

        isDash = false;
    }
    #endregion

    #region WallGrip
    private void WallGripUpdate()
    {
        if(isWallGrip)
        {
            Resistance(movementData.frictionAmount);

            if (CanClimbing())
                Climbing();

        }
    }

    private void Climbing()
    {
        float targetSpeed = movementManager.input.moveDir.y * movementData.runMaxSpeed;
        float speedDif = targetSpeed - rig2D.velocity.y;
        float accleRate = targetSpeed > 0.01f ? movementData.climbingAccel : movementData.climbingDeccel;
        float velocityPower = movementData.accelPower;

        if (Mathf.Abs(rig2D.velocity.y) > Mathf.Abs(targetSpeed))
            accleRate = 0.0f;



        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accleRate, velocityPower) * Mathf.Sign(speedDif);
        Debug.Log(movement);

        rig2D.AddForce(movement * Vector2.up);

    }

    private bool CanClimbing()
    {
        float moveDirY = movementManager.input.moveDir.y;

        return (moveDirY > 0.0f && movementManager.wallSensorManager.UpSensorGrounded()) || 
            (moveDirY < 0.0f && movementManager.wallSensorManager.DownSensorGrounded());
    }

    private bool CanWallGrip()
    {
        return lastOnWallTime > 0.0f ;
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
        lastDashEnterTime = movementData.dashBufferTime;
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


    #endregion

}
