using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleToon<UIManager>
{
    [SerializeField]
    private UI_IngameView m_ingameView;

    protected override bool Init()
    {
        bool returnValue = base.Init();
        if (returnValue)
        {
            m_ingameView.Init();
        }
        return returnValue;
    }

    private void Start()
    {
        Init();


        inGameView.Toggle(true);
    }


    public UI_IngameView inGameView
    {
        get
        {
            return m_ingameView;
        }
    }

}
