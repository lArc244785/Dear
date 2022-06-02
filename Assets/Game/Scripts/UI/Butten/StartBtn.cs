using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public enum fadeState { fadeIn = 0, fadeout, fadeinout, fadeLoop}
public class StartBtn : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float m_fadeTime;
    [SerializeField]
    private AnimationCurve m_fadeCurve;
    private Image m_image;
    private fadeState m_fadeState;

    private void Start()
    {
        m_image = GetComponent<Image>();
        StartCoroutine(fadeLoop());
    }

    private IEnumerator fadeLoop()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0));
            yield return StartCoroutine(Fade(0, 1));
        }
    }
    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / m_fadeTime;

            Color color = m_image.color;
            color.a = Mathf.Lerp(start, end, m_fadeCurve.Evaluate(percent));
            m_image.color = color;

            yield return null;
        }
    }

}
