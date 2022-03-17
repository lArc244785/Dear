using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_IngameView : UI_ViewBase
{
    [SerializeField]
    private UI_SlingShot m_slingShot;

    public override void Init()
    {
        base.Init();
        slingShot.Init();
    }

    public UI_SlingShot slingShot
    {
        get
        {
            return m_slingShot;
        }
    }

  
}
