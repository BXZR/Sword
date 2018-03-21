using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_RunAfter : FSMBasic {

	float timer = 2.5f;

	public override int geID ()
	{
		return 3;
	}

	public override void actInThisState ()
	{
		//Debug.Log ("run after");
		theAnimator.Play("rotatePoseForward");
		this.theThis.transform.LookAt (theEMY.transform);
		if(this.theMoveController.isActiveAndEnabled)
		this.theMoveController.SetDestination(theEMY.transform .position);

		timer -= Time.deltaTime;
		//Debug.Log ("runafterTimer : "+ timer);
	}

	public override FSMBasic moveToNextState ()
	{

		//为了确保能真的攻击到，留下10%的多余空间
		if (Vector3.Distance (this.theThis.transform.position , this.theEMY.transform .position) <=  this.theThis.theAttackAreaLength)
		{
			//Debug.Log ("runafter to attack");
			FSM_Attack attack = new FSM_Attack ();
			attack.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis,this.theEMY);
			return attack;
		}
		if (this.theThis.transform.position.y < this.theEMY.transform.position.y-0.75 )
		{
			//Debug.Log ("runafter to jump");
			FSM_Jump jump = new FSM_Jump ();
			jump.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis,this.theEMY);
			return jump;
		}
		//Debug.Log ("theEMY name is "+ theEMY.name);
		if (timer < 0)
		{
			//Debug.Log ("runafter to search");
			FSM_Search search = new FSM_Search ();
			search.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis);
			return search;
		}
		return this;
	}

	public override void OnChangeToThisState ()
	{
		Collider [] AIs = Physics.OverlapSphere (this.theThis.transform .position , 1f);
		for (int i = 0; i < AIs.Length; i++) 
		{
			FSMStage theStage = AIs [i].GetComponent <FSMStage> ();
			if (AIs [i].tag == "AI" && theStage) 
			{
				//ID = 4表示还在search，这个时候应该把自己的敌人通知给身边的单位
				if (theStage.theStateNow.geID () == 4 || theStage.theStateNow.geID () == 2) 
				{
					FSM_RunAfter runafter = new FSM_RunAfter ();
					runafter.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis,this.theEMY);
					theStage.theStateNow = runafter;
					theStage.theStateNow.OnChangeToThisState ();
				}
			}
		}
	}
}
