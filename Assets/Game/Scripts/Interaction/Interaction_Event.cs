using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction_Event : InteractionBase
{
    [SerializeField]
    private UnityEvent m_interactionEvent;

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
            collision.gameObject.GetComponent<PlayerInteraction>().InteractionEvent = m_interactionEvent;
       
    }



    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        collision.gameObject.GetComponent<PlayerInteraction>().InteractionEvent = null;
    }



}
