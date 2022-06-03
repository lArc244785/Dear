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

        GameManager.instance.Gotitle();

        PopUpManager.instance.ToggleOpenClosePopup(PopUpManager.instance.esc);
        SceneManager.LoadScene("Title");
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

        //UnityEditor.EditorApplication.isPlaying = false;
    }

}
