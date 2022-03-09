using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClibingSensor : MonoBehaviour
{
    [SerializeField]
    private Transform m_upTr;
    [SerializeField]
    private Transform m_downTr;
    [SerializeField]
    private float m_distance;
    private Vector2 m_dir;

    [SerializeField]
    private LayerMask m_layerMask;

    [SerializeField]
    private Color m_gizmosColor;



    public void Init()
    {

    }

    public void SensorOn(float xDir)
    {
        m_dir = Vector2.right;

        if (xDir < 0.0f)
        {
            m_dir *= -1.0f;
        }
        
    }

    private void OnDrawGizmos()
    {
        if (m_dir == Vector2.zero)
            m_dir = Vector2.right;

        Vector3 upEndPoint = m_upTr.position + (Vector3)(m_dir * m_distance);
        Vector3 downEndPoint = m_downTr.position + (Vector3)(m_dir * m_distance);

        Gizmos.color = m_gizmosColor;

        Gizmos.DrawLine(m_upTr.position, upEndPoint);
        Gizmos.DrawLine(m_downTr.position, downEndPoint);
    }

    public bool isClibingAble(float yDir)
    {
        bool isUp = true;
        if (yDir < 0.0f)
            isUp = false;


        return isWall(isUp);
    }

    private bool isWall(bool isUp)
    {
        Vector2 physicsPoint = m_upTr.position;
        if (!isUp)
            physicsPoint = m_downTr.position;

        return Physics2D.Raycast(physicsPoint, m_dir, m_distance, m_layerMask);
    }

}
