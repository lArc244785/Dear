using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleToon<T> : MonoBehaviour where T : SingleToon<T>
{
    private static T m_instance;
    public static T Instance
    {
        get
        {
            return m_instance;
        }
    }

    protected virtual bool init()
    {
        if(Instance != null)
        {
            Debug.LogError("overlap SingleToon " + this.ToString());
            Destroy(gameObject);
            return false;
        }

        m_instance = this as T;
        DontDestroyOnLoad(gameObject);
        return true;
    }



}
