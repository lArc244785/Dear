using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirSensor : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_boxSize;
    [SerializeField]
    private LayerMask m_layerMask;

    private float m_distance;

    public void Init()
    {
        m_distance = transform.localPosition.x;
    }


    public float distance
    {
        set
        {
            transform.localPosition = new Vector3(value, 0.0f, 0.0f);
        }
        get
        {
            return transform.localPosition.x;
        }
    }

    public Collider2D getWall(float xDir)
    {

        if (xDir > .0f)
            distance = m_distance;
        else if (xDir < .0f)
            distance = m_distance * -1.0f;

        RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, m_boxSize, .0f, Vector2.zero, .0f, m_layerMask);

        if (hit2D.collider != null)
            return hit2D.collider;

        return null;
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(transform.position, m_boxSize);
    }


}
