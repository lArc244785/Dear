using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider audioSlider;

   

    public void ToggleAudioValum()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }

    public void SetAudioVolum()
    {
        float sound = audioSlider.value;

        if (sound == -40f) audioMixer.SetFloat("BGM", -80);
        else audioMixer.SetFloat("BGM", sound);


    }
}
