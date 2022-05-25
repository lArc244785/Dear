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


    #region grapplingGun
    private GrapplingGun m_grapplingGun;
    private GrapplingGun grapplingGun
    {
        get { return m_grapplingGun; }
    }
    #endregion

    #region targetPos
    private Vector2 m_targetPos;
    private Vector2 targetPos
    {
        get { return m_targetPos; }
    }
    #endregion

    private float currentTime { set; get; }

    private Vector2 m_startPos;
    private Vector2 startPos
    {
        get
        {
            return m_startPos;
        }
    }


    public void Init(GrapplingGun grapplingGun)
    {
        m_grapplingGun = grapplingGun;
    }


    public void Fire(Vector2 startPos, Vector2 targetPos)
    {
        transform.parent = null;
        m_startPos = startPos;

        currentTime = 0.0f;

        m_targetPos = targetPos;
    }

    private void Update()
    {
        if (m_grapplingGun == null)
            return;

        if (grapplingGun.currentState == GrapplingGun.State.Fire)
        {
            if (grapplingGun.isContant(transform.position, targetPos))
            {
                GrapplingGun.PickType pickType = grapplingGun.GetPickType("HookShlingshot");
                grapplingGun.HookPick(pickType);
                return;
            }

            currentTime += Time.deltaTime;
            grapplingGun.LerpMovement(transform, startPos, targetPos, currentTime, grapplingGun.data.hookSpeed, grapplingGun.data.hookProgessionCurve);

        }

    }



}



