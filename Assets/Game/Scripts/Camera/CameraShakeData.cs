using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CameraDatas/CameraShakeData")]
public class CameraShakeData : ScriptableObject
{
    [SerializeField]
    private float m_intensity;
    public float Intensity
    {
        get
        {
            return m_intensity;
        }
    }
    [SerializeField]
    private float m_time;
    public float time
    {
        get
        {
            return m_time;
        }
    }


}
