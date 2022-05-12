using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shoulder : MonoBehaviour
{

    private enum LookType
    {
        Mouse, Position
    }


    [SerializeField]
    private Transform m_shoulderTr;
    [SerializeField]
    private GameObject m_hand;
    [SerializeField]
    private GrapplingShooter m_shooter;
    [SerializeField]
    private GameObject m_armModel;


    private Vector2 m_targetPostion;


    private LookType m_currentType;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        m_currentType = LookType.Mouse;
        SetArmVisible(false);
    }

    private void Update()
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        

        if (m_currentType == LookType.Mouse)
        {
            targetPostion = InputManager.instance.inGameMousePosition2D;
        }
        else if (m_currentType == LookType.Position)
        {
            targetPostion = m_lookPosition;
        }

        UpdateShoulder();
    }










    [SerializeField]
    private Vector2 m_lookPosition;

    public void UpdateShoulder()
    {

        float updateRotionAngle = Utility.GetRotaionAngleByTargetPosition(transform.position, targetPostion, .0f);

        m_shoulderTr.rotation = Quaternion.Euler(0.0f, 0.0f, updateRotionAngle);

    }


    public void setLookPosition(Vector2 pos)
    {
        m_lookPosition = pos;
        m_currentType = LookType.Position;

        UpdateShoulder();
    }


    public void SetMouse()
    {
        m_currentType = LookType.Mouse;
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

    public void SetArmVisible(bool isVisible)
    {
        m_armModel.SetActive(isVisible);
        
    }
}
