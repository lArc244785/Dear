using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MadStateBase
{
    public abstract void Enter(Mad mad);

    public abstract void Exit(Mad mad);

    public abstract void UpdateProcesses(Mad mad);

    public abstract void FixedProcesses(Mad mad);

    protected abstract void ChangeUpdate(Mad mad);

}
