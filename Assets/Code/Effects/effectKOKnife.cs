using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectKOKnife : effectBasic{

	int attackCount = 0;
	int attackCountMax = 4;
	float attackPercent= 0.02f;

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
		theEffectName = "重剑无锋";
		//注意的是，最大生命值每回合都会更新的，这个最大生命值的削弱仅仅限制于本回合(如果削减最大斗气值就太变态了)
		theEffectInformation ="每第"+ attackCountMax +"攻击命中附加目标最大生命值"+attackPercent*100+"%真实伤害";
		makeStart ();
	}

	public override void OnAttack (PlayerBasic aim)
	{
		attackCount++;
		if (attackCount >= attackCountMax)
		{
			aim.ActerHp -= aim.ActerHpMax * attackPercent;
		}
	}
}
