using UnityEngine;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;

public class MovementStateAir : I_MovementState
{
    //private float lastGroundPoundLandingTime { set; get; }


    private enum GroundPoundType
    {
        None, Ready, Pound
    }

    private GroundPoundType currentGroundPoundType { set; get; }

    public void Enter(PlayerMovementManager movementManager)
    {
        if (!movementManager.isJump)
            movementManager.player.animationManager.TriggerAir();
        currentGroundPoundType = GroundPoundType.None;
    }

    public void Exit(PlayerMovementManager movementManager)
    {
        movementManager.coyoteSystem.ResetJumpExitTime();
    }

    public void FixedExcute(PlayerMovementManager movementManager)
    {
        movementManager.Resistance(movementManager.movementData.resistanceInAirAmount);
        movementManager.Run(1.0f, true);
    }

    public void UpdateExcute(PlayerMovementManager movementManager)
    {
        TimeUpdate(movementManager.coyoteSystem);
        GracityUpdate(movementManager);

        movementManager.TrunUpdate();

        JumpUpdate(movementManager);

        GroundPoundUpdate(movementManager);

        ChangeState(movementManager);
    }

    private void TimeUpdate(CoyoteSystem coyoteSystem)
    {
        coyoteSystem.GroundCoyoteTime();
        coyoteSystem.JumpCoyoteTime();
        coyoteSystem.JumpCutCoyoteTime();
        coyoteSystem.WallCoyoteTime();
    }

    private void GroundPoundUpdate(PlayerMovementManager movementManager)
    {
        if(currentGroundPoundType == GroundPoundType.None)
        {
            if (CanGroundPound(movementManager))
            {
                GroundPoundReady(movementManager);
            }
        }
        else if(currentGroundPoundType == GroundPoundType.Pound)
        {
            Collider2D poundLapCollider2D = movementManager.groundPoundSensor.GetGroundCollider2D();
            if (poundLapCollider2D != null)
            {
                Debug.Log("Ground Pound End");



                if(poundLapCollider2D.gameObject.layer == LayerMask.NameToLayer("GroundPoundInteractionObject"))
                {
                    GroundPoundInteraction(movementManager, poundLapCollider2D);
                }
                GroundPoundLanding(movementManager);


            }
        }



    }

    private void GroundPoundLanding(PlayerMovementManager movementManager)
    {
        movementManager.player.particleManager.GroundPoundEffect();
        movementManager.player.GhostFrozen(movementManager.movementData.groundPoundLandingTime);
        currentGroundPoundType = GroundPoundType.None;
    }


    //private void OnLastGroundPoundLadingTime(MovementData data)
    //{
    //    lastGroundPoundLandingTime = data.groundPoundLandingTime;
    //}

    //private void OnLastGroundPoundLadingCoyoteTime()
    //{
    //    lastGroundPoundLandingTime -= Time.deltaTime;
    //}


    private void GroundPoundReady(PlayerMovementManager movementManager)
    {
        movementManager.player.sound.GroundPound();

        movementManager.player.SetGhostLayer();
        movementManager.isJump = false;
        movementManager.player.inputPlayer.SetControl(false);
        movementManager.player.rig2D.velocity = Vector2.zero;

        currentGroundPoundType = GroundPoundType.Ready;


        float readyMoveY = CalculationGroundPoundReadyMoveY(
            movementManager.player.GetModelColliderTop(),
            movementManager.movementData.groundPoundMoveY,
            movementManager.groundSensor.groundLayer);

        

        Vector3[] moveYPath = movementManager.CalculationGroundPoundPath(readyMoveY);

        Path path = new Path(PathType.Linear, moveYPath, 1);

        movementManager.player.transform.DOPath(
            path,
            movementManager.movementData.groundPoundReadyTime).OnComplete(() => 
            {
                movementManager.player.rig2D.velocity = Vector2.down * movementManager.movementData.groundPoundPower;
                currentGroundPoundType = GroundPoundType.Pound;
            }).Play();

    }

    private float CalculationGroundPoundReadyMoveY(Vector2 colliderTopPos, float moveY, LayerMask groundLayer)
    {
        float readyMoveY = 0.0f;

        RaycastHit2D hit2D = Physics2D.Raycast(colliderTopPos, Vector2.up, moveY, groundLayer);
        if(hit2D.collider != null)
        {
            readyMoveY = hit2D.distance;
        }
        else
        {
            readyMoveY = moveY;
        }
        return readyMoveY;
    }




    private bool CanGroundPound(PlayerMovementManager movementManager)
    {
        return movementManager.player.toolManager.IsPassiveToolAcheive(ToolManager.PassiveToolType.GroundPound) &&
            currentGroundPoundType == GroundPoundType.None &&
            movementManager.player.inputPlayer.moveDir.y < 0.0f;
    }


    private void ChangeState(PlayerMovementManager movementManager)
    {
        if (currentGroundPoundType != GroundPoundType.None)
            return;

        if (movementManager.IsWallGrouned())
            movementManager.currentState = PlayerMovementManager.State.Wall;
        else if (!movementManager.isJump && movementManager.IsGrounded())
        {
            movementManager.currentState = PlayerMovementManager.State.Ground;

            movementManager.player.particleManager.LandingEffect();
            Collider2D groundCollider = movementManager.groundSensor.GetGroundCollider2D();

            float value = 0.0f;
            if (groundCollider.tag == "Forest")
            {
                value = 1.0f;
            }
            else if (groundCollider.tag == "Asphalt")
            {
                value = 2.0f;
            }

            movementManager.player.sound.Landing(value);


        }

    }


    private void GracityUpdate(PlayerMovementManager movementManager)
    {
        if (movementManager.player.rig2D.velocity.y < 0.0f)
            movementManager.SetGravity(movementManager.movementData.gravityScale * movementManager.movementData.fallGravityMult);
        else
            movementManager.SetGravity(movementManager.movementData.gravityScale);
    }

    private void JumpUpdate(PlayerMovementManager movementManager)
    {
        if (movementManager.isJump && movementManager.player.rig2D.velocity.y <= 0.0f)
        {
            movementManager.isJump = false;
            movementManager.isInteractionJump = false;
        }

        if (CanJumpCut(movementManager))
        {
            movementManager.JumpCut();
        }

        if (CanAirJump(movementManager))
        {
            movementManager.Jump(movementManager.movementData.airJumpForce);
            movementManager.jumpCount++;

        }

    }

    private bool CanAirJump(PlayerMovementManager movementManager)
    {
        return !movementManager.isJump && !movementManager.isOnInteractionJumpObject &&
            movementManager.jumpCount < movementManager.movementData.maxJumpCount &&
            movementManager.coyoteSystem.lastJumpEnterTime > 0.0f;
    }

    private bool CanJumpCut(PlayerMovementManager movementManager)
    {
        return
            movementManager.coyoteSystem.lastJumpExitTime > 0.0f &&
            movementManager.jumpCount == 1 &&
            movementManager.isJump &&
            !movementManager.isInteractionJump;
    }

    private void GroundPoundInteraction(PlayerMovementManager movementManager, Collider2D coll)
    {
        string tag = coll.gameObject.tag;

        if(tag == "Spring")
        {
            coll.GetComponent<InteractionSpring>().SuperJump();
 
        }
        else if(tag == "GroundPoundBroken")
        {
            coll.GetComponent<InteractionGroundPoundBroken>().BrokenTrigger();
        }
        
    }


}
