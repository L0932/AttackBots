using UnityEngine;
using System.Collections;

public class MouseController : Singleton<MouseController>
{
	//public Transform currentSelectedObject;
	//public LayerMask rightClickableMask;

	public MouseTarget mouseTarget;

	public Transform cameraControl;
	public float scrollSpeed = 3f;
	public LayerMask selectables;
	private int currentMouseOverLayer = 0;
	//private MouseTarget rightClickTarget;

	private float sensitivity = 1f;

	public delegate void ClickEvent (MouseTarget target);

	public static event ClickEvent OnRightClick;

	//public static event ClickEvent OnLeftClick;

	public int CurrentMouseOverLayer {
		get {
			return currentMouseOverLayer;
		}

		set {
			currentMouseOverLayer = value;
		}
	}

	bool isSelecting;
	Vector3 mouseSelectionPosition;
	// Use this for initialization
	void Awake ()
	{
		mouseTarget = FindObjectOfType<MouseTarget> ();
		//rightClickableMask = mouseTarget.rightClickables;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			isSelecting = true;
			mouseSelectionPosition = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp (0)) {
			isSelecting = false;
		}

		if (Input.GetMouseButtonDown (1)) {
			PlayerCommand ();
		}

		if (isSelecting) {
			PlayerSelection ();
		}



		// Mouse Zoom

		/*	if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			Camera.main.orthographicSize = Mathf.Max (Camera.main.orthographicSize - 1, 1);
			Debug.Log ("Max");
		} else if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			Camera.main.orthographicSize = Mathf.Min (Camera.main.orthographicSize - 1, 6);
			Debug.Log ("Min");
		}*/


/*
		cameraDistanceY += Input.GetAxis ("Mouse ScrollWheel") * scrollSpeed;

		Vector3 cameraPos = cameraControl.position;
		//Camera.main.transform.position = new Vector3*/
		MouseZoom ();
	}

	void OnGUI ()
	{
		if (isSelecting) {
			var rect = Utils.GetScreenRect (mouseSelectionPosition, Input.mousePosition);
			Utils.DrawScreenRect (rect, new Color (0.8f, 0.8f, 0.95f, 0.25f));
			Utils.DrawScreenRectBorder (rect, 2, new Color (0.8f, 0.8f, 0.95f));
		}
	}

	private float maxY = 60;
	private float minY = 20;

	void MouseZoom ()
	{
/*		float fov = Camera.main.fieldOfView;
		fov += Input.GetAxis ("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp (fov, minFOV, maxFOV);
		Camera.main.fieldOfView = fov;*/

/*		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			cameraControl.position.y = Mathf.Lerp (maxY, minY, Time.time);
			//cameraControl.position.z = Mathf.Lerp (transform.position.z, transform.position.z, Time.time);
		}*/
	}

	void PlayerSelection ()
	{
		foreach (SelectableComponent selectableUnit in FindObjectsOfType<SelectableComponent>()) {
			if (IsWithinSelectionBounds (selectableUnit.gameObject)) {
				selectableUnit.ActivateUnitSelector ();
				//currentSelectableUnits.Add (selectableUnit);
			} else {
				selectableUnit.DeactivateUnitSelector ();
			}
		}
	}

	void PlayerCommand ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			//pass right click target to callback
			string hitLayerName = LayerMask.LayerToName (hit.transform.gameObject.layer);
			
			mouseTarget.transform.position = hit.point;
			mouseTarget.SetTargetType (hitLayerName);
			mouseTarget.selectedTransform = hit.transform;
			

			if (OnRightClick != null && mouseTarget != null)
				OnRightClick (mouseTarget);
		}
	}

	public bool CompareToSelectedLayer (int layer)
	{
		/*
		LayerMask layerMask = gameObject.layer;
		//Debug.Log (layerTest.value);

		//MouseSelection.Instance.
		if (layerTest == (layerTest | (1 << gameObject.layer)))
			Debug.Log ("You're hovering over a/an " + layerMask.value);
		//Debug.Log ("Hehe, I'm being moused over!");
		*/

		return false;
		//if()
	}

	public void SetCurrentSelectedLayer (int _currentLayer)
	{
		currentMouseOverLayer = _currentLayer;
	}


	public string GetCurrentMouseOverLayerName ()
	{
		return LayerMask.LayerToName (currentMouseOverLayer);
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
			Utils.GetViewportBounds (camera, mouseSelectionPosition, Input.mousePosition);

		return viewportBounds.Contains (
			camera.WorldToViewportPoint (gameObject.transform.position));
	}

	public bool IsMouseOverLayer (int layer)
	{
		// execute if layer is included in the layerMask
		if ((selectables.value & (1 << layer)) > 0) {

			if (currentMouseOverLayer == layer) {
				return true;
			}
		}
		return false;
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
}
