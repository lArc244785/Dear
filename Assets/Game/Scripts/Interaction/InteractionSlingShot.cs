using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSlingShot : InteractionBase
{
    [SerializeField]
    private BustMoveData m_bustMoveData;

    private PlayerMovementManager m_playerMovementManager;


    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);

        m_playerMovementManager = collision.GetComponent<Unit_Player>().playerMovemntManager;

        m_playerMovementManager.bustMovement.enterSlingShot = this;

    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);

        m_playerMovementManager.bustMovement.enterSlingShot = null;
    }

    public BustMoveData bustMoveData
    {
        get
        {
            return m_bustMoveData;
        }
    }
}
