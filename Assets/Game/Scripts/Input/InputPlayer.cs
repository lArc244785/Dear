using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement m_movement;

    private Vector2 m_moveDir;

    private bool m_isJumpPressed;
    public bool isJumpPressed { get => m_isJumpPressed; private set => m_isJumpPressed = value; }

    private bool m_isJumpEnter;

    private float m_lastOnJumpEnterTime;




    public void JumpEnter()
    {
        isJumpPressed = true;
        m_movement.OnJumpEnter();
    }

    public void JumpUp()
    {
        isJumpPressed = false;
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

    }

    public void LeftMouseUp()
    {

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


    public float lastOnJumpEnterTime { get => m_lastOnJumpEnterTime; set => m_lastOnJumpEnterTime = value; }
}
