using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActSceneManager : MonoBehaviour
{

    public void Talk(string name, string sentence)
    {
        Debug.Log("Name " + name + "\n Sentence " + sentence);
    }

    public void Action(UnityEvent act)
    {
        act.Invoke();
    }

}
