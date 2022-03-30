using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingleToon<InputManager>
{

    [SerializeField]
    private Camera m_brainCam;
    [SerializeField]
    private InputPlayer m_inputPlayer;



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
        m_inputPlayer.moveDir = context.ReadValue<Vector2>();
    }



    public void OnLeftMouseButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            m_inputPlayer.LeftMouseEnter();
        }
        else if(context.canceled)
        {
            m_inputPlayer.LeftMouseUp();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            m_inputPlayer.JumpEnter();
        else if (context.canceled)
            m_inputPlayer.JumpUp();
    }

    public void OnPull(InputAction.CallbackContext context)
    {

    }

    public void OnRightMouseButton(InputAction.CallbackContext context)
    {

    }

    public void OnRightRopeRebound(InputAction.CallbackContext context)
    {
        if (context.started)
            m_inputPlayer.ReboundRight();
    }

    public void OnLeftRopeRebound(InputAction.CallbackContext context)
    {
        if (context.started)
            m_inputPlayer.ReboundLeft();
    }

    public void OnWallGrip(InputAction.CallbackContext context)
    {
        if (context.started)
            m_inputPlayer.WallGripEnter();
        else if (context.canceled)
            m_inputPlayer.WallGripUp();
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {

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
