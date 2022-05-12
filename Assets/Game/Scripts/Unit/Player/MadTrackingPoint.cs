using UnityEngine;

public class MadTrackingPoint : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_offset;
    
    public Vector2 offset { get { return m_offset; } }
    private bool m_isRight;
    public bool isRight { set { m_isRight = value; } get { return m_isRight; } }

    private Mad m_madHandler;
    

    public void Init(bool isRight, Mad madHandler)
    {
        UpdateOffset(isRight);
        m_madHandler = madHandler;
    }

    public void UpdateOffset(bool isPlayerDirRight)
    {
        Vector2 dirOffset = offset;
        if (isPlayerDirRight)
            dirOffset.x = -offset.x;

        transform.localPosition = (Vector3)dirOffset;
        isRight = isPlayerDirRight;
    }

}
