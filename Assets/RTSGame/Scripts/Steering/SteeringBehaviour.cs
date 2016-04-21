using UnityEngine;
using System.Collections;

public abstract class SteeringBehaviour : MonoBehaviour
{
	#region variables

	public float mass, maxVelocity;
	public Transform target;

	protected Vector3 position, velocity, acceleration, force;

	#endregion

	#region public properties

	public Vector3 Force {
		get { return force; }
	}

	///<summary>>
	/// Steering event handler for arrival notification
	/// </summary>
	public System.Action<SteeringBehaviour> OnArrival = delegate {
	};

	///<summary>
	/// Steering event handler for moving notification
	/// </summary>
	public System.Action<SteeringBehaviour> OnStartMoving { get; set; }

	#endregion

	#region Methods

	protected abstract Vector3 CalculateForce ();

	public void truncate (Vector3 v, float max)
	{
		float i = max / v.magnitude;
		i = i < 1.0f ? i : 1.0f;
		v *= i;
	}

	public abstract void Move ();

	#endregion
}