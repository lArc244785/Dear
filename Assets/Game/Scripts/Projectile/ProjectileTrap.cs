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

    [Header("Animator")]
    [SerializeField]
    private Animator m_ani;
    [Header("FireImfect")]
    [SerializeField]
    private GameObject m_fireImfect;


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


    #region FireDir
    private enum FireDirType
    {
        Left, Right
    }
    [SerializeField]
    private FireDirType m_fireDirType;
    #endregion

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        m_firePointTransfrom = transform.Find("FirePoint");
        FireDirSetting();
    }

    private void FireDirSetting()
    {
        Vector2 dir = new Vector2();

        switch (m_fireDirType)
        {
            case FireDirType.Left:
                dir = Vector2.left;
                break;
            case FireDirType.Right:
                dir = Vector2.right;
                break;
            //case FireDirType.Up:
            //    dir = Vector2.up;
            //    break;
            //case FireDirType.Down:
            //    dir = Vector2.down;
            //    break;
        }
        m_fireDir = dir;
    }




    public void Fire()
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab);
        projectile.GetComponent<ProjectileBase>().HandleSpawn(firePointTransfrom.position, m_fireDir, targetLayerMask);
        if(m_fireImfect != null)
        {
            FireImfect();
        }
    }

    public void AnimationFire()
    {
        m_ani.SetTrigger("Fire");
    }


    private void FireImfect()
    {
        GameObject fireImfect = GameObject.Instantiate(m_fireImfect);
        fireImfect.transform.position = firePointTransfrom.position;

        


    }

}
