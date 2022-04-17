using UnityEngine;
using DG.Tweening;

public class UnitPlayer : UnitBase
{
    [SerializeField]
    private PlayerMovementManager m_movement;
    public PlayerMovementManager movement
    {
        get
        {
            return m_movement;
        }
    }

    [SerializeField]
    private GrapplingShooter m_grapplingShooter;
    public GrapplingShooter grapplingShooter
    {
        get
        {
            return m_grapplingShooter;
        }
    }

    [SerializeField]
    private PlayerAnimation m_animation;
    public PlayerAnimation animation
    {
        get
        {
            return m_animation;
        }
    }

    [SerializeField]
    private Shoulder m_shoulder;
    public Shoulder shoulder
    {
        get
        {
            return m_shoulder;
        }
    }

    [SerializeField]
    private Transform m_model;
    public Transform model { get { return m_model; } }

    [SerializeField]
    private PlayerSound m_sound;
    public PlayerSound sound { get { return m_sound; } }

    [SerializeField]
    private InputPlayer m_inputPlayer;
    public InputPlayer inputPlayer { get { return m_inputPlayer; } }

    public override void Init()
    {
        base.Init();
        m_movement.Init(this);
        m_grapplingShooter.Init();
        m_shoulder.Init();
        m_sound.Init(this);
        
    }

    public override void HitEvent(Vector2 hitPoint)
    {
        base.HitEvent(hitPoint);

        Vector2 hitToUnitDir = (Vector2)m_model.position - hitPoint;
        hitToUnitDir.Normalize();

        movement.HitMovement(hitToUnitDir);

    }

    private Vector2 m_oldMoveDir;


    public override bool isControl 
    { 
        get => base.isControl;
        set
        {
            if (!value)
            {
                m_oldMoveDir = inputPlayer.moveDir;
                inputPlayer.moveDir = Vector2.zero;
            }
            else if(value && !isControl)
            {
                inputPlayer.moveDir = m_oldMoveDir;
            }

            base.isControl = value;

        }
    }



}
