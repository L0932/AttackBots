using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthScaler : MonoBehaviour
{
	//public delegate void HealthUpdate ();

	//public static event HealthUpdate OnHealthZero;
	public enum SelectedAxis
	{
		xAxis,
		yAxis,
		zAxis
	}

	public float currentHealth = 1f;
	public float maxHealth = 1f;

	public SelectedAxis selectedAxis = SelectedAxis.xAxis;

	// Target
	public Image image;

	// Parameters
	public float minValue = 0.0f;
	public float maxValue = 1.0f;
	public Color minColor = Color.red;
	public Color maxColor = Color.green;

	// Use this for initialization
	void Start ()
	{
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.localScale = new Vector3 (currentHealth / maxHealth, transform.localScale.y, transform.localScale.z);

		switch (selectedAxis) {
		case SelectedAxis.xAxis:
			//Lerp color depending on the scale factor
			image.color = Color.Lerp (minColor, maxColor, Mathf.Lerp (minValue, maxValue, transform.localScale.x));
			break;
		case SelectedAxis.yAxis:
			image.color = Color.Lerp (minColor, maxColor, Mathf.Lerp (minValue, maxValue, transform.localScale.y));
			break;
		case SelectedAxis.zAxis:
			image.color = Color.Lerp (minColor, maxColor, Mathf.Lerp (minValue, maxValue, transform.localScale.z));
			break;
		}

		/*
		if (currentHealth <= 0) {
			if (OnHealthZero != null)
				OnHealthZero ();
		}*/
	}
}
