using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySensor : MonoBehaviour
{

    [SerializeField]
    private LayerMask m_layer;
    [SerializeField]
    private bool m_isRightDir;

    private WallSensor m_manager;

    //Gizom Defalute Distance
    private float distance
    {
        get
        {
            return m_manager.distance;
        }
    }

    [SerializeField]
    private Color m_gizomColor;

    public void Init(WallSensor manager)
    {
        m_manager = manager;
    }

    public bool isWallGrounded
    {
        get
        {
            return Physics2D.Raycast((Vector2)transform.position, direction, distance, m_layer);
        }
    }

    private Vector2 direction
    {
        get
        {
            if (m_isRightDir)
                return Vector2.right;
            else
                return Vector2.left;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = m_gizomColor;
        float drawDistance = m_manager == null ? 1.0f : distance;
        Vector3 endPoint = (transform.position + (Vector3)(direction * drawDistance));
        Gizmos.DrawLine(transform.position, endPoint);
    }

}
