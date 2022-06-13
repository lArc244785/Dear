using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    private bool m_isPlayerExit;
    public bool isPlayerExit
    {
        get
        {
            return m_isPlayerExit;
        }
    }
    private void Awake()
    {
        m_isPlayerExit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        m_isPlayerExit = true;
    }
}
