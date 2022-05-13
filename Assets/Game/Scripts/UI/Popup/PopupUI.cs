using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PopupUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Button m_closeBtn;
    public Button closeBtn
    {
        get
        {
            return m_closeBtn;
        }

    }

    [SerializeField]
    public event Action m_onFocus;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        m_onFocus();
    }
}
