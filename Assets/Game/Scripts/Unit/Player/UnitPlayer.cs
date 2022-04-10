using UnityEngine;

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


    public void Start()
    {
        Init();
        health.Hit(1);
    }


    protected override void Init()
    {
        base.Init();
        m_movement.Init(this);

        m_grapplingShooter.Init();
        m_shoulder.Init();
    }





}
