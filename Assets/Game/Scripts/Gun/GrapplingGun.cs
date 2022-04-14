using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrapplingGun : MonoBehaviour
{
    [Header("General Reference")]
    [SerializeField]
    private SpringJoint2D m_springJoint2D;
    [SerializeField]
    private Rigidbody2D m_springRig2D;
    [SerializeField]
    private Hook m_hook;
    [SerializeField]
    private float m_hookSpeed;
    [SerializeField]
    private RopeRenderer m_ropeRenderer;
    [SerializeField]
    private Transform m_firePoint;
    [SerializeField]
    private Shoulder m_shouderMovement;
    [SerializeField]
    private GrapplingShooter m_shooter;
    //----------------
    private Shoulder shouder { get => m_shouderMovement; }
    private GrapplingShooter shooter { get => m_shooter; }



    [Header("Option")]
    [SerializeField]
    private bool m_bBodyFix;
    [SerializeField]
    private LayerMask m_pullLayerMask;
    [SerializeField]
    private float m_pullDistance;
    [SerializeField]
    private float m_pullMaxClampLength;
    [SerializeField]
    private float m_pullFrequency;
    [SerializeField]
    private float m_cancelMaxClampLength;
    [SerializeField]
    private float m_ropeMaxDistance;
    [SerializeField]
    private GrapplingRebound m_rebound;

    [SerializeField]
    private UnityEvent m_reboundEvent;


    public enum E_State
    {
        E_NONE, E_HOOKFIRE, E_GRAPPLING, E_PULL
    }

    public E_State m_eState;


    public void init()
    {
        m_hook.init(m_hookSpeed, this);
        m_rebound.init();
    }

    public void Fire()
    {
        
        m_eState = E_State.E_HOOKFIRE;



        Vector2 dir = InputManager.instance.inGameMousePosition2D - (Vector2)m_hook.transform.position;
        dir.Normalize();

        shouder.setLookPosition(InputManager.instance.inGameMousePosition2D);

        m_hook.Fire(dir);
        m_ropeRenderer.isDraw = true;

        StartCoroutine(HookDistanceChack());


        if (shooter.movementManager.IsGrounded())
            shooter.movementManager.playerManager.animation.ropeMovement = 0.5f;
        else
            shooter.movementManager.playerManager.animation.ropeMovement = 0.0f;

        shooter.movementManager.playerManager.animation.TriggerRope();
        shooter.movementManager.playerManager.shoulder.SetArmVisible(true);

    }

    public void Grappling()
    {
        float distance = GetHookDistance();

        m_springJoint2D.distance = distance;
        m_springJoint2D.enabled = true;

        shouder.setLookPosition((Vector2)m_hook.transform.position);

        m_eState = GrapplingGun.E_State.E_GRAPPLING;
        m_shooter.RopeMovementChange();
    }

    public void Cancel()
    {

        JointDisable();
        m_ropeRenderer.isDraw = false;

        if (m_eState == E_State.E_GRAPPLING && m_rebound.isRebound((Vector2)m_hook.transform.position))
        {
            shooter.CancleRopeRebound();
        }

        m_hook.Reset(m_firePoint);

        shouder.SetMouse();


        shooter.movementManager.playerManager.shoulder.SetArmVisible(false);
        if (shooter.movementManager.currentState == PlayerMovementManager.State.Ground)
            shooter.movementManager.playerManager.animation.TriggerLanding();
        else if(shooter.movementManager.currentState == PlayerMovementManager.State.Air)
            shooter.movementManager.playerManager.animation.TriggerAir();

        m_eState = GrapplingGun.E_State.E_NONE;
    }


    private void JointDisable()
    {
        m_springJoint2D.enabled = false;
        m_springJoint2D.frequency = .0f;
        m_springRig2D.velocity = Vector2.ClampMagnitude(m_springRig2D.velocity, m_cancelMaxClampLength);
    }

    private bool isRopeDistanceMax
    {
        get
        {
            if (GetHookDistance() >= m_ropeMaxDistance)
                return true;

            return false;
        }
    }


    public void Pull()
    {
        m_eState = GrapplingGun.E_State.E_PULL;
        StartCoroutine(PullProcesses());
    }

    private IEnumerator PullProcesses()
    {
        //m_playerMovementManager.unitBase.isControl = false;

        m_springJoint2D.distance = m_pullDistance;
        m_springJoint2D.frequency = m_pullFrequency;

        Vector2 dir = (Vector2)m_hook.transform.position - (Vector2)m_firePoint.position;
        dir.Normalize();

        while (m_springJoint2D.enabled && !Physics2D.Raycast(m_firePoint.position, dir, m_springJoint2D.distance, m_pullLayerMask))
        {
            m_springRig2D.velocity = Vector2.ClampMagnitude(m_springRig2D.velocity, m_pullMaxClampLength);
            yield return null;
        }

        if (m_springJoint2D.enabled)
        {
            m_springRig2D.velocity = Vector2.zero;
            m_springJoint2D.frequency = 0.0f;
        }
        m_eState = GrapplingGun.E_State.E_GRAPPLING;
       // m_playerMovementManager.unitBase.isControl = true;
    }

    private float GetHookDistance()
    {
        return Vector2.Distance(m_hook.transform.position, m_springRig2D.transform.position);
    }

    private IEnumerator HookDistanceChack()
    {
        while(m_eState == E_State.E_HOOKFIRE && !isRopeDistanceMax)
        {
            yield return null;
        }

        if (isRopeDistanceMax)
            Cancel();

            
    }

}
