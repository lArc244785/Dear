using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmHandler : MonoBehaviour
{
    public void SetBgmPrograss(float prograss)
    {
        SoundManager.instance.bgm.SetParamaterPrograss(prograss);
    }
}
