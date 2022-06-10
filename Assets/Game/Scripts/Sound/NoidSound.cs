using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoidSound : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.EventReference m_hitEvent;

    [SerializeField]
    private FMODUnity.EventReference m_deathEvent;

    public void Hit()
    {
        SoundManager.instance.SoundOneShot(m_hitEvent);
        SoundManager.instance.bgm.SetParamaterHitIndexID(1.0f);
    }
    public void Death()
    {
        SoundManager.instance.SoundOneShot(m_deathEvent);
    }


}
