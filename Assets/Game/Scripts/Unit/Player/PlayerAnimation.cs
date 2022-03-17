using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    PlayerMovementManager m_pmm;
    [SerializeField]
    Animator ani;


    private void Update()
    {
        float move = Mathf.Abs(m_pmm.moveDir.x);

        ani.SetFloat("Move", move);
    }


}
