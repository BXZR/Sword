using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectDragonAttack  : effectBasic{

	float hpupValue = 2f;
	float hpUpWhenattack = 0.018f;
	float upGate  = 0.35f;
	float shieldPercentNew = 0.10f;

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
		theEffectName = "降龙十八掌";
		//注意的是，最大生命值每回合都会更新的，这个最大生命值的削弱仅仅限制于本回合(如果削减最大斗气值就太变态了)
		theEffectInformation ="攻击起手时恢复("+hpupValue+"+已损生命"+hpUpWhenattack*100+"%)\n生命百分比低于"+ upGate*100 +"%效果翻倍\n（连招攻击此效果只触发一次）\n溢出的治疗效果将转化为护盾但护盾上限降低为"+shieldPercentNew*100+"%";
		makeStart ();
		thePlayer.ActerShieldMaxPercent = shieldPercentNew;
	}

	public override void onAttackAction ()
	{
		float hpupTrue = hpUpWhenattack * (thePlayer.ActerHpMax - thePlayer.ActerHp) +hpupValue ;
		float overflow = this.thePlayer.ActerHp + hpupTrue - thePlayer.ActerHpMax;
		if (overflow >= 0) 
		{
			thePlayer.ActerShieldHp += overflow;
			hpupTrue -= overflow;
		}
		if (thePlayer.ActerHp / thePlayer.ActerHpMax < upGate)
			hpupTrue *= 2;
		
		this.thePlayer.ActerHp += hpupTrue;
		//附加的各种效果
		effectBasic [] effects = this.thePlayer.GetComponents<effectBasic> ();
		foreach (effectBasic EF in effects)
			EF.OnHpUp (hpupTrue);
	}

	public override string getOnTimeFlashInformation ()
	{
		if (thePlayer.ActerHp / thePlayer.ActerHpMax > upGate)
			return this.theEffectName;
		return this.theEffectName +"×2";
	}
}
