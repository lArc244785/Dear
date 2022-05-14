using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBase : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_targetLayerMask;
    private LayerMask targetLayerMask
    {
        get
        {
            return m_targetLayerMask;
        }
    }

    protected virtual void Enter(Collider2D collision) { }

    protected virtual void Exit(Collider2D collision) { }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utility.IsTargetLayerInMask(targetLayerMask, collision.gameObject.layer))
            Enter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Utility.IsTargetLayerInMask(targetLayerMask, collision.gameObject.layer))
            Exit(collision);
    }

    //private bool isTargetlayerInMask(int targetLayer)
    //{
    //    int targetMask = 1 << targetLayer;

    //    if ((m_layerMask & targetMask) != 0)
    //        return true;
    //    return false;
    //}
}
