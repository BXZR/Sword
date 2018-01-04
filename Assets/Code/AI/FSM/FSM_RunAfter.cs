using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_RunAfter : FSMBasic {

	float timer = 7f;


	public override void actInThisState ()
	{
		//Debug.Log ("run after");
		theAnimator.Play("rotatePoseForward");
		this.theThis.transform.LookAt (theEMY.transform);
		this.theMoveController.SetDestination(theEMY.transform .position);

		timer -= Time.deltaTime;
	}

	public override FSMBasic moveToNextState ()
	{

		if (this.theThis.transform.position.y < this.theEMY.transform.position.y-0.75 )
		{
			Debug.Log ("runafter to jump");
			FSM_Jump jump = new FSM_Jump ();
			jump.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis,this.theEMY);
			return jump;
		}
		//Debug.Log ("theEMY name is "+ theEMY.name);
		if (Vector3.Distance (this.theThis.transform.position , this.theEMY.transform .position) <=  this.theThis.theAttackAreaLength)
		{
			Debug.Log ("runafter to attack");
			FSM_Attack attack = new FSM_Attack ();
			attack.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis,this.theEMY);
			return attack;
		}
		if (timer < 0)
		{
			Debug.Log ("runafter to search");
			FSM_Search search = new FSM_Search ();
			search.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis);
			return search;
		}
		return this;
	}
}
