using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNomal : PlayerMovementBase
{

    [Header("NomalMovement Parameter")]
    [SerializeField]
    private float m_moveSpeed;



    [SerializeField]
    private float m_jumpPower;
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

        if (!movementManager.isWall)
        {
            rig2D.velocity = new Vector2(movementManager.moveDir.x * m_moveSpeed, rig2D.velocity.y);
        }

    }

    public void Jump()
    {
        if (m_currentJump >= m_maxJumpCount)
            return;

        rig2D.AddForce(Vector2.up * m_jumpPower);

        m_currentJump++;
    }

    public void JumpReset()
    {
        m_currentJump = 0;
    }







}
