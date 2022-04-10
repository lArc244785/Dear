using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJump : InteractionBase, ItemBase
{
    //º¸·ù

    protected override void Enter(Collider2D collision)
    {
            base.Enter(collision);
        PlayerMovementManager movement = collision.GetComponent<PlayerMovementManager>();
        movement.jumpCount--;

        gameObject.SetActive(false);
    }


    public void Use(UnitPlayer player)
    {

    }
}
