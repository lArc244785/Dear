using DG.Tweening;
using UnityEngine;

public class StateImfectTween : MonoBehaviour
{
    [Header("Hit")]
    [SerializeField]
    private Color m_hitColor;
    private Color hitColor { get { return m_hitColor; } }

    [SerializeField]
    private float m_hitTickTime;
    private float hitTickTime { get { return m_hitTickTime; } }


    [Header("Ghost")]
    [SerializeField]
    private Color m_ghostColor;
    private Color ghostColor { get { return m_ghostColor; } }

    [SerializeField]
    private float m_ghostTickTime;
    private float ghostTickTime { get { return m_ghostTickTime; } }

    private Tween m_imfectTween;

    private SpriteRenderer m_model;
    private SpriteRenderer model { get { return m_model; } }

    public void Init(SpriteRenderer spriteRenderer)
    {
        m_model = spriteRenderer;
    }

    private void ImfectTweenReset()
    {
        if (m_imfectTween == null)
            return;
        m_imfectTween.Kill();
        m_imfectTween = null;
        model.color = Color.white;
    }


    public void HitImfect(float hitDuringTime, float ghostDuringTime)
    {
        ImfectTweenReset();

        int hitLoopCount = (int)(hitDuringTime / hitTickTime);
        int ghostLoopCount = (int)(ghostDuringTime / ghostTickTime);

        Sequence sequence = DOTween.Sequence();

        Tween hitTween = model.DOColor(hitColor, hitTickTime).SetLoops(hitLoopCount, LoopType.Yoyo);
        Tween ghostTween = model.DOColor(ghostColor, ghostTickTime).SetLoops(ghostLoopCount, LoopType.Yoyo).OnComplete(() =>
        {
            ImfectTweenReset();
        }
        );

        sequence.Append(hitTween);
        sequence.Append(ghostTween);

        m_imfectTween = sequence;

        m_imfectTween.Play();


    }


}
