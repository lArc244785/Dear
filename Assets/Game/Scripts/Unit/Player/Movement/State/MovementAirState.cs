using UnityEngine;

public class MovementAirState : I_MovementState
{
    public void Enter(PlayerMovementManager manager)
    {
       
    }

    public void Exit(PlayerMovementManager manager)
    {
        manager.coyoteSystem.ResetJumpExitTime();
    }

    public void FixedExcute(PlayerMovementManager manager)
    {
        manager.Resistance(manager.movementData.resistanceInAirAmount);
        manager.Run(1.0f, true);
    }

    public void UpdateExcute(PlayerMovementManager manager)
    {
        TimeUpdate(manager.coyoteSystem);
        GracityUpdate(manager);

        manager.TrunUpdate();

        JumpUpdate(manager);


       ChangeState(manager);
    }

    private void TimeUpdate(CoyoteSystem coyoteSystem)
    {
        coyoteSystem.GroundCoyoteTime();
        coyoteSystem.JumpCoyoteTime();
        coyoteSystem.JumpCutCoyoteTime();
        coyoteSystem.WallCoyoteTime();
    }



    private void ChangeState(PlayerMovementManager manager)
    {
        if (manager.IsWallGrouned())
            manager.currentState = PlayerMovementManager.State.Wall;
        else if(!manager.isJump && manager.IsGrounded())
        {
            manager.jumpCount = 0;
            manager.currentState = PlayerMovementManager.State.Ground;

            Collider2D groundCollider = manager.groundSensor.GetGroundCollider2D();

            float value = 0.0f;
            if (groundCollider.tag == "Forest")
            {
                value = 1.0f;
            }
            else if (groundCollider.tag == "Asphalt")
            {
                value = 2.0f;
            }

            manager.playerManager.sound.Landing(value);
        }

    }


    private void GracityUpdate(PlayerMovementManager manager)
    {
        if (manager.rig2D.velocity.y < 0.0f)
            manager.SetGravity(manager.movementData.gravityScale * manager.movementData.fallGravityMult);
        else
            manager.SetGravity(manager.movementData.gravityScale);
    }

    private void JumpUpdate(PlayerMovementManager manager)
    {
        if (manager.isJump && manager.rig2D.velocity.y <= 0.0f)
        {
            manager.isJump = false;
        }

        if (CanJumpCut(manager))
        {
            manager.JumpCut();
        }

        if (CanAirJump(manager))
        {
            
            manager.Jump(manager.movementData.airJumpForce);
        }

    }

    private bool CanAirJump(PlayerMovementManager manager)
    {
        return !manager.isJump &&
            manager.jumpCount < manager.movementData.maxJumpCount &&
            manager.coyoteSystem.lastJumpEnterTime > 0.0f;
    }

    private bool CanJumpCut(PlayerMovementManager manager)
    {
        return
            manager.coyoteSystem.lastJumpExitTime > 0.0f &&
            manager.jumpCount == 1 &&
            manager.isJump;
    }




}
