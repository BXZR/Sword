﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMStage : effectBasic   {

	//操作FSM的操作类，也就是整个AI任务的操作类
	private NavMeshAgent theMoveController;//AI人物的移动控制类
	private attackLinkController theAttackLlinkController;//AI人物的动作控制类
	public FSMBasic theStateNow ;
	private PlayerBasic thethis; 
	private Animator theAnimator;
	public float AIThinkTimer = 0.20f;//AI每隔一段时间再进行思考
	//这是一个值得优化的点，在一些条件下关闭掉AI计算会非常省事
	//AI的计算很重并且同时计算的很多
	public bool theAiIsActing = true;//AI是否计算的标记

	//仇恨时间
	public float angerTimer = 4f;//仇恨时间，也是追击的总时长
	public float angetTimerMax = 4f;//仇恨时间上限

	bool isDeadMake = false;

	void Start ()
	{
		thethis = this.GetComponent <PlayerBasic> ();
		theMoveController = this.GetComponentInChildren<NavMeshAgent> ();
		theAttackLlinkController = this.GetComponent<attackLinkController> ();
		theAnimator = this.GetComponentInChildren<Animator> ();
		//print ("AI stage is inited");

		theStatesSaved = new List<FSMBasic> ();
		//这里需要建立一个初始的状态
		//theStateNow = new FSM_Search ();
		theStateNow = getState(4);
		theStateNow.makeState (theMoveController , theAttackLlinkController , theAnimator ,thethis);
		theStateNow.OnFSMStateStart ();

		makeAIStart();//AI重新激活，刷新时间
		//带MeshgRenderer的有部分可优化之处
		MeshRenderer theRender = this.GetComponentInChildren<MeshRenderer>();
		if (theRender)
			theRender.gameObject.AddComponent <FSMRenderSwitch> ();
		this.transform.root.tag = "AI";//打上标记方便找
	}

	public override bool isBE ()
	{
		return true;
	}

	public override bool isShowing ()
	{
		return false;
	}

	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		makeAIStart();//AI重新激活，刷新时间
	}

	public override void OnBeAttack (PlayerBasic attacker)
	{
		//Vector3 minus = new Vector3 (0f , attacker.transform.rotation.eulerAngles.y - this.transform.rotation.eulerAngles.y , 0f);
		//this.transform.rotation = Quaternion.Lerp(this.transform.rotation , Quaternion.Euler(minus + this.transform .rotation.eulerAngles) , 360f);
		this.transform.LookAt (attacker.transform);
		//FSM_Attack attack = new FSM_Attack ();
		//attack.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.thethis  ,attacker);
		//theStateNow = attack;
		theStateNow = getState(1);
		theStateNow.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.thethis  ,attacker);
		theStateNow.OnFSMStateStart ();
		think ();//强制思考一下，否则会有延迟
//		try
//		{
//			NavMeshAgent theAgent = this.GetComponent <NavMeshAgent> ();
//			if(theAgent.isActiveAndEnabled)
//			{
//				theAgent.isStopped = true;
//				theAgent.isStopped = false;
//			    theAgent.SetDestination (this.transform .position);
//			}
//		}
//		catch
//		{
//			 print ("当前处于无法被设置目标的状态，因此AI会继续向原先的目的地移动");
//		}
		angerTimer = angetTimerMax;//收到攻击的时候刷新仇恨
		makeAIStart();//AI重新激活，刷新时间
	}


	void angerCanculate()
	{
		if (theStateNow != null && thethis.isAlive) 
		{
			if (theStateNow.geID () == 1)//攻击状态
			{
				angerTimer = angetTimerMax;//攻击的时候刷新仇恨
			}
			else if(theStateNow.geID () == 2 || theStateNow.geID () == 3)//跳跃和追击状态实际上都是在追杀目标
			{
				angerTimer --;
				theStateNow.timer = angerTimer;
			}
		}
	}

	//有关AI计算状态-----------------------------------------------------------

	public void  moveTotheState(FSMBasic theStateIn , PlayerBasic aim )
	{
		theStateIn.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, this.thethis ,aim);
		theStateNow.OnFSMStateEnd ();
		theStateNow = theStateIn;
		theStateNow.OnFSMStateStart ();
	}

	//刷新AI计算时间
	public void makeAIStart()
	{
		CancelInvoke ();
		theAiIsActing = true;

		//这里可能会因为数值设定有一定时间上的误差，但是我觉得没什么所谓
		InvokeRepeating ("angerCanculate" , 2f , 1f);
		InvokeRepeating ("think", 2f, AIThinkTimer);
	}

	void  think()
	{
		if (theStateNow != null && thethis.isAlive)
		{
			//AI转换状态
			FSMBasic theStateNew = theStateNow.moveToNextState (this);//思考进入到下一个状态或许可以慢一点进行
			if (theStateNew.geID () != theStateNow.geID ())
			{
				theStateNow.OnFSMStateEnd ();//结束效果
				theStateNow = theStateNew;
				theStateNow.OnFSMStateStart ();//开始效果
			}
		}
	}

	//有关AI计算状态-----------------------------------------------------------


	//AI状态私有的池================================================================
	//优势：状态保存并且由同一控制单元存取
	//劣势，文本不够清晰，ID的绑定很紧密
	public List<FSMBasic> theStatesSaved ;
	public FSMBasic getState(int ID)
	{
		for (int i = 0; i < theStatesSaved.Count; i++)
			if (theStatesSaved [i].geID () == ID)
				return theStatesSaved [i];

		FSMBasic theOne = new FSMBasic ();
		switch (ID) 
		{
		case 1:
			{
				theOne = new FSM_Attack ();
				theStatesSaved.Add (theOne);
			}
			break;
		case 2:
			{
				theOne = new FSM_Jump ();
				theStatesSaved.Add (theOne);
			}
			break;
		case 3:
			{
				theOne = new FSM_RunAfter ();
				theStatesSaved.Add (theOne);
			}
			break;
		case 4:
			{
				theOne = new FSM_Search ();
				theStatesSaved.Add (theOne);
			}
			break;
		}
		return theOne;
	}
	//===============================================================================

    //很多操作都是连续的，对于AI来说或许用连续的方法计算会比较好
	void Update () 
	{
		//出于优化考虑不必让AI一直计算下去
		//此外这也可是“怪物僵直”状态的一个做法
		if (theAiIsActing && systemValues.isGamming)
		{
			if (thethis.isAlive) 
			{
				if (theStateNow != null) 
				{
					theStateNow.actInThisState ();
				} 
				else 
 				{
					//print ("no state");
				} 
			}
			else if (isDeadMake == false) 
			{
				theAiIsActing = false;//结束循环
				isDeadMake = true;
				this.GetComponent <NavMeshAgent> ().enabled = false;
				this.gameObject.AddComponent<Rigidbody> ();
				this.gameObject.AddComponent<BoxCollider> ();
				Destroy (this.gameObject, 15f);//尸体保留久一点似乎更好玩（尸体也是路障啊）
			}
		}
	}
}
