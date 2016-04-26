using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SelectableComponent : NetworkBehaviour
{
	public LayerMask layerTest;

	public Projector projectorComponent;
	public GameObject statusBar;

	private MouseController mouseController;
	private GG_Controller controllerUnit;
	private bool selected;

	private bool hovered;
	private bool registered;

	public bool Selected { get { return selected; } set { selected = value; } }

	void Awake ()
	{
		registered = false;
		hovered = false;

		projectorComponent = GetComponentInChildren<Projector> ();
		controllerUnit = GetComponent<GG_Controller> ();

		mouseController = MouseController.Instance;
	}

	public void ActivateUnitSelector ()
	{
		Debug.Log ("Activate unit selector");

		if (!registered) {
			//Debug.Log ("Activated Unit Selector");
			selected = true;

			if (projectorComponent != null)
				projectorComponent.enabled = true;

			if (controllerUnit != null)
				controllerUnit.OnSelected ();

			if (statusBar != null)
				statusBar.SetActive (true);

			if (isLocalPlayer) {
				MouseController.OnRightClick += OnRightClick;
			}
			registered = true;
		}
	}

	public void DeactivateUnitSelector ()
	{
		if (registered) {
			selected = false;

			if (projectorComponent != null)
				projectorComponent.enabled = false;

			if (controllerUnit != null)
				controllerUnit.OnDeselected ();

			if (statusBar != null)
				statusBar.SetActive (false);

			if (isLocalPlayer) {
				MouseController.OnRightClick -= OnRightClick;
			}
			registered = false;
		}
	}

	void OnRightClick (MouseTarget _mouseTarget)
	{
		controllerUnit.OnPlayerCommand (_mouseTarget);
	}

	void OnMouseOver ()
	{
		if (!hovered) {
			//MouseController.Instance.CurrentHoveredLayer = gameObject.layer;

			//if (mouseController != null) {
			//	mouseController.CurrentMouseOverLayer = gameObject.layer;
			//}
			hovered = true;
		}
		/*
		LayerMask layerMask = gameObject.layer;
		//Debug.Log (layerTest.value);

		//MouseSelection.Instance.
		if (layerTest == (layerTest | (1 << gameObject.layer))) {
			Debug.Log ("You're hovering over a/an " + layerMask.value);
		}*/
	}

	void OnMouseExit ()
	{
		if (hovered) {
			//if (mouseController != null) { 
			//	mouseController.CurrentMouseOverLayer = 0;
			//	}
			//MouseController.Instance.CurrentHoveredLayer = 0;
			hovered = false;
		}
	}

	public override void OnStartLocalPlayer ()
	{
		projectorComponent.material = Resources.Load ("Materials/Projectors/Projector_LocalPlayer", typeof(Material)) as Material;
	}
}
