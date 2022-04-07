using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRopeState : I_MovementState
{
    public void Enter(A_MovementManager manager)
    {
        //manager.SetGravity(0.0f);
    }

    public void Exit(A_MovementManager manager)
    {

    }

    public void FixedExcute(A_MovementManager manager)
    {
        if (Mathf.Abs(manager.inputPlayer.moveDir.x) >= 0.0f)
            manager.Run(1.0f);
    }

    public void UpdateExcute(A_MovementManager manager)
    {
        
    }
}
