using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    private ParallaxLayer[] m_parallaxLayers;

    private Area m_area;
    private Area area
    {
        get
        {
            return m_area;
        }
    }

    public void Init(Area area)
    {
        m_area = area;

        m_parallaxLayers = new ParallaxLayer[transform.childCount];

        for (int i = 0; i < m_parallaxLayers.Length; i++)
        {
            m_parallaxLayers[i] = transform.GetChild(i).GetComponent<ParallaxLayer>();
            m_parallaxLayers[i].Init();
        }

    }



    public void Move(Vector2 deltaMove)
    {
        foreach (ParallaxLayer layer in m_parallaxLayers)
            layer.Move(deltaMove, m_area);
    }

}
