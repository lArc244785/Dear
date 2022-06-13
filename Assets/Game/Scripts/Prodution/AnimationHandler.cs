using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_event;

    public void Action()
    {
        m_event.Invoke();
    }

}
