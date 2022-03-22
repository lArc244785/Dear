using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSlingShot : InteractionBase
{
    [SerializeField]
    private float m_power;

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        PlayerMovementManager pmm = collision.GetComponent<Unit_Player>().playerMovemntManager;
        pmm.isSlingAction = true;
        pmm.bustMovement.power = m_power;
        UIManager.instance.inGameView.slingShot.targetTr = transform;
    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        collision.GetComponent<Unit_Player>().playerMovemntManager.isSlingAction = false;
        UIManager.instance.inGameView.slingShot.targetTr = null;
    }
}
