using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Player : UnitBase
{
    [SerializeField]
    private PlayerMovement m_movement;
    [SerializeField]
    private GrapplingShooter m_grapplingShooter;
    [SerializeField]
    private PlayerAnimation m_animation;
    [SerializeField]
    private Shoulder m_shoulder;

    public void Start()
    {
        Init();
        health.Hit(1);
    }


    protected override void Init()
    {
        base.Init();
        m_movement.Init();

        m_grapplingShooter.Init();
        m_shoulder.Init();
    }





}
