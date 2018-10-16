using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectKOKnife : effectBasic{

	int attackCount = 0;
	int attackCountMax = 5;
	float attackPercent= 0.02f;

	void Start ()
	{
		Init ();
	}


	public override bool isBE ()
	{
		return true;
	}

	public override void OnLvUp ()
	{
		//print ("lvup");
		attackPercent += 0.003f;
		theEffectInformation = "攻击命中积累刀气，最多" + attackCountMax + "层\n满刀气攻击附加目标最大生命" + (attackPercent * 100).ToString("f1") + "%物理伤害\n伤害可随等级提升\n暴击可获得额外刀气";
	}

	public override void Init ()
	{
		theEffectName = "重剑无锋";
		theEffectInformation = "攻击命中积累刀气，最多" + attackCountMax + "层\n满刀气攻击附加目标最大生命" + (attackPercent * 100).ToString("f1") + "%物理伤害\n伤害可随等级提升\n暴击可获得额外刀气";
		makeStart ();
	}

	public override void OnAttack (PlayerBasic aim)
	{
		attackCount++;
		if (attackCount >= attackCountMax)
		{
			float damage = aim.ActerHpMax * attackPercent;
			this.thePlayer.OnAttackWithoutEffect (aim,damage,false,true);
			attackCount = 0;

		}
	}

	public override void OnSuperBlade (PlayerBasic aim, float Damage = 0)
	{
		attackCount++;
	}
		

	public override string getOnTimeFlashInformation ()
	{
		return this.theEffectName + "\n(" + attackCount + "/" + (attackCountMax-1) + "充能)";
	}


}
