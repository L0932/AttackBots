using UnityEngine;
using System.Collections;

public class SteeringEntity : MonoBehaviour
{

	private float mass = 1;

	public float Mass {
		get { return mass; }
		set { mass = Mathf.Max (0, value); }
	}

	public Vector3 GetSeekVector (Vector3 target, bool considerVelocity = false)
	{

		var force = Vector3.zero;

		return force;
		//var difference = target - position;
	}
}
