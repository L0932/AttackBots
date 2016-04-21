using UnityEngine;
using System.Collections;

public class BillboardTransform : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
	{
		transform.LookAt (transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
	}
}
