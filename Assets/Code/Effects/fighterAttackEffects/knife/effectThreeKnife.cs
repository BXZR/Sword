﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectThreeKnife : effectBasic {

	int step = 0;//不同阶段的不同效果

	//第一次
	float aimDamageMinus = 0.25f;//削减伤害百分比
	float damageMinus = 0;//寄存被削减的攻击力
	PlayerBasic theAim ;//保留目标引用

	//第二次
	float acterhpUp = 25;//直接回复的生命值
	float actershieldHp = 40;//获得的护盾值
	//第三次
	float damagePercent = 0.07f;//追加的斩杀效果伤害百分比

	void Start () 
	{
		Init ();//进行初始化
	}

	//void OnDestroy()
	//{
		
	//}

	public override void Init ()
	{
		lifeTimerAll = 7f;
		timerForEffect = 3f;
		theEffectName = "摩诃婆娑";
		theEffectInformation ="在"+timerForEffect+"秒内的三次攻击命中触发不同特效：\n";
		theEffectInformation += "第一次，削减目标"+aimDamageMinus*100+"%攻击力，最多"+timerForEffect+"秒\n";
		theEffectInformation += "第二次，恢复"+acterhpUp+"生命值,获得"+actershieldHp+"护盾\n";
		theEffectInformation += "第三次，额外造成目标"+damagePercent*100+"%已损生命真实伤害\n";
		theEffectInformation += "冷却时间：" +(lifeTimerAll - timerForEffect ) +"秒";
		makeStart ();
		Destroy (this.GetComponent (this.GetType()),lifeTimerAll);



	} 

	public override void effectOnUpdateTime ()
	{
		addTimer ();
		if (isEffecting && timerForAdd > timerForEffect) 
		{
			isEffecting = false;
			//第一次
			if(theAim)
				theAim .ActerWuliDamage += damageMinus;
		}
	}

	public override void OnAttack (PlayerBasic aim)
	{
		if(thePlayer && isEffecting)
		{ 
			//第一次是伴随着初始化的
			if (step == 0)
			{ 
				//第一次削减攻击力
				theAim = aim;
				damageMinus = aim.ActerWuliDamage * aimDamageMinus;
				theAim.ActerWuliDamage -= damageMinus;
				step++;
			} 

			if (step == 1)
			{
				//第二次，偷取生命值
				hpup ();
				step++;
			} 
			else if (step == 2) 
			{
				//第三次，追加伤害
				float damage = (aim.ActerHpMax - aim.ActerHp) * damagePercent;
				aim.ActerHp -= damage;
				this.thePlayer.OnAttackWithoutEffect (aim, damage, true, true);
				step++;
			} 
			else
			{
				isEffecting = false;//标记，已经失效
			}
		}

	}

	void hpup()
	{
			this.thePlayer.ActerHp += acterhpUp;
			this.thePlayer.ActerShieldHp += actershieldHp;
			//附加的各种效果
			effectBasic [] effects = this.thePlayer.GetComponents<effectBasic> ();
			foreach (effectBasic EF in effects)
				EF.OnHpUp (acterhpUp);
	}

	public override string getOnTimeFlashInformation ()
	{
		if (isEffecting)
			return this.theEffectName + "\n(" + step + "/3刀)";
		return this.theEffectName + "\n[失效]";
	}
}
