using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private float m_currentTime;



    private bool m_isShake;

    private CinemachineBasicMultiChannelPerlin m_perlinShake;


    public bool Shake(CinemachineBasicMultiChannelPerlin perlinShake, float intensity, float time)
    {
        if (perlinShake == null)
            return false;

        m_perlinShake = perlinShake;
        m_currentTime = time;

        m_perlinShake.m_AmplitudeGain = intensity;
        m_isShake = true;

        return true;
    }



    // Update is called once per frame
    void Update()
    {
        if (!m_isShake)
            return;

        m_currentTime -= Time.deltaTime;


        if(m_currentTime <= 0.0f)
        {
            m_perlinShake.m_AmplitudeGain = 0.0f;
            m_isShake = false;
        }

    }
}
