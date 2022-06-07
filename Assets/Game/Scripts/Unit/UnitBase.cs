using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    #region Health
    private Health m_health;
    public Health health
    {
        get { return m_health; }
    }
    #endregion

    #region Model
    private SpriteRenderer m_model;
    public SpriteRenderer model
    {
        get
        {
            return m_model;
        }
    }
    #endregion

    #region animator
    private Animator m_modelAnimator;
    public Animator modelAnimator
    {
        get
        {
            return m_modelAnimator;
        }
    }
    #endregion

    #region rig2D
    private Rigidbody2D m_rig2D;
    public Rigidbody2D rig2D
    {
        get
        {
            return m_rig2D;
        }
    }



    #endregion


    #region isInit
    private bool m_isInit = false;
    public bool isInit
    {
        protected set
        {
            m_isInit = value;
        }
        get 
        {
            return m_isInit; 
        }
    }
    #endregion


    #region isMoveAble
    private bool m_isMoveAble;
    public bool isMoveAble
    {
        set
        {
            m_isMoveAble = value;
        }
        get
        {
            return m_isMoveAble;
        }
    }
    #endregion


    public Vector2 unitPos
    {
        get
        {
            return transform.position;
        }
    }

    //�ʱ�ȭ �۾��� �Ϸ�Ǹ� isInit�� True�� ������ ��
    public virtual void Init()
    {
        ComponentInit();
    }

    protected virtual void ComponentInit()
    {
        m_health = GetComponent<Health>();

        Transform modelTr = transform.Find("model");
        m_model = modelTr.GetComponent<SpriteRenderer>();
        m_modelAnimator = modelTr.GetComponent<Animator>();

        m_rig2D = GetComponent<Rigidbody2D>();

        health.Init();
    }


    #region Hit Method
    public virtual void OnHitUnit(UnitBase attackUnit, int damage)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        HitHp(damage);
        HitUniqueEventUnit(attackUnit);
    }

    public virtual void OnHitObject(GameObject attackObject, int damage)
    {
        if (GameManager.instance.gameState != GameManager.GameSate.GamePlaying)
            return;

        HitHp(damage);
        HitUniqueEventObject(attackObject);
    }


    protected virtual void HitHp(int damage)
    {
    }

    protected virtual void HitUniqueEventUnit(UnitBase attackUnit)
    {

    }

    protected virtual void HitUniqueEventObject(GameObject attackObject)
    {

    }

    #endregion

    public bool IsDead()
    {
        return health.hp == 0;
    }
}
