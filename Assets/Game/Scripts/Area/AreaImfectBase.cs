using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaImfectBase : InteractionBase
{


    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);

        UnitBase unit = collision.GetComponent<UnitBase>();
        EnterUnitSetting(unit);
       // unit.movementManager.areaImfect = this;
    }
    //SubClass���� ��������ߵ˴ϴ�.
    protected virtual void EnterUnitSetting(UnitBase unit)
    {

    }


    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        UnitBase unit = collision.GetComponent<UnitBase>();
        ExitUnitSetting(unit);
        //unit.movementManager.areaImfect = null;
    }

    //SubClass���� ��������ߵ˴ϴ�.
    protected virtual void ExitUnitSetting(UnitBase unit)
    {

    }

}
