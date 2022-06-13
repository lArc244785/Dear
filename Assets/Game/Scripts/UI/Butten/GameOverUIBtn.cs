using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIBtn : MonoBehaviour
{
   public void EXitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void ReTryBtn()
    {
        PopUpManager.instance.ClosePopup(PopUpManager.instance.gameOver);
        GameManager.instance.Continue();
    }
}
