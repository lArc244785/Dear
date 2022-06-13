using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntecationHp : InteractionBase
{
    [SerializeField]
    private int m_heal;

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        UnitPlayer player = collision.GetComponent<UnitPlayer>();
        if (player == null)
            return;

        player.HealHp(m_heal);

        gameObject.SetActive(false);
    }
}
