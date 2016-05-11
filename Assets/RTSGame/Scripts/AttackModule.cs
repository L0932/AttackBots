using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class AttackModule : NetworkBehaviour
{
	public Firearm currentFireArm;
	public bool attackNearbyThreats;
	public List<GameObject> detectedThreats;

	protected bool isCurrentlyShooting;


	public Transform activeNearbyThreatTarget;
	[SerializeField] public GG_PlayerUnitController unitController;
	//[SerializeField] protected Transform playerSelectedTarget;

	//protected MouseTarget playerSelectedTarget;

	/*
	public Transform PlayerSelectedTarget {
		get {
			return playerSelectedTarget;
		}

		set {
			//Externally, only rightClickTargets can be removed from activeThreatTarget. Internally, this C# property isn't used.
			if (playerSelectedTarget != null && playerSelectedTarget.tag == "RightClickTarget") {
				playerSelectedTarget.tag = "Untagged";
			}

			playerSelectedTarget = value;
		}
	}*/

	void OnStartAuthority(){
		Debug.Log ("This AttackModule has authority!");
	}

	 
	protected virtual void Awake ()
	{
		unitController = GetComponent<GG_PlayerUnitController> ();
	}

	// Update is called once per frame
	public virtual void Update ()
	{
		/*
		if (attackNearbyThreats && playerSelectedTarget == null) {
			PlayerSelectionUpdate (false);
		}*/
	}


	public void AddNearbyThreat (GameObject _threat)
	{
		detectedThreats.Add (_threat);

		if (activeNearbyThreatTarget == null) {
			activeNearbyThreatTarget = _threat.transform;
		}
	}

	public virtual void RemoveNearbyThreat (GameObject _threat)
	{
		detectedThreats.Remove (_threat);

/*		if (unitController.PlayerSelectedTarget.transform == _threat.transform) {
			if (detectedThreats.Count > 0) {
				unitController.PlayerSelectedTarget.transform = detectedThreats [0].transform;
			} else {
				unitController.PlayerSelectedTarget.transform = null;
			}
		}*/
	}

	public void OnShootAnimationEvent (AnimationEvent val)
	{
/*		
		if (activeNearbyThreatTarget != null) {
			Vector3 toTarget = activeNearbyThreatTarget.position - (transform.position + transform.forward); 
			currentFireArm.FireWeapon (toTarget);
		}

		Debug.Log ("OnShootAnimationEvent() called in AttackModule");*/
	}

	public virtual void OnShootStart()
	{
		//robotModel.
		//Debug.Log ("OnShootAnimationEvent called from attackModuleIK");
		currentFireArm.FireWeapon (activeNearbyThreatTarget.position - transform.position);
	}
}
