using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadStateIdle : MadStateBase
{


    public override void Enter(Mad mad)
    {
        Debug.Log("Idle");
    }

    public override void Exit(Mad mad)
    {
        
    }

    public override void UpdateProcesses(Mad mad)
    {
        ChangeUpdate(mad);
    }

    public override void FixedProcesses(Mad mad)
    {
        
    }

    protected override void ChangeUpdate(Mad mad)
    {
        if (CanTracking(mad))
            mad.ChangeState(Mad.State.Tracking);
    }

    private bool CanTracking(Mad mad)
    {
        return mad.IsTrackingRangeOver();
    }
}
