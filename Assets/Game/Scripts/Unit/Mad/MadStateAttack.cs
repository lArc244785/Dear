using UnityEngine;

public class MadStateAttack : MadStateBase
{
    private float lastAttackWaitTime { set; get; }
    private float lastAttackEndWaitTime { set; get; }

    private int currentAttack { set; get; }

    private Vector2 attackPoint { set; get; }

    private bool isAttackEndWait { set; get; }


    public override void Enter(Mad mad)
    {
        OnAttackWaitTime(mad.data);
        isAttackEndWait = false;
        currentAttack = 0;
        attackPoint = InputManager.instance.inGameMousePosition2D;
        mad.SetLookPoint(attackPoint);

    }

    public override void Exit(Mad mad)
    {
        mad.OnLastOnCoolTime();
    }

    public override void FixedProcesses(Mad mad)
    {

    }

    public override void UpdateProcesses(Mad mad)
    {
        CoyoteTimeUpdate();
        AttackUpdate(mad);
        ChangeUpdate(mad);


    }

    private void AttackUpdate(Mad mad)
    {
        if (isAttackEndWait)
            return;

        if (lastAttackWaitTime <= 0.0f)
        {
            if (currentAttack < mad.data.attackAmount)
            {
                Attack(mad, attackPoint);
                OnAttackWaitTime(mad.data);
                currentAttack++;

            }
            else
            {
                isAttackEndWait = true;
                OnAttackEndWaitTime(mad.data);
            }
        }

    }


    protected override void ChangeUpdate(Mad mad)
    {
        if (isAttackEndWait && lastAttackEndWaitTime <= 0.0f)
            mad.ChangeState(Mad.State.Tracking);
    }


    private void CoyoteTimeUpdate()
    {
        AttackWaitCoyoteTime();
        AttackEndWaitCoyoteTime();
    }


    private void OnAttackEndWaitTime(MadData data)
    {
        lastAttackEndWaitTime = data.attackEndWaitTime;
    }

    private void AttackEndWaitCoyoteTime()
    {
        lastAttackEndWaitTime -= Time.deltaTime;
    }

    private void OnAttackWaitTime(MadData data)
    {
        lastAttackWaitTime = data.attackWaitTime;
    }

    private void AttackWaitCoyoteTime()
    {
        lastAttackWaitTime -= Time.deltaTime;
    }



    private void Attack(Mad mad, Vector2 point)
    {
        GameObject goMissile = GameObject.Instantiate(mad.missileObject);

        mad.SoundFire();

        Vector2 spawnPoint = (Vector2)mad.firePointTransform.position;
        Vector2 fireDir = point - spawnPoint;
        fireDir.Normalize();

        goMissile.GetComponent<ProjectileMissile>().HandleSpawn(spawnPoint, fireDir, mad.data.targetLayerMask);

    }
}
