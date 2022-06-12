using UnityEngine;

public class MovementWallState : I_MovementState
{
    private float m_wallJumpStartTime;

    private float m_currentWallEffectTime;

    private bool m_isWallSlideRight;
    private GroundInfo m_wallSlideInfo;
    public void Enter(PlayerMovementManager movementManager)
    {
        movementManager.coyoteSystem.ResetJumpEnterTime();
        movementManager.player.rig2D.velocity = new Vector2(movementManager.player.rig2D.velocity.x, 0.0f);

        if (movementManager.IsWallLeft())
            movementManager.Trun(Vector2.left);
        else
            movementManager.Trun(Vector2.right);

        
        movementManager.player.animationManager.TriggerWall();
        m_currentWallEffectTime = 0.0f;
        movementManager.isWallSilde = false;

    }

    public void Exit(PlayerMovementManager manager)
    {

    }

    public void FixedExcute(PlayerMovementManager manager)
    {
        manager.Resistance(manager.movementData.resistanceInAirAmount);

        if (manager.isWallJump)
        {
            manager.Run(manager.movementData.wallJumpRunLerp, true);
        }
        else if (manager.isWallGrip || manager.isWallGripInteraction)
        {
            ClimbingUpdate(manager);
        }
        else
        {
            manager.Run(1.0f,true);
            WallSildeUpdate(manager);
            
        }

    }



    public void UpdateExcute(PlayerMovementManager manager)
    {
        TimeUpdate(manager.coyoteSystem);

        PhysicesUpdate(manager);

        GravityUpdate(manager);

        WallJumpUpdate(manager);

        ChangeState(manager);
    }

    private void PhysicesUpdate(PlayerMovementManager manager)
    {
        if (!manager.isWallJump)
        {

            if (manager.IsWallLeft())
            {
                //Debug.Log("Left");
                manager.coyoteSystem.OnWallLeftTime();
            }

            if (manager.IsWallRight())
            {
                Debug.Log("Right");
                manager.coyoteSystem.OnWallRightTime();
            }

        }

    }

    private void ClimbingUpdate(PlayerMovementManager movementManager)
    {
        movementManager.Resistance(movementManager.movementData.frictionAmount);
        if (CanClimbing(movementManager))
            movementManager.Climbing(movementManager.movementData.climbingMaxSpeed, movementManager.player.inputPlayer.moveDir.y);
    }

    private bool CanClimbing(PlayerMovementManager movementManager)
    {
        float moveDirY = movementManager.player.inputPlayer.moveDir.y;

        return (moveDirY > 0.0f && movementManager.wallSensor.UpSensorGrounded()) ||
            (moveDirY < 0.0f && movementManager.wallSensor.DownSensorGrounded());
    }





    private void TimeUpdate(CoyoteSystem coyoteSystem)
    {
        coyoteSystem.WallCoyoteTime();
        coyoteSystem.JumpCoyoteTime();
    }


    private void WallSilde(PlayerMovementManager movementManager)
    {
        

        movementManager.player.rig2D.velocity = new Vector2(movementManager.player.rig2D.velocity.x, -movementManager.movementData.wallSlideVelocity);

    }

    private void WallSildeUpdate(PlayerMovementManager movementManager)
    {
        

        if (movementManager.coyoteSystem.lastOnWallTime > 0.0f && !movementManager.isWallJump)
        {
            if (!movementManager.isWallSilde)
            {
                Vector2 lookDir = Vector2.left;
                m_isWallSlideRight = true;
                m_wallSlideInfo = movementManager.wallSensor.lastContactRightWallInfo;

                if (movementManager.coyoteSystem.lastOnWallLeftTime > 0.0f)
                {
                    lookDir = Vector2.right;
                    m_isWallSlideRight = false;
                    m_wallSlideInfo = movementManager.wallSensor.lastContactLeftWallInfo;
                }
  

                movementManager.Trun(lookDir);

                WallSlideEffect(movementManager);

            }
            else
            {//effect
                m_currentWallEffectTime -= Time.deltaTime;
                 if (m_currentWallEffectTime <= 0.0f)
                    WallSlideEffect(movementManager);
            }

            WallSilde(movementManager);

            movementManager.isWallSilde = true;
        }
        else
        {
            movementManager.isWallSilde = false;
        }
    }




    private void WallSlideEffect(PlayerMovementManager movementManager)
    {
        movementManager.player.particleManager.WallSlideEffect(m_isWallSlideRight, m_wallSlideInfo.type);
        OnEffectTimmer(movementManager);

    }

    private void OnEffectTimmer(PlayerMovementManager movementManager)
    {
        m_currentWallEffectTime = 0.5f;
    }

    


    private void WallJumpUpdate(PlayerMovementManager movementManager)
    {
        if (movementManager.isWallJump)
        {
            float wallJumpDuringTime = Time.time - m_wallJumpStartTime;
            if (wallJumpDuringTime >= movementManager.movementData.wallJumpTime)
            {
                movementManager.isWallJump = false;
               // movementManager.player.inputPlayer.SetControl(true);
            }

        }

        if (CanWallJump(movementManager))
        {
            Debug.Log("WW " + movementManager.coyoteSystem.lastJumpEnterTime);

            int dir = 1;
            if (movementManager.coyoteSystem.lastOnWallRightTime > 0.0f)
                dir = -1;

            WallJump(dir, movementManager);
        }

    }

    private bool CanWallJump(PlayerMovementManager movementManager)
    {
        return
            movementManager.coyoteSystem.lastJumpEnterTime > 0.0f &&
            movementManager.coyoteSystem.lastOnWallTime > 0.0f &&
            !movementManager.isWallJump;
    }


    private void ChangeState(PlayerMovementManager movementManager)
    {
        if (movementManager.coyoteSystem.lastOnWallTime < 0.0f && !movementManager.isWallJump)
        {
            movementManager.currentState = PlayerMovementManager.State.Air;

        }
        else if (movementManager.IsGrounded())
        {
            movementManager.currentState = PlayerMovementManager.State.Ground;

        }


    }

    private void RunUpdate(PlayerMovementManager movementManager)
    {
        if (!movementManager.isWallJump)
        {
            movementManager.Run(1.0f,true);
        }
        else
        {
            movementManager.Run(movementManager.movementData.wallJumpRunLerp,true);
        }
    }

    private void GravityUpdate(PlayerMovementManager movementManager)
    {
        if ((movementManager.isWallGrip || movementManager.isWallGripInteraction) && !movementManager.isWallJump)
            movementManager.SetGravity(0.0f);
        else
            movementManager.SetGravity(movementManager.movementData.gravityScale);
    }

    private void WallJump(int dir, PlayerMovementManager movementManager)
    {
        bool isRight = true;

        if (dir == -1)
        {
            isRight = false;
        }


        movementManager.player.animationManager.TriggerWallJump();
        movementManager.player.particleManager.WallJumpEffect(isRight, m_wallSlideInfo.type);

        if (isRight)
            movementManager.player.sound.WallJumpRight();
        else
            movementManager.player.sound.WallJumpLeft();

        Debug.Log("WallJump | Dir: " + dir);
        Vector2 force = movementManager.movementData.wallJumpForce;

        force.x *= Mathf.Sign(dir);

        //if (Mathf.Sign(manager.rig2D.velocity.x) != Mathf.Sign(force.x))
        //    force.x -= manager.rig2D.velocity.x;

        //if (manager.rig2D.velocity.y < 0.0f)
        //    force.y -= manager.rig2D.velocity.y;


        //manager.rig2D.AddForce(force, ForceMode2D.Impulse);

        movementManager.VectorJump(force);


        if (movementManager.coyoteSystem.lastOnWallLeftTime > 0.0f)
            movementManager.Trun(Vector2.right);
        else
            movementManager.Trun(Vector2.left);


        movementManager.coyoteSystem.ResetWallLeftTime();
        movementManager.coyoteSystem.ResetWallRightTime();
        movementManager.coyoteSystem.ResetJumpEnterTime();

        movementManager.coyoteSystem.WallCoyoteTime();
        Debug.Log("WallG : " + movementManager.coyoteSystem.lastOnWallTime);


        movementManager.isWallJump = true;


       // movementManager.player.inputPlayer.SetControl(false);

        m_wallJumpStartTime = Time.time;
    }

}
