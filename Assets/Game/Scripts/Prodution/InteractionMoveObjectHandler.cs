using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionMoveObjectHandler : MonoBehaviour
{
    [SerializeField]
    private InteractionMoveObject m_interactionMoveObject;

    public void Move()
    {
        m_interactionMoveObject.Move();
    }


    public void RideSound()
    {
        m_interactionMoveObject.RideSound();
    }

    public void MoveStopSound()
    {
        m_interactionMoveObject.MoveStopSound();
    }

}
