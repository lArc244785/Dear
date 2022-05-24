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
    [SerializeField]
    private AnimationCurve m_ropeAnimationCurve;
    public AnimationCurve ropeAnimationCurve
    {
        get
        {
            return m_ropeAnimationCurve;
        }
    }

    #endregion

    #region Hook
    [Header("Hook")]
    [SerializeField]
    private AnimationCurve m_hookProgessionCurve;
    public AnimationCurve hookProgessionCurve
    {
        get
        {
            return m_hookProgessionCurve;
        }
    }
    [SerializeField]
    private float m_hookSpeed;
    public float hookSpeed
    {
        get
        {
            return m_hookSpeed;
        }
    }
    #endregion

    #region Pull
    [Header("Pull")]
    [SerializeField]
    private AnimationCurve m_pullProgessionCurve;
    public AnimationCurve pullProgessionCurve
    {
        get
        {
            return m_pullProgessionCurve;
        }
    }
    [SerializeField]
    private float m_pullSpeed;
    public float pullSpeed
    {
        get
        {
            return m_pullSpeed;
        }
    }

    #endregion
}
