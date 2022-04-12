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


    public void Start()
    {
        Init();
    }


    protected override void Init()
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



}
