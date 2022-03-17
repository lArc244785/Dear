using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    [SerializeField]
    private Health m_health;

    protected MovementMangerBase m_MovementManager;

    private bool m_control;


    protected virtual void Init(MovementMangerBase mmb)
    {
        m_MovementManager = mmb;
        m_MovementManager.Init(this);
    }

    public bool isControl
    {
        set
        {
            m_control = value;
        }
        get
        {
            return m_control;
        }
    }

    public MovementMangerBase movementManager
    {
        get
        {
            return m_MovementManager;
        }
    }

    public Vector2 unitPos
    {
        get
        {
            return (Vector2)transform.position;
        }
    }


}
