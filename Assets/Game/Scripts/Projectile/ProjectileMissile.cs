using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMissile : ProjectileBase
{
    [SerializeField]
    private ProjectileMissileData m_missileData;
    public ProjectileMissileData missileData
    {
        get
        {
            return m_missileData;
        }
    }

    private Vector2 m_movement;

    private Vector2 m_targetVelocity;
    public Vector2 targetVelocity
    {
        get
        {
            return m_targetVelocity;
        }
    }

    private float m_currentTime;

    protected override void Init(Vector2 fireDir, LayerMask targetLayerMask)
    {
        base.Init(fireDir, targetLayerMask);
        m_movement = new Vector2();
        m_targetVelocity = dir * missileData.maxSpeed;
    }



    private void Update()
    {
        if (m_currentTime >= missileData.lifeTime)
        {
            Destory();
            return;
        }

        Acceleration();

        m_currentTime += Time.deltaTime;
    }

    private void Acceleration()
    {

        Vector2 velocityDif = targetVelocity - rig2D.velocity;

        m_movement.x = Mathf.Pow(Mathf.Abs(velocityDif.x) * missileData.accele, missileData.acclePower) * Mathf.Sign(velocityDif.x);
        m_movement.y = Mathf.Pow(Mathf.Abs(velocityDif.y) * missileData.accele, missileData.acclePower) * Mathf.Sign(velocityDif.y);


        rig2D.AddForce(m_movement);
    }

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);

        Destory();
    }

}
