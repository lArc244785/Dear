using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ProdutionView : UI_ViewBase
{
    [SerializeField]
    private UI_Fade m_fade;
    
    public override void Init()
    {
        base.Init();
    }

    public UI_Fade fade { get { return m_fade; } }

}
