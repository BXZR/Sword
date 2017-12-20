using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBasic : MonoBehaviour {

	//单机版游戏人物AI的有限状态机的基类

	//统一的状态机初始化参数
	private move theMoveController;//AI人物的移动控制类
	private attackLinkController theAttackLlinkController;//AI人物的动作控制类
	public void makeState(move theMoveControllerIn , attackLinkController theAttacklinkControllerIn)
	{
		theMoveController = theMoveControllerIn;
		theAttackLlinkController = theAttacklinkControllerIn;
	}


	//在这个状态下需要做什么
	public virtual void  actInThisState()
	{
		
	}

	//思考转换到下一个状态
	//返回下一个状态，这个状态可以是自身
	public virtual FSMBasic moveToNextState()
	{
		return this;
	}
}
