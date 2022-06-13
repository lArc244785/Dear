using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscUIBtn : MonoBehaviour
{
  
    public void ingameBtn()
    {
        GameManager.instance.ChaneGameState(GameManager.GameSate.GamePlaying);
        PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.esc);
    }
    public void GotitleBtn()
    {
        PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.esc);
        UIManager.instance.AllToggleFase();
        GameManager.instance.ChaneGameState(GameManager.GameSate.Title);
        GameManager.instance.NextState(7);

    }
    public void OptionBtn()
    {
        PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.option);
        PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.esc);
    }
    public void ExitBtn()
    {
        PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.check);
        PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.esc);
    }
    public void ExitCheckBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
