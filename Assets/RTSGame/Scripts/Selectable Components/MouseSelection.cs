using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Unit Selection by Jeff Zimmer http://hyunkell.com/blog/
public class MouseSelection : Singleton <MouseSelection>
{
	public Transform targetPosition;

	bool isSelecting = false;
	Vector3 mousePosition1;
	//List<SelectableComponent> currentSelectableUnits;

	void Start ()
	{
		//currentSelectableUnits = new List<SelectableComponent> ();
		//SimpleTargetMover.OnPlayerMove += OnPlayerMoveCommand;
	}

	protected MouseSelection ()
	{
		//Debug.Log ("Unit Selection Singleton called.");
	}

	void Update ()
	{
		// If we press the left mouse button, save mouse location and begin selection
		if (Input.GetMouseButtonDown (0)) {
			isSelecting = true;
			mousePosition1 = Input.mousePosition;
		}
		// If we let go of the left mouse button, end selection
		if (Input.GetMouseButtonUp (0))
			isSelecting = false;

		if (isSelecting) {
			foreach (SelectableComponent selectableUnit in FindObjectsOfType<SelectableComponent>()) {
				if (IsWithinSelectionBounds (selectableUnit.gameObject)) {
					selectableUnit.ActivateUnitSelector ();
					//currentSelectableUnits.Add (selectableUnit);
				} else {
					selectableUnit.DeactivateUnitSelector ();
				}
			}
		}
	}

	void OnGUI ()
	{
		if (isSelecting) {
			// Create a rect from both mouse positions
			var rect = Utils.GetScreenRect (mousePosition1, Input.mousePosition);
			Utils.DrawScreenRect (rect, new Color (0.8f, 0.8f, 0.95f, 0.25f));
			Utils.DrawScreenRectBorder (rect, 2, new Color (0.8f, 0.8f, 0.95f));
		}
	}

	public Transform GetTargetPosition ()
	{
		return targetPosition;
	}

	public bool IsWithinSelectionBounds (GameObject gameObject)
	{
		if (!isSelecting)
			return false;
		// Allow single selection
		if (MouseOverObject (gameObject))
			return true;

		var camera = Camera.main;
		var viewportBounds =
			Utils.GetViewportBounds (camera, mousePosition1, Input.mousePosition);

		return viewportBounds.Contains (
			camera.WorldToViewportPoint (gameObject.transform.position));
	}

	public bool MouseOverObject (GameObject gameObject)
	{
		//gameObject.GetComponent<
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		CharacterController characterController = gameObject.GetComponent<CharacterController> ();

		if (characterController == null) {
			return false;
		}
	
		if (characterController.Raycast (ray, out hit, Mathf.Infinity)) {
			return true;
		}
		return false;
	}

	/*
	public void OnPlayerMoveCommand ()
	{
	}*/
}
