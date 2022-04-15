using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRopeState : I_MovementState
{
    private float cancleJumpEnterTime { set; get; }
    private float reboundJumpEnterTime { set; get; }
    private bool m_changeState = false;

    public void Enter(PlayerMovementManager manager)
    {
        manager.coyoteSystem.RestRopeCancleJumpTime();

        manager.isRopeCancleRebound = false;
        m_changeState = false;
    }

    public void Exit(PlayerMovementManager manager)
    {

    }

    public void FixedExcute(PlayerMovementManager manager)
    {
       // Debug.Log(m_changeState + "  " + manager.isRopeCancleRebound);
        if (m_changeState)
            return;

        if (!manager.isRopeCancleRebound && !manager.isRopeRebound)
        {
            if (Mathf.Abs(manager.inputPlayer.moveDir.x) >= 0.0f)
                manager.Run(1.0f, true);
        }
        else if(manager.isRopeCancleRebound)
        {
            manager.Resistance(manager.movementData.resistanceInAirAmount);
            //Debug.Log("CancleRun " + manager.inputPlayer.moveDir);
            manager.Run(1.0f, true);

        }

    }

    public void UpdateExcute(PlayerMovementManager manager)
    {
        TimeUpdate(manager.coyoteSystem);
        CancleJumpUpdate(manager);
        if (m_changeState)
            return;
        ReboundUpdate(manager);
        ChangeStaetUpdate(manager);
    }


    #region CancleJump
    private void CancleJumpUpdate(PlayerMovementManager manager)
    {
        if (manager.isRopeCancleRebound)
        {
            float cancleDurringTime = Time.time - cancleJumpEnterTime;
            if (cancleDurringTime >= manager.movementData.ropeCancleReboundTime)
            {
                manager.isRopeCancleRebound = false;
            }

        }

        if(CanCancleJump(manager))
        {
            int dir = 1;
            if (manager.lookDir.x < 0.0f)
                dir = -1;

            CancleReboundJump(Mathf.CeilToInt(dir), manager);
        }
    }

    private void CancleReboundJump(int dir, PlayerMovementManager manager)
    {
        Debug.Log("CancleReboundJump");
        manager.playerManager.animation.TriggerAir();
        manager.playerManager.shoulder.SetArmVisible(false);


        Vector2 force = manager.movementData.ropeCancleJumpForce;
        if (dir == -1)
            force.x = -force.x;

        if (Mathf.Sign(manager.rig2D.velocity.x) != Mathf.Sign(force.x))
            force.x -= manager.rig2D.velocity.x;

        if (manager.rig2D.velocity.y < 0.0f)
            force.y -= manager.rig2D.velocity.y;


        manager.rig2D.AddForce(force, ForceMode2D.Impulse);

        manager.coyoteSystem.RestRopeCancleJumpTime();

        manager.isRopeCancleRebound = true;
        cancleJumpEnterTime = Time.time;

    }

    private bool CanCancleJump(PlayerMovementManager manager)
    {
        return !manager.isRopeCancleRebound && manager.coyoteSystem.lastOnCancleRopeJump > 0.0f  && !manager.IsGrounded();
    }
    #endregion

    #region Rebound

    private void ReboundUpdate(PlayerMovementManager manager)
    {
        if(manager.isRopeRebound)
        {
            float duringReboundTime = Time.time - reboundJumpEnterTime;
            if (duringReboundTime >= manager.movementData.ropeReboundCoolTime)
                manager.isRopeRebound = false;
        }

        if(CanRebound(manager))
        {
            Rebound(manager.isRopeReboundDirRight, manager);
        }
    }

    private void Rebound(bool isRight, PlayerMovementManager manager)
    {
        Vector2 reboundDir = Vector2.right;
        if (!isRight)
            reboundDir *= -1.0f;

        float reboundPower = manager.movementData.ropeReboundPower;

        manager.rig2D.velocity = Vector2.zero;


        Debug.Log("Rebound");
        manager.rig2D.AddForce(reboundDir * reboundPower, ForceMode2D.Impulse);

        manager.isRopeRebound = true;
        reboundJumpEnterTime = Time.time;

        manager.coyoteSystem.RestRopeReboundTime();

    }

    private bool CanRebound(PlayerMovementManager manager)
    {
        return !manager.isRopeRebound && manager.coyoteSystem.lastOnRopeReboundTime > 0.0f;
    }

    #endregion

    private void TimeUpdate(CoyoteSystem coyoteSystem)
    {
        coyoteSystem.RopeCancleJumpCototeTime();
        coyoteSystem.RopeReboundCototeTime();
    }

    private void ChangeStaetUpdate(PlayerMovementManager manager)
    {
        if (manager.playerManager.grapplingShooter.isNoneGrappling && !manager.isRopeCancleRebound)
            ChangeStae(manager);
    }

    private void ChangeStae(PlayerMovementManager manager)
    {
        manager.playerManager.shoulder.SetArmVisible(false);
        if (!manager.IsGrounded())
            manager.currentState = PlayerMovementManager.State.Air;
        else
            manager.currentState = PlayerMovementManager.State.Ground;


        //shooter.movementManager.playerManager.shoulder.SetArmVisible(false);
        //if (shooter.movementManager.currentState == PlayerMovementManager.State.Ground)
        //    shooter.movementManager.playerManager.animation.TriggerLanding();
        //else if (shooter.movementManager.currentState == PlayerMovementManager.State.Air)
        //    shooter.movementManager.playerManager.animation.TriggerAir();
        m_changeState = true;
    }
}
