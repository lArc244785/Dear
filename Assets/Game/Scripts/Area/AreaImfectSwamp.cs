using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaImfectSwamp : InteractionBase
{
    [SerializeField]
    MovementData m_areaMovementData;
    private MovementData m_beforeMovementData;

    private A_MovementManager movementManager;

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);

        movementManager = collision.GetComponent<A_MovementManager>();

        m_beforeMovementData = movementManager.movementData;

        movementManager.movementData = m_areaMovementData;

    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);

        movementManager.movementData = m_beforeMovementData;
    }


}
