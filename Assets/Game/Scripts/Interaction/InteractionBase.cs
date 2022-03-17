using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBase : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_layerMask;

    protected virtual void Enter(Collider2D collision) { }

    protected virtual void Exit(Collider2D collision) { }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTargetlayerInMask(collision.gameObject.layer))
            Enter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isTargetlayerInMask(collision.gameObject.layer))
            Exit(collision);
    }

    private bool isTargetlayerInMask(int targetLayer)
    {
        int targetMask = 1 << targetLayer;

        if ((m_layerMask & targetMask) != 0)
            return true;
        return false;
    }
}
