using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ToolDatas/GrappingData")]
public class GrappingGunMovementData : ScriptableObject
{
    [Header("CoolTime")]
    [SerializeField]
    private float m_coolTime;
    public float coolTime
    {
        get
        {
            return m_coolTime;
        }
    }

    #region Rope
    [Header("Rope")]
    [SerializeField]
    private float m_maxDistance;
    public float maxDistance
    {
        get { return m_maxDistance; }
    }

    #endregion
            
    #region Hook
    [Header("Hook")]
    [SerializeField]
    private float m_hookMaxSpeed;
    public float hookMaxSpeed
    {
        get
        {
            return m_hookMaxSpeed;
        }
    }
    [SerializeField]
    private float m_hookAcclelation;
    public float hookAcclelation
    {
        get
        {
            return m_hookAcclelation;
        }
    }
    [Range(0.0f, 3.0f)]
    [SerializeField]
    private float m_hookVelocityPower;
    public float hookVelocityPower
    {
        get
        {
            return m_hookVelocityPower;
        }
    }
    #endregion

    #region Pull
    [Header("Pull")]
    [SerializeField]
    private float m_pullMaxSpeed;
    public float pullMaxSpeed
    {
        get
        {
            return m_pullMaxSpeed;
        }
    }
    [SerializeField]
    private float m_pullAcclelation;
    public float pullAcclelation
    {
        get
        {
            return m_pullAcclelation;
        }
    }
    [Range(0.0f, 3.0f)]
    [SerializeField]
    private float m_pullVelcoityPower;
    public float pullVelcoityPower
    {
        get
        {
            return m_pullVelcoityPower;
        }
    }

    [SerializeField]
    private float m_pullStopMaxLength;
    public float pullStopMaxLength
    {
        get
        {
            return m_pullStopMaxLength;
        }
    }

    #endregion



}
