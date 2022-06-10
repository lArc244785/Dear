using UnityEngine;

public class WallSensor : MonoBehaviour
{
    [SerializeField]
    private RaySensor[] m_rightSensors;
    [SerializeField]
    private RaySensor[] m_leftSensors;
    [SerializeField]
    private float m_distance;

    [SerializeField]
    private GroundInfo m_lastContactRightWallInfo;
    public GroundInfo lastContactRightWallInfo
    {
        get
        {
            return m_lastContactRightWallInfo;
        }
    }
    [SerializeField]
    private GroundInfo m_lastContactLeftWallInfo;
    public GroundInfo lastContactLeftWallInfo
    {
        get
        {
            return m_lastContactLeftWallInfo;
        }
    }


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        foreach (RaySensor raySensor in m_rightSensors)
            raySensor.Init(this);
        foreach (RaySensor raySensor in m_leftSensors)
            raySensor.Init(this);
    }

    public float distance
    {
        get
        {
            return m_distance;
        }
    }

    public bool IsRightSensorGrounded()
    {
        GameObject contactObject = null;
        foreach (RaySensor sensor in m_rightSensors)
        {
             contactObject = sensor.SensorContact();
        }


        if (contactObject == null)
            return false ;


        m_lastContactRightWallInfo = contactObject.GetComponent<GroundInfo>();
        return true;
    }





    public bool IsLeftSensorGrounded()
    {
        GameObject contactObject = null;
        foreach (RaySensor sensor in m_leftSensors)
        {
            contactObject = sensor.SensorContact();
        }


        if (contactObject == null)
            return false;


        m_lastContactLeftWallInfo = contactObject.GetComponent<GroundInfo>();
        return true;
    }

    public bool UpSensorGrounded()
    {
        return (m_rightSensors[0].SensorContact() != null) || (m_leftSensors[0].SensorContact() != null);
    }
    public bool DownSensorGrounded()
    {
        return (m_rightSensors[1].SensorContact() != null) || (m_leftSensors[1].SensorContact() != null);
    }




}
