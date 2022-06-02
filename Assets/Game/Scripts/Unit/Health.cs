using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField]
    private int m_maxHP;
    [SerializeField]
    private HpUI m_hpui;
    public int maxhp
    {
        get
        {
            return m_maxHP;
        }
    }

    [SerializeField]
    private int m_hp;
    public int hp
    {
        private set
        {
            m_hp = Mathf.Clamp(value, 0, m_maxHP);

        }
        get
        {
            return m_hp;
        }
    } 

    public void Init()
    {
      
        hp = m_maxHP;
    }

    public void OnRecovery(int recovery)
    {
        hp = recovery;

        m_hpui.OnHeal(recovery);
    }

    public void OnDamage(int damage)
    {
        m_hpui.OnDamage(damage);
        hp -= damage;
    }

}
