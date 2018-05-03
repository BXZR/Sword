using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder2 : lingBasic {


	private float addPercent = 0.008f;
	public override void makeStart ()
	{
		lingName = "雷•阴  北境沧潭";
		theType = wulingType.thunder;
	}

	public override void OnAttack (PlayerBasic user, PlayerBasic aim, float TrueDamage)
	{
		float damage = (aim.ActerHpMax - aim.ActerHp) * addPercent;
		aim.ActerHp -= damage;
		user.OnAttackWithoutEffect (aim, damage, true, true);

	}

	public override string wulingInformation ()
	{
		return "攻击附加目标已损生命值"+addPercent*100+"%的真实伤害";
	}

	public override int getYinYagType ()
	{
		return 2;
	}
}
