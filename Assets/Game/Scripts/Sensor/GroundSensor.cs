using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundSensor : MonoBehaviour
{
    [SerializeField]
    private Transform m_startTr;
    [SerializeField]
    private Transform m_endTr;
    private float m_distance;

    [SerializeField]
    private LayerMask m_groundLayerMask;

    private bool m_newIsGround;
    private bool m_oldIsGround;

    [SerializeField]
    private UnityEvent m_randingEvent;

    public void Init()
    {
        m_distance = Vector2.Distance(m_startTr.position, m_endTr.position);
        UpdateSensor();
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_startTr.position, m_endTr.position);
    }

    private void Update()
    {
        UpdateSensor();
    }


    private void UpdateSensor()
    {
        m_newIsGround = Physics2D.Raycast(m_startTr.position, Vector2.down, m_distance, m_groundLayerMask);

        if (m_newIsGround && !m_oldIsGround)
            m_randingEvent.Invoke();


        m_oldIsGround = m_newIsGround;
    }


}
