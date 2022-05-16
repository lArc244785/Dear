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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           
            m_isserch = true;
        }
    }
 

    private void OnTriggerExit2D(Collider2D collision)
    {

        m_isserch = false;
    }
 

}
