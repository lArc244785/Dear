using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraPrioritySwitching : MonoBehaviour
{
    private CinemachineVirtualCamera m_mainVirtualCamera;
    private CinemachineVirtualCamera mainVirtualCamera { get { return m_mainVirtualCamera; } }

    private int m_defalutPriority;
    private int defalutPriority { get { return m_defalutPriority; } }


    private void Start()
    {
        m_mainVirtualCamera = GameObject.Find("vcam_Main").GetComponent<CinemachineVirtualCamera>();
        m_defalutPriority = m_mainVirtualCamera.Priority;
    }

    public void SetPriority(int priority)
    {
        mainVirtualCamera.Priority = priority;
    }

    public void SetMainPriority()
    {
        mainVirtualCamera.Priority = defalutPriority;
    }


}
