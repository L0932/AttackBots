using UnityEngine;
using System.Collections;

public class SeekBehaviour : SteeringBehaviour
{

	void Start ()
	{
		position = transform.position;
	}

	void Update ()
	{
		//position = transform.position;
		//Move ();
		//transform.position = position;
	}

	public override void Move ()
	{
		velocity = (target.position - position).normalized * maxVelocity;
		position = position + velocity;
		Debug.Log ("Updating Movement..");
	}

	protected override Vector3 CalculateForce ()
	{
		return (target = null) ? Vector3.zero : Vector3.zero;
	}
}
