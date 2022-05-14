using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrapEvent : InteractionBase
{
    [SerializeField]
    private UnityEvent m_trapEvent;
    [SerializeField]
    private UnityEvent m_trapResetEvent;


    [Header("Option")]
    [SerializeField]
    private float m_trapActiveTime;
    [SerializeField]
    private bool m_lopeTrap;
    [SerializeField]
    private float m_resetTime;


    private bool m_unitPush;

    private IEnumerator m_trapCoroutine = null;

    protected override void Enter(Collider2D collision)
    {
        if (isTrapAction)
            return;

        base.Enter(collision);
        m_unitPush = true;
        TrapStart();
    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        m_unitPush = false;
    }

    private void TrapOn()
    {
        m_trapEvent.Invoke();
    }

    private void TrapReset()
    {
        m_trapResetEvent.Invoke();
    }

    private IEnumerator trapEventCoroutine()
    {
        float time = 0.0f;
        while (time <= m_trapActiveTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        TrapOn();

        if (m_lopeTrap)
        {
            time = 0.0f;
            while (time <= m_resetTime)
            {
                time += Time.deltaTime;
                yield return null;
            }

            TrapReset();
            if(m_unitPush)
            {
                TrapStart();
            }
            else
            {
                TrapStop();
            }
        }
    }

    private void TrapStart()
    {
        m_trapCoroutine = trapEventCoroutine();
        StartCoroutine(m_trapCoroutine);
    }


    private void TrapStop()
    {
        if (m_trapCoroutine != null)
            StopCoroutine(m_trapCoroutine);

        m_trapCoroutine = null;

    }

    private bool isTrapAction
    {
        get
        {
            return m_trapCoroutine != null;
        }
    }


}
