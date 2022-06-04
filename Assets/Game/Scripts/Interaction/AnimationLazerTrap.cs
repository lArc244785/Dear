using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLazerTrap : MonoBehaviour
{
    [SerializeField]
    private ObjectWeaponAlmost m_rayzer;
    [SerializeField]
    private Animator m_ani;
    [SerializeField]
    private GameObject m_attackImfect;
    


    private void Start()
    {
        m_rayzer.AttackEnd();
    }


    public void AnimationAttack()
    {
        m_ani.SetTrigger("Attack");
    }

    public void Attack()
    {
        m_rayzer.Attack();
        if(m_attackImfect != null)
        {
            AttackImfect();
        }
    }


    private void AttackImfect()
    {
        GameObject attackImfect = Instantiate(m_attackImfect);
        attackImfect.transform.position = m_rayzer.transform.position;
    }

}
