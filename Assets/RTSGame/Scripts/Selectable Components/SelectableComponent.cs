using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SelectableComponent : NetworkBehaviour
{
	public LayerMask layerTest;

	public Projector projectorComponent;
	public GameObject healthBar;

	protected MouseController mouseController;
	protected GG_Controller controllerUnit;
	protected bool selected;

	protected bool hovered;
	protected bool registered;

	public bool Selected { get { return selected; } set { selected = value; } }

    protected virtual void Awake ()
	{
		registered = false;
		hovered = false;

		projectorComponent = GetComponentInChildren<Projector> ();
		controllerUnit = GetComponent<GG_Controller> ();
		//mouseController = MouseController.Instance;
	}

	public virtual void ActivateUnitSelector ()
	{
		//Debug.Log ("Activate unit selector");

		if (!registered) {
			//Debug.Log ("Activated Unit Selector");
			selected = true;

			if (projectorComponent != null)
				projectorComponent.enabled = true;

			if (controllerUnit != null)
				controllerUnit.OnSelected ();

			if (healthBar != null)
				healthBar.SetActive (true);
			
			MouseController.OnRightClick += OnRightClick;
			registered = true;
		}
	}

	public virtual void DeactivateUnitSelector ()
	{
		if (registered) {
			selected = false;

			if (projectorComponent != null)
				projectorComponent.enabled = false;

			if (controllerUnit != null)
				controllerUnit.OnDeselected ();

			if (healthBar != null)
				healthBar.SetActive (false);
			
			MouseController.OnRightClick -= OnRightClick;
			registered = false;
		}
	}

	protected virtual void OnRightClick (MouseTarget _mouseTarget)
	{
		controllerUnit.OnPlayerCommand (_mouseTarget);
	}

	protected virtual void OnMouseOver ()
	{
		if (!hovered) {
			hovered = true;
		}
	}

	protected virtual void OnMouseExit ()
	{
		if (hovered) {
			hovered = false;
		}
	}
}
