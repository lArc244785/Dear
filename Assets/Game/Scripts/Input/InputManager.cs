using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class InputManager : SingleToon<InputManager>
{

    [SerializeField]
    private Camera m_brainCam;
    [SerializeField]
    private InputPlayer m_inputPlayer;
    [SerializeField]
    private GameObject m_PopUpUI;



    private void Awake()
    {
        Init();
        if (!m_brainCam|| !m_inputPlayer) return; 

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

        if (GameManager.instance.stageManager.player == null)
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
            m_inputPlayer.MadAttackAble(true);
        else if(context.canceled)
            m_inputPlayer.MadAttackAble(false);

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

        if (context.started)
            m_inputPlayer.Interaction();

    }

    
    public void EscapeUI(InputAction.CallbackContext contex)
    {
        if (GameObject.Find("PopUpUIManager") == null) Instantiate(m_PopUpUI);

        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;
        if (contex.started)
        {
            if (PopUpManager.instance.activePopupList.Count > 0)
            {
                GameManager.instance.ChaneGameState(GameManager.GameSate.GamePlaying);
                PopUpManager.instance.ClosePopup(PopUpManager.instance.activePopupList.First.Value);
            }   
            else
            {

                GameManager.instance.ChaneGameState(GameManager.GameSate.Pause);
                PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.esc);
            }
        }
    }


    public void gameStart(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.Title)
            return;
        if (context.started)
        {

            GameManager.instance.NextState(6);
       
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
