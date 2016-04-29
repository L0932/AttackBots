using UnityEngine;
using System.Collections;

public class RotateSpine : MonoBehaviour {

	public Transform target;
	public float bodyWeight = .45f;
	public Transform spine;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void LateUpdate () {

		//Code that works!
		/*		
 		* spine.Rotate (xRot, 0, 0);

		//float angle = Mathf.Atan2 (target.position.x - spine.position.x, target.position.) * Mathf.Deg2Rad;
		float angle = Vector3.Angle (target.position-transform.position, transform.forward);

		Debug.Log (angle);

		int sign = Vector3.Cross (transform.position, target.position).z < 0 ? -1 : 1;
		spine.Rotate (angle * sign, 0, 0);
		*/

		//spine.rotation = Quaternion.AngleAxis (180, Vector3.right);
		float angle = Vector3.Angle (target.position-transform.position, transform.forward);
		int sign = Vector3.Cross (transform.position, target.position).z < 0 ? -1 : 1;

		spine.Rotate ((angle * sign) * bodyWeight, 0, 0);
	}
}
