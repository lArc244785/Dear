using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
    public static float GetRotaionAngleByTargetPosition(Vector2 pivot, Vector2 targetPoint, float pivotAngle)
    {
        Vector2 diff = targetPoint - pivot;

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        float finalAngle = angle - pivotAngle;

        return finalAngle;
    }

    public static Vector2 IngamePosToViewPos(Vector2 position)
    {
        return InputManager.instance.brainCam.WorldToScreenPoint(position);
    }

    public static Quaternion GetRoationZ(float angle)
    {
        return Quaternion.Euler(0.0f, 0.0f, angle);
    }

}
