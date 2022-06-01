using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSerch : MonoBehaviour
{
    private bool m_isserch;
    public bool isserch
    {
        get
        {
            return m_isserch;
        }
        set
        {
            m_isserch = value;
        }
    }
    private Collider2D m_player;
  
    public Collider2D player
    {
        get
        {
            return m_player;
        }
    }
    [SerializeField]
    private Transform m_enemyTransform;

    [SerializeField]
    private bool m_isFollowModel;

    private void Update()
    {
        if (m_enemyTransform)
        {
            if (m_isFollowModel)
                transform.position = m_enemyTransform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
      
        if (collision.tag == "Player")
        {
            m_isserch = true;
            m_player = collision;
        }
    }
 

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_isserch = false;

    }
 

}
