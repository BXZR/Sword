using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class jifei :effectBasic {

	//通用效果之击飞

	Vector3 aim = Vector3.zero;
	private float heightAdd = 1f;//击飞高度
	private Vector3 positionSave ;//记录一下原来的坐标
	float timerUsed = 0f;//记录飞行时间，因为飞行分成两个部分，一个上升一个下降

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
		lifeTimerAll = 5f;//每一个段时间才能够使用这个伤害
		timerForEffect = 2f; 
		heightAdd = 1.6f;//击飞高度
		theEffectName = "击飞";
		theEffectInformation ="将目标击飞,期间目标无法移动,持续"+ timerForEffect +"秒\n效果生效时间内目标可以连续被击飞\n击飞效果结束之后该目标免疫击飞"+(lifeTimerAll-timerForEffect)+"秒";
		makeStart ();
		Destroy (this,lifeTimerAll);
		//额外限制
		makeShut();
		if (this.thePlayer)//很多用于显示的调用没有playerBasic对象也不会有效果，所以需要常常考虑关于这个错误的小屏蔽		
		{
			positionSave = this.thePlayer.transform.position;
			aim = new Vector3 (this.thePlayer.transform.position.x, heightAdd + this.thePlayer.transform.position.y, this.thePlayer.transform.position.z);
		}

	}
	public override void updateEffect ()
	{
		if (isEffecting)
		{
			//可以连续击飞
			//做一下时间上的回退
			timerUsed *= 0.3f;
			aim = new Vector3 (this.thePlayer.transform.position.x, heightAdd + this.thePlayer.transform.position.y, this.thePlayer.transform.position.z);
		}
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
		if (isEffecting) 
		{
			timerUsed += Time.deltaTime;
			if (timerUsed < timerForEffect) 
			{
				if(timerUsed < timerForEffect*0.6f)
				   this.transform.position = Vector3.Lerp (this.transform.position, aim, 7f * Time.deltaTime );
				else
					this.transform.position = Vector3.Lerp (this.transform.position, positionSave , 7f * Time.deltaTime );
			}
		}
	}

	public override string getOnTimeFlashInformation ()
	{
		if(isEffecting)
		    return this.theEffectName+"\n[不可移动]";
		return this.theEffectName+"\n[免疫]";
	}
}
