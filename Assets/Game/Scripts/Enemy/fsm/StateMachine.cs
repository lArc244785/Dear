using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class
{
    private T m_ownerEntity;
    private State<T> m_currentState;

    public void SetUp(T owner, State<T> entryState)
    {
        m_ownerEntity = owner;
        m_currentState = null;

        ChangeState(entryState);
    }
    public void Excute()
    {
        if (m_currentState != null)
        {
            m_currentState.Excute(m_ownerEntity);
        }
    }

    public void ChangeState(State<T> newState)
    {
        if (newState == null) return;

        if(m_currentState != null)
        {
            m_currentState.Exit(m_ownerEntity);
        }
        m_currentState = newState;
        m_currentState.Enter(m_ownerEntity);

    }

}
