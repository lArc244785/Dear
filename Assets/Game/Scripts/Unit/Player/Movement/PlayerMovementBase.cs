using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementBase :MonoBehaviour
{
    private PlayerMovementManager m_movementManager;
    private Rigidbody2D m_rig2D;

    public virtual void Init(PlayerMovementManager pmm)
    {
        m_movementManager = pmm;
        m_rig2D = pmm.rig2D;
    }

    public virtual void Enter()
    {

    }

    public virtual void Movement()
    {

    }

    public PlayerMovementManager movementManager
    {
        get
        {
            return m_movementManager;
        }
    }

    public Rigidbody2D rig2D
    {
        get
        {
            return m_rig2D;
        }
    }
}
