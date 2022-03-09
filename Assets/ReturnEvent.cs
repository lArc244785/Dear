using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnEvent : MonoBehaviour
{
    [SerializeField]
    Transform player;
    public void Return()
    {
        player.position = transform.position;
    }
}
