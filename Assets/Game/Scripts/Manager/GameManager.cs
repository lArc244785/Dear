using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleToon<GameManager>
{
    #region GameState

    public enum GameSate
    {
        None = -1, Title, Load, GameStart, GamePlaying, InGameUISetting , GameOver, GameClear,
        Total
    }

    private GameSate m_gameState;
    public GameSate gameState
    {
        private set
        {
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



    protected override bool Init()
    {
        if (!base.Init())
            return false;

        GameStateInit();

        return true;
    }

    private void GameStateInit()
    {
        m_changeGameStaet = new ChangeGameState[(int)GameSate.Total];

        m_changeGameStaet[(int)GameSate.GamePlaying] = ChangeGamePlaying;
        m_changeGameStaet[(int)GameSate.InGameUISetting] = ChangeGamePlaying;
    }



    private void Awake()
    {
        Init();
        gameState = GameSate.GamePlaying;
    }

    #region State Funtion

    private void ChangeGamePlaying()
    {
        //Player.isControl = true;
    }

    private void ChangeInGameUISetting()
    {
        //Player.isControl = false;
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





}
