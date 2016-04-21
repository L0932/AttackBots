using UnityEngine;
using System.Collections;

public class OnMouseOverComponent : MonoBehaviour
{

	Ray ray;
	RaycastHit hit;

	// Update is called once per frame
	void Update ()
	{

		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit)) {
			Debug.Log (hit.collider.name);
		}
	}
}
