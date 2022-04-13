using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private UnitPlayer m_player;
    [SerializeField]
    private InputPlayer m_inputPlayer;
    [SerializeField]
    private Camera m_brainCam;
   
    public UnitPlayer player { get { return m_player; } }

    public void Init()
    {
        m_player.Init();
        InputManager.instance.SetStage(m_inputPlayer, m_brainCam);
    }
}
