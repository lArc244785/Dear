using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Player : UnitBase
{
    [SerializeField]
    private PlayerInteraction m_interaction;
    [SerializeField]
    private GrapplingShooter m_shooter;
    [SerializeField]
    private PlayerMovementManager m_playerMovementManger;


    private void Start()
    {
        Init(m_playerMovementManger);
    }

    protected override void Init(MovementMangerBase mmb)
    {
        base.Init(mmb);
        m_interaction.Init();
        m_shooter.Init();
    }

    public PlayerMovementManager playerMovemntManager
    {
        get
        {
            return m_playerMovementManger;
        }
    }

}
