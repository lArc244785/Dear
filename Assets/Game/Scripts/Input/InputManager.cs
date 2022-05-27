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

        m_inputPlayer.SetMoveDir(context.ReadValue<Vector2>());
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
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        if (context.started)
            m_inputPlayer.MadAttack();
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

    public void ActiveInventory(InputAction.CallbackContext contex)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.inventory);
    }
    public void ActivecharUI(InputAction.CallbackContext contex)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.character);
    }

    public void ActiveTestUI(InputAction.CallbackContext contex)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        Debug.Log("a");
        PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.test);
    }
    public void EscapeUI(InputAction.CallbackContext contex)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;
        if (contex.started)
        {
            if (PopUpManager.instance.activePopupList.Count > 0)
            {
                PopUpManager.instance.ClosePopup(PopUpManager.instance.activePopupList.First.Value);
            }
            else
            {
                PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.esc);
            }
        }

    }

    public void OnToolNone(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;
        if (context.started)
        {
            m_inputPlayer.SetTool(ToolManager.ActiveToolType.None);
        }
    }

    public void OnToolGrapping(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;
        if (context.started)
        {
            m_inputPlayer.SetTool(ToolManager.ActiveToolType.GrappingGun);
        }
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
