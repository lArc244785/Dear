using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private GameObject m_roomStartPoint;
    [SerializeField]
    private PolygonCollider2D m_cameraConfiner;

    public Vector2 startPoint
    {
        get
        {
            return (Vector2)m_roomStartPoint.transform.position;
        }
    }

    public PolygonCollider2D cameraConfiner
    {
        get
        {
            return m_cameraConfiner;
        }
    }


}
