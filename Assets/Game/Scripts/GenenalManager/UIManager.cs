using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleToon<UIManager>
{
    [SerializeField]
    private UI_DialogueView m_dialogueView;
    [SerializeField]
    private UI_LoadingView m_loadingView;
    [SerializeField]
    private UI_ProdutionView m_produtionView;
    [SerializeField]
    private IngameHpUI m_ingameUIView;
    [SerializeField]
    private UI_title m_titleView;
    

    private List<UI_ViewBase> m_uiList;


    protected override bool Init()
    {
        bool returnValue = base.Init();
        if (returnValue)
        {
            m_uiList = new List<UI_ViewBase>();
            m_dialogueView.Init();
            m_uiList.Add(dialogueView);
            loadingView.Init();
            m_uiList.Add(loadingView);
            produtionView.Init();
            m_uiList.Add(produtionView);
            m_ingameUIView.Init();
            m_uiList.Add(m_ingameUIView);
            m_titleView.Init();
            m_uiList.Add(m_titleView);
        }
        return returnValue;
    }

    private void Awake()
    {
        Init();

    }

    public void AllToggleFase()
    {
        foreach (UI_ViewBase ui in m_uiList)
            ui.Toggle(false);
    }



    public UI_DialogueView dialogueView
    { 
        get { return m_dialogueView; } 
    }

    public UI_LoadingView loadingView
    {
        get
        {
            return m_loadingView;
        }
    }

    public UI_ProdutionView produtionView 
    {
        get 
        { 
            return m_produtionView; 
        } 
    }
     public IngameHpUI ingameHpUI
    {
        get
        {
            return m_ingameUIView;
        }
    }
     
}
