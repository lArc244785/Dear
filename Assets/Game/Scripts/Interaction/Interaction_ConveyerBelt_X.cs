using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_ConveyerBelt_X : InteractionBase
{
    [SerializeField]
    private float m_power;


    private Rigidbody2D m_unitRig2D;



    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        m_unitRig2D = collision.GetComponent<Rigidbody2D>();

    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        m_unitRig2D = null;
 
    }

    private void Update()
    {
        if (m_unitRig2D == null)
            return;


        m_unitRig2D.AddForce(m_power * Vector2.right);
    }

}
