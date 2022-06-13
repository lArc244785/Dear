using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTool : MonoBehaviour
{
    public void Get()
    {
        GameManager.instance.GetGrapplingGun();
        GameManager.instance.stageManager.player.toolManager.SetTool(ToolManager.ActiveToolType.GrappingGun);
    }
}
