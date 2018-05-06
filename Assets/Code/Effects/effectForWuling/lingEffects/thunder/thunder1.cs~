using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder1 : lingBasic {


	private float addPercent = 0.004f;
	public override void makeStart ()
	{
		lingName = "雷•阳  心府绛宫";
		theType = wulingType.thunder;
	}

	public override void OnAttack (PlayerBasic user, PlayerBasic aim, float TrueDamage)
	{
		float damage = user.ActerHpMax * addPercent;
		aim.ActerHp -= damage;
		user.OnAttackWithoutEffect (aim, damage, true, true);

	}

	public override void onAttackAction (PlayerBasic user)
	{
		base.onAttackAction (user);
	}

	public override string wulingInformation ()
	{
		return "攻击附加自身最大生命值"+(addPercent*100)+"%的真实伤害";
	}
}

