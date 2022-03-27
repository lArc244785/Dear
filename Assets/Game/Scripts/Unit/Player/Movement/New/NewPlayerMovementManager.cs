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

    [Header("Model")]
    [SerializeField]
    private SpriteRenderer m_model;

    private float m_lookDir;
    private float m_oldLookDir;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
       // m_movement.Init(this);
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

    public void Turn()
    {
        m_model.flipX = !m_model.flipX;
    }


    public WallSensorManager wallSensorManager { get => m_wallSensorManager; set => m_wallSensorManager = value; }
    public float lookDir { get => m_lookDir; set => m_lookDir = value; }
    public float oldLookDir { get => m_oldLookDir; set => m_oldLookDir = value; }
}
