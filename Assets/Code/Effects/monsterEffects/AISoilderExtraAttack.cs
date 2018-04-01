using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISoilderExtraAttack : effectBasic {

	//怪物普攻附加的伤害
	public float extraDamage = 5f;

	void Start ()
	{
		Init ();
	}
		
	public override void OnAttack (PlayerBasic aim)
	{
		if (isEffecting)
		{
			isEffecting = false;
			this.thePlayer.OnAttackWithoutEffect (aim, extraDamage, true, true);
			this.thePlayer.ActerSp -= 5f;
		}
	}

	public override void Init ()
	{
		//print ("灭却浮屠发动");
		lifeTimerAll = 7f;//每一个段时间才能够使用这个伤害
		timerForEffect = 7f; 
		theEffectName = "尖牙利爪";
		theEffectInformation ="每隔"+lifeTimerAll+"秒，普攻附加"+extraDamage+"真实伤害，额外消耗自身"+5+"斗气\n这个伤害不再触发额外攻击效果";
		makeStart ();
	}
	public override void effectOnUpdateTime ()
	{
		if (!isEffecting) 
		{
			timerForAdd += systemValues.updateTimeWait;
			if (timerForAdd > lifeTimerAll)
			{
				timerForAdd = 0;
				isEffecting = true;
			}
		}
	}
}
