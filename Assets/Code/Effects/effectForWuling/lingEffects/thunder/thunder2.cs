using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder2 : lingBasic {


	private float addPercent = 0.1f;
	public override void makeStart ()
	{
		lingName = "雷•阳  水脏";
		theType = wulingType.thunder;
	}

	public override void OnAttack (PlayerBasic user, PlayerBasic aim, float TrueDamage)
	{
		float damage = (aim.ActerHpMax - aim.ActerHp)  * 0.015f;
		aim.ActerHp -= damage;
		user.OnAttackWithoutEffect (aim, damage, true, true);

	}

	public override string wulingInformation ()
	{
		return "攻击附加目标已损生命值1.5%的真实伤害";
	}
}
