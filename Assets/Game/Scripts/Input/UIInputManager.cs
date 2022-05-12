using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputManager : SingleToon<UIInputManager>
{
    private void Awake()
    {
        Init();
    }

    protected override bool Init()
    {
        return base.Init();
    }



    #region testInven
    public void ActiveInventory(InputAction.CallbackContext contex)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;
        Debug.Log("눌림");
        // PopUpManager.instance.OpenPopup(PopUpManager.instance.inventory);
    }

    #endregion

}
