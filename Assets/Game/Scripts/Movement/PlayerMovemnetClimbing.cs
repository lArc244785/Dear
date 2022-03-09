using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemnetClimbing : MonoBehaviour, I_PlayerMovement
{
    private Rigidbody2D m_rig2D;
    private PlayerMovementManager m_movementManager;

    [SerializeField]
    private float m_clibingVelocity;

    [Header("Jump Option")]
    [SerializeField]
    private float m_jumpXVelocity;
    [SerializeField]
    private AnimationCurve m_jumpYDirCurve;
    [SerializeField]
    private float m_jumpYVelocityPower;
    [SerializeField]
    private float m_jumpEventTime;

    private float m_enterLookDirX;

    public void Enter()
    {
        m_enterLookDirX = m_movementManager.lookDirX;
        m_rig2D.gravityScale = .0f;
        m_movementManager.ClimbingSensorOn();
    }

    public void Init(PlayerMovementManager pmm)
    {
        m_movementManager = pmm;
        m_rig2D = m_movementManager.rig2D;


    }

    public void Movement()
    {
        Climbing();
    }

    private void Climbing()
    {
        if (m_movementManager.isClimbingWall)
        {
            m_rig2D.velocity = new Vector2(0.0f, m_clibingVelocity * m_movementManager.moveDir.y);
        }
        else
        {
            m_rig2D.velocity = Vector2.zero;
        }
    }

    public void Jump()
    {
        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        m_movementManager.isControl = false;
        m_movementManager.lookDirX = m_enterLookDirX * -1.0f;

        Vector2 jumpVelocity = new Vector2(m_jumpXVelocity * m_movementManager.lookDirX, .0f);

        float time = .0f;
        float yVelocity;

        float evaluate;

        while (!m_movementManager.isWall && time < m_jumpEventTime)
        {
            evaluate = time / m_jumpEventTime;
            yVelocity = m_jumpYDirCurve.Evaluate(evaluate) * m_jumpYVelocityPower;

            jumpVelocity.y = yVelocity;

            m_rig2D.velocity = jumpVelocity;

            time += Time.deltaTime;
            yield return null;
        }



        m_movementManager.currentType = PlayerMovementManager.MOVEMENT_TYPE.NOMAL;
        m_movementManager.isControl = true;
    }



}
