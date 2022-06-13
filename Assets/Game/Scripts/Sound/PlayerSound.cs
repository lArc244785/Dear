using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("General Sound")]
    [SerializeField]
    private FMODUnity.EventReference m_hitEvent;
    [SerializeField]
    private FMODUnity.EventReference m_deathEvent;


    [Header("Ground Sound")]
    #region Foot Step
    [SerializeField]
    private FMODUnity.EventReference m_footStepEvent;
    private FMOD.Studio.EventInstance m_footStepInstance;
    private FMOD.Studio.PARAMETER_ID m_footStepID;
    [SerializeField]
    private float m_footStepPlayTick;
    public float footStepPlayTick { get { return m_footStepPlayTick; } }
    #endregion

    #region Jump
    [Header("Jump Sound")]
    [SerializeField]
    private FMODUnity.EventReference m_jumpEvent;
    [SerializeField]
    private FMODUnity.EventReference m_jumpVoiceEvent;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float m_jumpVoiceRandomRange;
    #endregion

    #region Landing
    [SerializeField]
    private FMODUnity.EventReference m_landingEvent;
    private FMOD.Studio.EventInstance m_landingInstance;
    private FMOD.Studio.PARAMETER_ID m_landingID;
    #endregion

    #region Wall
    [Header("Wall Sound")]
    [SerializeField]
    private FMODUnity.EventReference m_wallJumpLeftEvent;
    [SerializeField]
    private FMODUnity.EventReference m_wallJumpRightEvent;
    [SerializeField]
    private FMODUnity.EventReference m_wallGripEvent;
    #endregion

    #region rope
    [Header("Rope")]
    [SerializeField]
    private FMODUnity.EventReference m_ropeShoot;
    [SerializeField]
    private FMODUnity.EventReference m_ropeMove;
    private FMOD.Studio.EventInstance m_ropeInstance;
    private FMOD.Studio.PARAMETER_ID m_parameterIndex;
    private string m_ropeIndexID = "rope_index";
    #endregion

    #region groundPound
    [Header("GroundPound")]
    [SerializeField]
    private FMODUnity.EventReference m_groundPoundEvent;
    #endregion





    private string m_surfaceIndex = "surface_index";
    private string surfaceIndex { get { return m_surfaceIndex; } }

    private CircleSensor m_groundSensor;

    public void Init(UnitPlayer player)
    {
        #region FootStop
        m_footStepInstance = FMODUnity.RuntimeManager.CreateInstance(m_footStepEvent);
        SoundManager.instance.GetID(m_footStepInstance, surfaceIndex, out m_footStepID);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_footStepInstance, player.transform, player.rig2D);
        #endregion

        #region Landing
        m_landingInstance = FMODUnity.RuntimeManager.CreateInstance(m_landingEvent);
        SoundManager.instance.GetID(m_landingInstance, surfaceIndex, out m_landingID);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_landingInstance, player.transform, player.rig2D);
        #endregion

        m_groundSensor = player.movementManager.groundSensor;

        m_ropeInstance = FMODUnity.RuntimeManager.CreateInstance(m_ropeMove);
        SoundManager.instance.GetID(m_ropeInstance, m_ropeIndexID, out m_parameterIndex);

        


        //footStepLoop = false;
        //m_footStepLoopCoroutine = FootStepCoroutine(footStepPlayTick);
        //StartCoroutine(m_footStepLoopCoroutine);
    }


    #region General Sound Funtion
    public void Hit()
    {
        SoundManager.instance.SoundOneShot(m_hitEvent);
        SoundManager.instance.bgm.SetParamaterHitIndexID(1.0f);
    }

    public void Death()
    {
        SoundManager.instance.SoundOneShot(m_deathEvent);   
    }
    #endregion

    #region Ground Sound Funtion
    public void FootStep(float index)
    {
        if (SoundManager.instance.GetPlayState(m_footStepInstance) == FMOD.Studio.PLAYBACK_STATE.PLAYING)
            SoundManager.instance.SoundStop(m_footStepInstance);


        m_footStepInstance.setParameterByID(m_footStepID, index);
        SoundManager.instance.SoundPlay(m_footStepInstance);
    }

    public void Jump()
    {
        SoundManager.instance.SoundOneShot(m_jumpEvent);
        if (Random.Range(0.0f, 1.0f) < m_jumpVoiceRandomRange)
            SoundManager.instance.SoundOneShot(m_jumpVoiceEvent);
    }

    public void Landing(float index)
    {
        if (SoundManager.instance.GetPlayState(m_landingInstance) == FMOD.Studio.PLAYBACK_STATE.PLAYING)
            SoundManager.instance.SoundStop(m_landingInstance);


        m_landingInstance.setParameterByID(m_footStepID, index);
        SoundManager.instance.SoundPlay(m_landingInstance);
    }

    //private IEnumerator m_footStepLoopCoroutine;
    //private bool m_footStepLoop;
    //public bool footStepLoop { set { m_footStepLoop = value; } get { return m_footStepLoop; } }


    //private IEnumerator FootStepCoroutine(float tickTime)
    //{
    //    float value;

    //    while (true)
    //    {
    //        if (footStepLoop)
    //        {
    //            Collider2D groundCollider = m_groundSensor.GetGroundCollider2D();
    //            value = 0.0f;

    //            if (groundCollider != null)
    //            {
    //                if (groundCollider.tag == "Forest")
    //                {
    //                    value = 1.0f;
    //                }
    //                else if (groundCollider.tag == "Asphalt")
    //                {
    //                    value = 2.0f;
    //                }
    //            }


    //            FootStep(value);
    //        }
    //        yield return new WaitForSeconds(tickTime);
    //    }

    //}



    #endregion

    #region Wall Sound Funtion
    public void WallJumpLeft()
    {
        SoundManager.instance.SoundOneShot(m_wallJumpLeftEvent);
    }

    public void WallJumpRight()
    {
        SoundManager.instance.SoundOneShot(m_wallJumpRightEvent);
    }

    public void WallGrip()
    {
        SoundManager.instance.SoundOneShot(m_wallGripEvent);
    }
    #endregion

    #region Rope Sound Funtion
    public void RopeShoot()
    {
        SoundManager.instance.SoundOneShot(m_ropeShoot);
    }

    public void RopeMove()
    {
        SoundManager.instance.SoundStop(m_ropeInstance);
        m_ropeInstance.setParameterByID(m_parameterIndex, 0.0f);
        SoundManager.instance.SoundPlay(m_ropeInstance);
    }

    public void RopeJump()
    {
        m_ropeInstance.setParameterByID(m_parameterIndex, 1.0f);
    }

    #endregion

    #region GroundPound Sound Funtion
    public void GroundPound()
    {
        SoundManager.instance.SoundOneShot(m_groundPoundEvent);
    }
    #endregion


}
