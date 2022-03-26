using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovementManager : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField]
    private InputPlayer m_input;
    [SerializeField]
    private PlayerMovement m_movement;
    [SerializeField]
    private Rigidbody2D m_rig2D;

    [Header("Sensors")]
    [SerializeField]
    private NewGroundSensor m_groundSensor;
    [SerializeField]
    private WallSensorManager m_wallSensorManager;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        m_movement.Init(this);
    }



    public Rigidbody2D rig2D
    {
        get
        {
            return m_rig2D;
        }
    }

    public InputPlayer input
    {
        get
        {
            return m_input;
        }
    }

    public bool isGrounded
    {
        get
        {
            return m_groundSensor.isGrounded();
        }
    }

    public WallSensorManager WallSensorManager { get => m_wallSensorManager; set => m_wallSensorManager = value; }
}
