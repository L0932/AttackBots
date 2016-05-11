using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Firearm : NetworkBehaviour
{
	//public Transform target;
	public GameObject projectile;
	public Transform shotPoint;

	//temp for debugging.
	public Transform target;

	public virtual void FireWeapon (Vector3 dir)
	{
		StartCoroutine (Fire (dir));
	}

	private IEnumerator Fire (Vector3 direction)
	{
		//Debug.Log ("Saucy");
		if (projectile != null) {
			//yield return new WaitForSeconds (.58f);
			Vector3 projPos = transform.TransformPoint (shotPoint.localPosition);

			//transform.LookAt (direction);
			//Vector3 normalDir = direction.normalized * 20f;
			//Vector3 toTarget = direction - transform.position;

			GameObject _projectile = (GameObject)Instantiate (projectile, projPos, Quaternion.LookRotation (transform.forward));//Quaternion.LookRotation (toTarget));
			//NetworkServer.Spawn (_projectile);
		}
		yield return null;
	}
}
