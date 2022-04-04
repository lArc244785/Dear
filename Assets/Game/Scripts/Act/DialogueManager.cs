using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialoguData inputData;

    private DialogueNode m_currentNode;

    private char[] m_filterDatas = { '<', '>' };
    private char m_commandHeader = '$';

    private delegate void DialogueEvent(ref string[] data, ref int index);

    private Dictionary<string, DialogueEvent> m_dialogueEventDictionary;


    private void Start()
    {
        StartDialogue(inputData.node);
    }

    private void DialogueEvnetInit()
    {
        m_dialogueEventDictionary = new Dictionary<string, DialogueEvent>();

        m_dialogueEventDictionary.Add("Name", NameEvent);

    }



    public void StartDialogue(DialogueNode rootNode)
    {
        m_currentNode = rootNode;
        string [] splitDatas = rootNode.data.Split(m_filterDatas);

        int index = 0;
        while(index < splitDatas.Length)
        {
            if(IsCommand(splitDatas[index]))
            {
                string commandID = GetCommandID(splitDatas[index]);
                m_dialogueEventDictionary[commandID](ref splitDatas, ref index);
            }

            index++;
        }

    }

   private bool IsCommand(string data)
    {
        return data[0] == m_commandHeader;
    }

    private string GetCommandID(string commandData)
    {
        string commandID = commandData.Split(m_commandHeader)[0];
        return commandID;
    }

    private void NameEvent(ref string[] data, ref int index)
    {
        string nameData ="";
        while(data[index] != "$/Name")
        {
            nameData += data[index];
            index++;
        }
        Debug.Log("Name Data" + nameData);

    }

    private void SenteceEvent(ref string[] data, ref int index)
    {

    }

}
