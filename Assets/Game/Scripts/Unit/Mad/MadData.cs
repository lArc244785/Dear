using UnityEngine;
[CreateAssetMenu(menuName = "MadDatas/MadData")]
public class MadData : ScriptableObject
{
    [Header("TrackingRange")]
    [SerializeField]
    private float m_trackingRange;
    public float trackingRange
    {
        get { return m_trackingRange; }
    }

    [SerializeField]
    private float m_trackingDeadRange;
    public float trackingDeadRange
    {
        get
        {
            return m_trackingDeadRange;
        }
    }



    [SerializeField]
    private float m_trackingEndRange;
    public float trackingEndRange
    {
        get
        {
            return m_trackingEndRange;
        }
    }

    [Header("DeadRange Effect")]
    [SerializeField]
    private float m_deadRangeYoyoRange;
    public float deadRangeYoyoRange
    {
        get
        {
            return m_deadRangeYoyoRange;
        }
    }

    [SerializeField]
    private float m_yoyoEffectTime;
    public float yoyoEffectTime
    {
        get
        {
            return m_yoyoEffectTime;
        }
    }
    public float yoyoLoopHalfTime
    {
        get
        {
            return yoyoEffectTime * 0.5f;
        }
    }


    [Header("Tracking")]
    [SerializeField]
    private float m_trackingSmoothTime;
    public float trackingSmoothTime
    {
        get
        {
            return m_trackingSmoothTime;
        }
    }

    [Header("Attack")]
    [SerializeField]
    private LayerMask m_targetLayerMask;
    public LayerMask targetLayerMask
    {
        get
        {
            return m_targetLayerMask;
        }
    }
    [SerializeField]
    private float m_attackWaitTime;
    public float attackWaitTime
    {
        get
        {
            return m_attackWaitTime;
        }
    }
   
    [SerializeField]
    [Range(0.0f , 2.0f)]
    private float m_firePointDistance;
    public float firePointDistance
    {
        get
        {
            return m_firePointDistance;
        }
    }

}
