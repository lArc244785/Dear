using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    [SerializeField]
    private ParallaxBackGround m_backGround;

    private Vector2 m_oldPos;

    private void Start()
    {
        m_oldPos = (Vector2)transform.position;
    }


    private void LateUpdate()
    {
        if (m_backGround == null)
            return;

        Vector2 currentPos = (Vector2)transform.position;
        if (m_oldPos == currentPos)
            return;

        Vector2 deltaMove = currentPos - m_oldPos;
        m_backGround.Move(deltaMove);

        m_oldPos = currentPos;
    }
}
