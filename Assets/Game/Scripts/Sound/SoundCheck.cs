using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCheck : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.EventReference m_checkEvent;
    private FMOD.Studio.EventInstance m_checkInstance;

    private void Start()
    {
        m_checkInstance = FMODUnity.RuntimeManager.CreateInstance(m_checkEvent);
        SoundManager.instance.SoundPlay(m_checkInstance);
    }
}
