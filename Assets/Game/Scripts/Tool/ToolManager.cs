using System.Collections;
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
        Wall, GroundPound,
        Total
    }

    private List<ToolInfo> m_passiveToolList = new List<ToolInfo>();



    #endregion

    [SerializeField]
    private Transform m_handTr;
    public Transform handTr
    {
        get
        {
            return m_handTr;
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


    public void Init()
    {
        for(int i = 0; i > (int)PassiveToolType.Total; i ++)
            m_passiveToolList.Add(new ToolInfo());
        
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
        if(m_activeToolList[(int)currentActiveTool].IsUse)
            m_activeToolList[(int)currentActiveTool].LeftUse();
    }

    public void RightUse()
    {
        if (m_activeToolList[(int)currentActiveTool].IsUse)
            m_activeToolList[(int)currentActiveTool].RightUse();
    }
    #endregion

    #region ChoiceMethod
    public void ChoiceTool()
    {

    }

    public void SetTool(ActiveToolType activeTool)
    {
        if (activeTool == ActiveToolType.None)
            return;

        if(currentActiveTool != ActiveToolType.None)
        {
            int exitToolIndex = (int)currentActiveTool;

            m_activeToolList[exitToolIndex].Exit();
            m_activeToolList[exitToolIndex].transform.parent = tools.transform;
            m_activeToolList[exitToolIndex].transform.localPosition = Vector3.zero;
            m_activeToolList[exitToolIndex].transform.localRotation = Quaternion.identity;
        }

        currentActiveTool = activeTool;
        int enterIndex = (int)currentActiveTool;
        m_activeToolList[enterIndex].transform.parent = handTr;
        m_activeToolList[enterIndex].transform.localRotation = Quaternion.identity;
        m_activeToolList[enterIndex].transform.localPosition = Vector3.zero;
        m_activeToolList[enterIndex].Enter();
    }
    #endregion




}
