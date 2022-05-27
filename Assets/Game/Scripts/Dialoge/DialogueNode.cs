using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class DialogueNode
{
    [TextArea(5,10)]
    public string data;

    public DialogueNode[] nextNode;
    public DialogueNode previousNode;


    public DialogueNode NextNode(int index)
    {
       if(index < 0 || index >= nextNode.Length)
            return null;

       return nextNode[index];
    }

    public bool CanNextNode()
    {
        return nextNode.Length > 0;
    }


}
