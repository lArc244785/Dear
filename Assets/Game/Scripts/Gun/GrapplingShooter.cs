using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingShooter : MonoBehaviour
{
    [SerializeField]
    private GrapplingGun m_grpplingGun;

    private void Start()
    {
        Init();
    }


    public void Init()
    {
        m_grpplingGun.init();
    }


    public void Fire()
    {
        if(m_grpplingGun.m_eState == GrapplingGun.E_State.E_NONE)
            m_grpplingGun.Fire();
    }

    public void Pull()
    {
        m_grpplingGun.Pull();
    }

    public void Cancel()
    {
        m_grpplingGun.Cancel();
    }

    public bool isGrappling
    {
        get
        {
            return m_grpplingGun.m_eState == GrapplingGun.E_State.E_GRAPPLING;
        }
    }

    public bool isNoneGrappling
    {
        get
        {
            return m_grpplingGun.m_eState == GrapplingGun.E_State.E_NONE;
        }
    }


    public bool isGrapplingFireAction
    {
        get
        {
            return m_grpplingGun.m_eState == GrapplingGun.E_State.E_HOOKFIRE;
        }
    }

    public bool isGrapplingPull
    {
        get
        {
            return m_grpplingGun.m_eState == GrapplingGun.E_State.E_PULL;
        }
    }
    public Transform unitTransfrom
    {
        get
        {
            return transform;
        }
    }


}
