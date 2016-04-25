using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerUnitAttackModuleIK : PlayerUnitAttackModule
{
	//Inherited variables
	//public bool attackNearbyThreats;
	//public List<GameObject> detectedThreats;
	//public Transform activeNearbyThreatTarget;
	//[SerializeField] private Transform activeThreatTarget;

	//Inherited Properties
	// public Transform ActiveThreatTarget{get; set}

	GG_AnimationIK animationIK;
	private bool isCurrentlyShooting;

	void Start ()
	{
		animationIK = GetComponent<GG_AnimationIK> ();
	}

	void ShootTarget ()
	{
		if (unitController.PlayerSelectedTarget != null) {
			 
		}
	}

	public override void Update ()
	{
		if (attackNearbyThreats && detectedThreats.Count != 0 && activeNearbyThreatTarget != null) {
			if (!isCurrentlyShooting) {
				StartCoroutine (ShootTarget (detectedThreats [0].transform));
			}
		}
	}

	IEnumerator ShootTarget (Transform _target)
	{
		isCurrentlyShooting = true;
		activeNearbyThreatTarget = _target;

		float animationTime = 2f;

		while (true) {
			if (activeNearbyThreatTarget != null) {
				//Vector3 toTarget = activeNearbyThreatTarget.position - (transform.position + transform.forward); 
				//currentFireArm.FireWeapon (toTarget);

				animationIK.ShootTarget (activeNearbyThreatTarget, animationTime);
				unitController.RotateTowardsTarget (activeNearbyThreatTarget.position - transform.position);

				yield return new WaitForSeconds (animationTime);
			} else {
				/*
				RemoveNearbyThreat (activeNearbyThreatTarget.gameObject);
				if (detectedThreats.Count > 0) {
					activeNearbyThreatTarget = detectedThreats [0].transform;	
				} else {
					isCurrentlyShooting = false;
					yield break; // exit coroutine
				}
				*/

				Debug.Log ("ActiveNearbyThreat is now false");
				isCurrentlyShooting = false;
				yield break;
			}
		}
	}

	public override void AddNearbyThreat (GameObject _threat)
	{
		//Debug.Log ("Target detected");
		detectedThreats.Add (_threat);

		if (activeNearbyThreatTarget == null) {
			activeNearbyThreatTarget = _threat.transform;
		}
			
		if (_threat == unitController.PlayerSelectedTarget.transform.gameObject) {
			if (isCurrentlyShooting) {
				StopCoroutine ("ShootTarget");
			}
			StartCoroutine (ShootTarget (unitController.PlayerSelectedTarget.transform));
		}
	}

	public override void RemoveNearbyThreat (GameObject _threat)
	{
		//TODO: if the player has an enemy still selected, have this unit execute a follow until it 
		//finds the enemy if this unit is set to follow targets. 

		//Debug.Log ("Remove Nearby Threat called");

		if (_threat.transform == activeNearbyThreatTarget.transform) {
			activeNearbyThreatTarget = null; 
		}
		detectedThreats.Remove (_threat);
	}
}
