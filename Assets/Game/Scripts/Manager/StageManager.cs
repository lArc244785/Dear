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


    [SerializeField]
    private HpUI m_fullHPUI;


    #region playerDirRight
    [SerializeField]
    private bool m_playerDirRight;
    private bool playerDirRight { get { return m_playerDirRight; } }
    #endregion

    [SerializeField]
    private string m_stage_name;
    [SerializeField]
    private TextMeshProUGUI m_stageText;


    [Header("BGM")]
    [SerializeField]
    private Bgm.BgmType m_stageBgm;
    [SerializeField]
    private float m_bgmProgress;


    [Header("Save")]
    [SerializeField]
    private bool m_isSave;


    private void Start()
    {

        SoundManager.instance.bgm.BgmChange(m_stageBgm);
        SoundManager.instance.bgm.SetParamaterPrograss(m_bgmProgress);

        if (SoundManager.instance.bgm.IsSoundStop())
        {
            SoundManager.instance.bgm.BgmStart();
        }
    }



    public void Init(/*bool playerDirRight*/)
    {
        ComponentSetting();

        if (m_stage_name == null) m_stage_name = "";


        m_stageText.GetComponent<FadeText>().init();

        player.Init();
        GameManager.instance.LoadHp();
        
        cameraManager.Init();
        m_stageText.text = m_stage_name;
      
       

        m_fullHPUI.init();
        m_fullHPUI.initCnt = true;

        InputManager.instance.SetStage(player.inputPlayer, cameraManager.camera);
        player.Trun(playerDirRight);

        if(GameManager.instance.CanLoad())
        {
            GameManager.instance.LoadPos();
        }
        else if(m_isSave)
        {
            GameManager.instance.TempSavePos(player.unitPos);
        }

        GameManager.instance.deathProduction.RenderCameraSettting(cameraManager.camera);

    }

    private void ComponentSetting()
    {
        m_player = GameObject.Find("Player").GetComponent<UnitPlayer>();
        m_cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
        m_fullHPUI = GameObject.Find("Hpcontainer").GetComponent<HpUI>();
    }

   



}
