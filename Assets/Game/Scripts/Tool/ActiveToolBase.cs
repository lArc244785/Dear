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


    #region isUse
    private bool m_isUse;
    public bool IsUse
    {
        set
        {
            m_isUse = value;
        }
        get
        {
            return m_isUse;
        }
    }
    #endregion

    public void Init(ToolInfo.Type type)
    {
        m_toolInfo = new ToolInfo();
        toolInfo.Init(type);
        IsUse = true;
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
    public virtual void RightUse()
    {

    }


}
