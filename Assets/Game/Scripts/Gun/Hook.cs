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

    private Vector2 m_targetPos;
    private Vector2 targetPos
    {
        get { return m_targetPos; }
    }

    private Vector2 m_targetDir;
    private Vector2 targetDir
    {
        get
        {
            return m_targetDir;
        }
    }


   public void Init(GrapplingGun grapplingGun)
    {
        m_grapplingGun = grapplingGun;
        m_rig2D = GetComponent<Rigidbody2D>();
    }


    public void Fire(Vector2 dir, Vector2 targetPos)
    {

        transform.parent = null;

        rig2D.bodyType = RigidbodyType2D.Dynamic;
        rig2D.velocity = Vector2.zero;

        m_targetPos = targetPos;

        m_targetDir = dir;
    }

    private Vector2 m_nextMovePos;
    private float m_playerToHookDistance;
    private float m_nextPlayerToHookDistance;

    private void Update()
    {
        if (m_grapplingGun == null)
            return;


        if(grapplingGun.currentState == GrapplingGun.State.Fire)
        {
            if(grapplingGun.TargetAcceleration(
                rig2D, 
                grapplingGun.data.hookAcclelation, 
                grapplingGun.data.hookVelocityPower, 
                targetDir, 
                grapplingGun.data.hookMaxSpeed,
                targetPos))
            {
                GrapplingGun.PickType pickType = grapplingGun.GetPickType("HookShlingshot");

                
                rig2D.velocity = Vector2.zero;
                rig2D.bodyType = RigidbodyType2D.Kinematic;
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
