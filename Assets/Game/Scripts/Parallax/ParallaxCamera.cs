using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    [SerializeField]
    private AreaManager m_areaManger;
    private AreaManager areaManager
    {
        get
        {
            return m_areaManger;
        }
    }

    private Vector2 m_oldPos;

    private void Start()
    {
        m_oldPos = (Vector2)transform.position;
    }


    private void LateUpdate()
    {
        if (areaManager == null)
            return;

        Vector2 currentPos = (Vector2)transform.position;
        if (m_oldPos == currentPos)
            return;

        Vector2 deltaMove = currentPos - m_oldPos;


        m_oldPos = currentPos;
    }
}
