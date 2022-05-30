using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum fadeType { Loop,Fadeinout}

public class FadeText : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float m_fadeTime;
    public float fadeTime
    {
        get
        {
            return m_fadeTime;
        }
        set
        {
            m_fadeTime = value;
        }

    }

    [SerializeField]
    private AnimationCurve m_fadeCurve;
    [SerializeField]
    private fadeType type;
    private TextMeshProUGUI Tmp;


    public void init()
    { 
        Tmp = GetComponent<TextMeshProUGUI>();
        switch (type) {
            case fadeType.Loop:

                StartCoroutine(fadeLoop());
                break;
            case fadeType.Fadeinout:
                StartCoroutine(fadeInOut());
                break;
        }
    }

    private IEnumerator fadeLoop()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0));
            yield return StartCoroutine(Fade(0, 1));
        }
    }
    private IEnumerator fadeInOut()
    {
        
             yield return StartCoroutine(Fade(0, 1));
            yield return StartCoroutine(Fade(1, 0));
   
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / m_fadeTime;

            Color color = Tmp.color;
            color.a = Mathf.Lerp(start, end, m_fadeCurve.Evaluate(percent));
            Tmp.color = color;

            yield return null;
        }
    }
}
