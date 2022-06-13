using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum BossSpiderState{
    Idle,
    SlowMove,
    FastMove,
    CamOutMove,
    Dead
}


public class BossSpieder : EnemyBase
{
    [SerializeField]
    private BossMovement m_bossMovmentTween;
    [SerializeField]
    private PlayerSerch m_AreaCollider;
    public PlayerSerch areaCollider
    {
        get
        {
            return m_AreaCollider;
        }
        set
        {
            m_AreaCollider = value;
        }
    }

    [SerializeField]
    private Animator m_animator;
    public Animator animator
    {
        get
        {
            return m_animator;
        }
        set
        {
            m_animator = value;
        }
    }
    [SerializeField]
    private float m_bossMovementDelay;
    public float bossMovementDelay
    {
        get
        {
            return m_bossMovementDelay;
        }
    }

    private State<BossSpieder>[] m_state;
    private StateMachine<BossSpieder> m_stateMachine;



    private bool m_camOut;
    public bool camOut
    {
        get
        {
            return m_camOut;
        }
        set
        {
            m_camOut = value;
        }
    }
    private SpiderBossSound m_sound;
    public SpiderBossSound sound
    {
        get
        {
            return m_sound;
        }
       
    }
    [SerializeField]
    private PlayerExit m_isPlayerExit;
    public PlayerExit isPlayerExit
    {
        get
        {
            return m_isPlayerExit;
        }
    }

    public void OnEnable()
    {
        Init();
        ComponentInit();
    }

    public override void Init()
    {
        base.Init();

        m_bossMovmentTween = GetComponent<BossMovement>();

        m_state = new State<BossSpieder>[5];
        m_state[(int)BossSpiderState.Idle] = new SpiderBossState.Idle();
        m_state[(int)BossSpiderState.SlowMove] = new SpiderBossState.SlowMove();
        m_state[(int)BossSpiderState.FastMove] = new SpiderBossState.FastMove();
        m_state[(int)BossSpiderState.CamOutMove] = new SpiderBossState.CamOutMove();
        m_state[(int)BossSpiderState.Dead] = new SpiderBossState.Die();

        m_stateMachine = new StateMachine<BossSpieder>();
        m_AreaCollider = GameObject.Find("playerSerchArea").GetComponent<PlayerSerch>();
        m_sound = GetComponent<SpiderBossSound>();
        m_sound.init();

        m_camOut = false;

        m_stateMachine.SetUp(this, m_state[(int)BossSpiderState.Idle]);
    }
    private void Update()
    {
        m_stateMachine.Excute();
        if (PlayerDistance() >= m_bossMovmentTween.camOutdistance) m_camOut = true;
        else m_camOut = false;
    }

    public void ChangeState(BossSpiderState newState)
    {
        m_stateMachine.ChangeState(m_state[(int)newState]);
    }

    public void BossMovementFast()
    {
        m_bossMovmentTween.SpiderBossMove_Fast();
    }

    public void BossMovementSlow()
    {
        m_bossMovmentTween.SpiderBossMove_Slow();
    }
    public void BossMovementCamOut()
    {
        m_bossMovmentTween.SpiderBossMove_CamOut(areaCollider.player.transform.position.y
            - m_bossMovmentTween.camOutMoveDistance);
    }
    public float PlayerDistance()
    {
        return areaCollider.player.transform.position.y - transform.position.y; 
    }
    public void BossMovementExit()
    {
        m_bossMovmentTween.SpiderBossMove_Exit();
    }
    public bool PlayerExit()
    {
        return m_isPlayerExit.isPlayerExit;
    }

}
