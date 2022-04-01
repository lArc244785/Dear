using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int m_maxHP;
    private int m_hp;
    [SerializeField]
    private SpriteRenderer m_model;

    [SerializeField]
    private Color m_color;

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
        m_model.DOColor(m_color, 0.1f).SetLoops(10).OnComplete(() => { m_model.color = Color.white; });

    }

    public void Healing(int healing)
    {
        Debug.Log("Healing");
        hp += healing;
    }
     
}
