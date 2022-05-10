using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_groundLayer;
    [SerializeField]
    private Color m_gizomColor;
    [SerializeField]
    private float m_radius;


    public bool IsGrounded()
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