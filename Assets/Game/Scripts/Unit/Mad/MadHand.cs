using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadHand : MonoBehaviour
{
    [SerializeField]
    private Transform m_targetTr;
    [SerializeField]
    private float m_smoothTime;
    [SerializeField]
    private float m_deadRange;
    [SerializeField]
    private SpriteRenderer m_model;
    [SerializeField]
    private bool m_isPivotRight;

    [Header("Mad Model")]
    [SerializeField]
    private SpriteRenderer m_madModel;

    private Vector2 m_currentPos;
    private Vector2 m_currentVelcotiy;

    private bool m_isInit =false ;

    private float frontLocalPoint = 0.8f;
    private float backLocalPoint  = 1.0f;
    private float targetLocalY;

    public void Init()
    {
        transform.position = m_targetTr.position;
        targetLocalY = transform.localPosition.y;

        m_currentPos = transform.position;
        m_isInit = true;
    }


    private void Update()
    {
        if (!m_isInit)
            return;

        Tracking();
        if(isOverDeadRange())
        {
            PositionFix((Vector2)m_targetTr.position);
        }

        HandUpdate();
    }

    private void Tracking()
    {
        m_currentPos = Vector2.SmoothDamp(m_currentPos, m_targetTr.position, ref m_currentVelcotiy, m_smoothTime);
        transform.position = m_currentPos;
    }

    private void PositionFix(Vector2 targetPoint)
    {
        Vector2 pos = transform.position;
        Vector2 targetToMadDir = pos - targetPoint;
        targetToMadDir.Normalize();

        Vector2 trackingDeadPos = targetPoint + (targetToMadDir * m_deadRange);

        transform.position = trackingDeadPos;
    }


    private void HandUpdate()
    {
        m_model.flipX = m_madModel.flipX;

        if(m_isPivotRight && !m_model.flipX || !m_isPivotRight && m_model.flipX)
        {
            m_model.sortingOrder = 3001;
            float frontPoint = frontLocalPoint * Mathf.Sign(m_targetTr.localPosition.x);
            m_targetTr.localPosition = new Vector2(frontPoint, targetLocalY);
        }
        else
        {
            m_model.sortingOrder = 3003;
            float backPoint = backLocalPoint * Mathf.Sign(m_targetTr.localPosition.x);
            m_targetTr.localPosition = new Vector2(backPoint, targetLocalY);
        }

    }


    private bool isOverDeadRange()
    {
        return Vector2.Distance(transform.position, m_targetTr.position) > m_deadRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(m_targetTr.position, m_deadRange);
    }

    public void SetModelEnable(bool isEnable)
    {
        m_model.enabled = isEnable;
    }


}
