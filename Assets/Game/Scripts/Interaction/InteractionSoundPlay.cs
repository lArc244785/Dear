using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSoundPlay : InteractionBase
{
    [SerializeField]
    private FMODUnity.EventReference m_imfectEvnet;



    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        SoundManager.instance.SoundOneShot(m_imfectEvnet);
    }

}
