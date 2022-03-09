using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRope : MonoBehaviour, I_PlayerMovement
{
    [Header("General Reference")]
    [SerializeField]
    private SpringJoint2D m_springJoint2D;

    [Header("Rebound Option")]
    [SerializeField]
    private float m_reboundPower;
    private Vector2 m_reboundDir;
    [SerializeField]
    private float m_reboundCollTime;
    private bool m_reboundAble;

    [Header("CancleReboundEvent Option")]
    [SerializeField]
    private float m_reboundVelocityX;
    [SerializeField]
    private float m_reboundEventTime;

    private PlayerMovementManager m_movementManager;

    private Rigidbody2D m_rig2D;

    public void Init(PlayerMovementManager pmm)
    {
        m_movementManager = pmm;
        m_rig2D = pmm.rig2D;
        isReboundAble = true;
    }

    public void Enter()
    {

    }



    public void Movement()
    {

    }


    public void Rebound(bool isRight)
    {
        m_reboundDir = Vector2.right;
        if (!isRight)
            m_reboundDir *= -1.0f;

        m_rig2D.velocity = Vector2.zero;

        m_rig2D.AddForce(m_reboundDir * m_reboundPower);

        isReboundAble = false;
    }



    public void CancleReboundEvent()
    {
        m_movementManager.isControl = false;

        float xVelocity = m_reboundVelocityX;
        if (m_rig2D.velocity.x < 0.0f)
            xVelocity *= -1.0f;

        m_rig2D.velocity = new Vector2(xVelocity, m_rig2D.velocity.y);
        StartCoroutine(ReboundEventCoroutine());
    }

    private IEnumerator ReboundEventCoroutine()
    {
        float fTime = .0f;

        while (!m_movementManager.isWall && fTime <= m_reboundEventTime)
        {
            fTime += Time.deltaTime;
            yield return null;
        }

        m_movementManager.currentType = PlayerMovementManager.MOVEMENT_TYPE.NOMAL;
        m_movementManager.isControl = true;
    }

    public SpringJoint2D SpringJoint2D
    {
        get
        {
            return m_springJoint2D;
        }
    }

    public bool isReboundAble
    {
        set
        {
            m_reboundAble = value;
            if (!m_reboundAble)
                StartCoroutine(reboundCollTimeCoroutine());
        }
        get
        {
            return m_reboundAble;
        }
    }

    private IEnumerator reboundCollTimeCoroutine()
    {
        yield return new WaitForSeconds(m_reboundCollTime);
        m_reboundAble = true;
    }



}
