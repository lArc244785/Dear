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

    private Mad m_mad;
    private Mad mad
    {
        get
        {
            return m_mad;
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

    //private bool m_isMoveControl;

    //public bool isMoveControl
    //{
    //    private set
    //    {
    //        m_isMoveControl = value;
    //    }
    //    get
    //    {
    //        return m_isMoveControl;
    //    }
    //}



    public void Init(PlayerMovementManager movementManger, ToolManager toolManager, Mad mad)
    {
       m_movementManager = movementManger;
        m_toolManager = toolManager;
        m_mad = mad;

        isControl = true;
        //isMoveControl = true;
    }

    public void JumpEnter()
    {
        if (!isControl )
            return;

        movementManger.coyoteSystem.OnJumpEnterTime();        
    }

    public void JumpUp()
    {
        if (!isControl )
            return;
        movementManger.coyoteSystem.OnJumpExitTime();
    }

    public void JumpPressed()
    {

    }


    public void WallGripEnter()
    {
        if (!isControl )
            return;
        movementManger.isWallGrip = true;
    }

    public void WallGripUp()
    {
        if (!isControl )
            return;
        movementManger.isWallGrip = false;
    }

    public void LeftMouseEnter()
    {
        if (!isControl)
            return;
        toolManager.LeftUse();

    }

    public void LeftMouseUp()
    {
        if (!isControl)
            return;
        
    }

    public void ReboundRight()
    {
        if (!isControl )
            return;

        if (movementManger.currentState != PlayerMovementManager.State.Rope)
            return;
        movementManger.isRopeReboundDirRight = true;
        movementManger.coyoteSystem.OnRopeReboundTime();
    }

    public void ReboundLeft()
    {
        if (!isControl )
            return;

        if (movementManger.currentState != PlayerMovementManager.State.Rope)
            return;
        movementManger.isRopeReboundDirRight = false;
        movementManger.coyoteSystem.OnRopeReboundTime();
    }


    public void SetTool(ToolManager.ActiveToolType type)
    {
        if (!isControl )
            return;

        toolManager.SetTool(type);
    }


    public void ToolUseLeft()
    {
        if (!isControl)
            return;


        toolManager.LeftUse();
    }

    public void ToolCancleLeft()
    {
        if (!isControl)
            return;


        toolManager.LeftCancle();
    }

    public void MadAttack()
    {
        if (!isControl)
            return;
        //if (movementManger.currentState == PlayerMovementManager.State.Ground)
            mad.Attack();
    }



    public void ToolUseRight()
    {
        if (!isControl)
            return;

        toolManager.RightUse();
    }

    public void ToolCancleRight()
    {
        if (!isControl)
            return;

        toolManager.RightCancle();
    }

    public void SetMoveDir(Vector2 dir)
    {
        //if (!isControl )
        //    return;

        moveDir = dir;
       // Debug.Log("MoveDir: " + dir);
    }


    public Vector2 moveDir
    {
        private set
        {

            m_moveDir = value;

            if (Mathf.Abs(m_moveDir.x) > 0.01f)
                m_moveDir.x = 1.0f * Mathf.Sign(m_moveDir.x);

            if (Mathf.Abs(m_moveDir.y) > 0.01f)
                m_moveDir.y = 1.0f * Mathf.Sign(m_moveDir.y);

        }
        get
        {
            if (!isControl)
                return Vector2.zero;

            return m_moveDir;
        }
    }

    public void SetControl(bool control)
    {
        isControl = control;
    }

    

}
