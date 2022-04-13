using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UI_Fade : MonoBehaviour
{
    [SerializeField]
    private Image m_fadeBackGround;
    [Header("Fade In")]
    [SerializeField]
    private float m_fadeInProcessTime;
    [SerializeField]
    private Color m_fadeInColor;

    [Header("Fade Out")]
    [SerializeField]
    private float m_fadeOutProcessTime;
    [SerializeField]
    private Color m_fadeOutColor;


    private bool m_fadeProcessed;
    public bool fadeProcessed 
    {
        private set 
        { 
            m_fadeProcessed = value; 
        } 
        get 
        { 
            return m_fadeProcessed; 
        }
    }

    public void FadeIn()
    {
        m_fadeBackGround.color = m_fadeOutColor;
        fadeProcessed = false;
        Tween fadeInTween = m_fadeBackGround.DOColor(m_fadeInColor, m_fadeInProcessTime).OnComplete(() => { fadeProcessed = true; });
        fadeInTween.Play();

    }

    public void FadeOut()
    {
        m_fadeBackGround.color = m_fadeInColor;
        fadeProcessed = false;
        Tween fadeOutTween = m_fadeBackGround.DOColor(m_fadeOutColor, m_fadeOutProcessTime).OnComplete(() => { fadeProcessed = true; });
        fadeOutTween.Play();

    }


}
