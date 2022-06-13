using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    #region ActiveTool
    public enum ActiveToolType
    {
        None = -1,
        GrappingGun, PotarGun, IceMaker,
        Total
    }

    private ActiveToolType m_currentActiveTool;
    private ActiveToolType currentActiveTool
    {
        set
        {
            m_currentActiveTool = value;
        }
        get
        {
            return m_currentActiveTool;
        }
    }

    [SerializeField]
    private List<ActiveToolBase> m_activeToolList;
    #endregion

    #region PassiveTool
    public enum PassiveToolType
    {
        None = -1,
        Wall = 0, GroundPound,
        Total
    }

    private List<ToolInfo> m_passiveToolList = new List<ToolInfo>();



    #endregion

    [SerializeField]
    private Transform m_graplingGunPointTr;
    public Transform graplingGunPointTr
    {
        get
        {
            return m_graplingGunPointTr;
        }
    }

    [SerializeField]
    private GameObject m_tools;
    public GameObject tools
    {
        get
        {
            return m_tools;
        }
    }


    private UnitPlayer m_player;
    private UnitPlayer player
    {
        get
        {
            return m_player;
        }
    }


    public void Init(UnitPlayer player)
    {
        m_player = player;
        for (int i = 0; i < (int)PassiveToolType.Total; i++)
        {
            ToolInfo info = new ToolInfo();
            info.Init(ToolInfo.Type.Passive);

            m_passiveToolList.Add(info);
        }


        foreach (ActiveToolBase activeTool in m_activeToolList)
        {
            activeTool.Init(player);
            activeTool.gameObject.SetActive(false);
        }


        currentActiveTool = ActiveToolType.None;

        if (GameManager.instance.IsGetGrapplingGun() || GameManager.instance.stageManager.isTest)
            SetTool(ActiveToolType.GrappingGun);
    }



    #region AcheiveMethod
    public void AcheiveActiveTool(PassiveToolType passiveTool)
    {
        if (passiveTool == PassiveToolType.None)
        {
            Debug.LogError("PassiveTool type is None");
            return;
        }

        m_activeToolList[(int)passiveTool].toolInfo.isAcheive = true;
    }

    public void AcheivePassiveTool(ActiveToolType activeTool)
    {
        if (activeTool == ActiveToolType.None)
        {
            Debug.LogError("activeTool type is None");
            return;
        }
        m_activeToolList[(int)activeTool].toolInfo.isAcheive = true;
    }
    #endregion

    #region UseMethod
    public void LeftUse()
    {
        if (currentActiveTool == ActiveToolType.None)
            return;


        m_activeToolList[(int)currentActiveTool].LeftUse();
    }

    public void RightUse()
    {
        if (currentActiveTool == ActiveToolType.None)
            return;

        m_activeToolList[(int)currentActiveTool].RightUse();
    }
    #endregion

    #region CancleMethod

    public void LeftCancle()
    {
        if (currentActiveTool == ActiveToolType.None)
            return;

        m_activeToolList[(int)currentActiveTool].LeftCancle();
    }

    public void RightCancle()
    {
        if (currentActiveTool == ActiveToolType.None)
            return;


        m_activeToolList[(int)currentActiveTool].RightCancle();
    }

    #endregion





    #region ChoiceMethod
    public void ChoiceTool()
    {

    }

    public void SetTool(ActiveToolType activeTool)
    {


        if (currentActiveTool != ActiveToolType.None)
        {
            int exitToolIndex = (int)currentActiveTool;

            m_activeToolList[exitToolIndex].Exit();

            m_activeToolList[exitToolIndex].gameObject.SetActive(false);

            m_activeToolList[exitToolIndex].transform.parent = tools.transform;
            m_activeToolList[exitToolIndex].transform.localPosition = Vector3.zero;
            m_activeToolList[exitToolIndex].transform.localRotation = Quaternion.identity;
        }

        currentActiveTool = activeTool;
        if (currentActiveTool == ActiveToolType.None)
            return;


        int enterIndex = (int)currentActiveTool;




        m_activeToolList[enterIndex].transform.parent = GetToolPointTr(currentActiveTool);
        m_activeToolList[enterIndex].transform.localRotation = Quaternion.identity;
        m_activeToolList[enterIndex].transform.localPosition = Vector3.zero;
        m_activeToolList[enterIndex].Enter();

        m_activeToolList[enterIndex].gameObject.SetActive(true);
    }
    #endregion

    public bool IsPassiveToolAcheive(PassiveToolType type)
    {
        bool isAcheive = false;

        if (type == PassiveToolType.None)
            return false;

        return m_passiveToolList[(int)type].isAcheive;
    }

    private Transform GetToolPointTr(ActiveToolType type)
    {
        if (type == ActiveToolType.GrappingGun)
            return graplingGunPointTr;

        return null;
    }



}
