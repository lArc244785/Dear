using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SingleHpUI : MonoBehaviour
{
    [SerializeField]
    private Sprite m_fillHpUiSprite;
    [SerializeField]
    private Sprite m_emptyHpUisprite;
    [SerializeField]
    private Image m_thisImage;
    [SerializeField]
    private bool m_isfil;

    private void OnEnable()
    {
        m_isfil = true;
    }

    public bool isfill
    {
        set
        {
            m_isfil = value;
        }
        get
        {
            return m_isfil;
        }
    }
    

    public void ChangeSprite(string action)
    {
        switch (action)
        {
            case "fill":
                m_thisImage.sprite = m_fillHpUiSprite;
                isfill = true;
                break;
            case "empty":
                Debug.Log("피닳음");
                break;
            default:
                break;
        }
    }
}
