using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : UnitBase
{

    [SerializeField]
    private float m_hitDuringTime;
    private float hitDuringTime
    {
        get
        {
            return m_hitDuringTime;
        }
    }
    [SerializeField]
    private float m_ghostDuringTime;

    private float ghostDuringTime
    {
        get
        {
            return m_ghostDuringTime;
        }
    }


    private StateImfectTween m_hitImfect;
    private StateImfectTween hitImfect
    {
        get
        {
            return m_hitImfect;
        }
    }


    


    public override void Init()
    {
        base.Init();
        Debug.Log("init");

      
    }
    protected override void ComponentInit()
    {
        base.ComponentInit();
        m_hitImfect = GetComponent<StateImfectTween>();

        m_hitImfect.Init(model);
    }
   
    protected override void HitUniqueEventObject(GameObject attackObject)
    {
        base.HitUniqueEventObject(attackObject);
        if (model == null) return;
    }

    protected override void HitUniqueEventUnit(UnitBase attackUnit)
    {
        base.HitUniqueEventUnit(attackUnit);

        if (model == null) return;
    }
    protected override void HitHp(int damage)
    {
        base.HitHp(damage);
        health.OnDamage(damage);
    }
    public override void OnHitObject(GameObject attackObject, int damage)
    {
        base.OnHitObject(attackObject, damage);
        Debug.Log("총에 맞음");
        if (health.hp > 0)
            hitImfect.HitImfect(hitDuringTime, ghostDuringTime);
    }

}
