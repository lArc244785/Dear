using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{

    private UnityEvent m_interactionEvent;

    public void Init()
    {

    }


    public UnityEvent InteractionEvent
    {
        set
        {
            Debug.Log("Interaction: " + value);
            m_interactionEvent = value;
        }
        get
        {
            return m_interactionEvent;
        }
    }

    public void Interaction()
    {
        m_interactionEvent.Invoke();
    }


}
