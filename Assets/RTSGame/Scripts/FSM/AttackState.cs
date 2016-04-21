using UnityEngine;
using System.Collections;

public class AttackState : IState
{
	private readonly StateController stateController;
	private int nextWayPoint;
	private bool initAttack = false;

	public AttackState (StateController _stateController)
	{
		stateController = _stateController;
	}

	public void UpdateState ()
	{
		Look ();
		Attack ();
	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
			ToAlertState ();
	}

	public void ToPatrolState ()
	{
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
		Debug.LogError ("Can't transition to same state");
	}

	private void Look ()
	{
		//RaycastHit hit;
		//if(Physics.Raycast())
		/*
		if (stateController.attackModule.ThreatTarget != null) {
			Debug.Log ("Threat detected! Switching state to Alert!");
			ToAlertState ();
		}*/
	}

	void Attack ()
	{
		Debug.Log ("Attacking...");
		//TODO: Cache it
		/*if (stateController.attackModule.PlayerSelectedTarget != null) {
			
		}*/
	}
}
