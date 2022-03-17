using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSlingShot : InteractionBase
{
    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        collision.GetComponent<Unit_Player>().playerMovemntManager.isSlingAction = true;
        UIManager.instance.inGameView.slingShot.targetTr = transform;
    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        collision.GetComponent<Unit_Player>().playerMovemntManager.isSlingAction = false;
        UIManager.instance.inGameView.slingShot.targetTr = null;
    }
}
