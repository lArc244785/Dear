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
        None = -1, Idle, Tracking
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

    public float gizmosTrackingDeadRange { set; get; }

    #endregion

    #region Attack
    public bool isAttackAble {private set; get; }
    private bool m_isAttack;
    private float m_lastAttackTime { set; get; }

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

    #region fire
    [SerializeField]
    private FMODUnity.EventReference m_fireEvent;
    #endregion


    [SerializeField]
    private Animator m_animator;

    private bool m_isCoolTime;

    private MadHand m_handLeft;
    private MadHand m_handRight;


    public void Init(UnitPlayer player, MadTrackingPoint madTrackingPoint)
    {
        m_player = player;
        m_trackingPoint = madTrackingPoint;

        ComponentInit();


        currentState = State.Idle;

        transform.position = trackingPoint.transform.position;
        SetLook(trackingPoint.isRight);

        gizmosTrackingDeadRange = data.trackingDeadRange;


        StateInit();

        ChangeState(State.Idle);

        isInit = true;
    }

    private void ComponentInit()
    {
        m_model = transform.Find("Model").GetComponent<SpriteRenderer>();
        m_firePointTransform = transform.Find("FirePoint");

        m_handLeft = transform.Find("MadHandLeft").GetComponent<MadHand>();
        m_handRight = transform.Find("MadHandRight").GetComponent<MadHand>();

        m_handLeft.Init();
        m_handRight.Init();
    }

    private void StateInit()
    {
        stateList.Add(new MadStateIdle());
        stateList.Add(new MadStateTracking());
    }


    private void Update()
    {
        if (currentState == State.None)
            return;

        AttackUpdate();


        stateList[(int)currentState].UpdateProcesses(this);
    }

    private void AttackCoyoteTime()
    {
        if (m_lastAttackTime >= 0.0f)
            m_lastAttackTime -= Time.deltaTime;
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
        Gizmos.DrawWireSphere(player.transform.position, gizmosTrackingDeadRange);

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

    private void LookMouse()
    {
        float dif = InputManager.instance.inGameMousePosition2D.x - transform.position.x;
        if (dif > 0)
            SetLook(true);
        else
            SetLook(false);
    }


    private void Attack(Vector2 targetPoint)
    {
        SetTriggerAttack();

        GameObject goMissile = GameObject.Instantiate(missileObject);

        SoundFire();

        Vector2 spawnPoint = (Vector2)firePointTransform.position;
        Vector2 fireDir = targetPoint - spawnPoint;
        fireDir.Normalize();

        goMissile.GetComponent<ProjectileNormal>().HandleSpawn(spawnPoint, fireDir, data.targetLayerMask);
        
    }

    public void OnLastAttackTime()
    {
        m_lastAttackTime = data.attackWaitTime;
        m_isCoolTime = true;
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


    public void SoundFire()
    {
        SoundManager.instance.SoundOneShot(m_fireEvent);
    }

    public void SetTriggerIdle()
    {
        m_animator.SetTrigger("Idle");
    }

    public void SetTriggerTeleport()
    {
        m_animator.SetTrigger("Teleport");
    }

    public void SetTriggerAttack()
    {
        m_animator.SetTrigger("Attack");
    }

    public void SetAttackAble(bool isAttackAble)
    {
        this.isAttackAble = isAttackAble;
    }

    private void AttackUpdate()
    {
        if(m_isAttack)
        {
            LookMouse();

            AttackCoyoteTime();

            if (m_lastAttackTime <= 0.0f)
            {
                if(!CanAttack())
                {
                    m_isAttack = false;

                    m_handLeft.SetModelEnable(true);
                    m_handRight.SetModelEnable(true);
                    SetTriggerIdle();
                }
                else
                {
                    Attack(InputManager.instance.inGameMousePosition2D);
                    OnLastAttackTime();
                }
            }
        }
        else
        {
            if(CanAttack())
            {
                Attack(InputManager.instance.inGameMousePosition2D);
                m_handLeft.SetModelEnable(false);
                m_handRight.SetModelEnable(false);
                OnLastAttackTime();
                m_isAttack = true;
            }
        }
    }


    private bool CanAttack()
    {
        return player.inputPlayer.isControl && isAttackAble;
    }

}
