using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameHpUI : UI_ViewBase
{
    [SerializeField]
    private GameObject m_fullHPUI;
  
    public override void Init()
    {
        base.Init();
        m_fullHPUI.GetComponent<HpUI>().init();
      
    }


}
