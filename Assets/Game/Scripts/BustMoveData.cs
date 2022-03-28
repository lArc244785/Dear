using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BustMoveData", menuName = "Scriptable Object/BustMoveData", order = 1)]
public class BustMoveData : ScriptableObject
{
    [SerializeField]
    private Vector2 m_power;
    [SerializeField]
    private AnimationCurve m_bustGraph;



    public Vector2 power
    {
        get
        {
            return m_power;
        }
    }

    public AnimationCurve bustGraph
    {
        get
        {
            return m_bustGraph;
        }
    }
}
