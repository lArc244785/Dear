using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRopeJumpState : I_MovementState
{   
    private float duringTime { set; get; }


    public void Enter(PlayerMovementManager movementManager)
    {
        Vector2 jumpDir = Vector2.up;
        movementManager.player.rig2D.gravityScale = movementManager.movementData.ropeJumpGravityScale;
        
        
        jumpDir.x = movementManager.player.inputPlayer.moveDir.x * movementManager.movementData.ropeJumpXMultiply;

        movementManager.VectorJump(jumpDir * movementManager.movementData.ropeJumpForce);

        duringTime = movementManager.movementData.ropeJumpDuringTime;

        movementManager.player.inputPlayer.isControl = false;

        movementManager.player.animationManager.TriggerJump();
    }

    public void Exit(PlayerMovementManager movementManager)
    {
        movementManager.player.inputPlayer.isControl = true;
    }

    public void FixedExcute(PlayerMovementManager movementManager)
    {
        if(duringTime <= 0.0f)
        {
            ChangeState(movementManager);
            return;
        }

        movementManager.Resistance(movementManager.movementData.resistanceInAirAmount);
        movementManager.Run(0.25f, true);


        duringTime -= Time.deltaTime;
    }

    public void UpdateExcute(PlayerMovementManager movementManager)
    {
    }

    private void ChangeState(PlayerMovementManager movementManager)
    {
        movementManager.currentState = PlayerMovementManager.State.Air;
    }
}
