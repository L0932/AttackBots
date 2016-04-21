using UnityEngine;
using System.Collections;

public class SimpleTargetMover : Singleton<SimpleTargetMover>
{

	/** Moves the target in example scenes.
	 * This is a simple script which has the sole purpose
	 * of moving the target point of agents in the example
	 * scenes for the A* Pathfinding Project.
	 *
	 * It is not meant to be pretty, but it does the job.
	 */
	/** Mask for the raycast placement */
	public LayerMask rightClickableMask;
	public Transform target;

	/** Determines if the target position should be updated every frame or only on double-click */
	public bool onlyOnDoubleClick;
	public bool onRightClick;

	public delegate void MoveCommand (Transform tr);

	public static event MoveCommand OnPlayerMove;

	Camera cam;

	public void Start ()
	{
		//Cache the Main Camera
		cam = Camera.main;
		useGUILayout = false;
	}

	public void OnGUI ()
	{
		if (cam == null)
			return;

		if (!onRightClick && onlyOnDoubleClick && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2) {
			UpdateTargetPosition ();
		}

		if (onRightClick && !onlyOnDoubleClick && Event.current.type == EventType.MouseDown && Event.current.button == 1) {
			UpdateTargetPosition ();
		}
	}

	/*
	// Update is called once per frame
	void Update ()
	{
		if (!onRightClick && !onlyOnDoubleClick && cam != null) {
			UpdateTargetPosition ();
		}
	}*/

	public void UpdateTargetPosition ()
	{

		//Fire a ray through the scene at the mouse position and place the target where it hits
		RaycastHit hit;

		if (Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, rightClickableMask)) {

			//Temp
			MoveTarget (hit);
			/*
			if (rightClickableMask == (rightClickableMask | (1 << LayerMask.NameToLayer ("EnemyUnit")))) {
				CommandAttack ();
			} else if (rightClickableMask == (rightClickableMask | (1 << LayerMask.NameToLayer ("Walkable")))) {
				CommandMove (hit);
			}*/
			/*
			switch (rightClickableMask) {
			case LayerMask.NameToLayer("EnemyUnit"):
				AttackCommand ();
				break;
			case LayerMask.NameToLayer("Walkable"):
				WalkCommand ();
				break;
			}*/

		}
	}

	void MoveTarget (RaycastHit hit)
	{
		//Debug.Log ("Command Called. Command: Move Forward!!!!!!");

		Vector3 newPosition = Vector3.zero;
		//bool positionFound = false;

		newPosition = hit.point;
		//positionFound = true;

		if (newPosition != target.position)
			target.position = newPosition;

		if (OnPlayerMove != null) {
			OnPlayerMove (target);
		}
	}

	/*
	void CommandAttack ()
	{
		//Debug.Log ("Command Called. Command: Attack!!!!!!");
	}

	void CommandMove (RaycastHit hit)
	{
		//Debug.Log ("Command Called. Command: Move Forward!!!!!!");

		Vector3 newPosition = Vector3.zero;
		//bool positionFound = false;

		newPosition = hit.point;
		//positionFound = true;

		if (newPosition != target.position)
			target.position = newPosition;

		if (OnPlayerMove != null) {
			OnPlayerMove (target);
		}
	}*/
}
