using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossMovement : MonoBehaviour
{
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
    private float m_camOutDistance;
    public float camOutdistance
    {
        get {
            return m_camOutDistance;
        }
    }
    [SerializeField]
    private float m_camOutMoveDistance;
    public float camOutMoveDistance
    {
        get
        {
            return m_camOutMoveDistance;
        }
    }

    [SerializeField]
    private AnimationCurve m_movementCurve;


    private Tween m_moveTween_fast;


    public void TweenReset()
    {
        if (m_moveTween_fast == null)
            return;
        m_moveTween_fast.Kill();
        m_moveTween_fast = null;
       
    }

    private void Update()
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            TweenReset();
    }

    public void SpiderBossMove_Fast()
    {
        m_moveTween_fast = transform.DOLocalMoveY(m_fastdistance, m_fasttime).SetEase(m_movementCurve).SetRelative().Play();
    }
    public void SpiderBossMove_Slow()
    {

        m_moveTween_fast = transform.DOLocalMoveY(m_slowdistance, m_slowtime).SetRelative().SetEase(m_movementCurve).SetRelative().Play();

    }
    public void SpiderBossMove_CamOut(float pos)
    {
        m_moveTween_fast = transform.DOMoveY(pos, m_slowtime).SetEase(m_movementCurve).Play();
       

    }
    public void SpiderBossMove_Exit()
    {
        m_moveTween_fast = transform.DOLocalMoveY(m_slowdistance + 120, m_slowtime * 7).SetRelative().SetEase(Ease.OutExpo).SetRelative().Play();

    }


}
