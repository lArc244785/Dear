using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_DialogueEvent 
{
    public IEnumerator DialogueEvent(DialogueManager dialogueManager);
}
