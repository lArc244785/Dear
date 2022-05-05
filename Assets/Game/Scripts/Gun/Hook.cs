using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [Header("Gizmos")]
    [SerializeField]
    private Color m_gizmosColor;
    private Color gizmosColor
    {
        get
        {
            return m_gizmosColor;
        }
    }


    [Header("Physics")]
    [SerializeField]
    private float m_radius;
    [SerializeField]
    private LayerMask m_pickLayerMask;
    private LayerMask pickLayerMask
    {
        get
        {
            return m_pickLayerMask;
        }
    }

    private float radius { get { return m_radius; } }
    private GrapplingGun m_grapplingGun;
    private GrapplingGun grapplingGun
    {
        get { return m_grapplingGun; }
    }

    private Rigidbody2D m_rig2D;
    public Rigidbody2D rig2D
    {
        get { return m_rig2D;}
    }

   public void Init(GrapplingGun grapplingGun)
    {
        m_grapplingGun = grapplingGun;
        m_rig2D = GetComponent<Rigidbody2D>();
    }


    public void Fire(Vector2 dir, float speed)
    {
        transform.parent = null;

        rig2D.velocity = dir * speed;

    }


    private void Update()
    {
        if (m_grapplingGun == null)
            return;

        if(grapplingGun.currentState == GrapplingGun.State.Fire)
        {
            Collider2D pickCollider2D = Physics2D.OverlapCircle(transform.position, radius, pickLayerMask);
            if (pickCollider2D != null)
            {
                GrapplingGun.PickType pickType = grapplingGun.GetPickType(pickCollider2D.tag);


                rig2D.velocity = Vector2.zero;
                grapplingGun.HookPick(pickType);
            }
        }
     
    }


    


    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
