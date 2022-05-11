using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSensor : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_groundLayer;
    public LayerMask groundLayer
    {
        get { return m_groundLayer; }
    }

    [SerializeField]
    private Color m_gizomColor;
    [SerializeField]
    private float m_radius;


    public bool IsOverlap()
    {
        return Physics2D.OverlapCircle(transform.position, m_radius, m_groundLayer);
    }

    public Collider2D GetGroundCollider2D()
    {
        return Physics2D.OverlapCircle(transform.position, m_radius, m_groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = m_gizomColor;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }

}
