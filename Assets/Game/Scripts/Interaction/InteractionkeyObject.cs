using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionkeyObject : InteractionBase
{
    [SerializeField]
    private GameObject m_onInteractionObject;
    [SerializeField]
    private InteractionEventBase m_onInteractionEvent;

    UnitPlayer m_player;



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
