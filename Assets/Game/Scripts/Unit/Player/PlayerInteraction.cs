using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{

    private UnityEvent m_interactionEvent;

    private bool m_isInteraction;


    public void Init()
    {
        m_interactionEvent = new UnityEvent();
        m_isInteraction = false;
    }


    public void Interaction()
    {
        m_interactionEvent.Invoke();
        RemoveAllInteracion();
    }

    public void RemoveAllInteracion()
    {
        m_interactionEvent.RemoveAllListeners();
        m_isInteraction = false;
    }


    public void AddInteraction(UnityAction action)
    {
        m_interactionEvent.AddListener(action);
        m_isInteraction = true;
    }

    public bool CanInteracion()
    {
        return m_isInteraction;
    }


}
