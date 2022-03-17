using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSlingShot : PlayerMovementBase
{
    [SerializeField]
    private float m_eventTime;


    private float m_enterTime;
    
    [SerializeField]
    private float m_power;

    private float m_runTime;

    private Vector2 m_dir;

    private bool m_slingActionWait = true;


    public override void Init(PlayerMovementManager pmm)
    {
        base.Init(pmm);
    }

    public override void Movement()
    {
        base.Movement();
        if (m_slingActionWait)
            return;

        m_runTime = Time.time - m_enterTime;


        if (movementManager.isWall || m_runTime > m_eventTime)
        {
            movementManager.currentType = PlayerMovementManager.MOVEMENT_TYPE.NOMAL;
            return;
        }



    }

    public override void Enter()
    {
        base.Enter();
        rig2D.velocity = Vector2.zero;
        rig2D.gravityScale = .0f;
        m_slingActionWait = true;
        UIManager.instance.inGameView.slingShot.isToggle = true;

    }


    public void Shoot(Vector2 dir)
    {
        m_enterTime = Time.time;
        m_runTime = 0.0f;

        m_dir = dir;

        movementManager.lookDirX = m_dir.x;

        rig2D.velocity = m_dir * m_power;

        UIManager.instance.inGameView.slingShot.isToggle = false;

        m_slingActionWait = false;
    }



}
