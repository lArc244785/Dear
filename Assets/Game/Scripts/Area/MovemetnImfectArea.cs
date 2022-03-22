using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovemetnImfectArea : InteractionBase,I_AddMovement
{
    [SerializeField]
    private float m_attenuationX;

    private Unit_Player m_player;

    public void Movement(MovementMangerBase mmb)
    {
        if (mmb.moveDir == Vector2.zero)
            return;

        Vector2 attenuation = new Vector2(m_attenuationX,0.0f);
        attenuation *= m_player.playerMovemntManager.lookDirX;

        mmb.rig2D.velocity -= attenuation;

    }

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        m_player = collision.GetComponent<Unit_Player>();
        m_player.isJump = false;
        m_player.movementManager.addMovement = this;
    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        m_player.isJump = true;

        m_player = null;
        m_player.movementManager.addMovement = null;
    }
}
