using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private UnitPlayer m_player;
    private UnitPlayer player
    {
        get
        {
            return m_player;
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



    public void Init(UnitPlayer player, Mad mad)
    {
        m_player = player;

        m_mad = mad;

        isControl = true;
        //isMoveControl = true;
    }

    public void JumpEnter()
    {
        if (!isControl )
            return;

        player.movementManager.coyoteSystem.OnJumpEnterTime();        
    }

    public void JumpUp()
    {
        if (!isControl )
            return;
        player.movementManager.coyoteSystem.OnJumpExitTime();
    }

    public void JumpPressed()
    {

    }


    public void WallGripEnter()
    {
        if (!isControl )
            return;
        player.movementManager.isWallGrip = true;
    }

    public void WallGripUp()
    {
        if (!isControl )
            return;
        player.movementManager.isWallGrip = false;
    }

    public void LeftMouseEnter()
    {
        if (!isControl)
            return;
        player.toolManager.LeftUse();

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

        if (player.movementManager.currentState != PlayerMovementManager.State.RopeJump)
            return;
        player.movementManager.isRopeReboundDirRight = true;
        player.movementManager.coyoteSystem.OnRopeReboundTime();
    }

    public void ReboundLeft()
    {
        if (!isControl )
            return;

        if (player.movementManager.currentState != PlayerMovementManager.State.RopeJump)
            return;
        player.movementManager.isRopeReboundDirRight = false;
        player.movementManager.coyoteSystem.OnRopeReboundTime();
    }


    public void SetTool(ToolManager.ActiveToolType type)
    {
        if (!isControl )
            return;

        player.toolManager.SetTool(type);
    }


    public void ToolUseLeft()
    {
        if (!isControl)
            return;


        player.toolManager.LeftUse();
    }

    public void ToolCancleLeft()
    {
        if (!isControl)
            return;


        player.toolManager.LeftCancle();
    }

    public void MadAttackAble(bool isAttackAble)
    {
        if (!isControl && isAttackAble)
            return;


        mad.SetAttackAble(isAttackAble);
    }



    public void ToolUseRight()
    {
        if (!isControl)
            return;

        player.toolManager.RightUse();
    }

    public void ToolCancleRight()
    {
        if (!isControl)
            return;

        player.toolManager.RightCancle();
    }

    public void SetMoveDir(Vector2 dir)
    {
        //if (!isControl )
        //    return;

        moveDir = dir;
       // Debug.Log("MoveDir: " + dir);
    }

    public void Interaction()
    {
        if (player.interaction.CanInteracion())
            player.interaction.Interaction();
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
