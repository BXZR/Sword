using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class jifei :effectBasic {

	//通用效果之击飞

	Vector3 aim = Vector3.zero;
	void Start ()
	{
		Init ();
	}

	//禁止移动
	void makeShut()
	{
		if (!thePlayer)
			return;
		
		move theMoveController = this.thePlayer.GetComponent<move> ();
		if (theMoveController)
			theMoveController.canMove = false;
		NavMeshAgent theMeshAgent = this.thePlayer.GetComponent <NavMeshAgent> ();
		if (theMeshAgent )
			theMeshAgent.enabled = false;
		FSMStage theStage = thePlayer.GetComponent <FSMStage> ();
		if (theStage)
			theStage.enabled = false;
	}

	void makeOpen()
	{
		if (!thePlayer)
			return;
		
		move theMoveController = this.thePlayer.GetComponent<move> ();
		if (theMoveController)
			theMoveController.canMove = true;
		NavMeshAgent theMeshAgent = this.thePlayer.GetComponent <NavMeshAgent> ();
		if (theMeshAgent )
			theMeshAgent.enabled = true;
		FSMStage theStage = thePlayer.GetComponent <FSMStage> ();
		if (theStage)
			theStage.enabled = true;
	}

	void OnDestroy()
	{
		makeOpen ();//重新允许移动
	}

	public override void Init ()
	{
		//print ("灭却浮屠发动");
		lifeTimerAll = 7f;//每一个段时间才能够使用这个伤害
		timerForEffect = 1.5f; 
		theEffectName = "击飞";
		theEffectInformation ="将目标击飞，持续"+ timerForEffect +"秒\n在击飞期间目标无法移动\n同一个单位在"+lifeTimerAll +"秒内只会被击飞一次";
		makeStart ();
		Destroy (this,lifeTimerAll);
		//额外限制
		makeShut();
		if(this.thePlayer)//很多用于显示的调用没有playerBasic对象也不会有效果，所以需要常常考虑关于这个错误的小屏蔽		 
        aim = new Vector3 (this.thePlayer.transform.position.x , 3f+ this.thePlayer.transform.position.y, this.thePlayer.transform.position.z);

	}
	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
		if (isEffecting) 
		{
			if (timerForAdd > timerForEffect)
			{
				isEffecting = false;
				makeOpen ();//重新允许移动
			}
		}

	}


	void Update()
	{
		if(isEffecting)
			this.transform.position = Vector3.Lerp (this.transform .position , aim , 7f *Time.deltaTime /timerForEffect  );
	}

	public override string getOnTimeFlashInformation ()
	{
		if(isEffecting)
		    return this.theEffectName+"\n[不可移动]";
		return this.theEffectName+"\n[免疫]";
	}
}
