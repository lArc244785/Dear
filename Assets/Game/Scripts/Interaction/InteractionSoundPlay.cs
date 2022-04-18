using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSoundPlay : InteractionBase
{
    [SerializeField]
    private FMODUnity.EventReference m_aroundEvent;
    private FMOD.Studio.EventInstance m_aroundInstance;

    [SerializeField]
    private FMODUnity.EventReference m_imfectEvnet;
    private FMOD.Studio.EventInstance m_imfectInstance;

    private void Start()
    {
        m_aroundInstance = FMODUnity.RuntimeManager.CreateInstance(m_aroundEvent);
       
        SoundManager.instance.SoundPlay(m_aroundInstance);

        if (!m_imfectEvnet.IsNull)
        {
            m_imfectInstance = FMODUnity.RuntimeManager.CreateInstance(m_imfectEvnet);
        }
    }

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        //if(!m_aroundEvent.IsNull)
        //SoundManager.instance.SoundPlay(m_aroundInstance);
    }

}
