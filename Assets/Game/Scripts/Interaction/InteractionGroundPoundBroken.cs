using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGroundPoundBroken : InteractionBase
{
    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
    }

    public void Broken()
    {
        Debug.Log(gameObject.name + " Broken");
        gameObject.SetActive(false);
    }
}
