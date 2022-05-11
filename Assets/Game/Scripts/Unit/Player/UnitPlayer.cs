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

        shoulder.Init();

        toolManager.Init(this);
        animationManager.Init(modelAnimator, shoulder);
        movementManager.Init(this);
        sound.Init(this);
        inputPlayer.Init(movementManager, toolManager);


    }



}
