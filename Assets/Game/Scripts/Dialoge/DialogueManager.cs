using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : SingleToon<DialogueManager>
{

    private DialogueData m_dialogueData;
    private int m_dialogueIndex;

    #region Command Parameter
    private char[] m_filterDatas = { '<', '>'};
    private char m_commandHeader = '$';
    #endregion

    #region EventDictionary
    private Dictionary<string, DialogueCoroutine> m_dialogueEventDictionary;
    private Dictionary<string, DialogueCoroutine> dialogueEventDictionary
    {
        get
        {
            return m_dialogueEventDictionary;
        }
    }
    #endregion

    private delegate IEnumerator DialogueCoroutine();


    #region DialogueEvent Paramater
    private string[] m_datas;
    private int m_dataIndex;

    public bool isCanNext { get; private set; }
    #endregion

    #region Reference
    private UI_DialogueView m_uiDialogue;
    private UI_DialogueView uiDialoge
    {
        get
        {
            return m_uiDialogue;
        }
    }
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
        m_uiDialogue = UIManager.instance.dialogueView;

        return true;
    }




    private void Start()
    {
        Init();
        //StartDialogue(inputData.node);

    }


    private void DialogueEventInit()
    {
        m_dialogueEventDictionary = new Dictionary<string, DialogueCoroutine>();

        dialogueEventDictionary.Add("Name", GetNameCoroutine);
        dialogueEventDictionary.Add("Sentences", GetSentenesCoroutine);
        dialogueEventDictionary.Add("UI_ON", GetDialogueUIOnCoroutine);
        dialogueEventDictionary.Add("UI_OFF", GetDialogueUIOffCoroutine);
        dialogueEventDictionary.Add("UI_LeftImage", GetSetLeftImageCoroutine);
        dialogueEventDictionary.Add("UI_RightImage", GetSetRightImageCoroutine);
        dialogueEventDictionary.Add("UI_BackGroundImage", GetSetBackgroundImageCoroutine);
    }


    public void StartDialogue(DialogueData data)
    {
        GameManager.instance.ChaneGameState(GameManager.GameSate.InGameUISetting);

        m_dialogueData = data;
        m_dialogueIndex = 0;

        StartCoroutine(DialogueEventCoroutine());
    }

    public void NextDialogue()
    {
        if (!isCanNext)
            return;

        m_dialogueIndex++;

        if (m_dialogueIndex >= m_dialogueData.nodes.Length)
        {
            m_uiDialogue.Toggle(false);
            GameManager.instance.ChaneGameState(GameManager.GameSate.GamePlaying);
            return;
        }
       

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
        m_datas = m_dialogueData.nodes[m_dialogueIndex].data.Split(m_filterDatas, System.StringSplitOptions.RemoveEmptyEntries);
        m_dataIndex = 0;

        uiDialoge.SetNameText(null);
        uiDialoge.SetSentenceText(null);
        m_uiDialogue.LeftImageOff();
        m_uiDialogue.RightImageOff();
        m_uiDialogue.BackGroundOff();


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

        yield return StartCoroutine(m_dialogueEventDictionary[commandID]()); 
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
        uiDialoge.SetNameText(data);

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
            else if(m_datas[m_dataIndex] != "/" )
            {
                yield return StartCoroutine(TextDurlationCoroutine(m_datas[m_dataIndex]));
            }
            m_dataIndex++;
        }

        //�ش� ������ �������� ���� �̺�Ʈ�� ����

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
        uiDialoge.Toggle(true);
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
        uiDialoge.Toggle(false);
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

    #region SetLeftImageCoroutine
    private IEnumerator GetSetLeftImageCoroutine()
    {
        string id = m_datas[m_dataIndex++];
        yield return SetLeftImageCoroutine(id);
    }

    private IEnumerator SetLeftImageCoroutine(string id)
    {
        Sprite sprite = ImageManager.instance.dialogeImageDictionary[id];
        UIManager.instance.dialogueView.LeftImageOn(sprite);
        m_dataIndex++;
        yield return null;
    }

    #endregion

    #region SetRightImageCoroutine
    private IEnumerator GetSetRightImageCoroutine()
    {
        string id = m_datas[m_dataIndex];
        yield return SetRightImageCoroutine(id);
    }

    private IEnumerator SetRightImageCoroutine(string id)
    {
        Sprite sprite = ImageManager.instance.dialogeImageDictionary[id];
        UIManager.instance.dialogueView.RightImageOn(sprite);
        m_dataIndex++;
        yield return null;
    }

    #endregion

    #region SetBackGroundImageCoroutine
    private IEnumerator GetSetBackgroundImageCoroutine()
    {
        string id = m_datas[m_dataIndex];
        yield return SetBackgroundImageCoroutine(id);
    }

    private IEnumerator SetBackgroundImageCoroutine(string id)
    {
        Sprite sprite = ImageManager.instance.dialogeImageDictionary[id];
        UIManager.instance.dialogueView.BackGroundOn(sprite);
        //m_dataIndex++;
        yield return null;
    }

    #endregion

    private IEnumerator TextDurlationCoroutine(string data)
    {
        foreach (char letter in data.ToCharArray())
        {
            uiDialoge.AddSentenceText(letter.ToString());
            yield return new WaitForSeconds(m_textDuration);
        }
    }


}
