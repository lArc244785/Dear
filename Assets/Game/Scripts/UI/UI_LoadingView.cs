using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_LoadingView : UI_ViewBase
{
    [SerializeField]
    private Image m_loadingImage;
    [SerializeField]
    private TextMeshProUGUI m_loadingGaugeText;
    [SerializeField]
    private TextMeshProUGUI m_loadingText;
    [SerializeField]
    private int m_dotMaxCount;
    [SerializeField]
    private float m_dotTick;

    private string m_load = "Loading";

    private bool m_fakeLoadingEnd;
    public bool fakeLoadingEnd 
    {
        private set 
        { 
            m_fakeLoadingEnd = value; 
        } 
        get 
        { 
            return m_fakeLoadingEnd; 
        } 
    }

    public override void Init()
    {
        base.Init();
    }

    public void LoadingProduction()
    {
        fakeLoadingEnd = false;
        StartCoroutine(LoadingTextCoroutine());
        StartCoroutine(FakeLoadingGauge());
    }


    public IEnumerator LoadingTextCoroutine()
    {
        int dotCount = 0;

        m_loadingText.text = m_load;

        while(!m_fakeLoadingEnd)
        {
            #region Loading Dot
            m_loadingText.text = m_load;
            for (int i = 0; i < dotCount; i++)
                m_loadingText.text += ".";
            dotCount = Mathf.Clamp(++dotCount, 0 ,m_dotMaxCount);
            #endregion

            yield return new WaitForSeconds(m_dotTick);
        }
    }

    public IEnumerator FakeLoadingGauge()
    {
        m_loadingImage.fillAmount = 0.0f;
        m_loadingImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        
        m_loadingImage.DOFillAmount(0.5f, 1.0f).Play();
        yield return new WaitForSeconds(1.0f);

        yield return new WaitForSeconds(0.5f);

        m_loadingImage.DOFillAmount(1.0f, 1.0f).Play();

        yield return new WaitForSeconds(0.5f);
        fakeLoadingEnd = true;

    }

    private IEnumerator FakeLoadingGaugeTextUpdateCoroutine()
    {
        int gauge = 0;
        while(!fakeLoadingEnd)
        {
            gauge = (int)(m_loadingImage.fillAmount * 100.0f);
            m_loadingGaugeText.text = gauge.ToString() + "%";
            yield return null;
        }
        m_loadingGaugeText.text = "100%";
    }


}
