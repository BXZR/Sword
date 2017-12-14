using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectDragonAttack  : effectBasic{

	float hpUpWhenattack = 18f;
	float upGate  = 0.5f;

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
		theEffectInformation ="任何攻击的起手阶段恢复"+hpUpWhenattack+"生命\n如果生命百分比低于"+ upGate*100 +"%,恢复效果翻倍";
		makeStart ();
	}

	public override void onAttackAction ()
	{
		this.thePlayer.ActerHp += hpUpWhenattack;
		if(thePlayer.ActerHp/thePlayer.ActerHpMax < upGate)
			this.thePlayer.ActerHp += hpUpWhenattack;
	}
}
