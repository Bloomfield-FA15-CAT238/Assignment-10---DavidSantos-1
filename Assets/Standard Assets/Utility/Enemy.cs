using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	private EnemyAIStates state = EnemyAIStates.Patrolling;
	static private List<GameObject> patrolPoints = null;
	
	#region Enemy Options
	public float walkingSpeed = 3.0f;
	public float chasingSpeed = 5.0f;
	public float attackingSpeed = 1.5f;
	public float attackingDistance = 1.0f;
	#endregion
	
	private GameObject patrollingInterestPoint;
	private GameObject playerOfInterest;
	
	void Start () {
		if(patrolPoints==null) {
			patrolPoints = new List<GameObject>();
			foreach(GameObject go in GameObject.FindGameObjectsWithTag("PatrolPoints")) {
				Debug.Log("Patrol Point: " + go.transform.position);
				patrolPoints.Add(go);
			}
		}
		SwitchToPatrolling();
	}
	
	void Update () {
		switch(state) {
		case EnemyAIStates.Attacking:
			OnAttackingUpdate();
			break;
		case EnemyAIStates.Chasing:
			OnChasingUpdate();
			break;
		case EnemyAIStates.Patrolling:
			OnPatrollingUpdate();
			break;
		}
	}
	
	void OnAttackingUpdate() {
		float step = attackingSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, playerOfInterest.transform.position, step);
		
		float distance = Vector3.Distance(transform.position, playerOfInterest.transform.position);
		if(distance>attackingDistance) {
			SwitchToChasing(playerOfInterest);
		}
	}
	
	void OnChasingUpdate() {
		float step = chasingSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, playerOfInterest.transform.position, step);
		
		float distance = Vector3.Distance(transform.position, playerOfInterest.transform.position);
		if(distance<=attackingDistance) {
			SwitchToAttacking(playerOfInterest);
		}
	}
	
	void OnPatrollingUpdate() {
		float step = walkingSpeed * Time.deltaTime;
		print ("Time.deltaTime: " + Time.deltaTime);
		transform.position = Vector3.MoveTowards(transform.position, patrollingInterestPoint.transform.position, step);
		
		float distance = Vector3.Distance(transform.position, patrollingInterestPoint.transform.position);
		if(distance==0) {
			SelectRandomPatrolPoint();
		}
	}
	
	void OnTriggerEnter(Collider collider) {
		SwitchToChasing(collider.gameObject);
	}
	
	void OnTriggerExit(Collider collider) {
		SwitchToPatrolling();
	}
	
	void SwitchToPatrolling() {
		state = EnemyAIStates.Patrolling;
		GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f);
		SelectRandomPatrolPoint();
		playerOfInterest = null;
	}
	
	void SwitchToAttacking(GameObject target) {
		state = EnemyAIStates.Attacking;
		GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
	}
	
	void SwitchToChasing(GameObject target) {
		state = EnemyAIStates.Chasing;
		GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 0.0f);
		playerOfInterest = target;
	}
	
	void SelectRandomPatrolPoint() {
		int choice = Random.Range(0,patrolPoints.Count);
		patrollingInterestPoint = patrolPoints[choice];
		Debug.Log("Enemy going to patrol to point " + patrollingInterestPoint.name);
	}
}