using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	public float speed = 5f;
	public float damage = .5f;
	public float lifeTime = 3f;

	void Start ()
	{
		StartCoroutine (LifeTime ());
	}

	void Update ()
	{
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}

	IEnumerator LifeTime ()
	{
		yield return new WaitForSeconds (lifeTime);
		Destroy (gameObject);
	}
}
