using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WebSpiderState
{
    Idle,
    Attack
}


public class WebSpider : UnitBase
{
    [SerializeField]
    private PlayerSerch m_serch;
    public PlayerSerch serch
    {
        get
        {
            return m_serch;
        }
    }


    private State<WebSpider>[] m_states;
    private StateMachine<WebSpider> m_stateMachine;

    public override void Init()
    {
        base.Init();
        Debug.Log("init");
        m_states = new State<WebSpider>[2];
        m_states[(int)WebSpiderState.Idle] = new WepSpiderOwnedState.Idle();
        m_states[(int)WebSpiderState.Attack] = new WepSpiderOwnedState.Attack();

        m_stateMachine = new StateMachine<WebSpider>();

        m_stateMachine.SetUp(this, m_states[(int)WebSpiderState.Idle]);
    }
    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        m_stateMachine.Excute();
    }

    public void ChangeState(WebSpiderState newState)
    {
        m_stateMachine.ChangeState(m_states[(int)newState]);
    }


}
