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
    private float m_yoyoLoopTime;
    public float yoyoLoopTime
    {
        get
        {
            return m_yoyoLoopTime;
        }
    }
    public float yoyoLoopHalfTime
    {
        get
        {
            return yoyoLoopTime * 0.5f;
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


}
