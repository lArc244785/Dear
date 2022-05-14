using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGroundPoundBroken : InteractionBase
{
    [SerializeField]
    private float m_brokenTime;
    private float brokenTime { get { return m_brokenTime; } }

    private IEnumerator m_brokenEvent = null;

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
    }

    public void BrokenTrigger()
    {
        if (m_brokenEvent != null)
            return;
        m_brokenEvent = BrokenEventCoroutine();
        StartCoroutine(m_brokenEvent);
    }

    private IEnumerator BrokenEventCoroutine()
    {
        yield return new WaitForSeconds(brokenTime);

        Debug.Log(gameObject.name + " Broken");
        gameObject.SetActive(false);
    } 
}
