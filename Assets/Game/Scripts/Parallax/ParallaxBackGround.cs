using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    private ParallaxLayer[] m_parallaxLayers;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        m_parallaxLayers = new ParallaxLayer[transform.childCount];

        for (int i = 0; i < m_parallaxLayers.Length; i++)
            m_parallaxLayers[i] = transform.GetChild(i).GetComponent<ParallaxLayer>();
    }

    public void Move(Vector2 deltaMove)
    {
        foreach (ParallaxLayer layer in m_parallaxLayers)
            layer.Move(deltaMove);
    }

}
