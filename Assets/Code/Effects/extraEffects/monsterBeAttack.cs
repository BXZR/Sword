﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class monsterBeAttack : effectBasic{

	//怪物被击中之后各种击飞
	Vector3 moveTowards = new Vector3 ();
	float moveTimer = 0.3f;
	float moveTimerAll = 0.3f;
	bool isMoving = false;
	private float moveSpeed = 3f;
	//NavMeshAgent theMoveController = null;
	void Start ()
	{
		//theMoveController = this.GetComponent <NavMeshAgent> ();
	}
	public override void OnBeAttack (PlayerBasic attacker)
	{
		if (attacker.gameObject.tag != "AI")
		{
			moveTowards = (this.transform.position - attacker.transform.position).normalized * 4 + new Vector3 (0, 2, 1);
			moveTimer = moveTimerAll;
			isMoving = true;
			//if(theMoveController)
			//	theMoveController.enabled = false;
			if (this.GetComponent <FSMStage> ())
				this.GetComponent <FSMStage> ().enabled = false;
			if (this.GetComponent <NavMeshAgent> ())
				this.GetComponent <NavMeshAgent> ().isStopped = true;
		}
	}
	void Update () 
	{
		if (isMoving)
		{

			this.transform.Translate (moveTowards * moveSpeed * Time.deltaTime);
			moveTimer -= Time.deltaTime;
			if (moveTimer < 0)
			{
				//if(theMoveController)
				//	theMoveController.enabled = true;
				moveTimer = moveTimerAll;
				isMoving = false;

				if (this.GetComponent <FSMStage> ())
					this.GetComponent <FSMStage> ().enabled = true;
				if (this.GetComponent <NavMeshAgent> ())
					this.GetComponent <NavMeshAgent> ().isStopped = false;
			}
		}
	}
}