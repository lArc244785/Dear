using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBgm : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.EventReference m_bgmEvent;
    [SerializeField]
    private string m_id;

    private FMOD.Studio.EventInstance m_bgmInstance;
    private FMOD.Studio.PARAMETER_ID m_bgmID;

    [SerializeField]
    private float a;

    public void Init()
    {
        m_bgmInstance = FMODUnity.RuntimeManager.CreateInstance(m_bgmEvent);
        SoundManager.instance.GetID(m_bgmInstance, m_id, out m_bgmID);
        
    }

    public void BgmStart()
    {
        SoundManager.instance.SoundPlay(m_bgmInstance);
    }

    public void SetParamater(float value)
    {
        m_bgmInstance.setParameterByID(m_bgmID, value);
    }

    private void Update()
    {
        SetParamater(a);
    }

}
