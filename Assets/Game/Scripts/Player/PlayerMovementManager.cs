using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [Header("Movement Reference")]
    [SerializeField]
    private PlayerMovementNomal m_nomalMovement;
    [SerializeField]
    private PlayerMovementRope m_ropeMovement;
    [SerializeField]
    private PlayerMovemnetClimbing m_climbingMovement;

    [SerializeField]
    private ShoulderMovement m_shoulderMovement;

 


    [SerializeField]
    private GrapplingShooter m_grapplingShooter;

    [Header("Sensor")]
    [SerializeField]
    private DirSensor m_dirSensor;
    [SerializeField]
    private GroundSensor m_groundSensor;
    [SerializeField]
    private ClibingSensor m_climbingSensor;
    private string m_climbingTag = "Climbing";

    private Vector2 m_moveDir;
    private float m_lookDirX;
    private bool m_control;

    private float m_defaultGravityScale;
    [SerializeField]
    private Rigidbody2D m_rig2D;
    public enum MOVEMENT_TYPE
    {
        NOMAL, ROPE , CLIMBING,TOTAL
    }

    private MOVEMENT_TYPE m_currentType;

    private I_PlayerMovement[] m_movements;

    private void Init()
    {
        m_defaultGravityScale = m_rig2D.gravityScale;

        m_dirSensor.Init();
        m_climbingSensor.Init();
        m_groundSensor.Init();

        m_shoulderMovement.init();

        MovementSetting();

        isControl = true;
    }

    private void MovementSetting()
    {
        m_movements = new I_PlayerMovement[(int)MOVEMENT_TYPE.TOTAL];

        m_movements[(int)MOVEMENT_TYPE.NOMAL] = nomalMovement;
        m_movements[(int)MOVEMENT_TYPE.ROPE] = ropeMovement;
        m_movements[(int)MOVEMENT_TYPE.CLIMBING] = climbingMovement;

        foreach (I_PlayerMovement pm in m_movements)
            pm.Init(this);

        currentType = MOVEMENT_TYPE.NOMAL;
    }

    private void Start()
    {
        Init();
    }


    private void Update()
    {
        if (isControl)
        {
            m_movements[(int)currentType].Movement();
        }

            m_shoulderMovement.UpdateProcess();
    }

    public MOVEMENT_TYPE currentType
    {
        set
        {
            m_currentType = value;
            m_movements[(int)m_currentType].Enter();
        }
        get
        {
            return m_currentType;
        }
    }



    public void setTypeGrappling(Vector2 lookPos, bool isBodyFix)
    {
        m_shoulderMovement.setLookPosition(lookPos);
        m_shoulderMovement.isBodyFix = isBodyFix;

        m_currentType = MOVEMENT_TYPE.ROPE;
    }

    public void setTypeNomal()
    {
        m_currentType = MOVEMENT_TYPE.NOMAL;
    }





    public PlayerMovementNomal nomalMovement
    {
        get
        {
            return m_nomalMovement;
        }
    }

    public PlayerMovementRope ropeMovement
    {
        get
        {
            return m_ropeMovement;
        }
    }

    public PlayerMovemnetClimbing climbingMovement
    {
        get
        {
            return m_climbingMovement;
        }
    }


    public ShoulderMovement shoulderMovement
    {
        get
        {
            return m_shoulderMovement;
        }
    }

    public Rigidbody2D rig2D
    {
        get
        {
            return m_rig2D;
        }
    }

    public Vector2 moveDir
    {
        set
        {
            m_moveDir = value;
            if (Mathf.Abs(m_moveDir.x) > 0.0f)
                lookDirX = m_moveDir.x;

        }
        get
        {
            return m_moveDir;
        }
    }

    public float lookDirX
    {
        set
        {
            m_lookDirX = value;
        }
        get
        {
            return m_lookDirX;
        }
    }


    public bool isWall
    {
        get
        {
            return m_dirSensor.getWall(lookDirX) != null;
        }
        
    }

    public bool isControl
    {
        set
        {
            m_control = value;
        }
        get
        {
            return m_control;
        }
    }

    public float defaultGravityScale
    {
        get
        {
            return m_defaultGravityScale;
        }
    }

    public bool isClimbingAble
    {
        get
        {
            Collider2D coll = m_dirSensor.getWall(lookDirX);

            if (coll != null && coll.tag == m_climbingTag)
                return true;

            return false;
        }
    }

    public void ClimbingSensorOn()
    {
        m_climbingSensor.SensorOn(m_lookDirX);
    }

    public bool isClimbingWall
    {
        get
        {
            return m_climbingSensor.isClibingAble(moveDir.y);
        }
    }

}
