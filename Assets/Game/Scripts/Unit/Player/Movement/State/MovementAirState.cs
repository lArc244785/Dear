using UnityEngine;

public class MovementAirState : I_MovementState
{
    public void Enter(PlayerMovementManager movementManager)
    {
        if (!movementManager.isJump)
            movementManager.player.animationManager.TriggerAir();
    }

    public void Exit(PlayerMovementManager movementManager)
    {
        movementManager.coyoteSystem.ResetJumpExitTime();
    }

    public void FixedExcute(PlayerMovementManager movementManager)
    {
        movementManager.Resistance(movementManager.movementData.resistanceInAirAmount);
        movementManager.Run(1.0f, true);
    }

    public void UpdateExcute(PlayerMovementManager movementManager)
    {
        TimeUpdate(movementManager.coyoteSystem);
        GracityUpdate(movementManager);

        movementManager.TrunUpdate();

        JumpUpdate(movementManager);


        ChangeState(movementManager);
    }

    private void TimeUpdate(CoyoteSystem coyoteSystem)
    {
        coyoteSystem.GroundCoyoteTime();
        coyoteSystem.JumpCoyoteTime();
        coyoteSystem.JumpCutCoyoteTime();
        coyoteSystem.WallCoyoteTime();
    }



    private void ChangeState(PlayerMovementManager movementManager)
    {


        if (movementManager.IsWallGrouned())
            movementManager.currentState = PlayerMovementManager.State.Wall;
        else if (!movementManager.isJump && movementManager.IsGrounded())
        {
            movementManager.jumpCount = 0;
            movementManager.currentState = PlayerMovementManager.State.Ground;


            Collider2D groundCollider = movementManager.groundSensor.GetGroundCollider2D();

            float value = 0.0f;
            if (groundCollider.tag == "Forest")
            {
                value = 1.0f;
            }
            else if (groundCollider.tag == "Asphalt")
            {
                value = 2.0f;
            }

            movementManager.player.sound.Landing(value);
        }

    }


    private void GracityUpdate(PlayerMovementManager movementManager)
    {
        if (movementManager.player.rig2D.velocity.y < 0.0f)
            movementManager.SetGravity(movementManager.movementData.gravityScale * movementManager.movementData.fallGravityMult);
        else
            movementManager.SetGravity(movementManager.movementData.gravityScale);
    }

    private void JumpUpdate(PlayerMovementManager movementManager)
    {
        if (movementManager.isJump && movementManager.player.rig2D.velocity.y <= 0.0f)
        {
            movementManager.isJump = false;
            movementManager.isInteractionJump = false;
        }

        if (CanJumpCut(movementManager))
        {
            movementManager.JumpCut();
        }

        if (CanAirJump(movementManager))
        {
            movementManager.Jump(movementManager.movementData.airJumpForce);
        }

    }

    private bool CanAirJump(PlayerMovementManager movementManager)
    {
        return !movementManager.isJump && !movementManager.isOnInteractionJumpObject &&
            movementManager.jumpCount < movementManager.movementData.maxJumpCount &&
            movementManager.coyoteSystem.lastJumpEnterTime > 0.0f;
    }

    private bool CanJumpCut(PlayerMovementManager movementManager)
    {
        return
            movementManager.coyoteSystem.lastJumpExitTime > 0.0f &&
            movementManager.jumpCount == 1 &&
            movementManager.isJump &&
            !movementManager.isInteractionJump;
    }




}
