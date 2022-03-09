using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_PlayerMovement
{
    public void Init(PlayerMovementManager pmm);

    public void Enter();

    public void Movement();
}
