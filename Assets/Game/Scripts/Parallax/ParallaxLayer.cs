using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_effectMultiplier;

    private Vector2 m_textureUnitHalfSize;
    private Vector2 textureUnitHalfSize
    {
        get
        {
            return m_textureUnitHalfSize;
        }
    }



    private Vector2 GetSpriteRightUpPos(Vector2 center)
    {
        return center + textureUnitHalfSize;
    }

    private Vector2 GetSpriteLeftDownPos(Vector2 center)
    {
        return center - textureUnitHalfSize;
    }



    public void Init()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;


        float halfSizeX = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
        halfSizeX *= 0.5f;

        float halfSizeY = (texture.height / sprite.pixelsPerUnit) * transform.localScale.y;
        halfSizeY *= 0.5f;

        m_textureUnitHalfSize = new Vector2(halfSizeX, halfSizeY);
    }





    public void Move(Vector2 deltaMove, Area area)
    {
        Vector2 newPos = transform.position;
        Vector2 newPosRightUpPos;
        Vector2 newPosLeftDownPos;

        newPos += deltaMove * m_effectMultiplier;
        newPosRightUpPos = GetSpriteRightUpPos(newPos);
        newPosLeftDownPos = GetSpriteLeftDownPos(newPos);

        //Sprite이미지 양 끝 위치 값 x가 넘어가는 경우 현재 위치 유지
        if ((deltaMove.x > 0.0f && area.IsOverRangeX(newPosRightUpPos.x)) || (deltaMove.x < 0.0f && area.IsOverRangeX(newPosLeftDownPos.x)))
        {
            newPos.x = transform.position.x;
        }

        //Sprite이미지 양 끝 위치 값 y가 넘어가는 경우 현재 위치 유지
        if ((deltaMove.y > 0.0f && area.IsOverRangeY(newPosRightUpPos.y)) || (deltaMove.y < 0.0f && area.IsOverRangeX(newPosLeftDownPos.y)))
        {
            newPos.y = transform.position.y;
        }

        transform.position = newPos;


    }
}
