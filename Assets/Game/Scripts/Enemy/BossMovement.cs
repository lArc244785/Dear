using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossMovement : MonoBehaviour
{
    private Tween m_moveTween;

    [SerializeField]
    private float m_fastdistance;
    public float fastdistance
    {
        get
        {
            return m_fastdistance;
        }
        set
        {
            m_fastdistance = value;
        }
    }
    [SerializeField]
    private float m_fasttime;
    public float fasttime
    {
        get
        {
            return m_fasttime;
        }
        set
        {
            m_fasttime = value;
        }
    }

    [SerializeField]
    private float m_slowdistance;
    public float slowdistance
    {
        get
        {
            return m_slowdistance;
        }
        set
        {
            m_slowdistance = value;
        }
    }
    [SerializeField]
    private float m_slowtime;
    public float slowtime
    {
        get
        {
            return m_slowtime;
        }
        set
        {
            m_slowtime = value;
        }
    }

    [SerializeField]
    private AnimationCurve m_movementCurve;




    public void TweenReset()
    {
        if (m_moveTween == null) return;
        m_moveTween.Kill();
        m_moveTween = null;
    }
    public void SpiderBossMove_Fast()
    {
        m_moveTween = transform.DOLocalMoveY(m_fastdistance, m_fasttime).SetEase(m_movementCurve).SetRelative().Play();
       
    }
    public void SpiderBossMove_Slow()
    {
        m_moveTween = transform.DOLocalMoveY(m_slowdistance, m_slowtime).SetRelative().SetEase(m_movementCurve).SetRelative().Play();
       
    }
    public void SpiderBossMove_CamOut(float pos)
    {
        m_moveTween = transform.DOMoveY(pos, m_slowtime).SetEase(m_movementCurve).Play();
    }


}
