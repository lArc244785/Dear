using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingleToon<InputManager>
{

    [SerializeField]
    private Camera m_brainCam;
    [SerializeField]
    private InputPlayer m_inputPlayer;

    public enum InputContextState
    {
        start, cancle
    };

    private InputContextState m_leftMouseState;
    public InputContextState leftMouseState
    {
        set
        {
            m_leftMouseState = value;
        }
        get
        {
            return m_leftMouseState;
        }
    }


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

        m_inputPlayer.SetMoveDir(context.ReadValue<Vector2>());


    }



    public void OnLeftMouseButton(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;


        if (context.started)
        {
            leftMouseState = InputContextState.start;
            m_inputPlayer.ToolUseLeft();
        }
        else if(context.canceled)
        {
            m_leftMouseState = InputContextState.cancle;
            m_inputPlayer.ToolCancleLeft();
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
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

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


    public void TestNoneTool(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;
        if(context.started)
        m_inputPlayer.SetTool(ToolManager.ActiveToolType.None);
    }

    public void TestGrappingTool(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;
        if (context.started)
            m_inputPlayer.SetTool(ToolManager.ActiveToolType.GrappingGun);
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
