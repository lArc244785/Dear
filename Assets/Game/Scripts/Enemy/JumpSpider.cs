using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum jumpSpiderState
{
    Idle,
    Atack,
    Dead

}

public class JumpSpider : EnemyBase
{
    [SerializeField]
    private float m_jumpDelay;
    public float jumpDelay
    {
        get
        {
            return m_jumpDelay;
        }
    }
    [SerializeField]
    private PlayerSerch m_AreaCollider;
    public PlayerSerch areaCollider
    {
        get
        {
            return m_AreaCollider;
        }
    }
    private int m_ghostLayer;
    private int ghostLayer
    {
        get
        {
            return m_ghostLayer;
        }
    }
    private int m_defaultLayer;
    private State<JumpSpider>[] m_states;
    private StateMachine<JumpSpider> m_stateMachine;

    private SpiderJumpTween m_jumpTween;
    [SerializeField]
    private bool m_isRight;

    private int defaultLayer
    {
        get
        {
            return m_defaultLayer;
        }
    }
    public override void Init()
    {
        base.Init();
        base.Init();
        Debug.Log("init");

        m_defaultLayer = LayerMask.NameToLayer("Player");
        m_ghostLayer = LayerMask.NameToLayer("Ghost");


        m_states = new State<JumpSpider>[3];
        m_states[(int)jumpSpiderState.Idle] = new JumpSpiderOwnedState.Idle();
        m_states[(int)jumpSpiderState.Atack] = new JumpSpiderOwnedState.Attack();
        m_states[(int)jumpSpiderState.Dead] = new JumpSpiderOwnedState.Die();

        m_stateMachine = new StateMachine<JumpSpider>();

        m_stateMachine.SetUp(this, m_states[(int)jumpSpiderState.Idle]);
    }

    private void Update()
    {
        m_stateMachine.Excute();
        playerCheck();
        if (m_isRight) model.flipX = false;
        else model.flipX = true;
       
    }
    public void OnEnable()
    {
        Init();
        ComponentInit();
    }

    protected override void ComponentInit()
    {
        base.ComponentInit();
        m_jumpTween = GetComponent<SpiderJumpTween>();
    }

    public void ChangeState(jumpSpiderState newState)
    {
        m_stateMachine.ChangeState(m_states[(int)newState]);
    }

    protected override void HitHp(int damage)
    {
        base.HitHp(damage);
    }
    protected override void HitUniqueEventObject(GameObject attackObject)
    {
        base.HitUniqueEventObject(attackObject);
    }
    protected override void HitUniqueEventUnit(UnitBase attackUnit)
    {
        base.HitUniqueEventUnit(attackUnit);
    }
    public override void OnHitObject(GameObject attackObject, int damage)
    {
        base.OnHitObject(attackObject, damage);
    }
    public override void OnHitUnit(UnitBase attackUnit, int damage)
    {
        base.OnHitUnit(attackUnit, damage);
    }
    public void Jump()
    {
        m_jumpTween.Jump(m_isRight);
    }

    public void playerCheck()
    {
        if (!m_AreaCollider.player) return;
        var dir = model.transform.position.x - m_AreaCollider.player.transform.position.x;
        if (dir >= 0)
        {
            m_isRight = true;
        }
        else
        {
            m_isRight = false;
        }

    }

}
