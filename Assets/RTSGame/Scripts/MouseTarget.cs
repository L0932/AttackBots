using UnityEngine;
using System.Collections;


public enum TargetType
{
	//Must match actual layer names
	Walkable,
	EnemyUnit,
	PlayerUnit,
	Friendly,
}

public class MouseTarget : MonoBehaviour
{
	public Transform selectedTransform;
	//public GameObject selectedTarget_G;

	public LayerMask rightClickables;
	public TargetType currentTargetType;

	public void SetTargetType (string str)
	{
		currentTargetType = (TargetType)System.Enum.Parse (typeof(TargetType), str);
	}

	public bool IsOfType (string str)
	{
		TargetType type = (TargetType)System.Enum.Parse (typeof(TargetType), str);
		return (type == currentTargetType); 
	}
}
