using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGroundState : I_MovementState
{
    public void Enter(PlayerMovementManager manager)
    {
        manager.SetGravity(manager.movementData.gravityScale);
    }

    public void Exit(PlayerMovementManager manager)
    {
        
    }

    public void FixedExcute(PlayerMovementManager manager)
    {
        manager.Resistance(manager.movementData.frictionAmount);
        manager.Run(1.0f, true);
    }

    public void UpdateExcute(PlayerMovementManager manager)
    {
        TimeUpdate(manager.coyoteSystem);
        PhysicUpdate(manager.groundSensor,manager.coyoteSystem);

        manager.TrunUpdate();

        JumpUpdate(manager);


        if (manager.coyoteSystem.lastOnGroundTime <= 0.0f)
            manager.currentState = PlayerMovementManager.State.Air;
    }

    private void TimeUpdate(CoyoteSystem coyoteSystem)
    {
        coyoteSystem.GroundCoyoteTime();
        coyoteSystem.JumpCoyoteTime();
    }

    private void PhysicUpdate(NewGroundSensor groundSensor, CoyoteSystem coyoteSystem)
    {
        if (groundSensor.IsGrounded())
        {
            coyoteSystem.OnGroundTimer();
        }
    }

    private void JumpUpdate(PlayerMovementManager manager)
    {
        if(CanJump(manager))
        {
            manager.Jump(manager.movementData.jumpForce);
            manager.currentState = PlayerMovementManager.State.Air;
        }
    }

    private bool CanJump(PlayerMovementManager manager)
    {
        return manager.coyoteSystem.lastJumpEnterTime > 0.0f && manager.coyoteSystem.lastOnGroundTime > 0.0f && manager.movementData.maxJumpCount > 0;
    }



}
