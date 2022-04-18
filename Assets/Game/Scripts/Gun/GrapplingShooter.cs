using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingShooter : MonoBehaviour
{
    [SerializeField]
    private GrapplingGun m_grpplingGun;
    [SerializeField]
    private PlayerMovementManager m_movementManager;
    public PlayerMovementManager movementManager { get { return m_movementManager; } }

    [SerializeField]
    private float m_fireCoolTime;

    private bool m_isFireCoolTime;
    public bool isFireCoolTime
    {
        private set
        {
            m_isFireCoolTime = value;
        }
        get
        {
            return m_isFireCoolTime;
        }
    }




    public void Init()
    {
        m_grpplingGun.init();
        isFireCoolTime = false;
    }


    public void Fire()
    {
        if(m_grpplingGun.m_eState == GrapplingGun.E_State.E_NONE && CanShoot())
        {
            m_grpplingGun.Fire();
        }

    }

    public void Pull()
    {
        m_grpplingGun.Pull();
    }

    public void Cancel()
    {
        if (isNoneGrappling)
            return;

        m_grpplingGun.Cancel();
        isFireCoolTime = true;
        Invoke("FireCoolEnd", m_fireCoolTime);
    }

    private void FireCoolEnd()
    {
        isFireCoolTime = false;
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

    public bool CanShoot()
    {
        return isFireCoolTime == false &&
            (m_movementManager.currentState == PlayerMovementManager.State.Ground ||
            m_movementManager.currentState == PlayerMovementManager.State.Air ||
            m_movementManager.currentState == PlayerMovementManager.State.Rope);
    }

    public void RopeMovementChange()
    {
        m_movementManager.currentState = PlayerMovementManager.State.Rope;
    }

    public void CancleRopeRebound()
    {
        m_movementManager.coyoteSystem.OnRopeCancleJumpTime(); ;
    }



    public Transform unitTransfrom
    {
        get
        {
            return transform;
        }
    }


}
