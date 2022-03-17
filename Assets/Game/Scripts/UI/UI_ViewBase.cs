using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_ViewBase : MonoBehaviour
{
    private RectTransform m_RectTransform;
    private Canvas m_canvas;
    private Vector2 m_offRectPos;


    public virtual void Init()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_canvas = GetComponent<Canvas>();
        m_offRectPos = m_RectTransform.localPosition;
    }

    public void Toggle(bool isToggle)
    {
        Vector2 pos = m_offRectPos;

        if (isToggle)
        {
            pos = Vector2.zero;
        }

        m_RectTransform.localPosition = pos;

        m_canvas.enabled = isToggle;
    }

}
