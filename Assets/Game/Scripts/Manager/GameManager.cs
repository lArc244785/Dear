using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : SingleToon<GameManager>
{
    #region GameState

    public enum GameSate
    {
        None = -1, Title, Load, StageLoad, GameStart, GamePlaying, InGameUISetting , GameOver, GameClear, Pause,
        Total
    }
    [SerializeField]
    private GameSate m_gameState;
    public GameSate gameState
    {
        private set
        {
            if(m_gameState == GameSate.Pause)
            {
                Time.timeScale = 1.0f;
            }

            m_gameState = value;
            if (m_gameState == GameSate.None)
                return;

            m_changeGameStaet[(int)m_gameState]();
        }

        get
        {
            return m_gameState;
        }
    }

    #endregion

    private delegate void ChangeGameState();
    private ChangeGameState[] m_changeGameStaet;
    private StageManager m_stageManager;
    public StageManager stageManager
    {
        private set
        {
            m_stageManager = value;
        }
        get
        {
            return m_stageManager;
        }
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        GameStateInit();

        DOTween.Init(false, false, LogBehaviour.Default);
        DOTween.defaultAutoPlay = AutoPlay.None;

        return true;
    }

    private void GameStateInit()
    {
        m_changeGameStaet = new ChangeGameState[(int)GameSate.Total];

        m_changeGameStaet[(int)GameSate.Title] = ChangeGameTitle;
        m_changeGameStaet[(int)GameSate.GamePlaying] = ChangeGamePlaying;
        m_changeGameStaet[(int)GameSate.Load] = ChangeLoad;
        m_changeGameStaet[(int)GameSate.StageLoad] = ChangeStageLoad;
        m_changeGameStaet[(int)GameSate.GameStart] = ChangeGameStart;
        m_changeGameStaet[(int)GameSate.InGameUISetting] = ChangeInGameUISetting;
        m_changeGameStaet[(int)GameSate.Pause] = ChangeGamePause;
    }



    private void Awake()
    {
        
        Init();
    }

    private void Start()
    {
        //NextState(1);
        gameState = GameSate.GameStart;
    }


    #region State Funtion

    private void ChangeGamePlaying()
    {
        stageManager.player.inputPlayer.isControl = true;
        
    }

    private void ChangeInGameUISetting()
    {
        stageManager.player.inputPlayer.isControl = false;
    }

    private void ChangeLoad()
    {
        UIManager.instance.AllToggleFase();
    }

    private void ChangeStageLoad()
    {
        UIManager.instance.AllToggleFase();
        UIManager.instance.produtionView.Toggle(true);
    }

    private void ChangeGameStart()
    {
        StartCoroutine(GameStartProcessCoroutine());
    }

    private void ChangeGamePause()
    {
        Time.timeScale = 0.0f;
    }

    private void ChangeGameTitle()
    {
        UIManager.instance.AllToggleFase();
        //UIManager.instance.titleView.Toggle(true)
    }

    #endregion

    #region State Change

    public void ChangeStateUISetting()
    {
        gameState = GameSate.InGameUISetting;
    }


    public void ChangeStateChangeGamePlaying()
    {
        gameState = GameSate.GamePlaying;
    }
    #endregion

    public void NextState(int index)
    {
        stageManager.player.inputPlayer.SetControl(false);
        gameState = GameSate.StageLoad;
        StartCoroutine(StageLoadCorutine(index));
    }
    public void Gotitle()
    {
       
        gameState = GameSate.StageLoad;
        StartCoroutine(GotitleCorutine());
    }
    public void ChaneGameState(GameSate nextState)
    {
        gameState = nextState;
    }

    private IEnumerator StageLoadCorutine(int index)
    {
        UIManager.instance.produtionView.fade.FadeOut();

        while (!UIManager.instance.produtionView.fade.isfadeProcessed)
            yield return null;


        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            yield return null;
        }

        gameState = GameSate.GameStart;
    }




    private IEnumerator SceneLoadCorutine(int index)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        UIManager.instance.loadingView.LoadingProduction();

        while(!operation.isDone || !UIManager.instance.loadingView.fakeLoadingEnd)
        {
            yield return null;
        }

        gameState = GameSate.GameStart;
    }
    private IEnumerator GotitleCorutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);
        UIManager.instance.loadingView.LoadingProduction();
        while (!operation.isDone || !UIManager.instance.loadingView.fakeLoadingEnd)
        {
            yield return null;
        }
        gameState = GameSate.Title;

    }

    private IEnumerator GameStartProcessCoroutine()
    {
        UIManager.instance.AllToggleFase();
        UIManager.instance.ingameHpUI.Toggle(true);
        UIManager.instance.produtionView.Toggle(true);
        UIManager.instance.produtionView.fade.FadeIn();

        stageManager = GameObject.FindObjectOfType<StageManager>();
        stageManager.Init();

        stageManager.player.inputPlayer.isControl = false;

        while (!UIManager.instance.produtionView.fade.isfadeProcessed)
            yield return null;

        stageManager.player.inputPlayer.isControl = true;
        gameState = GameSate.GamePlaying;

    }

}
