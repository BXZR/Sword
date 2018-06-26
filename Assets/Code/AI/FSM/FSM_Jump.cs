using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class FSM_Jump : FSMBasic {

	public override int geID ()
	{
		return 2;
	}

	public override void OnFSMStateEnd ()
	{

		if (this.theMoveController && this.theEMY != null)
		{
			this.theMoveController.enabled = true;
			this.theMoveController.SetDestination (theEMY.transform .position);
		}
	}

	public override void OnFSMStateStart ()
	{
		this.theMoveController.enabled = false;
		timer = 1.8f;
	}

	public override void actInThisState ()
	{
		
		//Debug.Log ("jump");
		theAnimator.Play("jump");
		this.theThis.transform.LookAt (theEMY.transform);
		this.theThis.transform.Translate (new Vector3(0,0,1)*2.5f*Time .deltaTime);
		timer -= Time.deltaTime;
	 
	}

	public override FSMBasic moveToNextState ()
	{
		if (this.theThis.transform.position .y >=  this.theEMY.transform .position.y -0.75|| timer< 0 )
		{
			//Debug.Log ("jump to runafter");
			FSM_RunAfter runafter = new FSM_RunAfter ();
			runafter.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis,this.theEMY);
			return runafter;
		}
		return this;
	}
	public override FSMBasic moveToNextState ( FSMStage theController)
	{
		if (this.theThis.transform.position .y >=  this.theEMY.transform .position.y -0.75|| timer< 0 )
		{
			//Debug.Log ("jump to runafter");
			//FSM_RunAfter runafter = new FSM_RunAfter ();
			FSMBasic runafter = theController.getState(3);
			runafter.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.theThis,this.theEMY);
			this.theMoveController.enabled = true;
			return runafter;
		}
		return this;
	}
}
