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


    private bool m_isfadeProcessed;
    public bool isfadeProcessed 
    {
        private set 
        { 
            m_isfadeProcessed = value; 
        } 
        get 
        { 
            return m_isfadeProcessed; 
        }
    }

    public void FadeIn()
    {
        m_fadeBackGround.color = m_fadeOutColor;
        isfadeProcessed = false;
        Tween fadeInTween = m_fadeBackGround.DOColor(m_fadeInColor, m_fadeInProcessTime).OnComplete(() => { isfadeProcessed = true; });
        fadeInTween.Play();

    }

    public void FadeOut()
    {
        m_fadeBackGround.color = m_fadeInColor;
        isfadeProcessed = false;
        Tween fadeOutTween = m_fadeBackGround.DOColor(m_fadeOutColor, m_fadeOutProcessTime).OnComplete(() => { isfadeProcessed = true; });
        fadeOutTween.Play();

    }


}
