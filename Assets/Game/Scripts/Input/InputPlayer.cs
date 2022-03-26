using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement m_movement;

    private Vector2 m_moveDir;

    private bool m_isJumpPressed;
    public bool IsJumpPressed { get => m_isJumpPressed; private set => m_isJumpPressed = value; }

    private float m_lastJumpPressedTime;


    [SerializeField]
    private float m_jumpPressedTime;



    public void JumpEnter()
    {
        IsJumpPressed = true;
        m_movement.LastJumpTimeReset();
        m_movement.LastWallJumpTimeReset();
    }

    public void JumpUp()
    {
        IsJumpPressed = false;
    }

    public void JumpPressed()
    {
    }


    public void WallGripEnter()
    {

    }

    public void WallGripUp()
    {

    }

    public void LeftMouseEnter()
    {

    }

    public void LeftMouseUp()
    {

    }

    public void ChackJumpPressed()
    {
        if(IsJumpPressed)
            lastJumpPressedTime = m_jumpPressedTime;
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

    public float lastJumpPressedTime
    {
        set
        {
            m_lastJumpPressedTime = value;
        }
        get
        {
            return m_lastJumpPressedTime;
        }
    }

}
