using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolInfo 
{
    #region isAcheive
    private bool m_isAcheive;
    public bool isAcheive
    {
        set
        {
            m_isAcheive = value;
        }
        get { return m_isAcheive; }
    }
    #endregion

    #region Type
    public enum Type
    {
        Passive, Active
    }
    private Type m_type;
    public Type type
    {
        get
        {
            return m_type;
        }
    }
    #endregion

    public void Init(Type type) 
    {
        m_type = type;
        isAcheive = true;
    }


}
