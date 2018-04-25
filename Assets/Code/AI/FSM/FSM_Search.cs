using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Search : FSMBasic {

	List<PlayerBasic> theEMYGet   = null;
	public float angle = 125;//视野角度范围的一半
	public float distance = 2.5f;//视野长度
	PlayerBasic theMainEMY = null;

	//个人认为比较稳健的方法
	//传入的是攻击范围和攻击扇形角度的一半
	//选择目标的方法，这年头普攻都是AOE
	float searchTimer = 0.4f;
	float searchTimerMax = 0.4f;
	void searchAIMs()//不使用射线而是使用向量计算方法
	{
		if (!theThis)
			return;

		searchTimer -= Time.deltaTime;
		if(searchTimer <0)
		{
			searchTimer = searchTimerMax;
			theEMYGet = systemValues.searchAIMs (angle, distance, theThis.transform);
			theMainEMY = getMainEMY ();
			//if (theMainEMY)
			//	Debug.Log (theMainEMY.ActerName + "is found");
		}
	}


    //找到的目标很多，排序找到最终的目标
	private PlayerBasic getMainEMY()
	{
		//Debug.Log ("first check count = "+ theEMYGet.Count);
		for (int i = 0; i < theEMYGet.Count; i++)
			if (theEMYGet [i].tag != "AI")
				return theEMYGet [i];

		return null;
	}


	private Vector3 randomAimPosition = Vector3.zero;
	float moveTimer = 1.75f;//每隔一段时间做一次就行
	float moveTimerMax = 1.75f;//时间间隔备份
	private void randomMove()
	{
		if (!this.theThis)
			return;
		
		moveTimer -= Time.deltaTime;
		Vector3 thisCheckVector = new Vector3 (this.theThis.transform.position.z, 0, this.theThis.transform.position.z);
		Vector3 randomCheckVector = new Vector3 (randomAimPosition.x, 0, randomAimPosition.z);
		bool reachCheck = (randomAimPosition == Vector3.zero  || Vector3.Distance (thisCheckVector,randomCheckVector)< 0.3f) ;
		//路径控制
		//没有路了就换一个目标走
		if (moveTimer < 0 || theMoveController && !theMoveController.hasPath) 
		{
			moveTimer =  moveTimerMax;
			randomAimPosition = this.theThis.transform.position + new Vector3 (Random.Range (0f, 8f)-4f , Random.Range (0f, 2f), Random.Range (0f, 8f)-4f );
			try
			{
				if(theMoveController.isActiveAndEnabled && theMoveController.isOnNavMesh)
			   theMoveController.SetDestination (randomAimPosition);
			}
			catch
			{
				Debug.Log ("AI 的当前状态不允许改变目标位置");
			}
		}
		//动画控制
		if (reachCheck )
		{
			theAnimator.Play ("moveMent");
		}
		else
		{
			theAnimator.Play ("rotatePoseForward");
		}
	}

//-------------------------------------------------------------------------//
	public override int geID ()
	{
		return 4;
	}


	public override void actInThisState ()
	{
		//Debug.Log ("search!");
		searchAIMs ();
		randomMove ();
	}

	public override FSMBasic moveToNextState ()
	{
		//找不到目标就继续找
		if (theMainEMY == null)
			return this;
		//找到了目标就转到下一个状态
		else 
		{
			//Debug.Log ("search to attack");
			FSM_Attack attack = new FSM_Attack ();
			attack.makeState (this.theMoveController, this.theAttackLlinkController, this.theAnimator,this.theThis,this.theMainEMY);
			return attack;
		}
 
	}
	public override FSMBasic moveToNextState (FSMStage theContrtoller)
	{
		//找不到目标就继续找
		if (theMainEMY == null)
			return this;
		//找到了目标就转到下一个状态
		else 
		{
			//Debug.Log ("search to attack");
			FSMBasic attack = theContrtoller.getState(1);
			attack.makeState (this.theMoveController, this.theAttackLlinkController, this.theAnimator,this.theThis,this.theMainEMY);
			return attack;
		}

	}
}
