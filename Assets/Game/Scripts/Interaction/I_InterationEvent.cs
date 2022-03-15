using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_InterationEvent 
{
    void EnterEvent(Collision2D coll);
    void ExitEvent(Collision2D coll);
}
