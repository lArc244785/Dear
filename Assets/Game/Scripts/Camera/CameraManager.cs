using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    //씬 카메라 데이터

    private Dictionary<string, CinemachineVirtualCamera> m_sinceVirtualCameraDictionary = new Dictionary<string, CinemachineVirtualCamera>();

    private CinemachineVirtualCamera m_currentVirtualCamera;

    //카메라 Shake
    private CinemachineShake m_shake;

    private string m_mainCamId = "vCam_Main";

    private Camera m_camera;
    public Camera camera
    {
        get
        {
            return m_camera;
        }
    }


    public void Init()
    {
        m_shake = GetComponent<CinemachineShake>();
        m_camera = GameObject.Find("Camera").GetComponent<Camera>();

        SinceVCamInit();
        SetVitrualCamera(m_mainCamId);
    }


    private void SinceVCamInit()
    {
        Transform vcamTr = GameObject.Find("VCAMS").transform;
        for(int i = 0; i < vcamTr.childCount; i++)
        {
            Transform childTr = vcamTr.GetChild(i);
            string id = childTr.name;
            if (i == 0)
            {
                id = m_mainCamId;
                childTr.name = id;
            }
              
            CinemachineVirtualCamera vCam = childTr.GetComponent<CinemachineVirtualCamera>();
            vCam.Priority = 0;

            m_sinceVirtualCameraDictionary.Add(id, vCam);
        }
    }

    public void SetVitrualCamera(string id)
    {
        if (m_currentVirtualCamera != null)
        {
            m_currentVirtualCamera.Priority = 0;
        }

        m_currentVirtualCamera = m_sinceVirtualCameraDictionary[id];
        m_currentVirtualCamera.Priority = 100;
    }

    public void ResetVirtualMainCamera()
    {
        SetVitrualCamera(m_mainCamId);
    }


    public bool Shake(float intensity, float time)
    {
        return m_shake.Shake(
            m_currentVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(),
            intensity, time);
    }

}
