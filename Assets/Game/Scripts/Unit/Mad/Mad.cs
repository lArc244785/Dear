using System.Collections.Generic;
using UnityEngine;

public class Mad : MonoBehaviour
{
    #region palyer
    private UnitPlayer m_player;
    public UnitPlayer player
    {
        get { return m_player; }
    }
    #endregion

    #region trackingPoint
    private MadTrackingPoint m_trackingPoint;
    public MadTrackingPoint trackingPoint
    {
        get { return m_trackingPoint; }
    }
    #endregion

    #region State
    public enum State
    {
        None = -1, Idle, Tracking, Attack
    }

    [SerializeField]
    private State m_currentState;
    public State currentState
    {
        private set
        {
            m_currentState = value;
        }
        get
        {
            return m_currentState;
        }
    }

    private List<MadStateBase> m_stateList = new List<MadStateBase>();
    private List<MadStateBase> stateList
    {
        get
        {
            return m_stateList;
        }
    }

    #endregion

    #region model

    private SpriteRenderer m_model;
    public SpriteRenderer model
    {
        get
        {
            return m_model;
        }
    }

    #endregion

    #region data
    [SerializeField]
    private MadData m_data;
    public MadData data
    {
        get
        {
            return m_data;
        }
    }
    #endregion

    #region Gizmos
    [Header("Gizmos")]
    [SerializeField]
    private Color m_gizmosTrackingColor;
    private Color gizmosTrackingColor
    {
        get
        {
            return m_gizmosTrackingColor;
        }
    }

    [SerializeField]
    private Color m_gizmosDeadTrackingColor;
    private Color gizmosDeadTrackingColor { get { return m_gizmosDeadTrackingColor; } }

    private bool m_isInit = false;
    public bool isInit { set { m_isInit = value; } get { return m_isInit; } }

    #endregion

    #region Attack
    private float lastAttackTime { set; get; }

    private Transform m_firePointTransform;
    public Transform firePointTransform
    {
        get
        {
            return m_firePointTransform;
        }
    }
    [SerializeField]
    private GameObject m_missileObject;
    public GameObject missileObject
    {
        get
        {
            return m_missileObject;
        }
    }
    #endregion



    public void Init(UnitPlayer player, MadTrackingPoint madTrackingPoint)
    {
        m_player = player;
        m_trackingPoint = madTrackingPoint;

        m_model = transform.Find("Model").GetComponent<SpriteRenderer>();
        m_firePointTransform = transform.Find("FirePoint");

        currentState = State.Idle;

        transform.position = trackingPoint.transform.position;
        SetLook(trackingPoint.isRight);




        StateInit();

        ChangeState(State.Idle);

        isInit = true;
    }

    private void StateInit()
    {
        stateList.Add(new MadStateIdle());
        stateList.Add(new MadStateTracking());
        stateList.Add(new MadStateAttack());
    }


    private void Update()
    {
        if (currentState == State.None)
            return;

        CoyoteTimeUpdate();

        stateList[(int)currentState].UpdateProcesses(this);
    }

    private void CoyoteTimeUpdate()
    {
        if (lastAttackTime >= 0.0f)
            lastAttackTime -= Time.deltaTime;
    }


    public void SetLook(bool isRight)
    {
        model.flipX = !isRight;
    }

    public bool CanTracking()
    {
        return m_currentState != State.Tracking;
    }

    public bool IsTrackingRangeOver()
    {
        return GetPlayerToMadDistance() > data.trackingRange;
    }

    public bool isTrackingDeadRangeOver()
    {
        return GetPlayerToMadDistance() > data.trackingDeadRange;
    }

    public bool isTrackingRangeOver(float range)
    {
        return GetPlayerToMadDistance() > range;
    }


    public float GetPlayerToMadDistance()
    {
        return Vector2.Distance(player.unitPos, (Vector2)transform.position);
    }

    public void ChangeState(State state)
    {
        if (state != State.None)
            stateList[(int)currentState].Exit(this);

        currentState = state;

        if (state != State.None)
            stateList[(int)currentState].Enter(this);
    }

    private void OnDrawGizmos()
    {
        if (!isInit)
            return;

        Gizmos.color = gizmosDeadTrackingColor;
        Gizmos.DrawWireSphere(player.transform.position, data.trackingDeadRange);

        Gizmos.color = gizmosTrackingColor;
        Gizmos.DrawWireSphere(player.transform.position, data.trackingRange);
    }

    public float GetTrakingToMadDistance()
    {
        return Vector2.Distance(trackingPoint.transform.position, transform.position);
    }

    public void LookPlayer()
    {
        float dif = player.transform.position.x - transform.position.x;
        if (dif > 0)
            SetLook(true);
        else
            SetLook(false);
    }

    public void Attack()
    {
        if(lastAttackTime < 0.0f && currentState != State.Attack)
        {
            ChangeState(State.Attack);
        }
    }

    public void OnLastOnCoolTime()
    {
        lastAttackTime = data.attackCoolTime;
    }

    public void SetLookPoint(Vector2 point)
    {
        Vector2 madToPointDir = point - (Vector2)transform.position;
        madToPointDir.Normalize();


        if (madToPointDir.x > 0.0f)
            SetLook(true);
        else
            SetLook(false);

        SetFirePointDir(madToPointDir);
    }

    private void SetFirePointDir(Vector2 dir)
    {
        firePointTransform.localPosition = dir * data.firePointDistance;
    }

}
