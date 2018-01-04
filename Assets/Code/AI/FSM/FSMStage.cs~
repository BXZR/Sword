using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMStage : MonoBehaviour {

	//操作FSM的操作类，也就是整个AI任务的操作类
	private NavMeshAgent theMoveController;//AI人物的移动控制类
	private attackLinkController theAttackLlinkController;//AI人物的动作控制类
	private FSMBasic theStateNow = new FSM_Search();
	private PlayerBasic thethis; 
	private Animator theAnimator;

	bool isDeadMake = false;

	void Start ()
	{
		thethis = this.GetComponent <PlayerBasic> ();
		theMoveController = this.GetComponentInChildren<NavMeshAgent> ();
		theAttackLlinkController = this.GetComponent<attackLinkController> ();
		theAnimator = this.GetComponentInChildren<Animator> ();
		//这里需要建立一个初始的状态
		theStateNow.makeState (theMoveController , theAttackLlinkController , theAnimator ,thethis);
		//print ("AI stage is inited");
	}
    //很多操作都是连续的，对于AI来说或许用连续的方法计算会比较好
	void Update () 
	{
		if (theStateNow != null && thethis.isAlive) 
		{
			//print ("AI is acting");
			theStateNow.actInThisState ();
			theStateNow = theStateNow.moveToNextState ();//思考进入到下一个状态或许可以慢一点进行
		}
		else if (isDeadMake == false)
		{
			isDeadMake = true;
			this.GetComponent <NavMeshAgent> ().enabled = false;
			this.gameObject.AddComponent<Rigidbody> ();
			Destroy (this.gameObject ,2f);
		}
	}
}
