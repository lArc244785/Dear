using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageTrigger : InteractionBase
{
    [SerializeField]
    private int m_nextStageIndex;
    private int nextStageIndex
    {
        get
        {
            return m_nextStageIndex;
        }
    }


    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        GameManager.instance.NextState(nextStageIndex);
    }
}
