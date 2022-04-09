using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : SingleToon<DialogueManager>
{
    public DialoguData inputData;

    private DialogueNode m_currentNode;

    #region Command Parameter
    private char[] m_filterDatas = { '<', '>'};
    private char m_commandHeader = '$';
    #endregion

    private Dictionary<string, DialogueCoroutine> m_dialogueEventDictionary;
    private delegate IEnumerator DialogueCoroutine();

    private Stack<IEnumerator> m_runDialogueCoroutineStack = new Stack<IEnumerator>();

    #region DialogueEvent Paramater
    private string[] m_datas;
    private int m_dataIndex;

    public bool isCanNext { get; private set; }
    #endregion

    #region Reference
    [Header("Reference")]
    private UI_Dialogue m_uiDialogue;
    #endregion

    #region Text
    [SerializeField]
    [Range(0f, 1f)]
    private float m_textDuration;
    #endregion


    protected override bool Init()
    {
        bool returnValue =base.Init();
        if (!returnValue)
            return false;


        DialogueEventInit();
        m_uiDialogue = UIManager.instance.dialogue;

        return true;
    }




    private void Start()
    {
        Init();
        StartDialogue(inputData.node);

    }


    private void DialogueEventInit()
    {
        m_dialogueEventDictionary = new Dictionary<string, DialogueCoroutine>();

        m_dialogueEventDictionary.Add("Name", GetNameCoroutine);
        m_dialogueEventDictionary.Add("Sentences", GetSentenesCoroutine);
        m_dialogueEventDictionary.Add("UI_ON", GetDialogueUIOnCoroutine);
        m_dialogueEventDictionary.Add("UI_OFF", GetDialogueUIOffCoroutine);
    }


    public void StartDialogue(DialogueNode rootNode)
    {
        GameManager.instance.ChangeStateUISetting();

        m_currentNode = rootNode;


        StartCoroutine(DialogueEventCoroutine());
    }

    public void NextDialogue()
    {
        if (!isCanNext)
            return;

        if(!m_currentNode.CanNextNode())
        {
            m_uiDialogue.Toggle(false);
            GameManager.instance.ChangeStateChangeGamePlaying();
            return;
        }

        m_currentNode = m_currentNode.nextNode[0];


        StartCoroutine(DialogueEventCoroutine());
    }


   private bool IsCommand(string data)
    {
        if(data.Length == 0)
            return false;

        return data[0] == m_commandHeader;
    }

    private string GetCommandID(string commandData)
    {
        string commandID = commandData.Substring(1);
        return commandID;
    }

    private IEnumerator DialogueEventCoroutine()
    {
        isCanNext = false;
        m_datas = m_currentNode.data.Split(m_filterDatas, System.StringSplitOptions.RemoveEmptyEntries);
        m_dataIndex = 0;

        m_uiDialogue.nameText = null;
        m_uiDialogue.sentencesText = null;
        m_uiDialogue.LeftImageOff();
        m_uiDialogue.RightImageOff();

        while(m_runDialogueCoroutineStack.Count > 0)
        {
            StopCoroutine(m_runDialogueCoroutineStack.Pop());
        }


        while (m_dataIndex < m_datas.Length)
        {
            if(IsCommand(m_datas[m_dataIndex]))
            {
                yield return StartCoroutine(StartCommandCoroutine());
            }

            m_dataIndex++;
        }
        isCanNext = true;


    }

    private IEnumerator StartCommandCoroutine()
    {
        string commandID = GetCommandID(m_datas[m_dataIndex]);
        m_dataIndex++;

        m_runDialogueCoroutineStack.Push(m_dialogueEventDictionary[commandID]());
        yield return StartCoroutine(m_runDialogueCoroutineStack.Peek()); 
    }

    #region NameCoroutine

    private IEnumerator GetNameCoroutine()
    {
        return NameCoroutine();
    }

    private IEnumerator NameCoroutine()
    {
        string loopEnd = "/Name";
        string data = "";

       while (m_dataIndex < m_datas.Length && m_datas[m_dataIndex] != loopEnd)
        {
            data += m_datas[m_dataIndex];

            m_dataIndex++;
        }

       m_uiDialogue.nameText = data;

        yield return null;
    }
    #endregion

    #region SentenesCoroutine
    private IEnumerator GetSentenesCoroutine()
    {
        return SentenesCoroutine();
    }

    private IEnumerator SentenesCoroutine()
    {
        string loopEnd = "/Sentences";
        string data = "";



        while (m_dataIndex < m_datas.Length && m_datas[m_dataIndex] != loopEnd)
        {
            if (IsCommand(m_datas[m_dataIndex]))
            {
                yield return StartCoroutine(StartCommandCoroutine());
            }
            else
            {
                yield return StartCoroutine(TextDurlationCoroutine(m_datas[m_dataIndex]));
            }
            m_dataIndex++;
        }

        //해당 데이터 내용으로 글자 이벤트를 진행

        Debug.Log("Sentences : " + data);
    }
    #endregion


    #region DialogueUIOnCoroutine
    private IEnumerator GetDialogueUIOnCoroutine()
    {
        return DialogueUIOnCoroutine();
    }

    private IEnumerator DialogueUIOnCoroutine()
    {
        m_uiDialogue.Toggle(true);



        yield return null;
    }
    #endregion


    #region DialogueUIOffCoroutine

    private IEnumerator GetDialogueUIOffCoroutine()
    {
        return DialogueUIOffCoroutine();
    }

    private IEnumerator DialogueUIOffCoroutine()
    {
        m_uiDialogue.Toggle(false);
        yield return null;
    }
    #endregion

    #region EventCoroutine

    private IEnumerator GetEventCoroutine()
    {
        return EventCoroutine();
    }

    private IEnumerator EventCoroutine()
    {
        yield return null;   
    }

    #endregion

    #region ImageChange
    //private IEnumerator ImageSettingCoroutine()
    //{

    //}
    #endregion

    private IEnumerator TextDurlationCoroutine(string data)
    {
        foreach (char letter in data.ToCharArray())
        {
            m_uiDialogue.sentencesText += letter;
            yield return new WaitForSeconds(m_textDuration);
        }
    }


}
