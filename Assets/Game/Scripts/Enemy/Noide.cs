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
    Move
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
        m_saveSpeed = m_moveSpeed;
        m_states = new State<Noide>[2];
        m_states[(int)enemyState.Idle] = new NoidOwnedState.Idle();
        m_states[(int)enemyState.Move] = new NoidOwnedState.Move();

        m_stateMachine = new StateMachine<Noide>();

        m_stateMachine.SetUp(this, m_states[(int)enemyState.Move]);
    }
    public void OnEnable()
    {
        Init();
    }





    private void Update()
    {
        m_stateMachine.Excute();
    }


    protected override void HitUniqueEventUnit(UnitBase attackUnit)
    {
        base.HitUniqueEventUnit(attackUnit);
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
