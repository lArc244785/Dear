using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSlingShot : InteractionBase
{
    [SerializeField]
    private BustMoveData m_bustMoveData;




    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);



    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);

    }

    public BustMoveData bustMoveData
    {
        get
        {
            return m_bustMoveData;
        }
    }
}
