using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_effectMultiplier;


    public void Move(Vector2 deltaMove)
    {
        Vector2 newPos = transform.position;
        newPos += deltaMove * m_effectMultiplier;


        transform.position = newPos;
    }
}
