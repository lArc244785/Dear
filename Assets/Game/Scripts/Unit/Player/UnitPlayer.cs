using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    #region Hit
    private StateImfectTween m_hitImfect;
    private StateImfectTween hitImfect
    {
        get
        {
            return m_hitImfect;
        }
    }


    [Header("Hit")]
    private int m_ghostLayer;
    private int ghostLayer
    {
        get
        {
            return m_ghostLayer;
        }
    }
    private int m_defaultLayer;
    private int defaultLayer
    {
        get
        {
            return m_defaultLayer;
        }
    }

    [Header("Hit")]
    [SerializeField]
    private float m_hitDuringTime;
    private float hitDuringTime
    {
        get
        {
            return m_hitDuringTime;
        }
    }
    [SerializeField]
    private float m_ghostDuringTime;

    private float ghostDuringTime
    {
        get
        {
            return m_ghostDuringTime;
        }
    }

    #endregion

    #region ParticleSystem
    private PlayerParticleManager m_playerParticleManager;
    public PlayerParticleManager particleManager

    {
        get
        {
            return m_playerParticleManager;
        }
    }
    #endregion

    #region FootStep
    private IEnumerator m_footStepLoopCoroutine;
    private bool m_footStepLoop;
    public bool footStepLoop { set { m_footStepLoop = value; } get { return m_footStepLoop; } }
    #endregion

    #region  Wall
    private IEnumerator m_wallSlideLoopCoroutine;
    private bool m_wallSlideLoop;
    public bool wallSlideLoop { set { m_wallSlideLoop = value; } get { return m_wallSlideLoop; } }
    #endregion

    #region Interaction

    private PlayerInteraction m_interaction;
    public PlayerInteraction interaction { get { return m_interaction; } }
    #endregion


    public override void Init()
    {
        base.Init();
        isInit = true;
        
        m_defaultLayer = LayerMask.NameToLayer("Player");
        m_ghostLayer = LayerMask.NameToLayer("Ghost");

        ghostFrozenEvent = null;
        hitLayerEvent = null;

        SetLayer(defaultLayer);

        footStepLoop = false;
        m_footStepLoopCoroutine = FootStepCoroutine(sound.footStepPlayTick);

        wallSlideLoop = false;
        m_wallSlideLoopCoroutine = WillSlideCoroutine(0.2f);


        StartCoroutine(m_footStepLoopCoroutine);

        StartCoroutine(m_wallSlideLoopCoroutine);

    }

    protected override void ComponentInit()
    {
        base.ComponentInit();
        m_movementManager = GetComponent<PlayerMovementManager>();
        m_inputPlayer = GetComponent<InputPlayer>();
        m_sound = GetComponent<PlayerSound>();
        m_animationManager = GetComponent<PlayerAnimationManager>();
        
        m_toolManager = GetComponent<ToolManager>();
        m_modelCollider = GetComponent<CapsuleCollider2D>();
        m_hitImfect = GetComponent<StateImfectTween>();
        m_playerParticleManager = transform.Find("ParticleManager").GetComponent<PlayerParticleManager>();

        m_madTrackingPoint = transform.Find("MadTrackingPoint").GetComponent<MadTrackingPoint>();
        m_mad = GameObject.Find("Mad").GetComponent<Mad>();

        m_interaction = GetComponent<PlayerInteraction>();
        

        toolManager.Init(this);
        
        movementManager.Init(this);
        sound.Init(this);
        m_hitImfect.Init(model);

        madTrackingPoint.Init(!movementManager.IsLookDirRight(), mad);
        mad.Init(this, madTrackingPoint);

        animationManager.Init();

        inputPlayer.Init(this, mad);
        interaction.Init();
    }

    public Vector2 GetModelColliderTop()
    {
        Vector2 modelTopPos = new Vector2();

        modelTopPos = unitPos + modelCollider.offset;
        modelTopPos.y += transform.localScale.y * (modelCollider.size.y * 0.5f);

        return modelTopPos;
    }


    protected override void HitHp(int damage)
    {
        base.HitHp(damage);
        GameObject.Find("Hpcontainer").GetComponent<HpUI>().OnDamage(damage);
        health.OnDamage(damage);
    }

    protected override void HitUniqueEventUnit(UnitBase attackUnit)
    {
        base.HitUniqueEventUnit(attackUnit);

        if(IsDead())
        {
            OnDead();
            return;
        }



        sound.Hit();
        GameManager.instance.stageManager.cameraManager.PlayerHitShake();

        Vector2 playerToAttackUnitDir = unitPos - attackUnit.unitPos ;
        playerToAttackUnitDir.Normalize();

        movementManager.HitMovement(playerToAttackUnitDir);
        hitImfect.HitImfect(hitDuringTime, ghostDuringTime);

        HitLayer();
    }

    public override void OnHitObject(GameObject attackObject, int damage)
    {
        base.OnHitObject(attackObject, damage);


        if (IsDead())
        {
            OnDead();
            return;
        }

        sound.Hit();
        GameManager.instance.stageManager.cameraManager.PlayerHitShake();

        Vector2 playerToAttackUnitDir = unitPos -(Vector2)attackObject.transform.position ;
        playerToAttackUnitDir.Normalize();

        movementManager.HitMovement(playerToAttackUnitDir);
        hitImfect.HitImfect(hitDuringTime, ghostDuringTime);

        HitLayer();
    }

    private IEnumerator hitLayerEvent { set; get; }

    private void HitLayer()
    {
        if (hitLayerEvent != null)
            StopCoroutine(hitLayerEvent);

        hitLayerEvent = HitLayerEventCoroutine();
        StartCoroutine(hitLayerEvent);
    }

    private IEnumerator HitLayerEventCoroutine()
    {
        SetGhostLayer();
        yield return new WaitForSeconds(hitDuringTime + ghostDuringTime);
        SetDefaultLayer();
        SoundManager.instance.bgm.SetParamaterHitIndexID(0.0f);
    }

    private void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }

    public void SetGhostLayer()
    {
        SetLayer(ghostLayer);
    }

    public void SetDefaultLayer()
    {
        SetLayer(defaultLayer);
    }


    private IEnumerator ghostFrozenEvent { set; get; }
    public void GhostFrozen(float fTime)
    {
        if (ghostFrozenEvent != null)
            StopCoroutine(ghostFrozenEvent);

        ghostFrozenEvent = GhostFrozenEventCoroutine(fTime);

        StartCoroutine(ghostFrozenEvent);
    }

    private IEnumerator GhostFrozenEventCoroutine(float fTime)
    {
        inputPlayer.SetControl(false);
        SetGhostLayer();
        yield return new WaitForSeconds(fTime);
        inputPlayer.SetControl(true);
        SetDefaultLayer();
    }





    private IEnumerator FootStepCoroutine(float tickTime)
    {
        float value;

        while (true)
        {
            if (footStepLoop)
            {
                Collider2D groundCollider = movementManager.groundSensor.GetGroundCollider2D();
                value = 0.0f;

                if (groundCollider != null)
                {
                    if (groundCollider.tag == "Forest")
                    {
                        value = 1.0f;
                    }
                    else if (groundCollider.tag == "Asphalt")
                    {
                        value = 2.0f;
                    }
                }

                particleManager.MoveEffect(inputPlayer.moveDir.x);
                sound.FootStep(value);
            }
            yield return new WaitForSeconds(tickTime);
        }

    }

    private IEnumerator WillSlideCoroutine(float tickTime)
    {
        bool isRight;
        while (true)
        {
            if (wallSlideLoop)
            {
                isRight = movementManager.IsWallRight();

                particleManager.WallSlideEffect(isRight);
                //sound.FootStep(value);
            }
            yield return new WaitForSeconds(tickTime);
        }
    }


    public void Trun(bool isRight)
    {
        Vector2 lookDir = Vector2.left;
        if (isRight)
            lookDir = Vector2.right;

        movementManager.Trun(lookDir);
    }


    public void OnRespawnHit(Vector2 respawnPos, int damage)
    {
        StartCoroutine(RespawnHitCoroutine(respawnPos, damage));

    }

    private IEnumerator RespawnHitCoroutine(Vector2 respawnPos, int damage)
    {
        inputPlayer.isControl = false;
        movementManager.currentState = PlayerMovementManager.State.None;
        rig2D.velocity = Vector2.zero;
        rig2D.gravityScale = 0.0f;

        GameManager.instance.stageManager.cameraManager.PlayerRespawnShake();
        HitHp(damage);

        if (IsDead())
        {
            OnDead();

            yield break;
        }




        UIManager.instance.produtionView.Toggle(true);
        UIManager.instance.produtionView.fade.FadeOut();

        while (!UIManager.instance.produtionView.fade.isfadeProcessed)
            yield return null;

        transform.position = respawnPos;
        movementManager.currentState = PlayerMovementManager.State.Ground;



        UIManager.instance.produtionView.fade.FadeIn();

        while (!UIManager.instance.produtionView.fade.isfadeProcessed)
            yield return null;
        inputPlayer.isControl = true;

    }


    private void OnDead()
    {
        inputPlayer.SetControl(false);
        movementManager.currentState = PlayerMovementManager.State.None;
        rig2D.gravityScale = 0.0f;
        rig2D.velocity = Vector2.zero;

        Debug.Log("DeadUI On");
        GameManager.instance.ChangeStateChangeGameOver();

    }

}
