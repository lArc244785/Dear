using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    #region animation
    [SerializeField]
    private Animator m_animation;
    public Animator animation
    {
        get { return m_animation; }
    }
    #endregion




    public void Init()
    {
    }

    public float movement
    {
        set
        {
            animation.SetFloat("Movement", value);
        }
        get
        {
            return animation.GetFloat("Movement");
        }
    }

    public int jumpCount
    {
        set
        {
            animation.SetInteger("JumpCount", value);
        }
        get
        {
            return animation.GetInteger("JumpCount");
        }
    }



    public void TriggerLanding()
    {
        ResetAllTrigger();
        SetTrigger("LandingA");
    }

    public void TriggerWall()
    {
        ResetAllTrigger();
        SetTrigger("Wall");
    }
    public void TriggerWallJump()
    {
        ResetAllTrigger();
        SetTrigger("WallJump");
    }

    public void TriggerJump()
    {
        ResetAllTrigger();
        SetTrigger("Jump");
    }

    public void TriggerRopeFire()
    {
        ResetAllTrigger();
        SetTrigger("RopeFire");
    }

    public void TriggerRopeMove()
    {
        ResetAllTrigger();
        SetTrigger("RopeMove");
    }

    
    public void TriggerAir()
    {
        ResetTrigger("LandingA");
        SetTrigger("Air");
    }

    public void TriggerGroundPound()
    {
        ResetAllTrigger();
        SetTrigger("GroundPound");
    }


    private void SetTrigger(string id)
    {
        animation.SetTrigger(id);
    }

    private void ResetTrigger(string id)
    {
        animation.ResetTrigger(id);
    }


    public void RopeToAirAnimation()
    {
        TriggerAir();
    }



    private void ResetAllTrigger()
    {
        foreach(var param in animation.parameters)
        {
            if(param.type == AnimatorControllerParameterType.Trigger)
                ResetTrigger(param.name);
        }
    }


}
 