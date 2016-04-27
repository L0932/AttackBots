using UnityEngine;
using System.Collections;

public class GG_Animation : MonoBehaviour
{
	public float deadZone = 5f;
	// The number of degrees for which the rotation isn't controlled by Mecanim.

	//public Transform targetTempTest;
	public Animator animator;
	private AnimatorSetup animatorSetup;
	private AttackModule attackModule;
	//private DoneAnimatorSetup animatorSetup;

	private GG_AIPath aiPath;
	//private DoneHashIDs hashIDs;
	void Awake ()
	{
		animator = GetComponent<Animator> ();
		animatorSetup = new AnimatorSetup (animator);
		attackModule = GetComponent<AttackModule> ();
		//hashIDs = new DoneHashIDs();
		// animatorSetup = new DoneAnimatorSetup(animator, hashIDs);

		aiPath = GetComponent<GG_AIPath> ();

		// Set the weights for the shooting and gun layers to 1.
		animator.SetLayerWeight (1, 1f);
		animator.SetLayerWeight (2, 1f);

		deadZone *= Mathf.Deg2Rad;
		//NavAnimSetup();
	}

	/*
    void Update()
    {
        NavAnimSetup();
    }*/

	void OnAnimatorMove ()
	{
		transform.rotation = animator.rootRotation;
	}

	public void MoveToTarget (Transform _target)
	{
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		Vector3 toTarget = _target.position - transform.position;

		//Debug.Log ("Forward " + transform.forward);
		Debug.Log ("Dot product value: " + Vector3.Dot (forward, toTarget));
	}

	public virtual void NavAnimSetup (Vector3 dir)
	{

		// Create the parameters to pass to the helper function.
		float speed;
		float angle;

		// if (aiPath.target == null) Debug.Log("target is null now!");
 
		Vector3 transformPos = transform.position;//aiPath.GetFeetPosition();

		//Vector3 desiredVelocity = (targetTempTest.position - transformPos);//transform.position);


		speed = Vector3.Project (dir, transform.forward).magnitude;
		angle = FindAngle (transform.forward, dir, transform.up);

		/*
		if (Mathf.Abs (angle) < deadZone) {
			//transform.LookAt (transformPos + dir);
			//angle = 0f;
		}*/

		/*
        // If the player is in sight...
        if (enemySight.playerInSight)
        {
            // ... the enemy should stop...
            speed = 0f;

            // ... and the angle to turn through is towards the player.
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
        }
        else
        {
            // Otherwise the speed is a projection of desired velocity on to the forward vector...
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;

            // ... and the angle is the angle between forward and the desired velocity.
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);

            // If the angle is within the deadZone...
            if (Mathf.Abs(angle) < deadZone)
            {
                // ... set the direction to be along the desired direction and set the angle to be zero.
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0f;
            }
        }*/
		// Call the Setup function of the helper class with the given parameters.
		animatorSetup.Move (speed, angle);
	}

	public void SetSpeed (float _speed)
	{
		animatorSetup.SetSpeed (_speed);
	}

	public void Shoot (bool boolState)
	{
		animatorSetup.Shoot (boolState);
	}

	public void Die (bool boolState)
	{
		animatorSetup.Die (boolState);
	}

	public void StopMovement ()
	{
		animatorSetup.SetSpeed (0f);
	}

	float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
	{
		// If the vector the angle is being calculated to is 0...
		if (toVector == Vector3.zero)
            // ... the angle between them is 0.
            return 0f;

		// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
		float angle = Vector3.Angle (fromVector, toVector);

		// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
		Vector3 normal = Vector3.Cross (fromVector, toVector);

		// The dot product of the normal with the upVector will be positive if they point in the same direction.
		angle *= Mathf.Sign (Vector3.Dot (normal, upVector));

		// We need to convert the angle we've found from degrees to radians.
		angle *= Mathf.Deg2Rad;

		return angle;
	}
		
	/*
	// Animation Events
	public void OnStartShootAnimationEvent (AnimationEvent val)
	{
		Debug.Log ("OnStartShootAnimationEvent() called in GG_Animation");
	}

	public void OnShootAnimationEvent (AnimationEvent val)
	{
		Debug.Log ("OnShootAnimationEvent() called in GG_Animation");
	}

	public void OnStartWeaponRaiseAnimationEvent (AnimationEvent val)
	{
		Debug.Log ("OnWeaponStartRaiseAnimationEvent() called in GG_Animation");
	}

	public void OnWeaponRaiseAnimationEvent (AnimationEvent val)
	{
		Debug.Log ("OnWeaponRaiseAnimationEvent() called in GG_Animation");
	}

	public void OnEndWeaponRaiseAnimationEvent (AnimationEvent val)
	{
		Debug.Log ("OnEndWeaponRaiseAnimationEvent() called in GG_Animation");
	}
	*/
}
