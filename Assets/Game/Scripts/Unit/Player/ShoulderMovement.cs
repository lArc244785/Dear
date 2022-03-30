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

    private Vector2 m_targetPostion;


    private Type m_currentType;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        m_currentType = Type.E_MOUSE;
    }

    private void Update()
    {

        if (m_currentType == Type.E_MOUSE)
        {
            targetPostion = InputManager.instance.inGameMousePosition2D;
        }
        else if (m_currentType == Type.E_POSITION)
        {
            targetPostion = m_lookPosition;
        }

        UpdateShoulder();
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

    public void UpdateShoulder()
    {

        float updateRotionAngle = Utility.GetRotaionAngleByTargetPosition(transform.position, targetPostion, .0f);

        m_shoulder.rotation = Quaternion.Euler(0.0f, 0.0f, updateRotionAngle);

        if (isBodyFix)
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, updateRotionAngle);

    }


    public void setLookPosition(Vector2 pos)
    {
        m_lookPosition = pos;
        m_currentType = Type.E_POSITION;

        UpdateShoulder();
    }


    public void SetMouse()
    {
        m_currentType = Type.E_MOUSE;
    }

    public Vector2 targetPostion
    {
        set
        {
            m_targetPostion = value;
        }
        get
        {
            return m_targetPostion;
        }
    }
}
