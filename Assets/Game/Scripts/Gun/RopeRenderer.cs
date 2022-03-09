using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer m_LineRenderer;
    [SerializeField]
    private float m_width;

    [Header("RenderingPoints")]
    [SerializeField]
    private Transform m_firePoint;
    [SerializeField]
    private Transform m_hook;

    private void Start()
    {
        isDraw = false;
    }

    public bool isDraw
    {
        set
        {
            m_LineRenderer.enabled = value;
        }
        get
        {
            return m_LineRenderer.enabled;
        }
    }

    private float width
    {
        set
        {
            m_width = value;
            m_LineRenderer.SetWidth(m_width, m_width);
        }
    }

    private void LateUpdate()
    {
        if (isDraw)
            Draw();
    }



    public void Draw()
    {
        m_LineRenderer.SetPosition(0, m_firePoint.position);
        m_LineRenderer.SetPosition(1, m_hook.position);
    }



}
