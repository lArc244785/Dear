using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementGroundState : I_MovementState
{
    private IEnumerator m_footStepCoroutine;

    public void Enter(PlayerMovementManager movementManager)
    {
        movementManager.player.animationManager.TriggerLanding();
        movementManager.SetGravity(movementManager.movementData.gravityScale);
        movementManager.jumpCount = 0;
    }

    public void Exit(PlayerMovementManager movementManager)
    {
        movementManager.player.sound.footStepLoop = false;
    }

    public void FixedExcute(PlayerMovementManager movementManager)
    {
        movementManager.Resistance(movementManager.movementData.frictionAmount);
        RunUpdate(movementManager);
    }

    public void UpdateExcute(PlayerMovementManager movementManager)
    {
        TimeUpdate(movementManager.coyoteSystem);
        PhysicUpdate(movementManager.groundSensor,movementManager.coyoteSystem);

        movementManager.TrunUpdate();

        JumpUpdate(movementManager);


        if (movementManager.coyoteSystem.lastOnGroundTime <= 0.0f)
        {
            movementManager.currentState = PlayerMovementManager.State.Air;
        }

    }

    private void TimeUpdate(CoyoteSystem coyoteSystem)
    {
        coyoteSystem.GroundCoyoteTime();
        coyoteSystem.JumpCoyoteTime();
    }

    private void PhysicUpdate(CircleSensor groundSensor, CoyoteSystem coyoteSystem)
    {
        if (groundSensor.IsOverlap())
        {
            coyoteSystem.OnGroundTimer();
        }
    }

    private void JumpUpdate(PlayerMovementManager movementManager)
    {
        if(CanJump(movementManager))
        {
            movementManager.Jump(movementManager.movementData.jumpForce);
            movementManager.jumpCount++;
            movementManager.currentState = PlayerMovementManager.State.Air;
        }
    }

    private bool CanJump(PlayerMovementManager manager)
    {
        return !manager.isOnInteractionJumpObject && 
            manager.coyoteSystem.lastJumpEnterTime > 0.0f && 
            manager.coyoteSystem.lastOnGroundTime > 0.0f && 
            manager.movementData.maxJumpCount > 0;
    }

    private void RunUpdate(PlayerMovementManager movementManager)
    {
        float moveDirXAbs = Mathf.Abs(movementManager.player.inputPlayer.moveDir.x);

        movementManager.player.animationManager.movement = moveDirXAbs;

        if (moveDirXAbs > 0)
        {
            movementManager.player.sound.footStepLoop  = true;
        }
        else
        {
            movementManager.player.sound.footStepLoop = false;
        }

        movementManager.Run(1.0f, true);
    }



}
