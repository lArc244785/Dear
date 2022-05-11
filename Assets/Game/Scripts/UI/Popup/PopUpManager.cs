using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : SingleToon<PopUpManager>
{
    [SerializeField]
    private PopupUI m_inventory;
    public PopupUI inventory{

        get 
        { 
            return m_inventory; 
        }
        set
        {
            m_inventory = value;
        }
    }
    [SerializeField]
    private PopupUI m_character;
    public PopupUI character
    {
        get
        {
            return m_character;
        }
        set
        {
            m_character = value;
        }

    }
    [SerializeField]
    private PopupUI m_test;
    public PopupUI test
    {
        get
        {
            return m_test;
        }
        set
        {
            m_test = value;
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
            m_inventory,m_character,m_test
        };
     
        foreach (var popup in m_popupUIs)
        {
            popup.m_onFocus += () =>
            {
                m_activePopupList.Remove(popup);
                m_activePopupList.AddFirst(popup);
                RefreshAllPopupDepth();
            };
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
