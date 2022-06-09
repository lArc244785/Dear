using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRespwanTrap : InteractionBase
{
    [SerializeField]
    private OneSound m_oneSound;

    private Vector2 m_lastPlayerGroundPos;
    private Vector2 lastPlayerGroundPos
    {
        get
        {
            return m_lastPlayerGroundPos;  
        }
    }

    public void SetLastPlayerGroundPoint(Transform tr)
    {
        m_lastPlayerGroundPos = tr.position;
    }

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        UnitPlayer player = collision.GetComponent<UnitPlayer>();
        if (player == null)
            return;
        if (m_oneSound != null)
            m_oneSound.Play();
        player.OnRespawnHit(lastPlayerGroundPos, 1);

    }





}
