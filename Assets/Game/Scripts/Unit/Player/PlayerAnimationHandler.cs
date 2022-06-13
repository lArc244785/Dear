using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField]
    private UnitPlayer m_player;

    public void FootStep()
    {
        m_player.FootStep();
    }
}
