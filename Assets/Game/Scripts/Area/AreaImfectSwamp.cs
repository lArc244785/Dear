using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaImfectSwamp : AreaImfectBase
{
    [SerializeField]
    private float m_attenuationX;

    public override void Movement(MovementMangerBase mmb)
    {
        base.Movement(mmb);

        if (mmb.moveDir == Vector2.zero)
            return;
        if (Mathf.Abs(mmb.rig2D.velocity.x) <= Mathf.Epsilon)
            return;
            
        Vector2 attenuation = new Vector2(m_attenuationX, 0.0f);
        attenuation *= mmb.lookDirX;

        mmb.rig2D.velocity -= attenuation;

    }

    protected override void EnterUnitSetting(UnitBase unit)
    {
        base.EnterUnitSetting(unit);
        unit.isJump = false;
    }

    protected override void ExitUnitSetting(UnitBase unit)
    {
        base.ExitUnitSetting(unit);
        unit.isJump = true;
    }


}
