using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBgmChange : InteractionBase
{
    [SerializeField]
    private float m_enterChangeIndex;
    
    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        GameManager.instance.stageManager.stageBgm.SetParamater(m_enterChangeIndex);

    }
}
