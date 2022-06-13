using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator m_animation;

    public float movement
    {
        set
        {
            m_animation.SetFloat("Movement", value);
        }
        get
        {
            return m_animation.GetFloat("Movement");
        }
    }
    public float ropeMovement
    {
        set
        {
            m_animation.SetFloat("RopeMovement", value);
        }
        get
        {
            return m_animation.GetFloat("RopeMovement");
        }
    }
    public int jumpCount
    {
        set
        {
           m_animation.SetInteger("JumpCount", value);
        }
        get
        {
            return m_animation.GetInteger("JumpCount");
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
        Debug.Log("Air Ani");

        ResetTrigger("LandingA");
        SetTrigger("Air");
    }






    private void SetTrigger(string id)
    {
        m_animation.SetTrigger(id);
    }

    private void ResetTrigger(string id)
    {
        m_animation.ResetTrigger(id);
    }


    public Animator animation
    {
        get { return m_animation; }
    }
}
