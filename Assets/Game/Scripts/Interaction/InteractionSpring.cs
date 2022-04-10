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
        PlayerMovementManager movement = collision.GetComponent<PlayerMovementManager>(); ;
        Rigidbody2D rig2D = movement.rig2D;

        float power = m_power;

        if(rig2D.velocity.y < 0.0f)
            power -= rig2D.velocity.y;

        
        rig2D.AddForce(Vector2.up * power, ForceMode2D.Impulse);
    }
}
