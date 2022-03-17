using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemnetClimbing : PlayerMovementBase
{

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

    public override void Init(PlayerMovementManager pmm)
    {
        base.Init(pmm);
    }

    public override void Movement()
    {
        base.Movement();
        Climbing();
    }


    public override void Enter()
    {
        base.Enter();

        m_enterLookDirX = movementManager.lookDirX;
        rig2D.gravityScale = .0f;
        movementManager.ClimbingSensorOn();
    }

    private void Climbing()
    {
        if (movementManager.isClimbingWall)
        {
            rig2D.velocity = new Vector2(0.0f, m_clibingVelocity * movementManager.moveDir.y);
        }
        else
        {
            rig2D.velocity = Vector2.zero;
        }
    }

    public void Jump()
    {
        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        movementManager.unitBase.isControl = false;
        movementManager.lookDirX = m_enterLookDirX * -1.0f;

        Vector2 jumpVelocity = new Vector2(m_jumpXVelocity * movementManager.lookDirX, .0f);

        float time = .0f;
        float yVelocity;

        float evaluate;

        while (!movementManager.isWall && time < m_jumpEventTime)
        {
            evaluate = time / m_jumpEventTime;
            yVelocity = m_jumpYDirCurve.Evaluate(evaluate) * m_jumpYVelocityPower;

            jumpVelocity.y = yVelocity;

            rig2D.velocity = jumpVelocity;

            time += Time.deltaTime;
            yield return null;
        }



        movementManager.currentType = PlayerMovementManager.MOVEMENT_TYPE.NOMAL;
        movementManager.unitBase.isControl = true;
    }



}
