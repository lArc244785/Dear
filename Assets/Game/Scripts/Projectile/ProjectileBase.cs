using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [Header("WeaponData")]
    [SerializeField]
    private WeaponData m_weaponData;
    protected WeaponData weaponData
    { 
        get {
            return m_weaponData; 
        } 
    }

    private LayerMask m_targetLayerMask;
    private LayerMask targetLayerMask
    {
        get
        {
            return m_targetLayerMask;
        }
    }

    private Rigidbody2D m_rig2D;
    protected Rigidbody2D rig2D
    {
        get
        {
            return m_rig2D;
        }
    }

    private Vector2 m_dir;
    protected Vector2 dir
    {
        get
        {
            return m_dir;
        }
        set
        {
            m_dir = value;
        }
    }




    protected virtual void Init(Vector2 fireDir, LayerMask targetLayerMask)
    {
        m_rig2D = GetComponent<Rigidbody2D>();
        m_targetLayerMask = targetLayerMask;

        m_dir = fireDir;

        float lookRotation = Utility.GetRotaionAngleByDir(dir, 0.0f);

        transform.rotation = Quaternion.Euler(0f, 0f, lookRotation);
    }

    public void HandleSpawn(Vector2 spawnPoint, Vector2 fireDir, LayerMask targetLayerMask)
    {
        transform.position = spawnPoint;
        Init(fireDir, targetLayerMask);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utility.IsTargetLayerInMask(targetLayerMask, collision.gameObject.layer))
            Enter(collision);
    }

    protected virtual void Enter(Collider2D collision) 
    {
        UnitBase unit = collision.GetComponent<UnitBase>();
        if (unit != null)
        {
            unit.OnHitObject(gameObject, weaponData.damage);
        }
        Destory();
    }


    protected virtual void Destory()
    {
        Debug.Log(gameObject.name + "is Destory");
        GameObject.Destroy(gameObject);
    }

}
