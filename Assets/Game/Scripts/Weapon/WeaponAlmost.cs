using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAlmost : WeaponBase
{
    [SerializeField]
    private Collider2D m_hitCollider;
    [SerializeField]
    private float m_exitTime;

    public override void Attack()
    {
        base.Attack();
        AttackStart();
        Invoke("AttackEnd", m_exitTime);
    }

    public override void AttackStart()
    {
        base.AttackStart();
        m_hitCollider.enabled = true;
    }

    public override void AttackEnd()
    {
        base.AttackEnd();
        m_hitCollider.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        int targetMask = 1 << collision.gameObject.layer;
        if((targetMask & hitLayerMask) != 0)
        {
            collision.GetComponent<Health>().Hit(data.damage, (Vector2)transform.position);
        }
    }

}
