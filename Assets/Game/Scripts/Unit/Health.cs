using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField]
    private int m_maxHP;
    private int m_hp;

    [Header("Hit Ghost")]
    [SerializeField]
    private float m_hitDuringTime;
    [SerializeField]
    private float m_ghostDuringTime;

    [Header("GhostLayer")]
    [SerializeField]
    private int m_ghostLayer;

    private int m_oldLayer;

    private UnitBase m_unit;

    public void Init(UnitBase unit)
    {
        hp = m_maxHP;
        m_unit = unit;
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

   public void Hit(int damage , Vector2 hitPoint)
    {
        Debug.Log("Hit");
        hp -= damage;
       
        m_unit.HitEvent(hitPoint);
        m_unit.stateImfect.HitImfect(m_hitDuringTime,m_ghostDuringTime);

        float ghostDuringTime = m_hitDuringTime + m_ghostDuringTime;
        GhostMode(ghostDuringTime);


    }

    public void Healing(int healing)
    {
        Debug.Log("Healing");
        hp += healing;
    }

    private void GhostMode(float duringTime)
    {
        m_oldLayer = gameObject.layer;
        gameObject.layer = m_ghostLayer;
        Invoke("GhostModeOff", duringTime);
    }
    private void GhostModeOff()
    {
        gameObject.layer = m_oldLayer;
    }
     
}
