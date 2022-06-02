using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingleToon<SoundManager>
{
    private Bgm m_bgm;

    public Bgm bgm
    {
        get
        {
            return m_bgm;
        }
    }

    private void Awake()
    {
        Init();
    }

    protected override bool Init()
    {
        bool isNotOverlap = base.Init();
        if (!isNotOverlap) 
            return false;

        m_bgm = GetComponent<Bgm>();
        m_bgm.Init();

        return true;
    }

    public void SoundPlay(FMOD.Studio.EventInstance eventInstance)
    {
        if(FMOD.Studio.PLAYBACK_STATE.PLAYING == GetPlayState(eventInstance))
        {
           
            Debug.LogWarning("Sound Overlap : " + eventInstance);
            return;
        }
        

        eventInstance.start();
    }

    public void SoundStop(FMOD.Studio.EventInstance eventInstance)
    {
        if (FMOD.Studio.PLAYBACK_STATE.PLAYING != GetPlayState(eventInstance))
        {
            Debug.LogWarning("Sound Not Play : " + eventInstance);
            return;
        }

        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SoundOneShot(FMODUnity.EventReference reference)
    {
        FMODUnity.RuntimeManager.PlayOneShot(reference);
    }

    public FMOD.Studio.PLAYBACK_STATE GetPlayState(FMOD.Studio.EventInstance eventInstance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        eventInstance.getPlaybackState(out state);
        return state;
    }

    public void GetID(in FMOD.Studio.EventInstance eventInstance, string name, out FMOD.Studio.PARAMETER_ID id)
    {
        FMOD.Studio.EventDescription description;
        FMOD.Studio.PARAMETER_DESCRIPTION parameter;

        eventInstance.getDescription(out description);
        description.getParameterDescriptionByName(name, out parameter);
        id = parameter.id;
    }
  

}
