using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;

public class InteractionSpriteFadeInOut : InteractionBase
{
    [SerializeField]
    private Tilemap m_tileMap;
    [SerializeField]
    private float m_duration;


    [SerializeField]
    private Color m_doColor;

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        StartCoroutine(AlphaZeroCoroutnie());
    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
    }

    private IEnumerator AlphaZeroCoroutnie()
    {
        m_tileMap.color = Color.white;


        float t = 0;
        while(t < 1.0f)
        {
            Color lerp = Color.Lerp(Color.white, m_doColor, t);
            t += Time.deltaTime / m_duration;
            m_tileMap.color = lerp;
            yield return null;
        }
        m_tileMap.color = m_doColor;

    }

}
