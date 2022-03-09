using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNomal : MonoBehaviour, I_PlayerMovement
{

    [Header("NomalMovement Parameter")]
    [SerializeField]
    private float m_moveSpeed;



    [SerializeField]
    private float m_jumpPower;
    [SerializeField]
    private int m_maxJumpCount;
    private int m_currentJump;




    private Rigidbody2D m_rig2D;


    private PlayerMovementManager m_playerMovementManager;


    public void Init(PlayerMovementManager pmm)
    {
        m_playerMovementManager = pmm;

        m_currentJump = 0;
        m_rig2D = pmm.rig2D;


    }

    public void Enter()
    {
        m_rig2D.gravityScale = m_playerMovementManager.defaultGravityScale;
    }

    public void Movement()
    {
        Move();
    }



    public void Move()
    {

        if (!m_playerMovementManager.isWall)
        {
            m_rig2D.velocity = new Vector2(m_playerMovementManager.moveDir.x * m_moveSpeed, m_rig2D.velocity.y);
        }

    }

    public void Jump()
    {
        if (m_currentJump >= m_maxJumpCount)
            return;

        m_rig2D.AddForce(Vector2.up * m_jumpPower);

        m_currentJump++;
    }

    public void JumpReset()
    {
        m_currentJump = 0;
    }







}
