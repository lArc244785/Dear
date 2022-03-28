using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBust : InteractionBase
{
    [SerializeField]
    private BustMoveData m_bustMoveData;

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);

        PlayerMovementManager pmm = collision.GetComponent<Unit_Player>().playerMovemntManager;
        pmm.currentType = PlayerMovementManager.MOVEMENT_TYPE.BUST;

        Vector2 dir = pmm.unitBase.unitPos -(Vector2)transform.position;
        dir.Normalize();

        pmm.bustMovement.bustMoveData = m_bustMoveData;
        pmm.bustMovement.Bust(dir);
    }
    


}
