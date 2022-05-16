using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : WeaponBase
{
    [SerializeField]
    private GameObject m_projectile;
    private GameObject projectile
    {
        get
        {
            return m_projectile;
        }
    }

    [SerializeField]
    private Transform m_firePoint;
    private Transform friePoint
    {
        get
        {
            return m_firePoint;
        }
    }

    public override void Attack()
    {
        base.Attack();
    }

    private void Fire()
    {
        Vector2 dir = (Vector2)(friePoint.position - transform.position);
        dir.Normalize();

        

    }

}
