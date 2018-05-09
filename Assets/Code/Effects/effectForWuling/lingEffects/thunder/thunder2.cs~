using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder2 : lingBasic {


	private float addPercent = 0.02f;
	public override void makeStart ()
	{
		lingName = "雷•阴  北境沧潭";
		theType = wulingType.thunder;
		coolingTimerMax = 10f;
		coolingTimer = 10f;
	}

	public override void OnAttack (PlayerBasic user, PlayerBasic aim, float TrueDamage)
	{
		if (isCooled)
		{
			isCooled = false;
			float damage = (aim.ActerHpMax - aim.ActerHp) * addPercent;
			aim.ActerHp -= damage;
			user.OnAttackWithoutEffect (aim, damage, true, true);
		}
		else 
		{
			coolingTimer-= 0.5f;
		}
	}
	public override void effectOnUpdateTime (PlayerBasic user)
	{
		makeCool ();
	}

	public override string wulingInformation ()
	{
		return "攻击附加目标已损生命值"+addPercent*100+"%的真实伤害\n冷却时间"+coolingTimerMax+"秒，攻击命中可减少0.5秒冷却时间";
	}

	public override int getYinYagType ()
	{
		return 2;
	}
}
