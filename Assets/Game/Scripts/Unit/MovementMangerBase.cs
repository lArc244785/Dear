using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementMangerBase : MonoBehaviour
{

    protected UnitBase m_unitBase;

    private I_AddMovement m_addMovement;
    private I_AddMovement m_areaEffect;

    private Vector2 m_moveDir;
    private float m_lookDirX;

    protected float m_defaultGravityScale;

    [SerializeField]
    private Rigidbody2D m_rig2D;




    public virtual void Init(UnitBase unitBase)
    {
        m_unitBase = unitBase;

        m_addMovement = null;
        m_areaEffect = null;
    }



    public UnitBase unitBase
    {
        get
        {
            return m_unitBase;
        }
    }



    public I_AddMovement addMovement
    {
        set
        {
            m_addMovement = value;
        }
        get
        {
            return m_addMovement;
        }
    }

    public I_AddMovement areaImfect
    {
        set
        {
            m_areaEffect = value;
        }
        get
        {
            return m_areaEffect;
        }
    }

    public Rigidbody2D rig2D
    {
        get
        {
            return m_rig2D;
        }
    }

    public Vector2 moveDir
    {
        set
        {
            m_moveDir = value;
            if (Mathf.Abs(m_moveDir.x) > 0.0f)
                lookDirX = m_moveDir.x;

        }
        get
        {
            return m_moveDir;
        }
    }

    public float lookDirX
    {
        set
        {
            m_lookDirX = value;
        }
        get
        {
            return m_lookDirX;
        }
    }

    public float defaultGravityScale
    {

        get
        {
            return m_defaultGravityScale;
        }
    }

    


}
