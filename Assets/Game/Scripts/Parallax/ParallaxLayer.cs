using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;
    public void Move(Vector3 delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos -= delta * -parallaxFactor;

        transform.localPosition = newPos;
    }
}