using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField]
    private A_MovementManager m_MovementManager;
    [SerializeField]
    private GrapplingShooter m_shooter;

    private Vector2 m_moveDir;


    public void JumpEnter()
    {
        m_MovementManager.coyoteSystem.OnJumpEnterTime();        
    }

    public void JumpUp()
    {
         m_MovementManager.coyoteSystem.OnJumpExitTime();
    }

    public void JumpPressed()
    {

    }


    public void WallGripEnter()
    {
        m_MovementManager.isWallGrip = true;
    }

    public void WallGripUp()
    {
        m_MovementManager.isWallGrip = false;
    }

    public void LeftMouseEnter()
    {
        m_shooter.Fire();
    }

    public void LeftMouseUp()
    {
        m_shooter.Cancel();
    }

    public void ReboundRight()
    {
        if (m_MovementManager.currentState != A_MovementManager.State.Rope)
            return;
        m_MovementManager.isRopeReboundDirRight = true;
        m_MovementManager.coyoteSystem.OnRopeReboundTime();
    }

    public void ReboundLeft()
    {
        if (m_MovementManager.currentState != A_MovementManager.State.Rope)
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
