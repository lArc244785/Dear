using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpiderJumpTween : MonoBehaviour
{
    [SerializeField]
    private Transform m_unitTransform;
    public Transform unitTransform
    {
        get {
            return m_unitTransform; 
            }
    }
    [SerializeField]
    private Transform m_station;
    public Transform station
    {
        get
        {
            return m_station;
        }
    }

    private Tween m_jumpTween;

    [SerializeField]
    private float m_jumpPower;
    


    public void JumpTweenReset()
    {
        Debug.Log("트윈삭제");
        if (m_jumpTween == null) return;
        m_jumpTween.Kill();
        m_jumpTween = null;
    }
    private static Tween a;
    public void Jump(bool isRight)
    {
        JumpTweenReset();
        Sequence sequence = DOTween.Sequence();
        
        
        
        a = transform.DOJump(new Vector2(station.position.x, transform.position.y), m_jumpPower, 1, 0.7f, false);
        
        sequence.Append(a);
        m_jumpTween = sequence;
        m_jumpTween.Play();


        Debug.Log("점프");

    }
}
