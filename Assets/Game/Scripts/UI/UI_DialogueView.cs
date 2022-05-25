using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_DialogueView : UI_ViewBase
{
    #region rightImage
    private Image m_rightImage;
    private Image rightImage
    {
        get
        {
            return m_rightImage;
        }
    }
    #endregion

    #region leftImage
    private Image m_leftImage;
    private Image leftImage
    {
        get
        {
            return m_leftImage;
        }
    }
    #endregion

    #region nameText
    private TextMeshProUGUI m_nameText;
    private TextMeshProUGUI nameText
    {
        get
        {
            return m_nameText;
        }
    }
    #endregion


    #region sentencesText
    private TextMeshProUGUI m_sentencesText;
    private TextMeshProUGUI sentencesText
    {
        get
        {
            return m_sentencesText;
        }
    }
    #endregion


    #region dialogeBackGround
    private Image m_dialogeBackGround;
    private Image dialogeBackGround
    {
        get { return m_dialogeBackGround; }
    }
    #endregion

    public override void Init()
    {
        base.Init();
        m_rightImage = transform.Find("RightImage").GetComponent<Image>();
        m_leftImage = transform.Find("LeftImage").GetComponent<Image>();
        m_nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        m_sentencesText = transform.Find("SentenceText").GetComponent<TextMeshProUGUI>();
        m_dialogeBackGround = transform.Find("BackGround").GetComponent <Image>();
    }


    public void BackGroundOff()
    {
        dialogeBackGround.sprite = null;
        dialogeBackGround.enabled = false;
    }
    public void BackGroundOn(Sprite sprite)
    {
        dialogeBackGround.sprite = sprite;
        dialogeBackGround.enabled = true;
    }


    public void RightImageOn(Sprite sprite)
    {
        rightImage.enabled = true;
        rightImage.sprite = sprite;
    }

    public void RightImageOff()
    {
       rightImage.sprite = null;
       rightImage.enabled=false;
    }

    public void LeftImageOn(Sprite sprite)
    {
       leftImage.enabled = true;
       leftImage.sprite = sprite;    
    }

    public void LeftImageOff()
    {
        leftImage.sprite = null;
        leftImage.enabled = false;
    }

    public void NextDialogue()
    {
        DialogueManager.instance.NextDialogue();
    }

    public void SetNameText(string text)
    {
        nameText.text = text;
    }

    public void AddNameText(string text)
    {
        string newText = nameText.text + text;
        SetNameText(newText);
    }

    public void SetSentenceText(string text)
    {
        sentencesText.text = text;
    }

    public void AddSentenceText(string text)
    {
        string newText = sentencesText.text + text;
        SetSentenceText(newText);
    }




}
