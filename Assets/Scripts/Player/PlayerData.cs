using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
	//PHYSICS
	[Header("Gravity")]
	public float gravityScale; 
	public float fallGravityMult;//떨어질때의 중력값

	[Header("Resistance")]
	public float resistanceInAirAmount; //공중에서의 저항 값
	public float frictionAmount; //지상에서의 저항 값


	[Header("Other Physics")]
	[Range(0, 0.5f)] public float coyoteTime; 


	//GROUND
	[Header("Run")]
	public float runMaxSpeed;
	public float runAccel;
	public float runDeccel;
	[Range(0, 1)] public float accelInAir;
	[Range(0, 1)] public float deccelInAir;
	[Space(5)]
	[Range(.5f, 2f)] public float accelPower;   
	[Range(.5f, 2f)] public float stopPower;
	[Range(.5f, 2f)] public float turnPower;


	//JUMP
	[Header("Jump")]
	public float jumpForce;
	[Range(0, 1)] public float jumpCutMultiplier;
	[Space(10)]
	[Range(0, 0.5f)] public float jumpBufferTime; 

	[Header("Wall Jump")]
	public Vector2 wallJumpForce;
	[Space(5)]
	[Range(0f, 1f)] public float wallJumpRunLerp;
	[Range(0f, 1.5f)] public float wallJumpTime;


	//WALL
	[Header("Wall Slide")]
	public float wallSlideVelocity;

	[Header("Climbing")]
	public float climbingAccel;
	public float climbingDeccel;




	//ABILITIES
	[Header("Dash")]
	public int dashAmount;
	public float dashSpeed;
	[Space(5)]
	public float dashAttackTime;
	public float dashAttackDragAmount;
	[Space(5)]
	public float dashEndTime; 
	[Range(0f, 1f)] public float dashUpEndMult; 
	[Range(0f, 1f)] public float dashEndRunLerp; 
	[Space(5)]
	[Range(0, 0.5f)] public float dashBufferTime;


	[Header("Rope")]
	public float ropeReboundPower;
	public float ropeReboundTime;
	public float reopCancleReboundPower;
	public float repeCancleReboundTime;

}


