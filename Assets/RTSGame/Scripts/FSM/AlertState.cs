using UnityEngine;
using System.Collections;

public class AlertState : IState
{
	private readonly StateController stateController;

	public AlertState (StateController stateControllerEnemy)
	{
	}

	public void UpdateState ()
	{
		Look ();
		Search ();
	}

	public void OnTriggerEnter (Collider other)
	{

	}

	public void ToPatrolState ()
	{

	}

	public void ToAlertState ()
	{
	}

	public void ToChaseState ()
	{

	}

	public void ToAttackState ()
	{
		
	}

	private void Look ()
	{
		Debug.Log ("In Alert State, Looking...");
	}

	private void Search ()
	{
	}
}
