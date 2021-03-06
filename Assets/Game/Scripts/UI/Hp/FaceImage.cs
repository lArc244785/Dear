using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceImage : MonoBehaviour
{
    [SerializeField]
    private Sprite m_highHPimage;
    [SerializeField]
    private Sprite m_midleHPimage;
    [SerializeField]
    private Sprite m_lowHPimage;

    private Health m_playerHp;

    [SerializeField]
    private Image faceImage;

    private void Awake()
    {
        m_playerHp = GameObject.Find("Player").GetComponent<Health>();
    }

    private void Update()
    {
        if (m_playerHp.hp >= 4) faceImage.sprite = m_highHPimage;
        else if (m_playerHp.hp < 2)faceImage.sprite = m_lowHPimage;
        else faceImage.sprite = m_midleHPimage;
    }


}
