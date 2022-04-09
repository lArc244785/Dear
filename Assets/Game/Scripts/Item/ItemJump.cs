using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJump : InteractionBase, ItemBase
{
    //����

    protected override void Enter(Collider2D collision)
    {
            base.Enter(collision);
        PlayerMovement movement = collision.GetComponent<PlayerMovement>();
        movement.currentJump--;

        gameObject.SetActive(false);
    }


    public void Use(UnitPlayer player)
    {

    }
}
