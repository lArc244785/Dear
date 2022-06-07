using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathProduction : MonoBehaviour
{
    private GameObject m_DeathBackGround;

    public void Init()
    {
        m_DeathBackGround = transform.GetChild(0).gameObject;
        SetDeathBackGroundActive(false);
    }

    public void SetDeathBackGroundActive(bool isActive)
    {
        m_DeathBackGround.active = isActive;
    }


}
