using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStage : MonoBehaviour {

	//操作FSM的操作类，也就是整个AI任务的操作类
	private move theMoveController;//AI人物的移动控制类
	private attackLinkController theAttackLlinkController;//AI人物的动作控制类
	private FSMBasic theStateNow = new FSM_Search();
	private PlayerBasic thethis; 

	void Start ()
	{
		thethis = this.GetComponent <PlayerBasic> ();
		theMoveController = this.GetComponentInChildren<move> ();
		theAttackLlinkController = this.GetComponent<attackLinkController> ();
		//这里需要建立一个初始的状态
		theStateNow.makeState (theMoveController , theAttackLlinkController , thethis);
		//print ("AI stage is inited");
	}
    //很多操作都是连续的，对于AI来说或许用连续的方法计算会比较好
	void Update () 
	{
		if (theStateNow != null)
		{
			//print ("AI is acting");
			theStateNow.actInThisState ();
			theStateNow = theStateNow.moveToNextState ();//思考进入到下一个状态或许可以慢一点进行
		}
	}
}
