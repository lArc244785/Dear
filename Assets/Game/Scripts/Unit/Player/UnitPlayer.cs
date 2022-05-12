using UnityEngine;

public class UnitPlayer : UnitBase
{
    #region movementManger
    private PlayerMovementManager m_movementManager;
    public PlayerMovementManager movementManager
    {
        get
        {
            return m_movementManager;
        }
    }
    #endregion

    #region shoulder
    private Shoulder m_shoulder;
    public Shoulder shoulder
    {
        get
        {
            return m_shoulder;
        }
    }
    #endregion

    #region toolManager
    private ToolManager m_toolManager;
    public ToolManager toolManager
    {
        get
        {
            return m_toolManager;
        }
    }
    #endregion

    #region inputPlayer;
    private InputPlayer m_inputPlayer;
    public InputPlayer inputPlayer
    {
        get
        {
            return m_inputPlayer;
        }
    }
    #endregion

    #region sound
    private PlayerSound m_sound;
    public PlayerSound sound
    {
        get
        {
            return m_sound;
        }
    }

    #endregion

    #region animationManager
    private PlayerAnimationManager m_animationManager;
    public PlayerAnimationManager animationManager
    {
        get
        {
            return m_animationManager;
        }
    }
    #endregion

    private CapsuleCollider2D m_modelCollider;
    public CapsuleCollider2D modelCollider
    {
        get
        {
            return m_modelCollider;
        }
    }

    #region Mad
    private MadTrackingPoint m_madTrackingPoint;
    public MadTrackingPoint madTrackingPoint
    {
        get { return m_madTrackingPoint; }
    }

    private Mad m_mad;
    public Mad mad { get { return m_mad; } }

    #endregion

    public override void Init()
    {
        base.Init();
        isInit = true;
        
    }

    protected override void ComponentInit()
    {
        base.ComponentInit();
        m_movementManager = GetComponent<PlayerMovementManager>();
        m_inputPlayer = GetComponent<InputPlayer>();
        m_sound = GetComponent<PlayerSound>();
        m_animationManager = GetComponent<PlayerAnimationManager>();
        m_shoulder = transform.Find("Shoulder").GetComponent<Shoulder>();
        m_toolManager = GetComponent<ToolManager>();
        m_modelCollider = GetComponent<CapsuleCollider2D>();

        m_madTrackingPoint = transform.Find("MadTrackingPoint").GetComponent<MadTrackingPoint>();
        m_mad = GameObject.Find("Mad").GetComponent<Mad>();


        shoulder.Init();

        toolManager.Init(this);
        animationManager.Init(modelAnimator, shoulder);
        movementManager.Init(this);
        sound.Init(this);
        inputPlayer.Init(movementManager, toolManager);

        madTrackingPoint.Init(movementManager.IsLookDirRight(), mad);
        mad.Init(this, madTrackingPoint);

    }

    public Vector2 GetModelColliderTop()
    {
        Vector2 modelTopPos = new Vector2();

        modelTopPos = unitPos + modelCollider.offset;
        modelTopPos.y += transform.localScale.y * (modelCollider.size.y * 0.5f);

        return modelTopPos;
    }


}
