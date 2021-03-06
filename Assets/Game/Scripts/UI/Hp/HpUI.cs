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

    private bool m_initCnt;

    public bool initCnt
    {
        get
        {
            return m_initCnt;
        }
        set
        {
            m_initCnt = value;
        }
    }


    public void Awake()
    {
        m_initCnt = false;
    }

    public void init()
    {
        m_playerHealth = GameObject.Find("Player").GetComponent<Health>();
        parent = GameObject.Find("Hpcontainer").transform;
        if (m_initCnt == true) return;
        m_HP = new List<SingleHpUI>();
        for(int i = 0; i < m_playerHealth.hp; i++)
        {
            m_HP.Add(new SingleHpUI());
        }
        foreach (var singleHp in m_HP)
        {
            Instantiate(m_hpPrefab, parent);
        
        }
        size = m_HP.Count;
    }

    private void Update()
    {
       
    }
    public void OnDamage(int dmg)
    {
        Debug.Log("체력 닳음");
        if (m_playerHealth.hp == 0) return;
        for (int i = 0; i < dmg; i++)
        {
           
            Destroy(parent.GetChild(0).gameObject);
        }
    }
    public void OnHeal(int dmg)
    {
        Instantiate(m_hpPrefab, parent);
    }

}
