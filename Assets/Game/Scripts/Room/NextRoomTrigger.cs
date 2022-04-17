using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomTrigger : InteractionBase
{
    [SerializeField]
    private int m_nextRoomIndex;

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        GameManager.instance.stageManager.roomManager.NextRoom(m_nextRoomIndex);
    }
}
