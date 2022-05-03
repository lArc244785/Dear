using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{

    private PlayerMovementManager m_movementManager;
    private PlayerMovementManager movementManger
    {
        get
        {
            return m_movementManager;
        }
    }

    private ToolManager m_toolManager;
    private ToolManager toolManager
    {
        get
        {
            return m_toolManager;
        }
    }


    private Vector2 m_moveDir;

    private bool m_isControl;
    public bool isControl
    {
        set
        {
            m_isControl = value;
        }
        get
        {
            return m_isControl;
        }
    }


    public void Init(PlayerMovementManager movementManger, ToolManager toolManager)
    {
       m_movementManager = movementManger;
        m_toolManager = toolManager;
        isControl = true;
    }

    public void JumpEnter()
    {
        if (!isControl)
            return;

        movementManger.coyoteSystem.OnJumpEnterTime();        
    }

    public void JumpUp()
    {
        if (!isControl)
            return;
        movementManger.coyoteSystem.OnJumpExitTime();
    }

    public void JumpPressed()
    {

    }


    public void WallGripEnter()
    {
        if (!isControl)
            return;
        movementManger.isWallGrip = true;
    }

    public void WallGripUp()
    {
        if (!isControl)
            return;
        movementManger.isWallGrip = false;
    }

    public void LeftMouseEnter()
    {
        if (!isControl)
            return;
        
    }

    public void LeftMouseUp()
    {
        if (!isControl)
            return;
        
    }

    public void ReboundRight()
    {
        if (!isControl)
            return;

        if (movementManger.currentState != PlayerMovementManager.State.Rope)
            return;
        movementManger.isRopeReboundDirRight = true;
        movementManger.coyoteSystem.OnRopeReboundTime();
    }

    public void ReboundLeft()
    {
        if (!isControl)
            return;

        if (movementManger.currentState != PlayerMovementManager.State.Rope)
            return;
        movementManger.isRopeReboundDirRight = false;
        movementManger.coyoteSystem.OnRopeReboundTime();
    }

    public void ToolUseLeft()
    {
        if (!isControl)
            return;


        toolManager.LeftUse();
    }

    public void ToolUseRight()
    {
        if (!isControl)
            return;

        toolManager.RightUse();
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
