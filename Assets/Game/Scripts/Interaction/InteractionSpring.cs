using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSpring : InteractionBase
{
    [SerializeField]
    private float m_force;
    private float force
    {
        get
        {
            return m_force;
        }
    }
    [SerializeField]
    private float m_superJumpMul;
    private float superJumpMul
    {
        get
        {
            return m_superJumpMul;
        }
    }


    [SerializeField]
    private float m_InteractionJumpWait;
    private float InteractionJumpWait 
    { 
        get 
        {
            return m_InteractionJumpWait;
        } 
    }

    private float m_currentWaitTime;


    private PlayerMovementManager m_movement;

    private bool m_isSuperJump;


    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        m_movement = collision.GetComponent<PlayerMovementManager>(); ;

        m_movement.isOnInteractionJumpObject = true;
         m_currentWaitTime = InteractionJumpWait;

    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);

        if (m_movement == null)
            return;

        m_isSuperJump = false;
        m_movement.isOnInteractionJumpObject = false;
    }

    private void Update()
    {
        if (m_movement == null)
            return;

        if(m_currentWaitTime <= 0.0f)
        {
            if (!m_isSuperJump)
                NormalJump();
            else
                SuperJump();
        }

        m_currentWaitTime -= Time.deltaTime;


    }


    private void NormalJump()
    {
        if (m_movement == null)
            return;

        m_movement.player.rig2D.velocity = Vector2.zero;
        m_movement.Jump(force);
        m_movement = null;
    }

    private void SuperJump()
    {
        if (m_movement == null)
            return;

        m_movement.player.rig2D.velocity = Vector2.zero;
        m_movement.Jump(force * superJumpMul);
        m_movement = null;
    }

    public void OnSuperJump()
    {
        m_isSuperJump = true;
    }


}
