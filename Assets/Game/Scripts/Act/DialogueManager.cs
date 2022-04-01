using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialoguData inputData;

    private DialogueNode m_currentNode;

    private void Start()
    {
        StartDialogue(inputData.node);
    }


    public void StartDialogue(DialogueNode rootNode)
    {
        m_currentNode = rootNode;
        DataDecoding(m_currentNode.data);
    }

    private void DataDecoding(string sentecesData)
    {
        //string name = sentecesData.Split("<Name>", "</Name>");
    }


}
