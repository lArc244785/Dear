using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJump : InteractionBase, ItemBase
{
    //º¸·ù

    protected override void Enter(Collider2D collision)
    {
            base.Enter(collision);
        PlayerMovement movement = collision.GetComponent<PlayerMovement>();
        movement.currentJump--;

        gameObject.SetActive(false);
    }


    public void Use(Unit_Player player)
    {
        player.playerMovemntManager.nomalMovement.JumpCount--;
        gameObject.SetActive(false);
    }
}
