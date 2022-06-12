using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    public enum BgmType
    {
        Forest, Factory, Total
    }
    [SerializeField]
    private BgmType m_currentBgm;


    [SerializeField]
    private FMODUnity.EventReference[] m_bgmEvents;
    [SerializeField]
    private string m_prograssID;
    [SerializeField]
    private string m_hitIndexID;

    private FMOD.Studio.EventInstance[] m_bgmInstances;
    private FMOD.Studio.PARAMETER_ID[] m_parameterPrograss;
    private FMOD.Studio.PARAMETER_ID[] m_parameterHitIndexIDs;

    public void Init()
    {
        m_bgmInstances = new FMOD.Studio.EventInstance[(int)BgmType.Total];
        m_parameterPrograss = new FMOD.Studio.PARAMETER_ID[(int)BgmType.Total];
        m_parameterHitIndexIDs = new FMOD.Studio.PARAMETER_ID[(int)BgmType.Total];

        for (int i = 0; i < m_bgmEvents.Length ; i ++)
        {
            m_bgmInstances[i] = FMODUnity.RuntimeManager.CreateInstance(m_bgmEvents[i]);
            SoundManager.instance.GetID(m_bgmInstances[i], m_prograssID, out m_parameterPrograss[i]);
            SoundManager.instance.GetID(m_bgmInstances[i], m_hitIndexID, out m_parameterHitIndexIDs[i]);
        }
        


        BgmStart();
    }


    public void BgmChange(BgmType type)
    {
        if (type == m_currentBgm)
            return;

        BgmStop();
        m_currentBgm = type;
        BgmStart();
    }

    public void BgmStart()
    {
        SoundManager.instance.SoundPlay(m_bgmInstances[(int)m_currentBgm]);
    }

    private void BgmStop()
    {
        SoundManager.instance.SoundStop(m_bgmInstances[(int)m_currentBgm]);
    }


    public void SetParamaterPrograss(float value)
    {
        m_bgmInstances[(int)m_currentBgm].setParameterByID(m_parameterPrograss[(int)m_currentBgm], value);
    }

    public void SetParamaterHitIndexID(float value)
    {
        m_bgmInstances[(int)m_currentBgm].setParameterByID(m_parameterHitIndexIDs[(int)m_currentBgm], value);
    }


}
