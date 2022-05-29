using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateHit : I_MovementState
{
    private float m_enterTime;

    public void Enter(PlayerMovementManager movementManager)
    {
        movementManager.player.inputPlayer.SetControl(false);

        m_enterTime = Time.time;
        Vector2 force = movementManager.hitImfectDir * movementManager.movementData.hitImfectPower;
        movementManager.player.rig2D.velocity = Vector2.zero;
        movementManager.VectorJump(force);
    }

    public void Exit(PlayerMovementManager movementManager)
    {
        movementManager.jumpCount = 0;
        movementManager.player.inputPlayer.SetControl(true);
    }

    public void FixedExcute(PlayerMovementManager movementManager)
    {
        movementManager.Resistance(movementManager.movementData.resistanceInAirAmount);
        movementManager.Run(movementManager.movementData.hitImfectRunLerp, false);
    }

    public void UpdateExcute(PlayerMovementManager movementManager)
    {
        float duringTime = Time.time - m_enterTime;
        if(duringTime > movementManager.movementData.hitImfectDuringTime)
        {
            NextState(movementManager);
            return;
        }



            
    }

    private void NextState(PlayerMovementManager movementManager)
    {
        if(movementManager.IsGrounded())
        {
            movementManager.currentState = PlayerMovementManager.State.Ground;
        }
        else
        {
            movementManager.currentState = PlayerMovementManager.State.Air;
        }
    }


}
