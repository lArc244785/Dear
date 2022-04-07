using UnityEngine;

public class CoyoteSystem 
{
    private MovementData m_classicMovementData;


    #region Ground Time
    private float m_lastOnGroundTime;
    public float lastOnGroundTime
    {
        get
        {
            return m_lastOnGroundTime;
        }
        private set
        {
            m_lastOnGroundTime = value;
        }
    }
    #endregion

    #region Wall Time
    private float m_lastOnWallRightTime;
    public float lastOnWallRightTime 
    { 
        get 
        { 
            return m_lastOnWallRightTime; 
        } 
        private set 
        { 
            m_lastOnWallRightTime = value; 
        } 
    }

    private float m_lastOnWallLeftTime;
    public float lastOnWallLeftTime 
    { 
        get 
        { 
            return m_lastOnWallLeftTime; 
        } 
        private set 
        { 
            m_lastOnWallLeftTime = value; 
        } 
    }

    private float m_lastOnWallTime;
    public float lastOnWallTime 
    { 
        get 
        { 
            return m_lastOnWallTime; 
        } 
        private set
        {
            m_lastOnWallTime = value;
        }
    }
    #endregion

    #region Jump Time
    private float m_lastJumpEnterTime;
    public float lastJumpEnterTime
    {
        get
        {
            return m_lastJumpEnterTime;
        }
        private set
        {
            m_lastJumpEnterTime = value;
        }
    }

    private float m_lastJumpExitTime;
    public float lastJumpExitTime 
    { 
        set 
        { 
            m_lastJumpExitTime = value; 
        } 
        get 
        { 
            return m_lastJumpExitTime; 
        } 
    }
    #endregion



    public void Init(MovementData classicMovementData)
    {
        m_classicMovementData = classicMovementData;
    }

    #region Ground
    public void OnGroundTimer()
    {
        lastOnGroundTime = m_classicMovementData.coyoteTime;
    }
    public void GroundCoyoteTime()
    {
        lastOnGroundTime -= Time.deltaTime;
    }
    public void ResetGroundTime()
    {
        lastOnGroundTime = 0.0f;
    }
    #endregion 

    #region Wall
    public void OnWallLeftTime()
    {
        Debug.Log("L");
        lastOnWallLeftTime = Time.deltaTime;
        lastOnWallTime = lastOnWallLeftTime;
    }

    public void ResetWallLeftTime()
    {
        lastOnWallLeftTime = 0.0f;
    }


    public void OnWallRightTime()
    {
        Debug.Log("R");
        lastOnWallRightTime = Time.deltaTime;
        lastOnWallTime = lastOnWallRightTime;
    }
    public void ResetWallRightTime()
    {
        lastOnWallRightTime = 0.0f;
    }



    public void WallCoyoteTime()
    {
        lastOnWallLeftTime -= Time.deltaTime;
        lastOnWallRightTime -= Time.deltaTime;

        lastOnWallTime = Mathf.Max(lastOnWallLeftTime, lastOnWallRightTime);
    }

    #endregion


    #region Jump
    public void OnJumpEnterTime()
    {
        lastJumpEnterTime = m_classicMovementData.jumpBufferTime;
    }

    public void JumpCoyoteTime()
    {
        lastJumpEnterTime -= Time.deltaTime;
    }

    public void ResetJumpEnterTime()
    {
        lastJumpEnterTime = 0.0f;
    }


    public void OnJumpExitTime()
    {
        lastJumpExitTime = m_classicMovementData.jumpBufferTime;
    }
    public void JumpCutCoyoteTime()
    {
        lastJumpExitTime -= Time.deltaTime;
    }
    public void ResetJumpExitTime()
    {
        lastJumpExitTime = 0.0f;
    }

    #endregion

}
