using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNomal : PlayerMovementBase
{

    [Header("Move Parameter")]
    [SerializeField]
    private float m_moveSpeed;

    [Header("Jump Parameter")]
    [SerializeField]
    private float m_coolJumpTime;
    [SerializeField]
    private float m_jumpSpeed;
    [SerializeField]
    private int m_maxJumpCount;
    private int m_currentJump;


    public override void Init(PlayerMovementManager pmm)
    {
        base.Init(pmm);
        m_currentJump = 0;
    }
    public override void Enter()
    {
        base.Enter();
        rig2D.gravityScale = movementManager.defaultGravityScale;
    }

    public override void Movement()
    {
        base.Movement();
        Move();
    }



    public void Move()
    {
        float moveVelocityX = .0f;

        if (!movementManager.isWall)
        {
            moveVelocityX = movementManager.moveDir.x * m_moveSpeed;
        }

        rig2D.velocity = new Vector2(moveVelocityX, rig2D.velocity.y);

    }

    public void Jump()
    {
        if (!movementManager.unitBase.isJump)
            return;
        if (JumpCount == m_maxJumpCount)
            return;

        float jumpVelocity = m_jumpSpeed;

        if (m_currentJump == 0)
            StartCoroutine(CoolJumpCoroutine(jumpVelocity));
        else
            rig2D.velocity = new Vector2(rig2D.velocity.x, jumpVelocity);

        m_currentJump++;
    }

    private IEnumerator CoolJumpCoroutine(float jumpVelocity)
    {
        //float time = .0f;
        //while(InputManager.instance.isJumpKeyPush && time < m_coolJumpTime)
        //{
        //    rig2D.velocity = new Vector2(rig2D.velocity.x, jumpVelocity);
        //    time += Time.deltaTime;
        //    yield return null;
        //}
        yield return null;
    }



    public void JumpReset()
    {
        JumpCount = 0;
    }

    public int JumpCount
    {
        set
        {
            m_currentJump = Mathf.Clamp(value, 0, m_maxJumpCount);

        }
        get
        {
            return m_currentJump;
        }
    }





}
