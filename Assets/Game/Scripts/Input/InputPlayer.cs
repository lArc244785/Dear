using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement m_movement;
    [SerializeField]
    private GrapplingShooter m_shooter;

    private Vector2 m_moveDir;


    public void JumpEnter()
    {

        m_movement.OnJumpEnter();
    }

    public void JumpUp()
    { 
        m_movement.OnJumpUp();  
    }

    public void JumpPressed()
    {

    }


    public void WallGripEnter()
    {
        m_movement.OnWallGripEnter();
    }

    public void WallGripUp()
    {
        m_movement.OnWallGripUp();
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
        if(m_movement.isRope)
            m_movement.Rebound(true);
    }

    public void ReboundLeft()
    {
        if (m_movement.isRope)
            m_movement.Rebound(false);
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
