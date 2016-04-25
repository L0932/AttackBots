using UnityEngine;
using System.Collections;

public class IKHandling : MonoBehaviour
{

	Animator animator;
	public float animationSpeed;
	public AnimationClip[] animationClips;

	public bool ikActive;
	public bool ikLookAtActive;

	public float ikWeight = 1;

	public Transform leftIKTarget;
	public Transform rightIKTarget;

	public Transform leftHandIKTarget;
	public Transform rightHandIKTarget;

	public Transform hintElbowLeft;
	public Transform hintElbowRight;

	public Transform hintLeft;
	public Transform hintRight;

	public float handWeight;
	public float lookIKWeight;
	public float bodyWeight;
	public float headWeight;
	public float eyesWeight;
	public float clampWeight;

	public Transform lookPos;

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
		animationClips = animator.runtimeAnimatorController.animationClips;
		animationSpeed = 1f;
		//animationClips[22].
		//animationClips [22].sp = animationSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.DrawRay (transform.position, (lookPos.position - transform.position), Color.green);
	}

	void OnAnimatorIK ()
	{
		//Debug.Log ("Right Hand T Val: " + anim.GetFloat ("Animator.Right Hand T.y"));

		//Debug.Log (anim.GetBoneTransform (HumanBodyBones.RightHand).transform.position.y);
		//Debug.Log (anim.GetFloat ("AimWeightIK"));

		animator.speed = animationSpeed;

		handWeight = animator.GetFloat ("AimWeightIK");
		bodyWeight = handWeight * 2.5f;
		headWeight = bodyWeight * 2f;

		// Get angle between target and this object's forward vector

		if (ikLookAtActive && ikActive) {
			Vector3 toTargetPos = lookPos.position - transform.position;
			float angle = Vector3.Angle (toTargetPos, transform.forward);
			Debug.Log (angle);
		}

		if (ikLookAtActive) {
			animator.SetLookAtWeight (lookIKWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
			animator.SetLookAtPosition (lookPos.position);
		}

		if (ikActive) {
			animator.SetIKPositionWeight (AvatarIKGoal.RightHand, handWeight);
			animator.SetIKPosition (AvatarIKGoal.RightHand, (lookPos.position - transform.position));//(lookPos.position - transform.position));

			animator.SetIKHintPositionWeight (AvatarIKHint.RightElbow, ikWeight);
			animator.SetIKHintPosition (AvatarIKHint.RightElbow, hintElbowRight.position);

			//animator.SetIKRotationWeight (AvatarIKGoal.RightHand, handWeight);
			//animator.SetIKRotation (AvatarIKGoal.RightHand, rightHandIKTarget.rotation);
		}

		/*	
		Vector3 relativeToTarget = lookPos.position - transform.position;
		relativeToTarget.y = 0;
		transform.rotation = Quaternion.LookRotation (relativeToTarget);
		//animator.bodyRotation = Quaternion.LookRotation (relativeToTarget);
		*/

		/*
		//*** Left and Right Feet ***
		//Weights: Left and Right Feet
		anim.SetIKPositionWeight (AvatarIKGoal.LeftFoot, ikWeight);
		anim.SetIKPositionWeight (AvatarIKGoal.RightFoot, ikWeight);

		//Positions: Left and Right Feet
		anim.SetIKPosition (AvatarIKGoal.LeftFoot, leftIKTarget.position);
		anim.SetIKPosition (AvatarIKGoal.RightFoot, rightIKTarget.position);

		//RotationWeights: Left and Right Feet
		anim.SetIKRotationWeight (AvatarIKGoal.LeftFoot, ikWeight);
		anim.SetIKRotationWeight (AvatarIKGoal.RightFoot, ikWeight);

		//Rotation: Left and Right Feet
		anim.SetIKRotation (AvatarIKGoal.LeftFoot, leftIKTarget.rotation);
		anim.SetIKRotation (AvatarIKGoal.RightFoot, rightIKTarget.rotation);
		//*****

		//*** Left and Right Knee ***
		//Weights: Left and Right Knee
		anim.SetIKHintPositionWeight (AvatarIKHint.LeftKnee, ikWeight);
		anim.SetIKHintPositionWeight (AvatarIKHint.RightKnee, ikWeight);

		//Positions: Left and Right Knee
		anim.SetIKHintPosition (AvatarIKHint.LeftKnee, hintLeft.position);
		anim.SetIKHintPosition (AvatarIKHint.RightKnee, hintRight.position);
		//*****
		*/
	}
}
