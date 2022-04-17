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

    public void SetStage(InputPlayer inputPlayer, Camera cam)
    {
        m_inputPlayer = inputPlayer;
        m_brainCam = cam;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

            m_inputPlayer.moveDir = context.ReadValue<Vector2>();


    }



    public void OnLeftMouseButton(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        if (context.started)
        {
            m_inputPlayer.LeftMouseEnter();
        }
        else if (context.canceled)
        {
            m_inputPlayer.LeftMouseUp();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        if (context.started)
            m_inputPlayer.JumpEnter();
        else if (context.canceled)
            m_inputPlayer.JumpUp();
    }

    public void OnPull(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;


    }

    public void OnRightMouseButton(InputAction.CallbackContext context)
    {

    }

    public void OnRightRopeRebound(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        if (context.started)
            m_inputPlayer.ReboundRight();
    }

    public void OnLeftRopeRebound(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        if (context.started)
            m_inputPlayer.ReboundLeft();
    }

    public void OnWallGrip(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        if (context.started)
            m_inputPlayer.WallGripEnter();
        else if (context.canceled)
            m_inputPlayer.WallGripUp();
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;


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
