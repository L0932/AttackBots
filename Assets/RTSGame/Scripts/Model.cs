using UnityEngine;
using System.Collections;

public class Model : MonoBehaviour
{
	//public Firearm currentFireArm;
	// Use this for initialization

	public void RotateModelTo (Vector3 targetPos)
	{
		Vector3 toDirection = targetPos - transform.position;
		toDirection.y = 0;
		transform.rotation = Quaternion.LookRotation (toDirection);
	}
}
