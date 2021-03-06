﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Firearm_N : Firearm
{
	//public Transform target;
	public override void FireWeapon(Vector3 dir){
		StartCoroutine (Fire (dir));
	}

/*	[Command]
	public void CmdFireWeapon (Vector3 dir)
	{
		StartCoroutine (Fire (dir));
	}*/

	/*	void Update ()
	{
		Debug.DrawRay (shotPoint.position, target.position - shotPoint.position, Color.green);
	}*/


	void Start(){
		Debug.Log("Firearm_N isLocalPlayer is :" + isLocalPlayer);
	}
	private IEnumerator Fire (Vector3 direction)
	{
		if (projectile != null) {
			//yield return new WaitForSeconds (.58f);
			Vector3 projPos = transform.TransformPoint (shotPoint.localPosition);

			//transform.LookAt (direction);
			//Vector3 normalDir = direction.normalized * 20f;
			//Vector3 toTarget = direction - transform.position;

			GameObject _projectile = (GameObject)Instantiate (projectile, projPos, Quaternion.LookRotation (transform.forward));//Quaternion.LookRotation (toTarget));
			NetworkServer.Spawn (_projectile);
		}
		yield return null;
	}
}
