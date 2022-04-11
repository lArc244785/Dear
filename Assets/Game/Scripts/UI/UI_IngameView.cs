using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_IngameView : UI_ViewBase
{

    [SerializeField]
    private Image m_hitImage;
    


    public override void Init()
    {
        base.Init();
        HitImageToggle(false);
    }

   
    public void HitImageToggle(bool toggle)
    {
        m_hitImage.gameObject.SetActive(toggle);
    }
  
}
