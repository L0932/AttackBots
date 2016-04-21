using UnityEngine;
using System.Collections;

public class CharacterAnimatorMove : MonoBehaviour
{

	Animator animator;
	public Firearm fireArm;
	IKHandling ikHandler;
	public bool shoot;

	public float speed;

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
		ikHandler = GetComponent<IKHandling> ();

		if (shoot) {
			StartCoroutine (ShootLoop ());
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		animator.SetFloat ("Speed", speed);
	}

	IEnumerator ShootLoop ()
	{
		while (true) {
			animator.Play ("WeaponRaise");
			//fireArm.FireWeapon (ikHandler.rightHandIKTarget.position);
			yield return new WaitForSeconds (2f);
		}
	}

	public void Shoot ()
	{
		//animator.Play ("WeaponShoot");
		animator.Play ("WeaponRaise");
		//animator.SetBool ("PlayerInSight", true);
	}
}
