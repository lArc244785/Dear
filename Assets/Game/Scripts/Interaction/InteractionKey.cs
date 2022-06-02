using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionKey : InteractionBase
{
    [SerializeField]
    private GameObject m_interactionAbleObject;

    [SerializeField]
    private UnityEvent m_event;

    private void Start()
    {
        SetUIObjectActive(false);
    }


    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        SetUIObjectActive(true);


    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        SetUIObjectActive(false);
    }

    public void SetUIObjectActive(bool isActive)
    {
        m_interactionAbleObject.SetActive(isActive);
    }

}
