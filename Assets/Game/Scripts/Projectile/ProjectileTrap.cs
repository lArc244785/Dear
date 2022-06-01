using UnityEngine;

public class ProjectileTrap : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField]
    private GameObject m_projectilePrefab;
    private GameObject projectilePrefab
    {
        get
        {
            return m_projectilePrefab;
        }
    }
    [Header("LayerMask")]
    [SerializeField]
    private LayerMask m_targetLayerMask;
    private LayerMask targetLayerMask
    {
        get
        {
            return m_targetLayerMask;
        }
    }
    [Header("FirePoint Offset")]
    [SerializeField]
    private Vector2 m_firePointOffset;
    private Vector2 firePointOffset
    {
        get
        {
            return m_firePointOffset;
        }
    }


    private Transform m_firePointTransfrom;
    private Transform firePointTransfrom
    {
        get
        {
            return m_firePointTransfrom;
        }
    }
    private Vector2 m_fireDir;
    private Vector2 fireDir
    {
        get 
        {
            return m_fireDir; 
        }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        m_firePointTransfrom = transform.Find("FirePoint");

        firePointTransfrom.localPosition = firePointOffset;

        m_fireDir = (Vector2)(m_firePointTransfrom.position - transform.position);
        m_fireDir.Normalize();
    }


    public void Fire()
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab);
        projectile.GetComponent<ProjectileBase>().HandleSpawn(firePointTransfrom.position, m_fireDir, targetLayerMask);
    }

    



}
