using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_size;
    private Vector2 size
    {
        get { return m_size; }
    }

    [SerializeField]
    private Color m_gizmosColor;
    private Color gizmosColor
    {
        get
        {
            return m_gizmosColor;
        }
    }

    private Vector2 m_rightUpPos;
    private Vector2 rightUpPos
    {
        set
        {
            m_rightUpPos = value;
        }
        get
        {
            return m_rightUpPos;
        }
    }

    private Vector2 m_leftDownPos;
    private Vector2 leftDownPos
    {
        set
        {
            m_leftDownPos = value;
        }
        get
        {
            return m_leftDownPos;
        }
    }

    private Vector2 m_center;
    private Vector2 center
    {
        set
        {
            m_center = value;
        }
        get
        {
            return m_center;
        }
    }



    private void Start()
    {
        Init();
    }

    public void Init()
    {
        center = (Vector2)transform.position;
        Vector2 halfSize = size / 2;

        rightUpPos = center + halfSize;
        leftDownPos = center - halfSize;

        Debug.Log(gameObject.name + "Right UP : " + rightUpPos + " Left Down : " + leftDownPos);


    }

    public bool IsOverRangeX(float x)
    {
        return IsOverRightRange(x) || IsOverLeftRange(x);
    }

    public bool IsOverRangeY(float y)
    {
        return IsOverUpRange(y) || IsOverDownRange(y);
    }


    private bool IsOverRightRange(float x)
    {
        return x > rightUpPos.x;
    }

    private bool IsOverLeftRange(float x)
    {
        return x < leftDownPos.x;
    }


    private bool IsOverUpRange(float y)
    {
        return y > rightUpPos.y;
    }

    private bool IsOverDownRange(float y)
    {
        return y < leftDownPos.y;
    }





    private void OnDrawGizmos()
    {
        Gizmos.color = m_gizmosColor;
        Gizmos.DrawWireCube(transform.position, (Vector3)size);
    }
}
