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
    [SerializeField]
    private Ease m_moveEase;


    [Header("Animation")]
    [SerializeField]
    private Animator m_ani;
    [SerializeField]
    private Transform m_aniTr;


    private UnitPlayer m_player;




    protected override void Enter(Collider2D collision)
    {
        if (m_isAction)
            return;

        base.Enter(collision);

        m_player = GameManager.instance.stageManager.player;

        m_player.inputPlayer.SetControl(false);
        m_player.rig2D.velocity = Vector2.zero;

        m_player.rig2D.bodyType = RigidbodyType2D.Static;
        m_player.transform.parent = m_aniTr;

        m_ani.SetTrigger("Contact");
    }


    public void Move()
    {


        m_moveObjectTr.DOMove(m_endPointTr.position, m_moveTime).OnComplete(() =>
        {
            m_player.inputPlayer.SetControl(true);
            m_player.rig2D.bodyType = RigidbodyType2D.Dynamic;
            m_player.transform.parent = null;


            m_isAction = true;
            
        }).SetEase(m_moveEase).Play();

        Invoke("StopAnimation", m_moveTime - 0.4f);

    }

    private void StopAnimation()
    {
        m_ani.SetTrigger("End");
    }



}
