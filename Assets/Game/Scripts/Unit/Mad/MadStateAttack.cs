using UnityEngine;

public class MadStateAttack : MadStateBase
{
    private float lastAttackWaitTime { set; get; }

    public override void Enter(Mad mad)
    {
        OnAttackWaitTime(mad.data);

        mad.SetLookPoint(InputManager.instance.inGameMousePosition2D);
        Attack(mad);

    }

    public override void Exit(Mad mad)
    {
        mad.OnLastOnCoolTime();
    }

    public override void FixedProcesses(Mad mad)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateProcesses(Mad mad)
    {
        CoyoteTimeUpdate();

        if(lastAttackWaitTime < 0.0f)
        {
            ChangeUpdate(mad);
        }


    }

    protected override void ChangeUpdate(Mad mad)
    {
        mad.ChangeState(Mad.State.Tracking);
    }


    private void CoyoteTimeUpdate()
    {
            AttackWaitCoyoteTime();
    }

    private void OnAttackWaitTime(MadData data)
    {
        lastAttackWaitTime = data.attackWaitTime;
    }

    private void AttackWaitCoyoteTime()
    {
        lastAttackWaitTime -= Time.deltaTime;
    }



    private void Attack(Mad mad)
    {
        GameObject goMissile = GameObject.Instantiate(mad.missileObject);

        mad.SoundFire();

        Vector2 spawnPoint = (Vector2)mad.firePointTransform.position;
        Vector2 fireDir = InputManager.instance.inGameMousePosition2D - spawnPoint;
        fireDir.Normalize();

        goMissile.GetComponent<ProjectileMissile>().HandleSpawn(spawnPoint, fireDir, mad.data.targetLayerMask);
    }
}
