using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HealthScaler_N : HealthScaler {

	protected override void Update()
	{		
		base.Update ();
	}

	public override void AffectNormalizedHealth(float amount){

		if(!isServer)
			return;
		
		currentHealth += amount;

		Debug.Log("Health has been modified!!!!!!");
	}
}
