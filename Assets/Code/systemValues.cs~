using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class systemValues : MonoBehaviour {
	//程序面板单元
	public static float updateTimeWait = 0.1f;
	public static  bool canAttack = false;


	public static bool isAttacking(Animator theAnimator)
	{
		//如果是移动状态说明没有攻击
		//如果不是移动状态就说明正在攻击
		//所以要加上“非”
		//此外因为所有的攻击动作都在第1层，所以层的选择需要是1
		return  !theAnimator.GetCurrentAnimatorStateInfo (1).IsName ("moveMent");
	}
}
