using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectForSpeedMoveForward : effectBasic {

	CharacterController theMoveController = null ;
	private int indexUse = 0;
	private float moveSpeed = 20f;
	private float hengPercent = 0.75f;//横向突进速度百分比
    //公有技能效果之“向前突进”
	void Start ()
	{
		Init ();
	}


	public override void Init ()
	{
		lifeTimerAll = 3f;
		timerForEffect = 0.15f;
		theEffectName = "突进";
		theEffectInformation = "迅速向指定方向移动一小段距离\n持续"+timerForEffect+"秒，冷却"+lifeTimerAll+"秒，额外消耗4%当前斗气值\n突进过程中可选定和转向，且无法穿越障碍\n横向突进速度是向前突进速度的"+hengPercent*100+"%";
		makeStart ();
		Destroy (this, lifeTimerAll);
		if (thePlayer)
		{
			theMoveController = this.thePlayer.GetComponent < CharacterController> ();
			thePlayer.ActerSp *= 0.96f;
		}
	}


	public override void SetAttackLinkIndex (int index = 0)
	{
		indexUse = index;
		print ("move index = "+ indexUse);
	}

	public override void addTimer ()
	{
		timerForAdd += systemValues.updateTimeWait;
		if (isEffecting) 
		{
			if (timerForAdd > timerForEffect)
			{
				isEffecting = false;
			}
		}
	}
	public override void effectOnUpdateTime ()
	{
		addTimer ();

	}
	void Update()
	{
		//print ("timer add = "+ timerForAdd);
		if ( isEffecting &&  thePlayer &&  theMoveController  && theMoveController .enabled)//有时候需要强制无法移动
		{
			if(indexUse == 0)
				theMoveController .Move (this.thePlayer.transform .forward *moveSpeed*Time .deltaTime);//真实地进行行动(因为使用的是characterController，因此使用坐标的方式似乎比较稳妥)
			else if(indexUse == 1)
				theMoveController .Move (this.thePlayer.transform .right * -moveSpeed*Time .deltaTime *hengPercent);//真实地进行行动(因为使用的是characterController，因此使用坐标的方式似乎比较稳妥)
			else if(indexUse == 2)
				theMoveController .Move (this.thePlayer.transform .right * moveSpeed*Time .deltaTime *hengPercent);//真实地进行行动(因为使用的是characterController，因此使用坐标的方式似乎比较稳妥)
		}
	}

}
