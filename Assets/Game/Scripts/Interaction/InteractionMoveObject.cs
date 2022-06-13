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

    [Header("Sound")]
    [SerializeField]
    private FMODUnity.EventReference m_rideEvent;

    [SerializeField]
    private FMODUnity.EventReference m_moveEvent;

    [SerializeField]
    private FMODUnity.EventReference m_moveEndEvent;
    [SerializeField]
    private float m_moveTick;


    private bool m_isMove = false;


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
        m_isMove = true;

        StartCoroutine(MoveSoundEventCoroutine());


        m_moveObjectTr.DOMove(m_endPointTr.position, m_moveTime).OnComplete(() =>
        {
            m_player.inputPlayer.SetControl(true);
            m_player.rig2D.bodyType = RigidbodyType2D.Dynamic;
            m_player.transform.parent = null;


            m_isAction = true;
            m_isMove = false;

        }).SetEase(m_moveEase).Play();

        Invoke("StopAnimation", m_moveTime - 0.4f);

    }

    private void StopAnimation()
    {
        m_ani.SetTrigger("End");
    }

    public void RideSound()
    {
        SoundManager.instance.SoundOneShot(m_rideEvent);
    }

    private IEnumerator MoveSoundEventCoroutine()
    {

        while(m_isMove)
        {
            SoundManager.instance.SoundOneShot(m_moveEvent);

            yield return new WaitForSeconds(m_moveTick);
        }
    }


    public void MoveStopSound()
    {
        SoundManager.instance.SoundOneShot(m_moveEndEvent);
    }


}
