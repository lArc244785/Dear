using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.EventReference m_bgmEvent;
    [SerializeField]
    private string m_prograssID;
    [SerializeField]
    private string m_hitIndexID;

    private FMOD.Studio.EventInstance m_bgmInstance;
    private FMOD.Studio.PARAMETER_ID m_parameterPrograss;
    private FMOD.Studio.PARAMETER_ID m_parameterHitIndexID;

    public void Init()
    {
        m_bgmInstance = FMODUnity.RuntimeManager.CreateInstance(m_bgmEvent);
        SoundManager.instance.GetID(m_bgmInstance, m_prograssID, out m_parameterPrograss);
        SoundManager.instance.GetID(m_bgmInstance, m_hitIndexID, out m_parameterHitIndexID);
        BgmStart();
    }

    public void BgmStart()
    {
        SoundManager.instance.SoundPlay(m_bgmInstance);
    }

    public void SetParamaterPrograss(float value)
    {
        m_bgmInstance.setParameterByID(m_parameterPrograss, value);
    }

    public void SetParamaterHitIndexID(float value)
    {
        m_bgmInstance.setParameterByID(m_parameterHitIndexID, value);
    }


}
