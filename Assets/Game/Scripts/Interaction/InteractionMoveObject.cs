using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractionMoveObject : InteractionBase
{
    private bool m_isAction = false;
    [SerializeField]
    private Transform m_moveObjectTr;
    [SerializeField]
    private Transform m_endPointTr;
    [SerializeField]
    private float m_moveTime;

    protected override void Enter(Collider2D collision)
    {
        if (m_isAction)
            return;

        base.Enter(collision);
        Move();
    }


    private void Move()
    {
        UnitPlayer player = GameManager.instance.stageManager.player;
        player.transform.parent = m_moveObjectTr;
        player.inputPlayer.SetControl(false);


        m_moveObjectTr.DOMove(m_endPointTr.position, m_moveTime).OnComplete(() =>
        {
            GameManager.instance.stageManager.player.inputPlayer.SetControl(true);
            m_isAction = true;
        }).Play();

    }



}
