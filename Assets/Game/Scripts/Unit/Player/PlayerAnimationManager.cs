using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    #region animation
    private Animator m_animation;
    public Animator animation
    {
        get { return m_animation; }
    }
    #endregion


    private Shoulder m_shoulder;
    private Shoulder shoulder { get { return m_shoulder; } }

    public void Init(Animator ani, Shoulder shoulder)
    {
        m_animation = ani;
        m_shoulder = shoulder;
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
    public float ropeMovement
    {
        set
        {
            animation.SetFloat("RopeMovement", value);
        }
        get
        {
            return animation.GetFloat("RopeMovement");
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
        ResetTrigger("Air");
        SetTrigger("LandingA");
    }

    public void TriggerWall()
    {
        SetTrigger("Wall");
        
    }

    public void TriggerJump()
    {
        ResetTrigger("Wall");
        SetTrigger("Jump");
    }

    public void TriggerRope()
    {
        Debug.Log("Rope Ani");
        ResetTrigger("Air");
        ResetTrigger("Wall");
        ResetTrigger("Jump");
        SetTrigger("Rope");
    }

    public void TriggerAir()
    {
        //Debug.Log("Air Ani");

        ResetTrigger("LandingA");
        SetTrigger("Air");
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
        shoulder.SetArmVisible(false);
        TriggerAir();
    }

    public void RopeAnimation()
    {
        shoulder.SetArmVisible(true);
        TriggerRope();
    }
}
