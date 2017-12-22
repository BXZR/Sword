using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_RunAfter : FSMBasic {

	float timer = 7f;

	public override void actInThisState ()
	{
		//Debug.Log ("run after");
		this.theThis.transform.LookAt (theEMY.transform);
		this.theMoveController.MoveForwardBack(3f);

		timer -= Time.deltaTime;
	}

	public override FSMBasic moveToNextState ()
	{
		//Debug.Log ("theEMY name is "+ theEMY.name);
		if (Vector3.Distance (this.theThis.transform.position , this.theEMY.transform .position) <=  this.theThis.theAttackAreaLength)
		{
			Debug.Log ("runafter to attack");
			FSM_Attack attack = new FSM_Attack ();
			attack.makeState (this.theMoveController, this.theAttackLlinkController, this.theThis,this.theEMY);
			return attack;
		}
		if (timer < 0)
		{
			Debug.Log ("runafter to search");
			FSM_Search search = new FSM_Search ();
			search.makeState (this.theMoveController, this.theAttackLlinkController, this.theThis);
			return search;
		}
		return this;
	}
}
