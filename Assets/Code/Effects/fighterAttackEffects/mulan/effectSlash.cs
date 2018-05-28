using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSlash :effectBasic{

	float hpsuckAdd = 0.04f;
	int ranNumber = 4; 
	float addPercent = 0.10f;

	float changeGate = 0.25f;
	float hpsuckOnChange = 0.03f;
	float damageSuckMax = 30;

	float spAddMax = 25f;

	private int mode = 0; //记录当前状态

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
		lifeTimerAll = 5f;
		timerForEffect = 5f;

		mode = 0;
		theEffectName = "劫刃";
		//注意的是，最大生命值每回合都会更新的，这个最大生命值的削弱仅仅限制于本回合(如果削减最大斗气值就太变态了)
		theEffectInformation = "额外获得" + hpsuckAdd * 100 + "%的生命偷取\n攻击时拥有" + ranNumber * 10 + "%机会使最终伤害提升" + addPercent * 100 + "%\n恢复额外伤害值的斗气，最多" + spAddMax + "斗气";
		theEffedctExtraInformation = "特性：魔中佛，生命低于" + changeGate * 100 + "%转为【红莲】\n"+ lifeTimerAll+"秒内被动转化只会发生一次";
		makeStart ();

		thePlayer.ActerHpSuckPercent += hpsuckAdd;
		thePlayer.CActerHpSuckPercent += hpsuckAdd;
	}

	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		//红莲的效果
		if (mode == 1)
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
				this.thePlayer.ActerSp += Mathf.Clamp( makeDamage,0, spAddMax );
			}
		}
	}


	public override void addTimer ()
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


	public override void effectOnUpdateTime ()
	{
		if (!thePlayer)
			return;
		addTimer ();
		if (!isEffecting)
			return;

		if (thePlayer.ActerHp / thePlayer.ActerHpMax > changeGate) 
		{
			if (mode != 0) 
			{
				theEffectName = "劫刃";
				//注意的是，最大生命值每回合都会更新的，这个最大生命值的削弱仅仅限制于本回合(如果削减最大斗气值就太变态了)
				theEffectInformation = "额外获得" + hpsuckAdd * 100 + "%的生命偷取\n攻击时拥有" + ranNumber * 10 + "%机会使最终伤害提升" + addPercent * 100 + "%\n恢复额外伤害值的斗气，最多" + spAddMax + "斗气";
				theEffedctExtraInformation = "特性：魔中佛，生命低于" + changeGate * 100 + "%转为【红莲】\n"+ lifeTimerAll+"秒内被动转化只会发生一次";
				mode = 0;
				isEffecting = false;
			}
		} 
		else 
		{
			if (mode != 1) 
			{
				theEffectName = "红莲";
				theEffectInformation = "额外获得" + hpsuckAdd * 100 + "%的生命偷取";
				theEffectInformation += "\n攻击时额外吸取目标最大生命值" + hpsuckOnChange * 100 + "%生命\n每一击最多额外吸取" + damageSuckMax + "生命值";
				theEffedctExtraInformation = "特性：佛中魔，生命高于" + changeGate * 100 + "%转为【劫刃】\n"+ lifeTimerAll+"秒内被动转化只会发生一次";
				mode = 1;
				isEffecting = false;
			}

		}
	}

	public override string getOnTimeFlashInformation ()
	{
		if (isEffecting)
			return this.theEffectName;
		return this.theEffectName + "\n[冷却中]";
	}

}
