using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileNormal : ProjectileBase
{
    [SerializeField]
    private float m_speed;
    private float speed { get { return m_speed; } }

    [SerializeField]
    private GameObject m_HitParticle;


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
        if(m_HitParticle != null)
        {
            HitImfect();
        }
        
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


    private void HitImfect()
    {
        GameObject hitImfect = Instantiate(m_HitParticle);
        hitImfect.transform.position = transform.position;
    }
}
