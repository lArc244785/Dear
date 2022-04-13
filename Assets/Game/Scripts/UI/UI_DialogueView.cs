using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_DialogueView : UI_ViewBase
{
    [SerializeField]
    private Image m_rightImage;
    [SerializeField]
    private Image m_leftImage;
    [SerializeField]
    private TextMeshProUGUI m_nameText;
    [SerializeField]
    private TextMeshProUGUI m_sentencesText;

    public string nameText
    {
        set
        {
            m_nameText.text = value;
        }
        get
        {
            return m_nameText.text;
        }
    }

    public string sentencesText
    {
        set { m_sentencesText.text = value; }
        get { return m_sentencesText.text; }
    }


    public override void Init()
    {
        base.Init();
    }



    public void RightImageOn(Sprite sprite)
    {
        m_rightImage.enabled = true;
    }

    public void RightImageOff()
    {
        m_rightImage.sprite = null;
        m_rightImage.enabled=false;
    }

    public void LeftImageOn(Sprite sprite)
    {
        m_leftImage.enabled = true;
        m_leftImage.sprite = sprite;    
    }

    public void LeftImageOff()
    {
        m_leftImage.sprite = null;
        m_leftImage.enabled = false;
    }

    public void NextDialogue()
    {
        DialogueManager.instance.NextDialogue();
    }

}
