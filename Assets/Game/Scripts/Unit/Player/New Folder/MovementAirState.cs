public class MovementAirState : I_MovementState
{
    public void Enter(A_MovementManager manager)
    {
       
    }

    public void Exit(A_MovementManager manager)
    {
        manager.coyoteSystem.ResetJumpExitTime();
    }

    public void FixedExcute(A_MovementManager manager)
    {
        manager.Resistance(manager.movementData.resistanceInAirAmount);
        manager.Run(1.0f, true);
    }

    public void UpdateExcute(A_MovementManager manager)
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



    private void ChangeState(A_MovementManager manager)
    {
        if (manager.IsWallGrouned())
            manager.currentState = A_MovementManager.State.Wall;
        else if(!manager.isJump && manager.IsGrounded())
        {
            manager.jumpCount = 0;
            manager.currentState = A_MovementManager.State.Ground;
        }

    }


    private void GracityUpdate(A_MovementManager manager)
    {
        if (manager.rig2D.velocity.y < 0.0f)
            manager.SetGravity(manager.movementData.gravityScale * manager.movementData.fallGravityMult);
        else
            manager.SetGravity(manager.movementData.gravityScale);
    }

    private void JumpUpdate(A_MovementManager manager)
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

    private bool CanAirJump(A_MovementManager manager)
    {
        return !manager.isJump &&
            manager.jumpCount < manager.movementData.maxJumpCount &&
            manager.coyoteSystem.lastJumpEnterTime > 0.0f;
    }

    private bool CanJumpCut(A_MovementManager manager)
    {
        return
            manager.coyoteSystem.lastJumpExitTime > 0.0f &&
            manager.jumpCount == 1 &&
            manager.isJump;
    }




}
