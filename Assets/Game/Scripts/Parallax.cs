using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Transform m_camTr;
    private Vector2 m_lastCamPosition;
    [SerializeField]
    private Vector2 m_parallaxMultiplier;

    private float textureUnitSizeX;
    private float textureUnitSizeY;


    // Start is called before the first frame update
    void Start()
    {
        m_lastCamPosition = m_camTr.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
        textureUnitSizeY = (texture.height / sprite.pixelsPerUnit) * transform.localScale.y;

        Debug.Log(gameObject.name +"  " +textureUnitSizeX + "  " + textureUnitSizeY);
    }

    private void LateUpdate()
    {

        Vector2 deltaMovement = (Vector2)m_camTr.position - m_lastCamPosition;
        Vector2 parallaxMove = deltaMovement * m_parallaxMultiplier;
        transform.position += (Vector3)parallaxMove;
        m_lastCamPosition = m_camTr.position;

        //if (Mathf.Abs(m_camTr.position.x - transform.position.x) >= (textureUnitSizeX * 0.5f))
        //{
        //    float offsetPositionX = (m_camTr.position.x - transform.position.x) % textureUnitSizeX;
        //    transform.position = new Vector3(m_camTr.position.x + offsetPositionX, transform.position.y);

        //}
    }
}
