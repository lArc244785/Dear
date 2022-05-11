using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappingGunHandler : MonoBehaviour
{
    [SerializeField]
    private GrapplingGun m_grapplingGun;
    public GrapplingGun grapplingGun
    {
        get
        {
            return m_grapplingGun;
        }
    }
}
