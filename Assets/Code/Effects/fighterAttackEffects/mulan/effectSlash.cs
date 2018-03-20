using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSlash :effectBasic{

	float hpsuckAdd = 0.05f;
	int ranNumber = 4; 
	float addPercent = 0.12f;

	float changeGate = 0.25f;
	float hpsuckOnChange = 0.03f;
	float damageSuckMax = 30;
	void Start ()
	{
		Init ();
	}


	public override bool isBE ()
	{
		return true;
	}

	public override void Init ()
	{
		theEffectName = "劫刃";
		//注意的是，最大生命值每回合都会更新的，这个最大生命值的削弱仅仅限制于本回合(如果削减最大斗气值就太变态了)
		theEffectInformation ="额外获得"+hpsuckAdd*100+"%的生命偷取\n攻击时拥有"+ranNumber*10 +"%机会使最终伤害提升"+addPercent *100+"%，同时恢复额外伤害等额的斗气";
		theEffedctExtraInformation = "特性：自身生命值低于"+changeGate*100+"%变化为[红莲]";
		makeStart ();

		thePlayer.ActerHpSuckPercent += hpsuckAdd;
		thePlayer.CActerHpSuckPercent += hpsuckAdd;
	}

	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		//红莲的效果
		if (thePlayer && thePlayer.ActerHp / thePlayer.ActerHpMax < changeGate)
		{
			float hpSuck = aim.ActerHpMax * hpsuckOnChange ;
			hpSuck = Mathf.Clamp (hpSuck , 0 , damageSuckMax);
			thePlayer.ActerHp += hpSuck;
			//附加的各种效果
			effectBasic[] effects = this.thePlayer.GetComponents<effectBasic> ();
			foreach (effectBasic EF in effects)
				EF.OnHpUp (hpSuck);
		}
		//劫刃的效果
		else
		{
			int RN = Random.Range (0, 10);
			if (RN < ranNumber) 
			{
				float makeDamage = TrueDamage * addPercent;
				this.thePlayer.OnAttackWithoutEffect (aim,makeDamage,true,true);
				this.thePlayer.ActerSp += makeDamage;
			}
		}
	}

	public override void effectOnUpdateTime ()
	{
		if (!thePlayer)
			return;
		
		if (thePlayer.ActerHp / thePlayer.ActerHpMax > changeGate) 
		{
			theEffectName = "劫刃";
			//注意的是，最大生命值每回合都会更新的，这个最大生命值的削弱仅仅限制于本回合(如果削减最大斗气值就太变态了)
			theEffectInformation = "额外获得" + hpsuckAdd * 100 + "%的生命偷取\n攻击时拥有" + ranNumber * 10 + "%机会使最终伤害提升" + addPercent * 100 + "%，同时恢复额外伤害等额的斗气";
			theEffedctExtraInformation = "特性：自身生命值低于"+changeGate*100+"%变化为[红莲]";
		} 
		else 
		{
			theEffectName = "红莲";
			theEffectInformation = "额外获得" + hpsuckAdd * 100 + "%的生命偷取";
			theEffectInformation += "\n攻击时额外吸取目标最大生命值"+hpsuckOnChange *100+"%生命\n每一击最多额外吸取"+damageSuckMax+"生命值";
			theEffedctExtraInformation = "特性：自身生命值高于"+changeGate*100+"%变化为[劫刃]";

		}
	}


}
