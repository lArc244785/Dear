using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MadDatas/MadMissileData")]
public class MadMissileData : ScriptableObject
{
    [Header("LifeTime")]
    [SerializeField]
    private float m_lifeTime;
    public float lifeTime
    {
        get
        {
            return m_lifeTime;
        }
    }



    [Header("Acceleration")]
    [SerializeField]
    private float m_maxSpeed;
    public float maxSpeed
    {
        get
        {
            return m_maxSpeed;
        }
    }
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float m_accle;
    public float accele
    {
        get
        {
            return m_accle;
        }
    }
    [SerializeField]
    [Range(.5f, 5.0f)]
    private float m_acclePower;
    public float acclePower
    {
        get
        {
            return m_acclePower;
        }
    }


}
