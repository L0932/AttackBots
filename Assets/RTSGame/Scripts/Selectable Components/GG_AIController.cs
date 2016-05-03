using UnityEngine;
using System.Collections;

public class GG_AIController : GG_Controller
{
	public Firearm primaryWeapon;

	private bool playerUnitDetected;
	private GG_Animation animationComponent;

	void Start ()
	{
		animationComponent = GetComponent<GG_Animation> ();
		playerUnitDetected = false;

		//animationComponent.Die (true);

		base.Start ();
	}

	void Update ()
	{
		if (canMove) {
			Vector3 dir = CalculateVelocity (GetFeetPosition ());

			if (animationComponent != null) {

				animationComponent.NavAnimSetup (targetDirection);

				if(playerUnitDetected) {
					animationComponent.Shoot (true);

					if (primaryWeapon != null) {
						primaryWeapon.FireWeapon (targetDirection);
					}

					playerUnitDetected = false;
				}
			}



			//Rotate towards targetDirection (filled in by CalculateVelocity)
			RotateTowards (targetDirection);
			controller.SimpleMove (targetDirection);
		}
	}

	public override void OnSelected ()
	{
		//throw new System.NotImplementedException ();
		//Debug.Log ("Selected");
	}

	public override void OnDeselected ()
	{
		//Debug.Log ("Not Selected");
		//throw new System.NotImplementedException ();
	}

	public override Vector3 GetFeetPosition ()
	{
		return tr.position;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("PlayerUnit")) {
			playerUnitDetected = true;
			target = other.transform;
		}
	}

	public override void OnPlayerCommand (MouseTarget _target)
	{
		//Do nothing.. for now.
	}
}
