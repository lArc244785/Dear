using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLazyerTrapHandler : MonoBehaviour
{
    [SerializeField]
    private AnimationLazerTrap m_lazerTrp;

    public void HandleAttack()
    {
        m_lazerTrp.Attack();
    }
}
