using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_ConveyerBelt : InteractionBase, I_AddMovement
{
    [SerializeField]
    private Vector2 m_moveDir;
    [SerializeField]
    private float m_power;


    public void Movement(MovementMangerBase mmb)
    {
        mmb.rig2D.velocity += m_moveDir * m_power;
    }

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        collision.GetComponent<UnitBase>().movementManager.addMovement = this;
    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        collision.GetComponent<UnitBase>().movementManager.addMovement = null;
    }
}
