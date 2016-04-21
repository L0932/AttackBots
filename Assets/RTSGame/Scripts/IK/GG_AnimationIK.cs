using UnityEngine;
using System.Collections;

[System.Serializable]
public struct IKWeights
{
	public float handleWeight;
	public float lookWeight;
	public float bodyWeight;
	public float headWeight;
	public float eyesWeight;
	public float clampWeight;
}

public class GG_AnimationIK : GG_Animation
{
	public float ikSpeed;
	public IKWeights ikWeights;
	//public Transform rightHandIKHandle;

	private Vector3 shootTarget;
	private bool shooting;
	//private Animator anim;

	private float startIKTime;
	private Vector3 originalHandlePos;
	private float lengthToDestination;

	// Update is called once per frame
	void Update ()
	{
	}

	/*
	public override void AddNearbyThreat ()
	{
		Debug.Log ("Subclass function called!! in AnimationIK");
	}

	public virtual void RemoveNearbyThreat ()
	{
		Debug.Log ("Subclass function called!! in animationIK");
	}*/

	public override void NavAnimSetup (Vector3 dir)
	{
		base.NavAnimSetup (dir); 
	}

	public void ShootTarget (Transform _target, float _time)
	{

		Vector3 toTargetPos = _target.position - transform.position;//(transform.position + transform.forward);
		float angle = Vector3.Angle (toTargetPos, transform.forward);

		if (angle < 135) {
			animator.Play ("WeaponShoot");
		
			shootTarget = _target.position + (_target.up * 1.5f);
			StartCoroutine (SetIKSystemTime (_time));
		} 
		//Debug.Log ("Angle to Target: " + angle);
	}

	IEnumerator SetIKSystemTime (float _time)
	{
		//yield return new WaitForSeconds (f);
		shooting = true;
		yield return new WaitForSeconds (_time);
		shooting = false;

		Debug.Log ("shooting set to false");
		yield return null;
	}

	void OnAnimatorIK ()
	{
		if (shooting) {

			Vector3 toTargetPos = shootTarget - transform.position;
			float angle = Vector3.Angle (toTargetPos, transform.forward);

			//animator.speed = speed;

			if (angle < 135) {

				ikWeights.handleWeight = animator.GetFloat ("AimWeightIK");
				//ikWeights.bodyWeight = ikWeights.handleWeight * 5f;
				//ikWeights.headWeight = ikWeights.bodyWeight * 2f;

				ikWeights.bodyWeight = ikWeights.handleWeight * 10f;
				ikWeights.headWeight = ikWeights.bodyWeight;

				//shooting = false;
				//Lerp the IK handle to target
				//float distCovered = (Time.time - startIKTime) * ikSpeed;
				//float fracToDestination = distCovered / lengthToDestination;
				//rightHandIKHandle.position = Vector3.Lerp (originalHandlePos, shootTarget.position, fracToDestination);

				animator.SetLookAtWeight (ikWeights.lookWeight, ikWeights.bodyWeight, ikWeights.headWeight, ikWeights.eyesWeight, ikWeights.clampWeight);
				animator.SetLookAtPosition (shootTarget);

				animator.SetIKPositionWeight (AvatarIKGoal.RightHand, ikWeights.handleWeight);
				animator.SetIKPosition (AvatarIKGoal.RightHand, shootTarget);

				//anim.SetIKHintPositionWeight (AvatarIKHint.RightElbow, ikWeights.handleWeight);
				//anim.SetIKHintPosition (AvatarIKHint.RightElbow, hintElbowRight.position);

				//animator.SetIKRotationWeight (AvatarIKGoal.RightHand, ikWeights.handleWeight);
				//animator.SetIKRotation (AvatarIKGoal.RightHand, shootTarget);}
			}
		}
	}
}
