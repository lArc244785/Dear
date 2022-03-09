using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShoulderMovement : MonoBehaviour
{
    private bool m_bBodyFix;

    private enum Type
    {
        E_MOUSE, E_POSITION
    }


    [SerializeField]
    private Transform m_shoulder;

    private Vector2 m_dir;


    private Vector2 m_nomalVector = Vector2.right;

    private Type m_currentType;


    public void init()
    {
        m_currentType = Type.E_MOUSE;
    }


    public void UpdateProcess()
    {
        if(m_currentType == Type.E_MOUSE)
        {
            CalcurationRotation(InputManager.Instance.inGameMousePosition2D);
        }
        else if(m_currentType == Type.E_POSITION)
        {
            CalcurationRotation(m_lookPosition);
        }

        UpdateShoulder();
    }




    public Vector2 dir
    {
        set
        {
            m_dir = value;
        }
        get
        {
            return m_dir;
        }
    }

    public bool isBodyFix
    {
        set
        {
            m_bBodyFix = value;
        }
        get
        {
            return m_bBodyFix;
        }
    }


    [SerializeField]
    private Vector2 m_lookPosition;


    private float updateRotionAngle;
    public void UpdateShoulder()
    {

        float dot = Vector2.Dot(m_dir, m_nomalVector);

        int nReverse = 1;

        if (m_dir.y < 0)
            nReverse *= -1;

        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg * nReverse;
        updateRotionAngle = angle;

        m_shoulder.rotation = Quaternion.Euler(0.0f, 0.0f, updateRotionAngle);

        if (isBodyFix)
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, updateRotionAngle);
    }


    private void CalcurationRotation(Vector2 lookPoint)
    {
        Vector2 lookPointDir = lookPoint - (Vector2)transform.position;
        lookPointDir.Normalize();

        dir = lookPointDir;
    }

    public void setLookPosition(Vector2 pos)
    {
        m_lookPosition = pos;
        m_currentType = Type.E_POSITION;

        CalcurationRotation(m_lookPosition);
        UpdateShoulder();
    }


    public void SetMouse()
    {
        m_currentType = Type.E_MOUSE;
    }


}
