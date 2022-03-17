using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField]
    private WeaponData m_data;
    [SerializeField]
    private LayerMask m_hitlayerMask;


    public virtual void Attack() { }

    protected LayerMask hitLayerMask
    {
        get
        {
            return m_hitlayerMask;
        }
    }

    public virtual void AttackStart()
    {

    }

    public virtual void AttackEnd()
    {

    }


    protected WeaponData data
    {
        get
        {
            return m_data;
        }
    }
}
