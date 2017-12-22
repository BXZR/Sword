using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBasic  {

	//单机版游戏人物AI的有限状态机的基类

	//统一的状态机初始化参数
	public move theMoveController;//AI人物的移动控制类
	public attackLinkController theAttackLlinkController;//AI人物的动作控制类
	//保留敌我引用
	public PlayerBasic theEMY ;
	public PlayerBasic theThis;

	public void makeState(move theMoveControllerIn , attackLinkController theAttacklinkControllerIn , PlayerBasic thethisIn,PlayerBasic theEMYIn = null)
	{
		theMoveController = theMoveControllerIn;
		theAttackLlinkController = theAttacklinkControllerIn;
		theEMY = theEMYIn;
		theThis = thethisIn;
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
