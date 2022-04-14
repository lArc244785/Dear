using UnityEngine;

public class MovementWallState : I_MovementState
{
    private float m_wallJumpStartTime;

    public void Enter(PlayerMovementManager manager)
    {
        manager.coyoteSystem.ResetJumpEnterTime();
        manager.rig2D.velocity = new Vector2(manager.rig2D.velocity.x, 0.0f);

        if (manager.IsWallLeft())
            manager.Trun(Vector2.left);
        else
            manager.Trun(Vector2.right);

        
        manager.playerManager.animation.TriggerWall();


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

    private void ClimbingUpdate(PlayerMovementManager manager)
    {
        manager.Resistance(manager.movementData.frictionAmount);
        if (CanClimbing(manager))
            manager.Climbing(manager.movementData.climbingMaxSpeed, manager.inputPlayer.moveDir.y);
    }

    private bool CanClimbing(PlayerMovementManager manager)
    {
        float moveDirY = manager.inputPlayer.moveDir.y;

        return (moveDirY > 0.0f && manager.wallSensor.UpSensorGrounded()) ||
            (moveDirY < 0.0f && manager.wallSensor.DownSensorGrounded());
    }





    private void TimeUpdate(CoyoteSystem coyoteSystem)
    {
        coyoteSystem.WallCoyoteTime();
        coyoteSystem.JumpCoyoteTime();
    }


    private void WallSilde(Rigidbody2D rig2D, float wallSlideVelocity)
    {
        rig2D.velocity = new Vector2(rig2D.velocity.x, -wallSlideVelocity);


    }

    private void WallSildeUpdate(PlayerMovementManager manager)
    {
        manager.isWallSilde = false;


        if (manager.coyoteSystem.lastOnWallTime > 0.0f && !manager.isWallJump)
        {
            WallSilde(manager.rig2D, manager.movementData.wallSlideVelocity);
            manager.isWallSilde = true;
        }
    }

    private void WallJumpUpdate(PlayerMovementManager manager)
    {
        if (manager.isWallJump)
        {
            float wallJumpDuringTime = Time.time - m_wallJumpStartTime;
            if (wallJumpDuringTime >= manager.movementData.wallJumpTime)
                manager.isWallJump = false;
        }

        if (CanWallJump(manager))
        {
            Debug.Log("WW " + manager.coyoteSystem.lastJumpEnterTime);

            int dir = 1;
            if (manager.coyoteSystem.lastOnWallRightTime > 0.0f)
                dir = -1;

            WallJump(dir, manager);
        }

    }

    private bool CanWallJump(PlayerMovementManager manager)
    {
        return
            manager.coyoteSystem.lastJumpEnterTime > 0.0f &&
            manager.coyoteSystem.lastOnWallTime > 0.0f &&
            !manager.isWallJump;
    }


    private void ChangeState(PlayerMovementManager manager)
    {
        if (manager.coyoteSystem.lastOnWallTime < 0.0f && !manager.isWallJump)
        {
            manager.currentState = PlayerMovementManager.State.Air;
        }
        else if (manager.IsGrounded())
            manager.currentState = PlayerMovementManager.State.Ground;
    }

    private void RunUpdate(PlayerMovementManager manager)
    {
        if (!manager.isWallJump)
        {
            manager.Run(1.0f,true);
        }
        else
        {
            manager.Run(manager.movementData.wallJumpRunLerp,true);
        }
    }

    private void GravityUpdate(PlayerMovementManager manager)
    {
        if ((manager.isWallGrip || manager.isWallGripInteraction) && !manager.isWallJump)
            manager.SetGravity(0.0f);
        else
            manager.SetGravity(manager.movementData.gravityScale);
    }

    private void WallJump(int dir, PlayerMovementManager manager)
    {
        manager.playerManager.animation.TriggerJump();

        if (dir == 1)
            manager.playerManager.sound.WallJumpRight();
        else
            manager.playerManager.sound.WallJumpLeft();

        Debug.Log("WallJump | Dir: " + dir);
        Vector2 force = manager.movementData.wallJumpForce;

        force.x *= Mathf.Sign(dir);

        //if (Mathf.Sign(manager.rig2D.velocity.x) != Mathf.Sign(force.x))
        //    force.x -= manager.rig2D.velocity.x;

        //if (manager.rig2D.velocity.y < 0.0f)
        //    force.y -= manager.rig2D.velocity.y;


        //manager.rig2D.AddForce(force, ForceMode2D.Impulse);

        manager.VectorJump(force);


        if (manager.coyoteSystem.lastOnWallLeftTime > 0.0f)
            manager.Trun(Vector2.right);
        else
            manager.Trun(Vector2.left);


        manager.coyoteSystem.ResetWallLeftTime();
        manager.coyoteSystem.ResetWallRightTime();
        manager.coyoteSystem.ResetJumpEnterTime();

        manager.coyoteSystem.WallCoyoteTime();
        Debug.Log("WallG : " + manager.coyoteSystem.lastOnWallTime);


        manager.isWallJump = true;
        m_wallJumpStartTime = Time.time;
    }

}
