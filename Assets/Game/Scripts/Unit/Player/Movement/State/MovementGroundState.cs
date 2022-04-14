using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementGroundState : I_MovementState
{
    private IEnumerator m_footStepCoroutine;

    public void Enter(PlayerMovementManager manager)
    {
        if (manager.playerManager.grapplingShooter.isNoneGrappling)
            manager.playerManager.animation.TriggerLanding();
        else
            manager.playerManager.animation.ropeMovement = 0.5f;
        manager.SetGravity(manager.movementData.gravityScale);
    }

    public void Exit(PlayerMovementManager manager)
    {
        manager.playerManager.sound.footStepLoop = false;
    }

    public void FixedExcute(PlayerMovementManager manager)
    {
        manager.Resistance(manager.movementData.frictionAmount);
        RunUpdate(manager);
    }

    public void UpdateExcute(PlayerMovementManager manager)
    {
        TimeUpdate(manager.coyoteSystem);
        PhysicUpdate(manager.groundSensor,manager.coyoteSystem);

        manager.TrunUpdate();

        JumpUpdate(manager);


        if (manager.coyoteSystem.lastOnGroundTime <= 0.0f)
        {
            manager.currentState = PlayerMovementManager.State.Air;
        }

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

    private void RunUpdate(PlayerMovementManager manager)
    {
        float moveDirXAbs = Mathf.Abs(manager.inputPlayer.moveDir.x);

        manager.playerManager.animation.movement = moveDirXAbs;

        if (moveDirXAbs > 0)
        {
            manager.playerManager.sound.footStepLoop  = true;
        }
        else
        {
            manager.playerManager.sound.footStepLoop = false;
        }

        manager.Run(1.0f, true);
    }



}
