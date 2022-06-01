using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WebSpiderState
{
    Idle,
    Attack,
    Die
}


public class WebSpider : UnitBase
{
    [SerializeField]
    private PlayerSerch m_serch;
    public PlayerSerch serch
    {
        get
        {
            return m_serch;
        }
    }

    [SerializeField]
    private float m_fireCycle;
    
    public float fireCycle
    {
        get
        {
            return m_fireCycle;
        }
    }

    [SerializeField]
    private GameObject m_missileObject;
    public GameObject missileObject
    {
        get
        {
            return m_missileObject;
        }
    }

    [SerializeField]
    private LayerMask m_targetLayerMask;
    public LayerMask targetLayerMask
    {
        get
        {
            return m_targetLayerMask;
        }
    }



    private StateImfectTween m_hitImfect;
    private StateImfectTween hitImfect
    {
        get
        {
            return m_hitImfect;
        }
    }


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

    [SerializeField]
    private GameObject m_webEndPoint;
    public GameObject webEndPoint
    {
        get {
            return m_webEndPoint;
            }
        set
        {
            m_webEndPoint = value;
        }
    }


    private State<WebSpider>[] m_states;
    private StateMachine<WebSpider> m_stateMachine;

    public override void Init()
    {
        base.Init();
        Debug.Log("init");
        m_states = new State<WebSpider>[3];
        m_states[(int)WebSpiderState.Idle] = new WepSpiderOwnedState.Idle();
        m_states[(int)WebSpiderState.Attack] = new WepSpiderOwnedState.Attack();

        m_states[(int)WebSpiderState.Die] = new WepSpiderOwnedState.Die();

        m_stateMachine = new StateMachine<WebSpider>();

        m_stateMachine.SetUp(this, m_states[(int)WebSpiderState.Idle]);
    }
    protected override void ComponentInit()
    {
        base.ComponentInit();
        m_hitImfect = GetComponent<StateImfectTween>();
        m_hitImfect.Init(model);
    }
    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        m_stateMachine.Excute();
    }
    public void Attack()
    {
        GameObject goMissile = GameObject.Instantiate(missileObject);


        Vector2 spawnPoint = (Vector2)transform.position;
        Vector2 fireDir =  (Vector2)m_serch.player.transform.position - spawnPoint;
        fireDir.Normalize();

        goMissile.GetComponent<ProjectileMissile>().HandleSpawn(spawnPoint, fireDir, targetLayerMask);

    }

    protected override void HitUniqueEventObject(GameObject attackObject)
    {
        base.HitUniqueEventObject(attackObject);
        if (model == null) return;
    }

    protected override void HitUniqueEventUnit(UnitBase attackUnit)
    {
        base.HitUniqueEventUnit(attackUnit);

        if (model == null) return;
    }
    protected override void HitHp(int damage)
    {
        base.HitHp(damage);
        health.OnDamage(damage);
    }
    public override void OnHitObject(GameObject attackObject, int damage)
    {
        base.OnHitObject(attackObject, damage);
        Debug.Log("총에 맞음");
        if (health.hp > 0)
            hitImfect.HitImfect(hitDuringTime, ghostDuringTime);
    }



    public void ChangeState(WebSpiderState newState)
    {
        m_stateMachine.ChangeState(m_states[(int)newState]);
    }


}
