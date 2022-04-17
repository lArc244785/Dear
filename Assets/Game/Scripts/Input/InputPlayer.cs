using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerMovementManager m_MovementManager;
    [SerializeField]
    private GrapplingShooter m_shooter;

    private Vector2 m_moveDir;


    public void JumpEnter()
    {
        if (!m_MovementManager.playerManager.isControl)
            return;
        m_MovementManager.coyoteSystem.OnJumpEnterTime();        
    }

    public void JumpUp()
    {
        if (!m_MovementManager.playerManager.isControl)
            return;
        m_MovementManager.coyoteSystem.OnJumpExitTime();
    }

    public void JumpPressed()
    {

    }


    public void WallGripEnter()
    {
        if (!m_MovementManager.playerManager.isControl)
            return;
            m_MovementManager.isWallGrip = true;
    }

    public void WallGripUp()
    {
        if (!m_MovementManager.playerManager.isControl)
            return;
        m_MovementManager.isWallGrip = false;
    }

    public void LeftMouseEnter()
    {
        if (!m_MovementManager.playerManager.isControl)
            return;
        m_shooter.Fire();
    }

    public void LeftMouseUp()
    {
        if (!m_MovementManager.playerManager.isControl)
            return;
        m_shooter.Cancel();
    }

    public void ReboundRight()
    {
        if (!m_MovementManager.playerManager.isControl)
            return;

        if (m_MovementManager.currentState != PlayerMovementManager.State.Rope)
            return;
        m_MovementManager.isRopeReboundDirRight = true;
        m_MovementManager.coyoteSystem.OnRopeReboundTime();
    }

    public void ReboundLeft()
    {
        if (!m_MovementManager.playerManager.isControl)
            return;

        if (m_MovementManager.currentState != PlayerMovementManager.State.Rope)
            return;
        m_MovementManager.isRopeReboundDirRight = false;
        m_MovementManager.coyoteSystem.OnRopeReboundTime();
    }





    public Vector2 moveDir
    {
        set
        {
            m_moveDir = value;

            if (Mathf.Abs(m_moveDir.x) > 0.01f)
                m_moveDir.x = 1.0f * Mathf.Sign(m_moveDir.x);

            if (Mathf.Abs(m_moveDir.y) > 0.01f)
                m_moveDir.y = 1.0f * Mathf.Sign(m_moveDir.y);

        }
        get
        {
            return m_moveDir;
        }
    }


}
