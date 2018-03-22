using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectForSpeedMoveForward : effectBasic {

	CharacterController theMoveController = null ;
    //公有技能效果之“向前突进”
	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{
		lifeTimerAll = 3f;
		timerForEffect = 0.18f;
		theEffectName = "突进";
		theEffectInformation = "迅速向前移动一小段距离，持续"+timerForEffect+"秒，冷却"+lifeTimerAll+"秒\n突进额外消耗4%当前斗气值\n(突进过程中可选定和转向，但无法穿越障碍)";
		makeStart ();
		Destroy (this, lifeTimerAll);
		if (thePlayer)
		{
			theMoveController = this.thePlayer.GetComponent < CharacterController> ();
			thePlayer.ActerSp *= 0.96f;
		}
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
			theMoveController .Move (this.thePlayer.transform .forward *18*Time .deltaTime);//真实地进行行动(因为使用的是characterController，因此使用坐标的方式似乎比较稳妥)
	}

}
