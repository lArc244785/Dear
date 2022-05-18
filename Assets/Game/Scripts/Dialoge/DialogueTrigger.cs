using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : InteractionBase
{
    [SerializeField]
    private DialoguData data;

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        DialogueManager.instance.StartDialogue(data.node);

    }
}
