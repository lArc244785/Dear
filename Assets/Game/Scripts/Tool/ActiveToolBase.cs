using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToolBase : MonoBehaviour
{
    #region ToolInfo
    private ToolInfo m_toolInfo;
    public ToolInfo toolInfo
    {
        get
        {
            return m_toolInfo;
        }
    }
    #endregion


    private UnitPlayer m_player;
    public UnitPlayer player
    {
        get
        {
            return m_player;
        }
    }

    private bool m_isControl;
    public bool isControl
    {
        set
        {
            m_isControl = value;
        }
        get
        {
            return m_isControl;
        }
    }


    public virtual void Init(UnitPlayer player)
    {
        m_toolInfo = new ToolInfo();
        toolInfo.Init(ToolInfo.Type.Active);
        m_player = player;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }


    public virtual void LeftUse()
    {

    }
    
    public virtual void LeftCancle()
    {

    }


    public virtual void RightUse()
    {

    }

    public virtual void RightCancle()
    {

    }


}
