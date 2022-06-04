using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrapHandler : MonoBehaviour
{
    [SerializeField]
    private ProjectileTrap m_projectile;

    public void HandleFire()
    {
        m_projectile.Fire();
    }

}
