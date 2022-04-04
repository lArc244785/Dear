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

    }
    


}
