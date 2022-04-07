using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGroundState : I_MovementState
{
    public void Enter(A_MovementManager manager)
    {
        manager.SetGravity(manager.movementData.gravityScale);
    }

    public void Exit(A_MovementManager manager)
    {
        
    }

    public void FixedExcute(A_MovementManager manager)
    {
        manager.Resistance(manager.movementData.frictionAmount);
        manager.Run(1.0f);
    }

    public void UpdateExcute(A_MovementManager manager)
    {
        TimeUpdate(manager.coyoteSystem);
        PhysicUpdate(manager.groundSensor,manager.coyoteSystem);

        manager.TrunUpdate();

        JumpUpdate(manager);


        if (manager.coyoteSystem.lastOnGroundTime <= 0.0f)
            manager.currentState = A_MovementManager.State.Air;
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

    private void JumpUpdate(A_MovementManager manager)
    {
        if(CanJump(manager.coyoteSystem))
        {
            manager.Jump(manager.movementData.jumpForce);
            manager.currentState = A_MovementManager.State.Air;
        }
    }

    private bool CanJump(CoyoteSystem coyoteSystem)
    {
        return coyoteSystem.lastJumpEnterTime > 0.0f && coyoteSystem.lastOnGroundTime > 0.0f;
    }



}
