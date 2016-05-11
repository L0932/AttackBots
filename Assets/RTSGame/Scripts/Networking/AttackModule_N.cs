using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AttackModule_N : AttackModule {

	GG_AnimationIK animationIK;
	public Model robotModel;

	public override void OnStartAuthority(){
		Debug.Log ("This AttackModule_N has authority!");
	}

	protected override void Awake ()
	{
		base.Awake ();
		animationIK = GetComponentInChildren<GG_AnimationIK> ();//GetComponent<GG_AnimationIK> ();
		animationIK.Event_OnShoot +=  OnShootStart;

		robotModel = GetComponentInChildren<Model> ();
		currentFireArm = GetComponentInChildren<Firearm_N> ();
	}

	// Use this for initialization
	public override void Update ()
	{
		// Begin Shooting if...
		if (attackNearbyThreats && detectedThreats.Count != 0 && activeNearbyThreatTarget != null) {
			if (!isCurrentlyShooting) {
				StartCoroutine (ShootTarget (detectedThreats [0].transform));
			}
		}

		//While shooting..
		if(isCurrentlyShooting && activeNearbyThreatTarget != null){
			robotModel.RotateModelTo (activeNearbyThreatTarget.position);
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
				//unitController.RotateTowardsTarget (activeNearbyThreatTarget.position - transform.position);

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

	// Callback timed with animation event
	public override void OnShootStart(){
		Debug.Log ("Calling networking firing command");
		CmdFireWeapon ();
	}

	// For Networking purposes
	[Command]
	public void CmdFireWeapon(){
		currentFireArm.FireWeapon (activeNearbyThreatTarget.position - transform.position);
	}
}
