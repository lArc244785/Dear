using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    #region player
    private UnitPlayer m_player;
    public UnitPlayer player { get { return m_player; } }
    #endregion

    #region CameraManager
    private CameraManager m_cameraManager;
    public CameraManager cameraManager
    {
        get
        {
            return m_cameraManager;
        }
    }
    #endregion

    #region stageBgm
    private StageBgm m_stageBgm;
    public StageBgm stageBgm { get { return m_stageBgm; } }
    #endregion

    #region playerDirRight
    [SerializeField]
    private bool m_playerDirRight;
    private bool playerDirRight { get { return m_playerDirRight; } }
    #endregion

    public void Init(/*bool playerDirRight*/)
    {
        ComponentSetting();

        player.Init();
        stageBgm.Init();
        cameraManager.Init();

        InputManager.instance.SetStage(player.inputPlayer, cameraManager.camera);
        player.Trun(playerDirRight);
    }

    private void ComponentSetting()
    {
        m_player = GameObject.Find("Player").GetComponent<UnitPlayer>();
        m_stageBgm = GetComponent<StageBgm>();
        m_cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();

    }


}
