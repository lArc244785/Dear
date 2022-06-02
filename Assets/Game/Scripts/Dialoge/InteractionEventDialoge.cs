using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEventDialoge : InteractionEventBase
{
    [SerializeField]
    private DialogueData data;


    public override void InteractionEvent()
    {
        DialogueManager.instance.StartDialogue(data);
    }
}
