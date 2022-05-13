using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadMissile : MonoBehaviour
{
    [SerializeField]
    private MadMissileData m_missileData;
    public MadMissileData missileData
    {
        get
        {
            return m_missileData;
        }
    }

    private Rigidbody2D m_rig2D;
    private Rigidbody2D rig2D
    {
        get
        {
            return m_rig2D;
        }
    }

    private Vector2 m_dir;
    public Vector2 dir
    {
        get
        {
            return m_dir;
        }
    }

    private Vector2 m_targetVelocity;
    public Vector2 targetVelocity
    {
        get
        {
            return m_targetVelocity;
        }
    }

    private Vector2 m_movement;


    private void Init(Vector2 targetPoint)
    {
        m_movement = new Vector2();
        m_dir = targetPoint - (Vector2)transform.position;
        m_dir.Normalize();

        m_targetVelocity = dir * missileData.maxSpeed;

        float lookRoation = Utility.GetRotaionAngleByTargetPosition((Vector2)transform.position, targetPoint, 0.0f);

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, lookRoation);


        m_rig2D = GetComponent<Rigidbody2D>();
    }

    public void HandleSpawn(Vector2 spawnPos, Vector2 targetPoint)
    {
        transform.position = spawnPos;
        Init(targetPoint);
    }

    private void Update()
    {
        Acceleration();
    }


    private void Acceleration()
    {

        Vector2 velocityDif = targetVelocity - rig2D.velocity;

        m_movement.x = Mathf.Pow(Mathf.Abs(velocityDif.x) * missileData.accele, missileData.acclePower) * Mathf.Sign(velocityDif.x);
        m_movement.y = Mathf.Pow(Mathf.Abs(velocityDif.y) * missileData.accele, missileData.acclePower) * Mathf.Sign(velocityDif.y);


        rig2D.AddForce(m_movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Hit();
    }

    private void Hit()
    {
        Debug.Log(gameObject.name + "  Hit");
        GameObject.Destroy(this);
    }

}
