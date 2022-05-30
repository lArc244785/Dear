using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundSetting : MonoBehaviour
{
    FMOD.Studio.EventInstance SPXVolumEvent;

    private FMOD.Studio.Bus m_music;
    private FMOD.Studio.Bus m_sfx;
    private FMOD.Studio.Bus m_master;

    private float m_musicVolum = 0.5f;
    private float m_SFXVolum = 0.5f;
    private float m_MasterVolum = 0.5f;

    [SerializeField]
    private Slider m_MasterSlider;

    [SerializeField]
    private Slider m_SFXSlider;

    [SerializeField]
    private Slider m_musicSlider;


    private void Awake()
    {
        
        m_sfx = FMODUnity.RuntimeManager.GetBus("bus:/Master/gameplay_sfx");
        m_music = FMODUnity.RuntimeManager.GetBus("bus:/Master/music");
        m_master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
    }
    private void Update()
    {
        m_sfx.setVolume(m_SFXVolum);
        m_music.setVolume(m_musicVolum);
        m_master.setVolume(m_MasterVolum);


        m_MasterVolum = m_MasterSlider.value;
        m_SFXVolum = m_SFXSlider.value;
        m_musicVolum = m_musicSlider.value;
    }



}
