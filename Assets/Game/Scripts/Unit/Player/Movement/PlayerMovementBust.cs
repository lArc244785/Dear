using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBust : PlayerMovementBase
{
    [SerializeField]
    private float m_eventTime;


    private float m_enterTime;
    

    private float m_runTime;

    private Vector2 m_dir;
    private float m_power;


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

    //Bust를 실행하기전에 항상 Power값을 설정을 하고 사용해야됩니다.
    public void Bust(Vector2 dir)
    {

        m_enterTime = Time.time;
        m_runTime = 0.0f;

        m_dir = dir;

        movementManager.lookDirX = m_dir.x;

        rig2D.velocity = m_dir * power;

        UIManager.instance.inGameView.slingShot.isToggle = false;

        m_slingActionWait = false;
    }

    public float power
    {
        set
        {
            m_power = value;
        }
        get
        {
            return m_power;
        }
    }

}
