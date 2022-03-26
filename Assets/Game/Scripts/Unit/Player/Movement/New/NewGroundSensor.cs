using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGroundSensor : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_groundLayer;
    [SerializeField]
    private Color m_gizomColor;
    [SerializeField]
    private float m_radius;


    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(transform.position, m_radius, m_groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = m_gizomColor;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }

}
