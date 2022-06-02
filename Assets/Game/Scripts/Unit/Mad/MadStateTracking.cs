using UnityEngine;

public class MadStateTracking : MadStateBase
{
    private Vector2 m_currentVelocity;
    private Vector2 m_currentPos;
    private Vector2 m_targetPos;

    private float m_deadRangeEnterTime;
    private bool isOnDeadRangeYoyoEffect { set; get; }

    private float m_currentDeadRange;

    public override void Enter(Mad mad)
    {
        isOnDeadRangeYoyoEffect = false;
        SetDeadRange(mad, mad.data.trackingDeadRange);
    }

    public override void Exit(Mad mad)
    {

    }

    public override void UpdateProcesses(Mad mad)
    {
        Tracking(mad);
        ChangeUpdate(mad);
    }

    public override void FixedProcesses(Mad mad)
    {

    }

    protected override void ChangeUpdate(Mad mad)
    {
        if (mad.GetTrakingToMadDistance() <= mad.data.trackingEndRange)
        {
            mad.ChangeState(Mad.State.Idle);

        }
    }

    private void Tracking(Mad mad)
    {
        m_currentPos = mad.transform.position;
        m_targetPos = mad.trackingPoint.transform.position;


        m_currentPos = Vector2.SmoothDamp(m_currentPos, m_targetPos, ref m_currentVelocity, mad.data.trackingSmoothTime);

        mad.transform.position = m_currentPos;

        if(!mad.isAttackAble)
        mad.LookPlayer();


        if (isOnDeadRangeYoyoEffect)
        {
            float time = Time.time - m_deadRangeEnterTime;

            if (time <= mad.data.yoyoEffectTime)
                SetDeadRange(mad, GetYoyoEffectDeadRange(mad.data, time));

           // Debug.Log("dead : " + m_currentDeadRange);
        }



        if (mad.isTrackingRangeOver(m_currentDeadRange))
        {
            if (!isOnDeadRangeYoyoEffect)
            {
                m_deadRangeEnterTime = Time.time;
                isOnDeadRangeYoyoEffect = true;
            }

            Vector2 playerPos = mad.player.unitPos;
            Vector2 playerToMadDir = (Vector2)mad.transform.position - playerPos;
            playerToMadDir.Normalize();

            Vector2 trackingDeadPos = playerPos + (playerToMadDir * m_currentDeadRange);

            mad.transform.position = trackingDeadPos;
        }
        else if (!mad.isTrackingDeadRangeOver())
        {
            if (isOnDeadRangeYoyoEffect)
            {
                isOnDeadRangeYoyoEffect = false;
                SetDeadRange(mad, mad.data.trackingDeadRange);
                
            }

        }

    }


    private void SetDeadRange(Mad mad, float range)
    {
        m_currentDeadRange = range;
        mad.gizmosTrackingDeadRange = m_currentDeadRange;
    }


    private float GetYoyoEffectDeadRange(MadData data, float time)
    {
        //float currentTime = Time.time - m_deadRangeEnterTime;
        float yoyoTimeInRange = time % data.yoyoEffectTime;
        //Debug.Log("YoyoTimeInRange: " + yoyoTimeInRange + "\nC: " + time + "\nL: " + data.yoyoEffectTime);


        float yoyoTimeLerp = yoyoTimeInRange / data.yoyoEffectTime;
        float lerp = 0.0f;
        float addRange;


        if (yoyoTimeLerp < 0.5f)
            lerp = yoyoTimeLerp * 2.0f;
        else
            lerp = 1 - ((yoyoTimeLerp - 0.5f) * 2.0f);

       // Debug.Log("Lerp :" + lerp + "Yoyo :" + yoyoTimeLerp);


        addRange = data.deadRangeYoyoRange * lerp;

        return data.trackingDeadRange + addRange;
    }



}
