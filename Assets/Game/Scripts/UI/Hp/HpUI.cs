using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    private List<SingleHpUI> m_HP;
    [SerializeField]
    private Health m_playerHealth;

    [SerializeField]
    private GameObject m_hpPrefab;
    [SerializeField]
    private int size;
    [SerializeField]
    private Transform parent;
    
    private void OnEnable()
    {
        init();
    }

    private void init()
    {
        m_HP = new List<SingleHpUI>();
        for(int i = 0; i < m_playerHealth.maxhp; i++)
        {
            m_HP.Add(new SingleHpUI());
        }
        foreach (var singleHp in m_HP)
        {
            Instantiate(m_hpPrefab, parent);
        
        }
        size = m_HP.Count;
    }

    public void OnDamage(int dmg)
    {
        if (m_playerHealth.hp == 0) return;
        Destroy(transform.GetChild(0).gameObject);
    }
    public void OnHeal(int dmg)
    {
        Instantiate(m_hpPrefab, parent);
    }

}
