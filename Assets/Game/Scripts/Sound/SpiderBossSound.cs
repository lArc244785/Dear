using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossSound : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.EventReference m_rightArmEvent;
    [SerializeField]
    private FMODUnity.EventReference m_leftArmEvent;
    [SerializeField]
    private FMODUnity.EventReference m_moveUpEvent;
    [SerializeField]
    private FMODUnity.EventReference m_ambEvent;
    private FMOD.Studio.EventInstance m_ambInstance;
    private FMOD.Studio.PARAMETER_ID m_ambID;
    [SerializeField]
    private FMODUnity.EventReference m_passWayEvent;

    public void init()
    {
        m_ambInstance = FMODUnity.RuntimeManager.CreateInstance(m_ambEvent);
    }

    public void LeftArm()
    {
        SoundManager.instance.SoundOneShot(m_leftArmEvent);
        
    }
    public void RightArm()
    {
        SoundManager.instance.SoundOneShot(m_rightArmEvent);
    }
    public void MoveUp()
    {
        SoundManager.instance.SoundOneShot(m_moveUpEvent);
    }
    public void Amb()
    {
        SoundManager.instance.SoundPlay(m_ambInstance);
    }
    public void AmbStop()
    {
        SoundManager.instance.SoundStop(m_ambInstance);

    }


    public void PassWay()
    {
        SoundManager.instance.SoundOneShot(m_passWayEvent);
    }

}
