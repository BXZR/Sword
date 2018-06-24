using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wudi : effectBasic {

	int  count = 0;
	int countMax = 2;
	void Start ()
	{
		Init ();
	}

	public override void OnBeAttack (float damage = 0)
	{
		if (thePlayer) 
		{
			thePlayer.ActerHp += damage;
			count++;
			if (count >= countMax)
			Destroy (this);
		}
	}

	public override void Init ()
	{
		//print ("灭却浮屠发动");
		lifeTimerAll = 8f;//每一个段时间才能够使用这个伤害
		timerForEffect = 8f; 
		theEffectName = "无敌金身";
		theEffectInformation ="受到伤害会直接返还损失的生命,最多抵挡两次攻击\n最多持续8秒";
		makeStart ();
		Destroy (this,lifeTimerAll);
	}
	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
	}

	public override string getOnTimeFlashInformation ()
	{
		return this.theEffectName +"\n("+count+"/"+countMax + ")";
	}
}
