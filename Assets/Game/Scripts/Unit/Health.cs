using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int m_maxHP;


    private int m_hp;


    public void Init()
    {
        hp = m_maxHP;

    }

    private int hp
    {
        set
        {
            m_hp = Mathf.Clamp(value, 0, m_maxHP);
        }
        get
        {
            return m_hp;
        }
    }

   public void Hit(int damage)
    {
        Debug.Log("Hit");
        hp -= damage;
    }

    public void Healing(int healing)
    {
        Debug.Log("Healing");
        hp += healing;
    }
     
}
