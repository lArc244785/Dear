using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSound : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.EventReference m_soundEvent;

    public void Play()
    {
        SoundManager.instance.SoundOneShot(m_soundEvent);
    }


}
