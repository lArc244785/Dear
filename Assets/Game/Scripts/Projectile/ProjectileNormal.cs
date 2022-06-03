using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileNormal : ProjectileBase
{
    [SerializeField]
    private float m_speed;
    private float speed { get { return m_speed; } }

    [SerializeField]
    private float m_lifeTime;
    private float lifeTime
    {
        get
        {
            return m_lifeTime;
        }
    }

    private float m_currentLifeTime;

    protected override void Init(Vector2 fireDir, LayerMask targetLayerMask)
    {
        base.Init(fireDir, targetLayerMask);
        rig2D.AddForce(dir * speed);
        m_currentLifeTime = 0.0f;
    }


    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
    }

    private void Update()
    {
        if(m_currentLifeTime >= lifeTime)
        {
            Destory();
            return;
        }
        m_currentLifeTime += Time.deltaTime;
    }
}
