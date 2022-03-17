using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRope : PlayerMovementBase
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




    public override void Init(PlayerMovementManager pmm)
    {
        base.Init(pmm);
        isReboundAble = true;
    }

    public override void Movement()
    {
        base.Movement();
    }


    public override void Enter()
    {
        base.Enter();
    }


    public void Rebound(bool isRight)
    {
        m_reboundDir = Vector2.right;
        if (!isRight)
            m_reboundDir *= -1.0f;

        rig2D.velocity = Vector2.zero;

        rig2D.AddForce(m_reboundDir * m_reboundPower);

        isReboundAble = false;
    }



    public void CancleReboundEvent()
    {
        movementManager.unitBase.isControl = false;

        float xVelocity = m_reboundVelocityX;
        if (rig2D.velocity.x < 0.0f)
            xVelocity *= -1.0f;

        rig2D.velocity = new Vector2(xVelocity, rig2D.velocity.y);
        StartCoroutine(ReboundEventCoroutine());
    }

    private IEnumerator ReboundEventCoroutine()
    {
        float fTime = .0f;

        while (!movementManager.isWall && fTime <= m_reboundEventTime)
        {
            fTime += Time.deltaTime;
            yield return null;
        }

        movementManager.currentType = PlayerMovementManager.MOVEMENT_TYPE.NOMAL;
        movementManager.unitBase.isControl = true;
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
