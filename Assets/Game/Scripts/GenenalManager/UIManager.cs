using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleToon<UIManager>
{
    [SerializeField]
    private UI_IngameView m_ingameView;
    [SerializeField]
    private UI_Dialogue m_dialogue;

    protected override bool Init()
    {
        bool returnValue = base.Init();
        if (returnValue)
        {
            m_ingameView.Init();
            m_dialogue.Init();
        }
        return returnValue;
    }

    private void Awake()
    {
        Init();


        inGameView.Toggle(true);
        dialogue.Toggle(false);
    }

   

    public UI_IngameView inGameView
    {
        get
        {
            return m_ingameView;
        }
    }

    public UI_Dialogue dialogue
    { 
        get { return m_dialogue; } 
    }
}
