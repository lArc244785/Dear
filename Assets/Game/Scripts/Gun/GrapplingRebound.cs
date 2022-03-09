using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRebound : MonoBehaviour
{

    [SerializeField]
    private Transform m_playerTr;

    
    [SerializeField]
    private float m_notXReboundAngleRange;

    struct RangeRad
    {
        public float m_leftRad;
        public float m_rightRad;
    }

    struct RangePosition
    {
        public Vector2 leftPos;
        public Vector2 rightPos;
    }


    private RangeRad[] m_range = new RangeRad[2];


    [Header("GizomOption")]
    [SerializeField]
    private float m_drawRange;
    [SerializeField]
    private bool m_isDebugDraw;
    [SerializeField]
    private Color m_gizomColor;

    private RangePosition[] m_drawPoints = new RangePosition[2];

    private float m_deg90toRad;

    public void init()
    {
        caculationRangeRad(m_notXReboundAngleRange);
        m_isDebugDraw = false;
        m_deg90toRad = Mathf.PI * 0.5f;
    }




    private void OnDrawGizmos()
    {
        calculateGizmoPoints(ref m_drawPoints);

        Gizmos.color = m_gizomColor;
        foreach(RangePosition rp in m_drawPoints)
        {
            Gizmos.DrawLine(m_playerTr.position, m_playerTr.position + (Vector3)rp.leftPos);
            Gizmos.DrawLine(m_playerTr.position, m_playerTr.position + (Vector3)rp.rightPos);
        }
    }

    private void caculationRangeRad(float angle)
    {
        float leftTheta = 90.0f + angle;
        float rightTheta = 90.0f - angle;

        m_range[0].m_leftRad = leftTheta * Mathf.Deg2Rad;
        m_range[0].m_rightRad = rightTheta * Mathf.Deg2Rad;


        m_range[1].m_leftRad = leftTheta  * - Mathf.Deg2Rad;
        m_range[1].m_rightRad = rightTheta * -Mathf.Deg2Rad;

        string debugStr = "RangeRadValue: ";
        foreach(RangeRad range in m_range)
        {
            debugStr += range.m_rightRad + "  " + range.m_leftRad + "|";
        }
        Debug.Log(debugStr);
    }

    private void calculateGizmoPoints(ref RangePosition[] returnPoints)
    {
        if(m_isDebugDraw)
            caculationRangeRad(m_notXReboundAngleRange);

        float leftPosX;
        float leftPosY;
        float rightPosX;
        float rightPosY;

        int i = 0;
        while(i < returnPoints.Length)
        {
            leftPosX = Mathf.Cos(m_range[i].m_leftRad) * m_drawRange;
            leftPosY = Mathf.Sin(m_range[i].m_leftRad) * m_drawRange;

            returnPoints[i].leftPos.x = leftPosX;
            returnPoints[i].leftPos.y = leftPosY;

            rightPosX = Mathf.Cos(m_range[i].m_rightRad) * m_drawRange;
            rightPosY = Mathf.Sin(m_range[i].m_rightRad) * m_drawRange;

            returnPoints[i].rightPos.x = rightPosX;
            returnPoints[i].rightPos.y = rightPosY;

            i++;
        }

    }

    public bool isRebound(Vector2 hookPos)
    {
        Vector2 diff = (Vector2)m_playerTr.position - hookPos;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        float finalAngle = angle - 90.0f;
        if (finalAngle < 0)
            finalAngle = 360.0f + finalAngle;

        float leftAngle;
        float rightAngle;

        foreach(RangeRad range in m_range)
        {
            leftAngle = range.m_leftRad * Mathf.Rad2Deg;
            rightAngle = range.m_rightRad * Mathf.Rad2Deg;

            if (leftAngle < 0)
                leftAngle = 360 + leftAngle;
            if (rightAngle < 0)
                rightAngle = 360 + rightAngle;

            if (leftAngle >= finalAngle && rightAngle <= finalAngle)
                return false;
        }


        return true;
    }


}
