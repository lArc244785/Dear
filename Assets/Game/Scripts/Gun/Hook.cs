using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private Rigidbody2D m_rig2D;
    private float m_speed;

    private GrapplingGun m_grapplingGun;


    [SerializeField]
    private LayerMask m_grapplingLayerMask;




    private Vector2 m_dir;


    public void init(float speed, GrapplingGun gun)
    {
        m_rig2D = GetComponent<Rigidbody2D>();
        m_grapplingGun = gun;
        m_speed = speed;
    }

    public void Fire(Vector2 dir)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        transform.parent = null;

        m_dir = dir;
        m_rig2D.velocity = m_dir * m_speed;


    }


    private void Update()
    {
        if (m_grapplingGun.m_eState == GrapplingGun.E_State.E_NONE)
            return;

        if(m_grapplingGun.m_eState == GrapplingGun.E_State.E_HOOKFIRE)
        {
            if(Physics2D.Raycast(transform.position, m_dir, 0.3f,m_grapplingLayerMask))
            {
                m_rig2D.velocity = Vector2.zero;
                m_grapplingGun.Grappling();
                
            }
        }


    }

    public void Reset(Transform parent)
    {
        transform.parent = parent;

        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.identity;

        m_rig2D.velocity = Vector2.zero;
    }






}
