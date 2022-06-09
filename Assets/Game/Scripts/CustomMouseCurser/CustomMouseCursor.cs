using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomMouseCursor : MonoBehaviour
{
    public enum CursorType
    {
        Aim = 0, Hook
    }

    private CursorType m_currentType;

    private RectTransform m_rect;

    [SerializeField]
    private Sprite[] m_Sprites;

    private Image m_image;


    public void Init()
    {
        m_rect = GetComponent<RectTransform>();
        //Cursor.visible = false;
        m_image = GetComponent<Image>();
    
        SetMouseCursor(CursorType.Aim);
        SetImageVisible(true);
    }


    private void FixedUpdate()
    {
        if(InputManager.instance != null)
        m_rect.transform.position = InputManager.instance.screenViewMousePos;
    }

    public void SetMouseCursor(CursorType type)
    {
        m_image.sprite = m_Sprites[(int)type];
    }

    public void SetImageVisible(bool isVisible)
    {
        m_image.enabled = isVisible;
    }



}
