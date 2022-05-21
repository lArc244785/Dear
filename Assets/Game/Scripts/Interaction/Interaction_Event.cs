using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction_Event : InteractionBase
{
    [SerializeField]
    private UnityEvent m_enterEvent;
    private UnityEvent enterEvent { get { return m_enterEvent; } }

    [SerializeField]
    private UnityEvent m_exitEvent;
    private UnityEvent exitEvent { get { return m_exitEvent; } }

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        enterEvent.Invoke();


    }



    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        exitEvent.Invoke();
    }



}
