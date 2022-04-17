using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    [SerializeField]
    private Health m_health;

    [SerializeField]
    private StateImfectTween m_stateImfect;
    public StateImfectTween stateImfect { get { return m_stateImfect; } }

    [SerializeField]
    private SpriteRenderer m_spriteRenderer;

    [SerializeField]
    private Rigidbody2D m_rig2D;
    public Rigidbody2D rig2D { get { return m_rig2D; } }



    private bool m_control;

    public virtual void Init()
    {
        health.Init(this);
        stateImfect.Init(m_spriteRenderer);
    }



    public virtual bool isControl
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

    public virtual void HitEvent(Vector2 hitPoint)
    {

    }
}
