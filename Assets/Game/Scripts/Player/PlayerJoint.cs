using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoint : MonoBehaviour
{
    [SerializeField]
    private FixedJoint2D m_bodyJoint;

    [SerializeField]
    private FixedJoint2D m_shoulderJoint;
    private Rigidbody2D m_shoulderRig2D;
    

    [SerializeField]
    private FixedJoint2D m_handJoint;
    private Rigidbody2D m_handRig2D;

    [SerializeField]
    private ShoulderMovement m_shoulderMovement;

    public void Start()
    {
        init();
        Connect(false);
    }

    private void init()
    {
        m_shoulderRig2D = m_shoulderJoint.GetComponent<Rigidbody2D>();
        m_handRig2D = m_handJoint.GetComponent<Rigidbody2D>();
    }



    public void Connect(bool isConnet)
    {
        RigidbodyType2D bodyType = RigidbodyType2D.Dynamic;

        if (!isConnet)
        {
            bodyType = RigidbodyType2D.Kinematic;
        }

        m_bodyJoint.enabled = isConnet;

        m_shoulderJoint.enabled = isConnet;
        m_shoulderRig2D.bodyType = bodyType;

        m_handJoint.enabled = isConnet;
        m_handRig2D.bodyType = bodyType;

    }


}
