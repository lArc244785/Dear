using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField]
    private string m_stage_name;
    [SerializeField]
    private TextMeshProUGUI m_stageText;

    public void Init(/*bool playerDirRight*/)
    {
        ComponentSetting();

        if (m_stage_name == null) m_stage_name = "";


        m_stageText.GetComponent<FadeText>().init();

        player.Init();
        stageBgm.Init();
        cameraManager.Init();
        m_stageText.text = m_stage_name;


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
