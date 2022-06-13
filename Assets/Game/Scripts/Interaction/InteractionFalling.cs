using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionFalling : InteractionBase
{
    [Header("Base Reference")]
    [SerializeField]
    private Rigidbody2D m_rig2D;
    [SerializeField]
    private Transform m_modelTr;
    
    [Header("Base Option")]
    [SerializeField]
    private float m_eventTime;
    private float m_enterTime;

    private bool m_evnetRun = false;

    [Header("Reset Option")]
    [SerializeField]
    private bool m_isReset;
    [SerializeField]
    private float m_resetTime;

    private Vector2 m_pos;

    private bool m_trapActive = true;

    [SerializeField]
    private FMODUnity.EventReference m_shakeEvent;
    [SerializeField]
    private FMODUnity.EventReference m_impactEvent;

    private bool m_isFalling;


    protected override void Enter(Collider2D collision)
    {
        if (!m_trapActive)
            return;


        base.Enter(collision);
        m_enterTime = Time.time;
        m_evnetRun = true;
        m_trapActive = false;
        m_isFalling = false;
        SoundManager.instance.SoundOneShot(m_shakeEvent);
    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
    }

    private void Update()
    {

        if (m_evnetRun)
        {
            float runTime = Time.time - m_enterTime;
            if(runTime >= m_eventTime)
            {
                m_rig2D.bodyType = RigidbodyType2D.Dynamic;
                SoundManager.instance.SoundOneShot(m_impactEvent);
                m_isFalling = true;
                if (m_isReset)
                    Invoke("Reset", m_resetTime);
                m_evnetRun = false;
                StartCoroutine(FallingCheckEventCoroutine());
            }


        }
    }

    private IEnumerator FallingCheckEventCoroutine()
    {
        yield return new WaitForSeconds(0.5f);


        while (m_isFalling)
        {
            if (m_rig2D.bodyType == RigidbodyType2D.Dynamic)
            {
                if (m_rig2D.velocity.y >= 0.0f)
                {
                    m_isFalling = false;
                    m_rig2D.bodyType = RigidbodyType2D.Static;
                }
            }
            yield return null;
        }
    }



    private void Reset()
    {
        m_rig2D.bodyType = RigidbodyType2D.Kinematic;
        m_modelTr.localPosition = Vector3.zero;
        m_trapActive = true;
    }

}
