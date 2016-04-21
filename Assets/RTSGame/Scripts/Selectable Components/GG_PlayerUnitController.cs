using UnityEngine;
using System.Collections;

//[RequireComponent (typeof(ControllableComponent))]
public class GG_PlayerUnitController : GG_Controller
{
	/** Animation component.
 	* Should hold animations "awake" and "forward"
 	*/

	private MouseTarget playerSelectedTarget;

	public MouseTarget PlayerSelectedTarget {
		get {
			return playerSelectedTarget;
		}

		set {
			playerSelectedTarget = value;
		}
	}

	public PlayerUnitAttackModule attackModule;

	//public Animation anim;
	public HealthScaler healthScaler;

	/** Minimum velocity for moving */
	public float sleepVelocity = 0.4F;

	/** Speed relative to velocity with which to play animations */
	public float animationSpeed = 0.2F;

	/** Effect which will be instantiated when end of path is reached.
     * \see OnTargetReached */
	public GameObject endOfPathEffect;

	public GG_Animation animationComponent;

	//Will be refactored soon..
	//private ControllableComponent selectableComponent;

	public new void Start ()
	{
		//Prioritize the walking animation
		//anim["forward"].layer = 10;

		//Play all animations
		// anim.Play("awake");
		// anim.Play("forward");

		//Setup awake animations properties
		// anim["awake"].wrapMode = WrapMode.Clamp;
		// anim["awake"].speed = 0;
		// anim["awake"].normalizedTime = 1F;

		//Call Start in base script (AIPath)
		attackModule = GetComponent<PlayerUnitAttackModule> ();
		animationComponent = GetComponent<GG_Animation> ();

		//healthStatus.OnHealthZero += OnHealthZero;
		//selectableComponent = GetComponent<ControllableComponent> ();
		base.Start ();
	}

	void OnEnable ()
	{
		//HealthStatus.OnHealthZero += OnHealthZero;
	}

	void OnDisable ()
	{
		// HealthStatus.OnHealthZero -= OnHealthZero;
	}

	public override void OnSelected ()
	{
		EnableSearch ();
	}

	public override void OnDeselected ()
	{
		DisableSearch ();	
	}

	/** Point for the last spawn of #endOfPathEffect */
	protected Vector3 lastTarget;

	/**
     * Called when the end of path has been reached.
     * An effect (#endOfPathEffect) is spawned when this function is called
     * However, since paths are recalculated quite often, we only spawn the effect
     * when the current position is some distance away from the previous spawn-point
     */
	public override void OnTargetReached ()
	{
		/*
	        if (endOfPathEffect != null && Vector3.Distance(tr.position, lastTarget) > 1)
	        {
	            GameObject.Instantiate(endOfPathEffect, tr.position, tr.rotation);
	            lastTarget = tr.position;
	        }*/

		//if (!selectableComponent.Selected)
		//	target = null;
		
		//Debug.Log ("Target reached.");
	}

	public override Vector3 GetFeetPosition ()
	{
		return tr.position;
	}

	protected new void Update ()
	{
		if (canMove) {
			Vector3 velocity = CalculateVelocity (GetFeetPosition ());
			//Debug.DrawRay (GetFeetPosition (), dir, Color.green);


			if (animationComponent != null) {
				animationComponent.NavAnimSetup (velocity);
				//if (target != null)
				//animationComponent.MoveToTarget (target);
			}

			//Rotate character towards targetDirection (filled in by CalculateVelocity)
			RotateTowards (targetDirection);

			controller.SimpleMove (velocity);

			// Move character in direction
			/*
			if (controller != null) {
				//controller.SimpleMove (dir);
			}*/
		}


		//Get velocity in world-space
		//Vector3 velocity;
		/*
		if (canMove) {
			Vector3 dir = CalculateVelocity (GetFeetPosition ());

			if (animationComponent != null)
				animationComponent.NavAnimSetup (dir);

			//Rotate towards targetDirection (filled in by CalculateVelocity)
			RotateTowards (targetDirection);

			dir.y = 0;
			if (dir.sqrMagnitude > sleepVelocity * sleepVelocity) {
				//If the velocity is large enough, move
			} else {
				//Otherwise, just stand still (this ensures gravity is applied)
				dir = Vector3.zero;
			}

			if (controller != null) {
				controller.SimpleMove (dir);
				velocity = controller.velocity;
			} else {
				Debug.LogWarning ("No NavmeshController or CharacterController attached to GameObject");
				velocity = Vector3.zero;
			}
		} else {
			velocity = Vector3.zero;
			animationComponent.SetSpeed (0f);
			ResetPath ();
			target = null;
			return;
		}*/
	}

	public void DisableSearch ()
	{
		if (canSearch) {
			target = null;
			canSearch = false;
			//SimpleTargetMover.OnPlayerMove -= UpdateTarget;
		}
	}

	public void EnableSearch ()
	{
		if (!canSearch) {
			//SimpleTargetMover.OnPlayerMove += UpdateTarget;
			canSearch = true;
		}
	}

	public void UpdateTarget (Transform tr)
	{
		target = tr;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Projectile")) {
			Projectile projectile = other.gameObject.GetComponent<Projectile> ();

			if (healthScaler.currentHealth > 0f) {
				healthScaler.currentHealth -= projectile.damage;
			}
			if (healthScaler.currentHealth <= 0f) {
				OnUnitDeath ();
			}
		}
	}

	void OnUnitDeath ()
	{
		Debug.Log ("I'm Dying..");
		canMove = false;
		animationComponent.Die (true);
	}

	public override void OnPlayerCommand (MouseTarget _mouseTarget)
	{

		// Do what you need with the mouse target..

		/*
		Debug.Log ("Name to Layer: " + LayerMask.NameToLayer ("EnemyUnit"));
		Debug.Log ("Layer to Name: " + LayerMask.LayerToName (13));

		if (MouseController.Instance.IsMouseOverLayer (LayerMask.NameToLayer ("EnemyUnit"))) {
			Debug.Log ("Attack the enemy!");
		}*/

		//string currentLayer = LayerMask.LayerToName (_mousearget.gameObject.layer);//MouseController.Instance.GetCurrentMouseOverLayerName ();
		//Debug.Log ("RIGHT CLICK TARGET IS: " + _target);

		playerSelectedTarget = _mouseTarget;

		switch (_mouseTarget.currentTargetType) {
		case TargetType.EnemyUnit:
			//playerSelectedTarget = _mouseTarget;
			target = null;
			Debug.Log ("Hostile detected!");
			break;
		case TargetType.Walkable:
			//playerSelectedTarget = null;
			target = playerSelectedTarget.transform;
			Debug.Log ("Move to position!");
			break;
		}
		/*switch (currentLayer) {
		case "EnemyUnit":
			//canMove = false;
			//animationComponent.StopMovement ();
			//DisableSearch ();
			_target.tag = "RightClickTarget";
			attackModule.PlayerSelectedTarget = _target;
			break;
		default:
			// Threat target from mouse event removed. If 'attackNearbyEnemies' is set, threatTarget is automatically updated for nearby enemies.
			//NOTE: This does not remove target from being reassigned as a nearby enemy. This is expected behavior.
			// Also note that setting the property to null does not affect targets added from RangedBounds. Only rightClickedTargets are removed.
			attackModule.PlayerSelectedTarget = null;
			break;
		}
	 */
		//throw new System.NotImplementedException ();
	}
}
