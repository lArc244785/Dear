using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : SingleToon<PopUpManager>
{


    [SerializeField]
    private PopupUI m_Esc;
    public PopupUI esc
    {
        get
        {
            return m_Esc;
        }
        set
        {
            m_Esc = value;
        }

    }
    [SerializeField]
    private PopupUI m_check;
    public PopupUI check
    {
        get
        {
            return m_check;
        }
        set
        {
            m_check = value;
        }

    }


    private LinkedList<PopupUI> m_activePopupList;
    private List<PopupUI> m_popupUIs;
    public LinkedList<PopupUI> activePopupList
    {
        get
        {
            return m_activePopupList;
        }
        set
        {
            m_activePopupList = value;
        }
    }

 

    private void Awake()
    {
        Init();
        m_activePopupList = new LinkedList<PopupUI>();
        PopupInit();
        InitCloseAll();
    }
    protected override bool Init()
    {
        return base.Init();
    }

  
        private void PopupInit()
    {
        m_popupUIs = new List<PopupUI>()
        {
            m_Esc,m_check
        };
     
        foreach (var popup in m_popupUIs)
        {
            popup.m_onFocus += () =>
            {
                m_activePopupList.Remove(popup);
                m_activePopupList.AddFirst(popup);
                RefreshAllPopupDepth();
            };
            if (!popup.closeBtn) return;
            popup.closeBtn.onClick.AddListener(() => ClosePopup(popup));   
        }
    }
    private void InitCloseAll()
    {
        foreach(var popup in m_popupUIs)
        {
            ClosePopup(popup);
        }
    }
    public void ToggleOpenClosePopup(PopupUI popup)
    {
        if (!popup.gameObject.activeSelf) OpenPopup(popup);
        else ClosePopup(popup);
    }
    public void OpenPopup(PopupUI popup)
    {
        m_activePopupList.AddFirst(popup);
        popup.gameObject.SetActive(true);
        RefreshAllPopupDepth();
    }

    public void ClosePopup(PopupUI popup)
    {
        m_activePopupList.Remove(popup);
        popup.gameObject.SetActive(false);
        RefreshAllPopupDepth();
    }
    


    private void RefreshAllPopupDepth()
    {
        foreach(var popup in m_activePopupList)
        {
            popup.transform.SetAsFirstSibling();
        }
    }

}
