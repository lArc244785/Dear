using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Scaffolding : InteractionBase
{
    [SerializeField]
    private GameObject m_modelObject;

    [SerializeField]
    private Collider2D m_collider;

    [SerializeField]
    private float m_disabledTime;


    [SerializeField]
    private bool m_isReturnActive;
    [SerializeField]
    private float m_returnActiveTime;


    private IEnumerator m_eventCoroutine = null;




    protected override void Enter(Collider2D collision)
    {
        if(m_eventCoroutine == null)
        {
            m_eventCoroutine = ObjectDisabledCoroutine();
            StartCoroutine(m_eventCoroutine);
        }
    }

    private void ObjectActive(bool isActive)
    {
        m_modelObject.active = isActive;
        m_collider.enabled = isActive;
    }

    private IEnumerator ObjectDisabledCoroutine()
    {
        yield return new WaitForSeconds(m_disabledTime);
        ObjectActive(false);

        if(m_isReturnActive)
        {
            yield return new WaitForSeconds(m_returnActiveTime);
            ObjectActive(true);
        }

        m_eventCoroutine = null;
    }



}
