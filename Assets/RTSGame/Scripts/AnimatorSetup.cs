﻿using UnityEngine;
using System.Collections;

public class AnimatorSetup
{

	public float speedDampTime = 0.1f;
	// Damping time for the Speed parameter.
	public float angularSpeedDampTime = 0.7f;
	// Damping time for the AngularSpeed parameter
	public float angleResponseTime = 0.6f;
	// Response time for turning an angle into angularSpeed.


	private Animator anim;
	// Reference to the animator component.
	private HashIDs hash;
	// Reference to the HashIDs script.


	// Constructor
	public AnimatorSetup (Animator animator)
	{
		anim = animator;
		hash = new HashIDs ();
	}

	public void Move (float speed, float angle)
	{
		// Angular speed is the number of degrees per second.
		float angularSpeed = angle / angleResponseTime;

		// Set the mecanim parameters and apply the appropriate damping to them.
		anim.SetFloat (hash.speedFloat, speed, speedDampTime, Time.deltaTime);
		anim.SetFloat (hash.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
	}

	public void Shoot (bool boolState)
	{
		anim.SetBool (hash.playerInSightBool, boolState);
	}

	public void Die (bool boolState)
	{
		//anim.SetBool (hash.deadBool, boolState);
		anim.Play ("Dying");
	}

	public void SetSpeed (float _speed)
	{
		anim.SetFloat (hash.speedFloat, _speed);
	}
}
