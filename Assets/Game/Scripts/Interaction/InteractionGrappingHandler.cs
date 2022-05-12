using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGrappingHandler : MonoBehaviour
{
    [SerializeField]
    private InteractionGrapping m_interactionGrapping;
    public InteractionGrapping interactionGrapping
    {
        get
        {
            return m_interactionGrapping;
        }
    }


}
