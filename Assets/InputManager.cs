using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingleToon<InputManager>
{

    [SerializeField]
    private Camera m_brainCam;
    [SerializeField]
    PlayerMovementManager m_playerMovementManager;
    [SerializeField]
    PlayerInteraction m_playerInteraction;
    [SerializeField]
    GrapplingShooter m_shooter;

    private void Awake()
    {
        Init();
    }

    protected override bool Init()
    {
        return base.Init();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveDir = context.ReadValue<Vector2>();
        m_playerMovementManager.moveDir = moveDir;

    }



    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (UIManager.instance.inGameView.slingShot.isToggle)
            {
                Vector2 dir = inGameMousePosition2D - m_playerMovementManager.unitBase.unitPos;
                dir.Normalize();

                m_playerMovementManager.bustMovement.Bust(dir);
            }

            if (!m_playerMovementManager.unitBase.isControl)
                return;

            if (m_playerMovementManager.currentType == PlayerMovementManager.MOVEMENT_TYPE.NOMAL)
                m_shooter.Fire();

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!m_playerMovementManager.unitBase.isControl)
                return;

            if (m_playerMovementManager.currentType == PlayerMovementManager.MOVEMENT_TYPE.NOMAL)
            {
                m_playerMovementManager.nomalMovement.Jump();
            }
            else if(m_playerMovementManager.currentType == PlayerMovementManager.MOVEMENT_TYPE.CLIMBING)
            {
                m_playerMovementManager.climbingMovement.Jump();
            }
        }

    }

    public void OnPull(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!m_playerMovementManager.unitBase.isControl)
                return;

            if (m_shooter.isGrappling)
                m_shooter.Pull();
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!m_playerMovementManager.unitBase.isControl)
                return;
            if(m_playerMovementManager.currentType == PlayerMovementManager.MOVEMENT_TYPE.ROPE)
            {
                m_shooter.Cancel();
                m_playerMovementManager.setTypeNomal();
                m_playerMovementManager.shoulderMovement.SetMouse();
            }
            else if(m_playerMovementManager.currentType == PlayerMovementManager.MOVEMENT_TYPE.NOMAL && m_playerMovementManager.isSlingAction)
            {
                m_playerMovementManager.currentType = PlayerMovementManager.MOVEMENT_TYPE.BUST;
            }


        }
    }

    public void OnRightRebound(InputAction.CallbackContext context)
    {
        if (m_playerMovementManager.currentType == PlayerMovementManager.MOVEMENT_TYPE.ROPE &&
            m_shooter.isGrappling &&
            context.started)
        {
            if (!m_playerMovementManager.unitBase.isControl)
                return;


            if (m_playerMovementManager.ropeMovement.isReboundAble)
                m_playerMovementManager.ropeMovement.Rebound(true);
        }
    }

    public void OnLeftRebound(InputAction.CallbackContext context)
    {
        if (m_playerMovementManager.currentType == PlayerMovementManager.MOVEMENT_TYPE.ROPE &&
            m_shooter.isGrappling &&
            context.started)
        {
            if (!m_playerMovementManager.unitBase.isControl)
                return;

            if (m_playerMovementManager.ropeMovement.isReboundAble)
                m_playerMovementManager.ropeMovement.Rebound(false);
        }
    }

    public void OnClimbing(InputAction.CallbackContext context)
    {
        if (!m_playerMovementManager.unitBase.isControl)
            return;

        if (!context.canceled)
        {
            if (m_playerMovementManager.currentType == PlayerMovementManager.MOVEMENT_TYPE.NOMAL &&
                m_playerMovementManager.isClimbingAble)
                m_playerMovementManager.currentType = PlayerMovementManager.MOVEMENT_TYPE.CLIMBING;
        }
        else
        {
            if (m_playerMovementManager.currentType == PlayerMovementManager.MOVEMENT_TYPE.CLIMBING)
                m_playerMovementManager.currentType = PlayerMovementManager.MOVEMENT_TYPE.NOMAL;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (!m_playerMovementManager.unitBase.isControl || m_playerInteraction.InteractionEvent == null)
            return;

        if (context.started)
            m_playerInteraction.Interaction();
    }


    public Vector2 inGameMousePosition2D
    {
        get
        {
            return (Vector2)m_brainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }

    public Camera brainCam
    {
        get
        {
            return m_brainCam;
        }
    }

    public Vector2 screenViewMousePos
    {
        get
        {
            return Mouse.current.position.ReadValue();
        }
    }
}
