using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_ConveyerBelt : InteractionBase
{
    [SerializeField]
    private Vector2 m_moveDir;
    [SerializeField]
    private float m_power;
    [SerializeField]
    private float m_targetSpeed;
    [SerializeField]
    private float m_accleRate;
    [SerializeField]
    private float m_velocityPower;

    private Rigidbody2D m_unitRig2D;



    public void Movement(MovementMangerBase mmb)
    {
        mmb.rig2D.velocity += m_moveDir * m_power;
    }

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

        float speedDif;
        if (Mathf.Abs(m_moveDir.x) > 0.0f)
            speedDif = m_targetSpeed - m_unitRig2D.velocity.x;
        else
            speedDif = m_targetSpeed - m_unitRig2D.velocity.y;

        float movement = Mathf.Pow(Mathf.Sign(speedDif) * m_accleRate, m_velocityPower);

        m_unitRig2D.AddForce(movement * m_moveDir);
    }

}
