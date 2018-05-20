using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectForSpeedMoveForward : effectBasic {

	CharacterController theMoveController = null ;
	private int indexUse = 0;
	private float moveSpeed = 20f;
	private float hengPercent = 0.75f;//横向突进速度百分比
	//允许额外连续突进一次
	bool isOverMove = false;

    //公有技能效果之“向前突进”
	void Start ()
	{
		Init ();
	}


	public override void Init ()
	{
		lifeTimerAll = 7f;
		timerForEffect = 0.15f;
		theEffectName = "突进";
		theEffectInformation = "迅速向指定方向移动一小段距离\n持续"+timerForEffect+"秒，冷却"+(lifeTimerAll-timerForEffect)+"秒\n横向突进速度是前向突进速度的"+hengPercent*100+"%";
		theEffedctExtraInformation = "特性：勉力冲刺，冷却中可消耗8%最大斗气再突进一次\n勉力冲刺后斗气恢复效率减少30%，直到冷却完毕";
		makeStart ();
		if (thePlayer)
		{
			theMoveController = this.thePlayer.GetComponent < CharacterController> ();
		}
		Destroy (this,lifeTimerAll);
	}


	public override void updateEffect ()
	{
		if (isOverMove == false)
		{
			isOverMove = true;

			theEffectName = "勉力冲刺";

			timerForEffect = timerForEffect + timerForAdd;

			float theSPUse = thePlayer.ActerSpMax * 0.08f;
			thePlayer.ActerSp -= theSPUse;
			effectBasic[] Effects = thePlayer.GetComponentsInChildren<effectBasic> ();
			for (int i = 0; i < Effects.Length; i++)
				Effects [i].OnUseSP (theSPUse);
			
			isEffecting = true;

		}
	}

	public override void SetAttackLinkIndex (int index = 0)
	{
		indexUse = index;
		//print ("move index = "+ indexUse);
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

		//斗气恢复效率减半
		if (isOverMove) 
		{
			thePlayer.ActerSp -= thePlayer.ActerSpUp * 0.3f * systemValues.updateTimeWait;
		}

	}

	public override string getOnTimeFlashInformation ()
	{
		if(isOverMove == false)
		return this.theEffectName +"\n(第一段)";
		return  this.theEffectName +"\n[已失效]";
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
