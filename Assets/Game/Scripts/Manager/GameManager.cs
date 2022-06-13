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

    private DeathProduction m_deathProduction;


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

    private int m_nextStageIndex;

    private struct PlayerTempSave
    {
        public int stageIndex;
        public int hp;
        public Vector3 savePos; 
    }

    private PlayerTempSave m_tempSave;

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        GameStateInit();

        DOTween.Init(false, false, LogBehaviour.Default);
        DOTween.defaultAutoPlay = AutoPlay.None;

        m_deathProduction = transform.GetChild(0).GetComponent<DeathProduction>();
        m_deathProduction.Init();

        m_tempSave.stageIndex = -1;
        m_tempSave.savePos = Vector3.zero;
        m_tempSave.hp = 6;


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
        m_changeGameStaet[(int)GameSate.GameOver] = ChangeGameOver;
    }



    private void Awake()
    {
        
        Init();
    }

    private void Start()
    {
        
        gameState = m_gameState;
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
        UIManager.instance.produtionView.Toggle(true);
        UIManager.instance.produtionView.fade.FadeOutSkip();
        m_deathProduction.SetDeathBackGroundActive(false);

        StartCoroutine(LoadCorutine(m_nextStageIndex));

    }

    private void ChangeStageLoad()
    {
        UIManager.instance.AllToggleFase();
        UIManager.instance.produtionView.Toggle(true);
        StartCoroutine(StageLoadCorutine(m_nextStageIndex));
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

    private void ChangeGameOver()
    {
        Debug.Log("Death");
        StartCoroutine(GameOverProcessCoroutine());  
    }
    #endregion



    public void NextState(int index)
    {
        TempSaveHp();
        stageManager.player.inputPlayer.SetControl(false);
        m_nextStageIndex = index;
        gameState = GameSate.StageLoad;

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

        yield return StartCoroutine(LoadCorutine(index));
    }

    private IEnumerator LoadCorutine(int index)
    {
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


    private IEnumerator GameOverProcessCoroutine()
    {
        UIManager.instance.AllToggleFase();
        m_deathProduction.SetDeathBackGroundActive(true);
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(1.0f);
        stageManager.cameraManager.SetVitrualCamera("vcam_Death");
        yield return new WaitForSeconds(1.0f);


        m_nextStageIndex = m_tempSave.stageIndex;
        ChaneGameState(GameSate.Load);
        Time.timeScale = 1.0f;

        yield return null;
    }


    public void TempSavePos(Vector3 savePos)
    {
        Debug.Log("SAVE:Temp Save");
        m_tempSave.stageIndex = SceneManager.GetActiveScene().buildIndex;
        m_tempSave.savePos = savePos;
    }

    public void TempSaveHp()
    {
        m_tempSave.hp = stageManager.player.health.hp;
    }



    public bool CanLoad()
    {
        return SceneManager.GetActiveScene().buildIndex == m_tempSave.stageIndex;
    }

    public void LoadPos()
    {
        stageManager.player.transform.position = m_tempSave.savePos;
    }

    public void LoadHp()
    {
        stageManager.player.health.SetHP(m_tempSave.hp);
    }



}
