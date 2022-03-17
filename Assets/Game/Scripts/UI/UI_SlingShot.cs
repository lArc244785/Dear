using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlingShot : UI_Object
{
    private Transform m_targetTr;
    private RectTransform m_rectTransform;

    public Transform targetTr
    {
        set
        {
            m_targetTr = value;
        }
        get
        {
            return m_targetTr;
        }
    }

    public override void Init()
    {
        base.Init();
        m_rectTransform = GetComponent<RectTransform>();
        isToggle = false;
    }



    protected override void Draw()
    {
        base.Draw();
        if (m_targetTr == null)
            return;


        Vector2 targetScreenPoint = Utility.IngamePosToViewPos(targetTr.position);
        Vector2 mouseScreenPoint = InputManager.instance.screenViewMousePos;

        float angle = Utility.GetRotaionAngleByTargetPosition(targetScreenPoint, mouseScreenPoint, 90.0f);

        m_rectTransform.localPosition = targetScreenPoint;
        m_rectTransform.rotation = Utility.GetRoationZ(angle);
    }




}
