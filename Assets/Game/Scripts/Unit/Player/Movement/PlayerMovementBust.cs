using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;
using DG.Tweening;

public class PlayerMovementBust : PlayerMovementBase
{
    [SerializeField]
    private float m_eventTime;
    [SerializeField]
    private float m_distance;
    [SerializeField]
    private Vector3[] m_posPaths;

    private float m_enterTime;


    private float m_runTime;

    private Vector2 m_dir;
    private BustMoveData m_bustMoveData;


    private bool m_slingActionWait = true;

    private InteractionSlingShot m_enterSlingShot;



    public override void Init(PlayerMovementManager pmm)
    {
        base.Init(pmm);
    }

    public override void Movement()
    {
        base.Movement();
        if (m_slingActionWait)
            return;

        m_runTime = Time.time;

        if (movementManager.isWall || m_runTime - m_enterTime >= m_eventTime)
        {
            movementManager.unitBase.isControl = true;
            setMovementTypeNomal();
            return;
        }

    }

    public override void Enter()
    {
        base.Enter();
        rig2D.velocity = Vector2.zero;
        rig2D.gravityScale = .0f;
        m_slingActionWait = true;
        UIManager.instance.inGameView.slingShot.isToggle = true;

    }

    //Bust를 실행하기전에 항상 Power값을 설정을 하고 사용해야됩니다.
    public void Bust(Vector2 dir)
    {
        //Debug.Log("Bust DIr:" + dir);
        //Vector2 pathEndPos = CalculationPathEndPos(dir);

        //Vector2 unitPos = movementManager.unitBase.unitPos;

        //float ratioX = (pathEndPos - unitPos).x / m_posPaths.Length;
        //float ratioY = (pathEndPos - unitPos).y / m_posPaths.Length;


        //Vector2 pathPos = movementManager.unitBase.unitPos;

        //for(int i = 0; i < m_posPaths.Length; i++)
        //{
        //    m_posPaths[i] = pathPos;

        //    float x = m_posPaths[i].x + ratioX;
        //    float y = m_posPaths[i].y + ratioY;

        //    pathPos.x = x;
        //    pathPos.y = y;
        //}

        //Path path = new Path(PathType.CatmullRom, m_posPaths, 1);

        //movementManager.unitBase.transform.DOPath(path, m_eventTime).OnComplete(() => setMovementTypeNomal());


        Vector2 velocity = dir * m_distance;

        rig2D.velocity = velocity;

        rig2D.gravityScale = 3.0f;

        m_enterTime = Time.time;

        m_slingActionWait = false;

        UIManager.instance.inGameView.slingShot.isToggle = false;
    }

    private Vector2 CalculationPathEndPos(Vector2 dir)
    {
        Vector2 pathEndPos = new Vector2();
        pathEndPos = movementManager.unitBase.unitPos;

        pathEndPos += dir * m_distance;


        return pathEndPos;
    }

    public BustMoveData bustMoveData
    {
        set
        {
            m_bustMoveData = value;
        }
        get
        {
            return m_bustMoveData;
        }
    }

    private void setMovementTypeNomal()
    {
        movementManager.currentType = PlayerMovementManager.MOVEMENT_TYPE.NOMAL;
    }

    public InteractionSlingShot enterSlingShot
    {
        set
        {
            m_enterSlingShot = value;

            movementManager.isSlingAction = m_enterSlingShot;

            if (m_enterSlingShot)
            {
                bustMoveData = m_enterSlingShot.bustMoveData;
                UIManager.instance.inGameView.slingShot.targetTr = m_enterSlingShot.transform;
            }

            UIManager.instance.inGameView.slingShot.isToggle = m_enterSlingShot;



        }
        get
        {
            return m_enterSlingShot;
        }
    }



}
