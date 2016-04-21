using UnityEngine;
using System.Collections;

public class Firearm : MonoBehaviour
{
	//public Transform target;

	public GameObject projectile;
	public Transform shotPoint;

	//temp for debugging.
	public Transform target;

	public void FireWeapon (Vector3 dir)
	{
		StartCoroutine (Fire (dir));
	}

	/*	void Update ()
	{
		Debug.DrawRay (shotPoint.position, target.position - shotPoint.position, Color.green);
	}*/

	private IEnumerator Fire (Vector3 direction)
	{
		if (projectile != null) {
			//yield return new WaitForSeconds (.58f);
			Vector3 projPos = transform.TransformPoint (shotPoint.localPosition);

			//transform.LookAt (direction);
			//Vector3 normalDir = direction.normalized * 20f;
			//Vector3 toTarget = direction - transform.position;
			Projectile proj = ((GameObject)Instantiate (projectile, projPos, Quaternion.LookRotation (transform.forward))).GetComponent<Projectile> ();//Quaternion.LookRotation (toTarget));
		}
		yield return null;
	}
}
