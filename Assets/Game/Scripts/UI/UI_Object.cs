using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Object : MonoBehaviour
{
    private bool m_toggle;


    public virtual void Init()
    {

    }


    public bool isToggle
    {
        set
        {
            m_toggle = value;
            gameObject.SetActive(m_toggle);
        }
        get
        {
            return m_toggle;
        }
    }

    protected virtual void Draw()
    {

    }


    private void Update()
    {
        if (!isToggle)
            return;

        Draw();
    }
}
