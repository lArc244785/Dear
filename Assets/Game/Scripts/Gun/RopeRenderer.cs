using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    #region lineRenderer
    private LineRenderer m_lineRenderer;

    public LineRenderer lineRenderer
    {
        get
        {
            return m_lineRenderer;
        }
    }

    #endregion

    #region precision
    [SerializeField]
    private int m_precision;
    private int precision
    {
        get
        {
            return m_precision;
        }
    }
    #endregion

    #region waveSize
    [SerializeField]
    private int m_waveSize;
    private int waveSize
    {
        get
        {
            return m_waveSize;
        }
    }
    #endregion

    #region ropeAnimationCurve
    [SerializeField]
    private AnimationCurve m_ropeAnimationCurve;
    private AnimationCurve ropeAnimationCurve
    {
        get
        {
            return m_ropeAnimationCurve;
        }
    }
    #endregion

    #region precisionDeltas
    private float[] m_precisionDeltas;
    private float[] precisionDeltas
    {
        get
        {
            return m_precisionDeltas;
        }
    }
    #endregion

    #region grapplingGun
    private GrapplingGun m_grapplingGun;
    private GrapplingGun grapplingGun
    {
        get
        {
            return m_grapplingGun;
        }
    }
    #endregion

    #region targetPos
    private Vector2[] m_targetPoss;
    private Vector2[] targetPoss
    {
        get { return m_targetPoss; }
    }
    #endregion

    private bool isDraw { set; get; }


    private float m_firePointToTargetPosDistacen;
    private float firePointToTargetPosDistacen
    {
        get
        {
            return m_firePointToTargetPosDistacen;
        }
    }

    [SerializeField]
    private float m_width;
    private float widht
    {
        get
        {
            return m_width;
        }
    }

    public void Init(GrapplingGun grapplingGun)
    {
        m_grapplingGun = grapplingGun;
        m_lineRenderer = GetComponent<LineRenderer>();
        m_precisionDeltas = new float[precision];
        m_targetPoss = new Vector2[precisionDeltas.Length];

        for (int i = 0; i < m_precisionDeltas.Length; i++)
        {
            m_precisionDeltas[i] = (float)i / (float)(m_precisionDeltas.Length - 1);
            //Debug.Log("m_precisionDeltas [" + i +"] : "+ m_precisionDeltas[i]);
        }

        SetDraw(false);
    }

    public void DrawInit(Vector2 firePoint, Vector2 dir)
    {
        lineRenderer.positionCount = precisionDeltas.Length;

        Vector2 dirPerpendicular = Vector2.Perpendicular(dir);
        Vector2 offset = new Vector2();

        m_firePointToTargetPosDistacen = Vector2.Distance(firePoint, grapplingGun.targetPos);

        for (int i = 0; i  < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, firePoint);
        }


        for (int i = 0; i < precisionDeltas.Length; i++)
        {
            offset = dirPerpendicular * ropeAnimationCurve.Evaluate(precisionDeltas[i]) * waveSize;
            m_targetPoss[i] = Vector2.Lerp(firePoint, grapplingGun.targetPos, precisionDeltas[i]) + offset;
        }

        SetDraw(true);
    }

    private void Update()
    {
        if (!isDraw)
            return;

        lineRenderer.SetWidth(widht, widht);


        if(grapplingGun.currentState == GrapplingGun.State.Fire)
        {
            RopeWaveUpdate();
        }
        else if(grapplingGun.currentState == GrapplingGun.State.Pull)
        {
            RopeStraightUpdate();
        }

        

    }

    private void SetDraw(bool draw)
    {
        isDraw = draw;
        lineRenderer.enabled = isDraw;
    }

    public void OffDraw()
    {
        SetDraw(false);
    }


    private void RopeWaveUpdate()
    {
        float delta = Vector2.Distance(grapplingGun.fireTr.position, grapplingGun.hook.transform.position) / firePointToTargetPosDistacen;
        
        Vector2 currentPos = new Vector2();
        for (int  i= 0; i < lineRenderer.positionCount; i ++)
        {
            currentPos = Vector2.Lerp((Vector2)grapplingGun.fireTr.position, targetPoss[i], delta);
            lineRenderer.SetPosition(i, (Vector3)currentPos);
        }
    }

    private void RopeStraightUpdate()
    {
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, grapplingGun.fireTr.position);
        lineRenderer.SetPosition(1, grapplingGun.hook.transform.position);
    }

}
