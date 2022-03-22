using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSpring : InteractionBase
{
    [SerializeField]
    private float m_power;

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        PlayerMovementManager pmm = collision.GetComponent<Unit_Player>().playerMovemntManager;
        pmm.rig2D.velocity = Vector2.up * m_power;
    }
}
