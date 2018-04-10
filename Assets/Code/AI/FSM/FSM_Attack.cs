using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Attack : FSMBasic {


	//进行攻击
	private void  makeAttack()
	{
		attackLink [] attackLinks = this.theAttackLlinkController.attackLinks;
		//Debug.Log ("attackLinks.Length = " + attackLinks.Length);
		if (attackLinks.Length > 0) 
		{
			if (systemValues.checkCanAttackAct (theAttackLlinkController.theAnimator)) 
			{
				//随机选择一种攻击方式
				int index = Random.Range (0, attackLinks.Length);
				theAttackLlinkController.makeAttackLink (attackLinks [index].attackLinkString, false);
			}
		}
	}
	//看向目标
	private void makeLook()
	{
		if(theEMY!= null &&  theThis!= null)
		  theThis.transform.LookAt (theEMY.transform);
	}

//真正的操作-----------------------------------------------------

	public override int geID ()
	{
		return 1;
	}

	public override void actInThisState ()
	{
		//Debug.Log ("attack!");
		makeAttack ();
		makeLook ();
	}

	public override FSMBasic moveToNextState ()
	{
		if (theEMY == null || theEMY.isAlive == false) 
		{
			//Debug.Log ("attack to search");
			FSM_Search search = new FSM_Search ();
			search.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis);
			return search;
		}
		if (this.theThis.transform.position.y < this.theEMY.transform.position.y -0.75)
		{
			//Debug.Log ("attack to jump");
			FSM_Jump jump = new FSM_Jump ();
			jump.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis,this.theEMY);
			return jump;
		}
		if (Vector3.Distance (this.theThis.transform .position, this.theEMY.transform .position) >  this.theThis.theAttackAreaLength)
		{
			//Debug.Log ("attack to runafter");
			FSM_RunAfter runafter = new FSM_RunAfter ();
			runafter.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis,this.theEMY);
			return runafter;
		}
		return this;
	}
}
