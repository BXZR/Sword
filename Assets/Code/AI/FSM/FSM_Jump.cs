using UnityEngine;
using System.Collections;

public class FSM_Jump : FSMBasic {

	float timer = 2f;
	public override void actInThisState ()
	{
		//Debug.Log ("jump");
		theAnimator.Play("jump");
		//this.theThis.transform.Translate (new Vector3 (0,1,0)*3*Time .deltaTime);
		timer -= Time.deltaTime;
	}

	public override FSMBasic moveToNextState ()
	{
		if (this.theThis.transform.position .y >=  this.theEMY.transform .position.y -0.75|| timer< 0 )
		{
			Debug.Log ("jump to runafter");
			FSM_RunAfter runafter = new FSM_RunAfter ();
			runafter.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis,this.theEMY);
			return runafter;
		}
		return this;
	}
}
