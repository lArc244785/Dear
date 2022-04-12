using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHitState : I_MovementState
{
    private float m_enterTime;

    public void Enter(PlayerMovementManager manager)
    {
        m_enterTime = Time.time;

        Vector2 force = manager.hitImfectDir * manager.movementData.hitImfectPower;
        manager.rig2D.velocity = Vector2.zero;
        manager.VectorJump(force);
    }

    public void Exit(PlayerMovementManager manager)
    {
        manager.jumpCount = 0;
    }

    public void FixedExcute(PlayerMovementManager manager)
    {
        manager.Resistance(manager.movementData.resistanceInAirAmount);
        manager.Run(manager.movementData.hitImfectRunLerp, false);
    }

    public void UpdateExcute(PlayerMovementManager manager)
    {
        float duringTime = Time.time - m_enterTime;
        if(duringTime > manager.movementData.hitImfectDuringTime)
        {
            NextState(manager);
            return;
        }



            
    }

    private void NextState(PlayerMovementManager manager)
    {
        if(manager.IsGrounded())
        {
            manager.currentState = PlayerMovementManager.State.Ground;
        }
        else
        {
            manager.currentState = PlayerMovementManager.State.Air;
        }
    }


}
