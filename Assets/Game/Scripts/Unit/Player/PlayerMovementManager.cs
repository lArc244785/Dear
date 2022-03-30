using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MovementMangerBase
{

    [Header("Movement Reference")]
    [SerializeField]
    private PlayerMovementNomal m_nomalMovement;
    [SerializeField]
    private PlayerMovementRope m_ropeMovement;
    [SerializeField]
    private PlayerMovemnetClimbing m_climbingMovement;
    [SerializeField]
    private PlayerMovementBust m_bustMovement;


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

    private bool m_slingAction;

    public enum MOVEMENT_TYPE
    {
        NOMAL, ROPE , CLIMBING, BUST, TOTAL
    }

    private MOVEMENT_TYPE m_currentType;

    private PlayerMovementBase[] m_movements;

    public override void Init(UnitBase unitBase)
    {
        base.Init(unitBase);

        m_defaultGravityScale = rig2D.gravityScale;

        m_dirSensor.Init();
        m_climbingSensor.Init();
        m_groundSensor.Init();


        MovementSetting();

        unitBase.isControl = true;


    }




    private void MovementSetting()
    {
        m_movements = new PlayerMovementBase[(int)MOVEMENT_TYPE.TOTAL];

        m_movements[(int)MOVEMENT_TYPE.NOMAL] = nomalMovement;
        m_movements[(int)MOVEMENT_TYPE.ROPE] = ropeMovement;
        m_movements[(int)MOVEMENT_TYPE.CLIMBING] = climbingMovement;
        m_movements[(int)MOVEMENT_TYPE.BUST] = bustMovement;

        foreach (PlayerMovementBase pm in m_movements)
            pm.Init(this);

        currentType = MOVEMENT_TYPE.NOMAL;
    }

   


    private void Update()
    {
        if (unitBase.isControl)
        {
            m_movements[(int)currentType].Movement();

            if (addMovement != null)
                addMovement.Movement(this);
        }



        if (areaImfect != null)
            areaImfect.Movement(this);

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
    public PlayerMovementBust bustMovement
    {
        get
        {
            return m_bustMovement;
        }
    }




    public ShoulderMovement shoulderMovement
    {
        get
        {
            return m_shoulderMovement;
        }
    }




    public bool isWall
    {
        get
        {
            return m_dirSensor.getWall(lookDirX) != null;
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
        m_climbingSensor.SensorOn(lookDirX);
    }

    public bool isClimbingWall
    {
        get
        {
            return m_climbingSensor.isClibingAble(moveDir.y);
        }
    }

    public bool isSlingAction
    {
        set
        {
            m_slingAction = value;
        }
        get
        {
            return m_slingAction;
        }
    }

    public bool isGround
    {
        get
        {
            return m_groundSensor.isGround;
        }
    }


}
