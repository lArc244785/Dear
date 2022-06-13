using UnityEngine;

public class Bgm : MonoBehaviour
{
    public enum BgmType
    {
        None = -1, Forest, Factory, Boss, Total
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


    [Header("Ambient")]
    [SerializeField]
    private FMODUnity.EventReference m_ambientEvent;
    private FMOD.Studio.EventInstance m_ambientEventInstances;
    private FMOD.Studio.PARAMETER_ID m_parameterAmbient;

    private string m_ambientID = "inout_index";


    public void Init()
    {
        m_bgmInstances = new FMOD.Studio.EventInstance[(int)BgmType.Total];
        m_parameterPrograss = new FMOD.Studio.PARAMETER_ID[(int)BgmType.Total];
        m_parameterHitIndexIDs = new FMOD.Studio.PARAMETER_ID[(int)BgmType.Total];

        for (int i = 0; i < m_bgmEvents.Length; i++)
        {
            m_bgmInstances[i] = FMODUnity.RuntimeManager.CreateInstance(m_bgmEvents[i]);
            SoundManager.instance.GetID(m_bgmInstances[i], m_prograssID, out m_parameterPrograss[i]);
            SoundManager.instance.GetID(m_bgmInstances[i], m_hitIndexID, out m_parameterHitIndexIDs[i]);
        }


        m_ambientEventInstances = FMODUnity.RuntimeManager.CreateInstance(m_ambientEvent);
        SoundManager.instance.GetID(m_ambientEventInstances, m_ambientID, out m_parameterAmbient);


        BgmStart();
    }


    public void BgmChange(BgmType type)
    {
        if (type == m_currentBgm || type == BgmType.None)
            return;

        float ambient = 0.0f;

        BgmStop();
        m_currentBgm = type;
        if (m_currentBgm == BgmType.Factory || m_currentBgm == BgmType.Boss)
            ambient = 1.0f;

        SetParamaterAmbient(ambient);
        BgmStart();
    }

    public void BgmStart()
    {
        if (m_currentBgm == BgmType.None)
            return;

        SoundManager.instance.SoundPlay(m_bgmInstances[(int)m_currentBgm]);
        SoundManager.instance.SoundPlay(m_ambientEventInstances);
    }


    public void BgmStop()
    {
        if (m_currentBgm == BgmType.None)
            return;

        SoundManager.instance.SoundStop(m_bgmInstances[(int)m_currentBgm]);
        SoundManager.instance.SoundStop(m_ambientEventInstances);
    }


    public void SetParamaterPrograss(float value)
    {
        m_bgmInstances[(int)m_currentBgm].setParameterByID(m_parameterPrograss[(int)m_currentBgm], value);
    }

    public void SetParamaterHitIndexID(float value)
    {
        m_bgmInstances[(int)m_currentBgm].setParameterByID(m_parameterHitIndexIDs[(int)m_currentBgm], value);
    }

    public void SetParamaterAmbient(float value)
    {
        m_ambientEventInstances.setParameterByID(m_parameterAmbient, value);
    }

    public bool IsSoundStop()
    {
        if (m_currentBgm == BgmType.None)
            return false;

        FMOD.Studio.PLAYBACK_STATE state = SoundManager.instance.GetPlayState(m_bgmInstances[(int)m_currentBgm]);

        return state == FMOD.Studio.PLAYBACK_STATE.STOPPED || state == FMOD.Studio.PLAYBACK_STATE.STOPPING;
    }


}
