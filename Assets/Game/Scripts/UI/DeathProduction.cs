using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathProduction : MonoBehaviour
{
    private GameObject m_DeathBackGround;
    private Canvas m_canvas;

    [SerializeField]
    private FMODUnity.EventReference m_deathEvent;

    public void Init()
    {
        m_canvas = GetComponent<Canvas>();
        m_DeathBackGround = transform.GetChild(0).gameObject;
        SetDeathBackGroundActive(false);
    }

    public void SetDeathBackGroundActive(bool isActive)
    {
        m_DeathBackGround.active = isActive;
    }


    public void RenderCameraSettting(Camera camera)
    {
        m_canvas.worldCamera = camera;
    }

    public void SoundPlay()
    {
        SoundManager.instance.SoundOneShot(m_deathEvent);
    }
    

}
