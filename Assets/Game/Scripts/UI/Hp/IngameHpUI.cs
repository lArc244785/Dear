using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameHpUI : UI_ViewBase
{
    [SerializeField]
    private GameObject m_fullHPUI;
    [SerializeField]
    private FaceImage m_faceImg;
  
    public override void Init()
    {
        base.Init();
        m_fullHPUI.GetComponent<HpUI>().init();
        m_faceImg = transform.GetChild(2).GetComponent<FaceImage>();
        m_faceImg.init();
    }
    private void Update()
    {
       // m_faceImg.imgUpdate();
    }


}
