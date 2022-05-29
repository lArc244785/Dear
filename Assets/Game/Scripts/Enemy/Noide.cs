using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum movedirection
{
    Left,
    Right
}
public enum enemyState
{
    Idle,
    Move,
    dead
}

public class Noide : UnitBase
{
    [SerializeField]
    private movedirection m_moveDirection;
    public movedirection enemyMoveDirection
    {
        get
        {
            return m_moveDirection;
        }
        set
        {
            m_moveDirection = value;
        }
    }
    [SerializeField]
    private float m_patrolTime;
    public float patrolTime
    {
        get
        {
            return m_patrolTime;
        }
        set
        {
            m_patrolTime = value;
        }
    }
    [SerializeField]
    private float m_moveSpeed;

    private float m_saveSpeed;
    public float saveSpeed
    {
        get
        {
            return m_saveSpeed;
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
    private int defaultLayer
    {
        get
        {
            return m_defaultLayer;
        }
    }

    [SerializeField]
    private float m_hitDuringTime;
    private float hitDuringTime
    {
        get
        {
            return m_hitDuringTime;
        }
    }
    [SerializeField]
    private float m_ghostDuringTime;

    private float ghostDuringTime
    {
        get
        {
            return m_ghostDuringTime;
        }
    }


    private StateImfectTween m_hitImfect;
    private StateImfectTween hitImfect
    {
        get
        {
            return m_hitImfect;
        }
    }


    private bool m_wallCheck;
    public bool wallCheck
    {
        get { 
            return m_wallCheck; 
        }
        set {
            m_wallCheck = value;
        }
    }
    


    public float moveSpeed
    {
        get
        {
            return m_moveSpeed;
        }
        set
        {
            m_moveSpeed = value;
        }

    }


    private State<Noide>[] m_states;
    private StateMachine<Noide> m_stateMachine;

    public override void Init()
    {
        base.Init();
        Debug.Log("init");

        m_defaultLayer = LayerMask.NameToLayer("Player");
        m_ghostLayer = LayerMask.NameToLayer("Ghost");

        m_saveSpeed = m_moveSpeed;
        m_states = new State<Noide>[3];
        m_states[(int)enemyState.Idle] = new NoidOwnedState.Idle();
        m_states[(int)enemyState.Move] = new NoidOwnedState.Move();
        m_states[(int)enemyState.dead] = new NoidOwnedState.Die();

        m_stateMachine = new StateMachine<Noide>();

        m_stateMachine.SetUp(this, m_states[(int)enemyState.Move]);
    }
    protected override void ComponentInit()
    {
        base.ComponentInit();
        m_hitImfect = GetComponent<StateImfectTween>();

        m_hitImfect.Init(model);
    }
    public void OnEnable()
    {
        Init();
        ComponentInit();

    }
    private void Update()
    {
       m_stateMachine.Excute();
    }

    protected override void HitUniqueEventObject(GameObject attackObject)
    {
        base.HitUniqueEventObject(attackObject);
        if (model == null) return;
    }

    protected override void HitUniqueEventUnit(UnitBase attackUnit)
    {
        base.HitUniqueEventUnit(attackUnit);

        if (model == null) return;
    }
    protected override void HitHp(int damage)
    {
        base.HitHp(damage);
        health.OnDamage(damage);
    }
    public override void OnHitObject(GameObject attackObject, int damage)
    {
        base.OnHitObject(attackObject, damage);
        Debug.Log("총에 맞음");
        if(health.hp > 0)
        hitImfect.HitImfect(hitDuringTime, ghostDuringTime);
    }

    public void ChangeState(enemyState newState)
    {
        m_stateMachine.ChangeState(m_states[(int)newState]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
          
            m_wallCheck = true;
            m_moveSpeed = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        m_wallCheck = false;
    }
    

}
