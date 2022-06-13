using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WaterMove : MonoBehaviour
{
    [SerializeField]
    private float m_speedX;

    private Transform[] m_childTrs;

    [Header("local Pivot")]
    [SerializeField]
    private float m_localLeftPivotX;
    [SerializeField]
    private float m_localRightPivotX;

    private float m_spriteWidth;



    // Start is called before the first frame update
    void Start()
    {
        m_childTrs = new Transform[transform.childCount];

        for (int i = 0; i < m_childTrs.Length; i++)
        {
            m_childTrs[i] = transform.GetChild(i);

        }

        m_spriteWidth = m_childTrs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;


    }

    // Update is called once per frame
    void Update()
    {
        WaterMoveUpdate();
        SpriteLinkeUpdate();
    }


    private void WaterMoveUpdate()
    {
        for (int i = 0; i < m_childTrs.Length; i++)
        {
            m_childTrs[i].Translate(Vector3.right * m_speedX * Time.deltaTime);
        }
    }

    private void SpriteLinkeUpdate()
    {
        for (int i = 0; i < m_childTrs.Length; i++)
        {
            if (m_childTrs[i].localPosition.x >= m_localRightPivotX)
            {
                int linkIndex = i + 1;
                if (linkIndex >= m_childTrs.Length)
                    linkIndex = 0;

                Vector3 loopPoint = m_childTrs[linkIndex].position;
                loopPoint.x -= m_spriteWidth;

                m_childTrs[i].position = new Vector3(loopPoint.x, loopPoint.y, loopPoint.z);
            }
        }
    }

    


}
