using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField]
    private Area[] m_areas;

    [SerializeField]
    private int m_index;
    public int index
    {
        get
        {
            return m_index;
        }
    }

    public Area currentArea
    {
        get
        {
            return m_areas[index];
        }
    }


}
