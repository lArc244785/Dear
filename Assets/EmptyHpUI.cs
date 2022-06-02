using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyHpUI : MonoBehaviour
{
    private List<SingleHpUI> m_HP;
    [SerializeField]
    private GameObject m_emptyHealthPrefab;

    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Health m_playerHealth;


    public void init()
    {

       
        m_playerHealth = GameObject.Find("Player").GetComponent<Health>();
       
        m_HP = new List<SingleHpUI>();
        for (int i = 0; i < m_playerHealth.maxhp; i++)
        {
            m_HP.Add(new SingleHpUI());
        }
        foreach (var singleHp in m_HP)
        {
            Instantiate(m_emptyHealthPrefab, parent);
        }
    }
}
