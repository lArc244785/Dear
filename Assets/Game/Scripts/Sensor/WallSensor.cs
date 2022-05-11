using UnityEngine;

public class WallSensor : MonoBehaviour
{
    [SerializeField]
    private RaySensor[] m_rightSensors;
    [SerializeField]
    private RaySensor[] m_leftSensors;
    [SerializeField]
    private float m_distance;


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
        bool isGrounded = false;
        foreach (RaySensor sensor in m_rightSensors)
            isGrounded = sensor.isWallGrounded;
        return isGrounded;

    }
    public bool IsLeftSensorGrounded()
    {
        bool isGrounded = false;
        foreach (RaySensor sensor in m_leftSensors)
            isGrounded = sensor.isWallGrounded;
        return isGrounded;
    }
    public bool UpSensorGrounded()
    {
        return m_rightSensors[0].isWallGrounded || m_leftSensors[0].isWallGrounded;
    }
    public bool DownSensorGrounded()
    {
        return m_rightSensors[1].isWallGrounded || m_leftSensors[1].isWallGrounded;
    }



}
