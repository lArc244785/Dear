using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    [SerializeField]
    private Health m_health;

    private bool m_control;

    protected virtual void Init()
    {
        health.Init();
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


    public Vector2 unitPos
    {
        get
        {
            return (Vector2)transform.position;
        }
    }

    public Health health
        { get { return m_health; } }


}
