using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectControl : MonoBehaviour
{
    [SerializeField]
    private GameObject m_gameObject;
    [SerializeField]
    private bool m_startActive;

    private void Start()
    {
        Active(m_startActive);
    }


    public void Active(bool isActive)
    {
        m_gameObject.SetActive(isActive);
    }
}
