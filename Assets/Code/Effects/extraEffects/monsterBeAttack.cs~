using System.Collections;
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

	FSMStage FS;
	NavMeshAgent NM;

	void Start ()
	{
		//theMoveController = this.GetComponent <NavMeshAgent> ();
	}
	public override void OnBeAttack (PlayerBasic attacker)
	{
		//攻击者不是AI才能打出来击飞
		if (attacker.gameObject.tag != "AI")
		{
			//moveTowards = new Vector3 (0f, 7f, 0f);
			float X= this.transform.position.x - attacker.transform.position.x ;
			float Z= this.transform.position.z - attacker.transform.position.z  ;

			moveTowards = new Vector3 (X, 0, Z) * 4;
			//moveTowards = attacker.transform .right * 4 ;
			moveTimer = moveTimerAll;
			isMoving = true;
			//if(theMoveController)
			//	theMoveController.enabled = false;

			FS = this.GetComponent <FSMStage> ();
			if (FS)  FS.enabled = false;

			NM =  this.GetComponent <NavMeshAgent> ();
			if (NM && NM.isOnNavMesh)  NM .isStopped = true;
		}
	}

	//脚本有可能根据需要提前销毁，需要保证状态能归位
	void OnDestroy()
	{
		if (FS)  FS.enabled = true;
		if (NM && NM.isActiveAndEnabled)  NM.isStopped = false;
	}

	void Update () 
	{
		if (isMoving)
		{

			this.transform.Translate (moveTowards * moveSpeed * Time.deltaTime,Space.World);
			moveTimer -= Time.deltaTime;
			if (moveTimer < 0)
			{
				//if(theMoveController)
				//	theMoveController.enabled = true;
				moveTimer = moveTimerAll;
				isMoving = false;

				if (FS) FS.enabled = true;
				if (NM) NM.isStopped = false;

				isEffecting = false;//标记，已经失效
			}
		}
	}
}
