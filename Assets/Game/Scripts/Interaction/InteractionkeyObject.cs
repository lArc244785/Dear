using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionkeyObject : InteractionBase
{
    [SerializeField]
    private GameObject m_onInteractionObject;
    [SerializeField]
    private InteractionEventBase m_onInteractionEvent;

    UnitPlayer m_player;

    [SerializeField]
    private GetTool m_getTool;


    private void Awake()
    {
        m_onInteractionObject.SetActive(false);
    }



    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);

        m_player = collision.GetComponent<UnitPlayer>();
        if (m_player == null)
            return;


        m_player.interaction.AddInteraction(m_onInteractionEvent.InteractionEvent);
        m_player.interaction.AddInteraction(OffInteracion);
        if (m_getTool != null)
            m_player.interaction.AddInteraction(m_getTool.Get);


        m_onInteractionObject.SetActive(true);


    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        OffInteracion();

        if (m_player == null)
            return;


        m_player.interaction.RemoveAllInteracion();

    }

    public void OffInteracion()
    {
        m_onInteractionObject.SetActive(false);
    }


}
