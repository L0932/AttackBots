using UnityEngine;
using System.Collections;

public class PatrolState : IState
{

	private readonly StateController stateController;
	private int nextWayPoint;

	public PatrolState (StateController _stateController)
	{
		stateController = _stateController;
	}

	public void UpdateState ()
	{
		Look ();
		Patrol ();
	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
			ToAlertState ();
	}

	public void ToPatrolState ()
	{
		Debug.LogError ("Can't transition to same state");
	}

	public void ToAlertState ()
	{
		stateController.currentState = stateController.alertState;

	}

	public void ToChaseState ()
	{
		
	}

	public void ToAttackState ()
	{
		stateController.currentState = stateController.attackState;
	}

	private void Look ()
	{
		//RaycastHit hit;
		//if(Physics.Raycast())
		/*	if (stateController.attackModule.PlayerSelectedTarget != null) {
			ToAttackState ();
		}*/
		//Debug.Log ("looking...");
	}

	void Patrol ()
	{
		//Debug.Log ("Patrolling...");
	}
}
