using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMovementDatas/MovementData")]
public class MovementData : ScriptableObject
{
    //PHYSICS
    [Header("Gravity")]
    [SerializeField]
    private float m_gravityScale;
    public float gravityScale { get { return m_gravityScale; } }

    [SerializeField]
    private float m_fallGravityMult;//떨어질때의 중력값
    public float fallGravityMult { get { return m_fallGravityMult; } }

    [Header("Resistance")]
    [SerializeField]
    private float m_resistanceInAirAmount; //공중에서의 저항 값
    public float resistanceInAirAmount { get { return m_resistanceInAirAmount; } }

    [SerializeField]
    private float m_frictionAmount; //지상에서의 저항 값
    public float frictionAmount { get { return m_frictionAmount; } }

    [Header("Other Physics")]
    [SerializeField]
    [Range(0, 0.5f)]
    private float m_coyoteTime;
    public float coyoteTime { get { return m_coyoteTime; } }


    //GROUND
    [Header("Run")]
    [SerializeField]
    private float m_runMaxSpeed;
    public float runMaxSpeed { get { return m_runMaxSpeed; } }

    [SerializeField]
    private float m_runAccel;
    public float runAccel { get { return m_runAccel; } }

    [SerializeField]
    private float m_runDeccel;
    public float runDeccel { get { return m_runDeccel; } }

    [SerializeField]
    [Range(0, 1)]
    private float m_accelInAir;
    public float accelInAir { get { return m_accelInAir; } }

    [SerializeField]
    [Range(0, 1)]
    private float m_deccelInAir;
    public float deccelInAir { get { return m_deccelInAir; } }

    [Space(5)]
    [SerializeField]
    [Range(.5f, 2f)]
    private float m_accelPower;
    public float accelPower { get { return m_accelPower; } }

    [SerializeField]
    [Range(.5f, 2f)]
    private float m_stopPower;
    public float stopPower { get { return m_stopPower; } }

    [SerializeField]
    [Range(.5f, 2f)]
    private float m_turnPower;
    public float turnPower { get { return m_turnPower; } }


    //JUMP
    [Header("Jump")]
    [SerializeField]
    private float m_jumpForce;
    public float jumpForce { get { return m_jumpForce; } }

    [SerializeField]
    [Range(0, 1)]
    private float m_jumpCutMultiplier;
    public float jumpCutMultiplier { get { return m_jumpCutMultiplier; } }


    [Space(10)]
    [SerializeField]
    [Range(0, 0.5f)]
    private float m_jumpBufferTime;
    public float jumpBufferTime { get { return m_jumpBufferTime; } }

    [SerializeField]
    private int m_maxJumpCount;
    public int maxJumpCount { get { return m_maxJumpCount; } }

    [SerializeField]
    private float m_airJumpForce;
    public float airJumpForce { get { return m_airJumpForce; } }

    [Header("Wall Jump")]
    [SerializeField]
    private Vector2 m_wallJumpForce;
    public Vector2 wallJumpForce { get { return m_wallJumpForce; } }

    [Space(5)]
    [SerializeField]
    [Range(0f, 1f)]
    private float m_wallJumpRunLerp;
    public float wallJumpRunLerp { get { return m_wallJumpRunLerp; } }

    [SerializeField]
    [Range(0f, 1.5f)]
    private float m_wallJumpTime;
    public float wallJumpTime { get { return m_wallJumpTime; } }


    [Header("Wall Slide")]
    [SerializeField]
    private float m_wallSlideVelocity;
    public float wallSlideVelocity { get { return m_wallSlideVelocity; } }

    [Header("Climbing")]
    [SerializeField]
    private float m_climbingMaxSpeed;
    public float climbingMaxSpeed { get { return m_climbingMaxSpeed; } }

    [SerializeField]
    private float m_climbingAccel;
    public float climbingAccel { get { return m_climbingAccel; } }

    [SerializeField]
    private float m_climbingDeccel;
    public float climbingDeccel { get { return m_climbingDeccel; } }

    /*
	 * 현재는 사용하지 않는 파라미터 입니다.
	//ABILITIES
	[Header("Dash")]
	[SerializeField]
	private int m_dashAmount;
	public int dashAmount { get { return m_dashAmount; } }

	[SerializeField]
	private float m_dashSpeed;
	public float dashSpeed { get { return m_dashSpeed; } }

	[SerializeField]
	private float m_dashEndTime; 
	public float dashEndTime { get { return m_dashEndTime; } }

	[SerializeField]
	[Range(0, 0.5f)] 
	private float m_dashBufferTime;
	public float dashBufferTime { get { return m_dashBufferTime; } }
	*/

    //   [Header("Rope")]
    //   [SerializeField]
    //   private float m_ropeReboundPower;
    //   public float ropeReboundPower { get { return m_ropeReboundPower; } }

    //   [SerializeField]
    //   private float m_ropeReboundCoolTime;
    //   public float ropeReboundCoolTime { get { return m_ropeReboundCoolTime; } }

    //[Space(5)]
    //[SerializeField]
    //   private Vector2 m_ropeCancleJumpForce;
    //   public Vector2 ropeCancleJumpForce { get { return m_ropeCancleJumpForce; } }

    //   [SerializeField]
    //   private float m_ropeCancleReboundTime;
    //   public float ropeCancleReboundTime { get { return m_ropeCancleReboundTime; } }
    //[SerializeField]
    //[Range(0f, 1f)]
    //private float m_ropeCancleReboundRunLerp;
    //public float ropeCancleReboundRunLerp { get { return m_ropeCancleReboundRunLerp; } }

    [Header("Hit")]
    [SerializeField]
    private float m_hitImfectPower;
    public float hitImfectPower { get { return m_hitImfectPower; } }

    [SerializeField]
    private float m_hitImfectDuringTime;
    public float hitImfectDuringTime { get { return m_hitImfectDuringTime; } }

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float m_hitImfectRunLerp;
    public float hitImfectRunLerp { get { return m_hitImfectRunLerp; } }


    [Header("Ground Pound")]
    [SerializeField]
    private float m_groundPoundPower;
    public float groundPoundPower
    {
        get
        {
            return m_groundPoundPower;
        }
    }

    [SerializeField]
    private float m_groundPoundMoveY;
    public float groundPoundMoveY
    {
        get
        {
            return m_groundPoundMoveY;
        }
    }

    [SerializeField]
    private float m_groundPoundReadyTime;
    public float groundPoundReadyTime
    {
        get
        {
            return m_groundPoundReadyTime;
        }
    }

    [SerializeField]
    private AnimationCurve m_groundPoundReadyMoveYCurve;
    public AnimationCurve groundPoundReadyMoveYCurve
    {
        get { return m_groundPoundReadyMoveYCurve; }
    }

    [SerializeField]
    private int m_groundPoundReadyPathLenth;
    public int groundPoundReadyPathLenth
    {
        get { return m_groundPoundReadyPathLenth; }
    }
    [SerializeField]
    private float m_groundPoundLandingTime;
    public float groundPoundLandingTime
    {
        get
        {
            return m_groundPoundLandingTime;
        }
    }
    
}


