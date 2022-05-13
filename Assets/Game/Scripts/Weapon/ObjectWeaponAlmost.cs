using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWeaponAlmost : WeaponBase
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




    private void OnTriggerStay2D(Collider2D collision)
    {
        int targetMask = 1 << collision.gameObject.layer;
        if ((targetMask & hitLayerMask) != 0)
        {
            collision.GetComponent<UnitBase>().OnHitObject(gameObject, data.damage);
        }
    }

}
