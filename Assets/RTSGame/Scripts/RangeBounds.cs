﻿using UnityEngine;
using System.Collections;

public class RangeBounds : MonoBehaviour
{
	//public GG_PlayerUnitController unitController;
	public PlayerUnitAttackModule attackModule;
	public LayerMask detectableMask;
	// Use this for initialization
	void Start ()
	{
		attackModule = transform.parent.GetComponent<PlayerUnitAttackModule> ();

		if (attackModule == null) {
			Debug.LogError ("RangeBounds: an AttackModule needs to be attached to the parent of the object containing RangeBounds!");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter (Collider other)
	{
		if ((detectableMask.value & (1 << other.gameObject.layer)) > 0) {
			attackModule.AddNearbyThreat (other.gameObject);
			//unitController.detectedThreats.Add (other.gameObject);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if ((detectableMask.value & (1 << other.gameObject.layer)) > 0) {
			attackModule.RemoveNearbyThreat (other.gameObject);
			//unitController.detectedThreats.Remove (other.gameObject);
		}
	}
}
