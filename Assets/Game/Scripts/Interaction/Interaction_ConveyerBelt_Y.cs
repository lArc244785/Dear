using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_ConveyerBelt_Y : InteractionBase
{
    [SerializeField]
    private float m_maxSpeed;
    [SerializeField]
    private float m_dirY;


    private Rigidbody2D m_unitRig2D;
    private PlayerMovementManager m_movementManager;


    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        m_movementManager = collision.GetComponent<PlayerMovementManager>();
        m_unitRig2D = m_movementManager.rig2D;
        m_movementManager.isWallGripInteraction = true;

    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        m_movementManager.isWallGripInteraction = false;
        m_movementManager = null;
        m_unitRig2D = null;
    }

    private void Update()
    {
        if (m_movementManager == null)
            return;

        if(m_dirY > 0.0f && (m_movementManager.wallSensor.UpSensorGrounded()|| m_movementManager.inputPlayer.moveDir.y < 0.0f  )||
            m_dirY < 0.0f && (m_movementManager.wallSensor.DownSensorGrounded() || m_movementManager.inputPlayer.moveDir.y > 0.0f))
        {
            m_unitRig2D.AddForce((Vector2.up*m_dirY) * m_maxSpeed);
        }
        else if(!m_movementManager.isWallJump)
        {
            m_unitRig2D.velocity = Vector2.zero;
        }


    }

}
