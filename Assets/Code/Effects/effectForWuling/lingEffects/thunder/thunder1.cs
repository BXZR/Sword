using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder1 : lingBasic {


	private float addPercent = 0.02f;
	public override void makeStart ()
	{
		lingName = "雷•阳";
		lingEffectName = "心府绛宫";
		theType = wulingType.thunder;
		coolingTimerMax = 10f;
		coolingTimer = 10f;
	}

	public override void OnAttack (PlayerBasic user, PlayerBasic aim, float TrueDamage)
	{
		if (isCooled) 
		{
			isCooled = false;
			float damage = user.ActerHpMax * addPercent;
			aim.ActerHp -= damage;
			user.OnAttackWithoutEffect (aim, damage, true, true);
		} 
		else 
		{
			coolingTimer-=0.5f;
		}
	}


	public override void effectOnUpdateTime (PlayerBasic user)
	{
		makeCool ();
	}

	public override string wulingInformation ()
	{
		return "攻击附加自身最大生命值"+(addPercent*100)+"%的真实伤害\n冷却时间"+coolingTimerMax+"秒，攻击命中可减少0.5秒冷却时间";
	}

	//学成奖励 --------------------------------------------------------------------------------------------//
	public override void learnedOverGet ()
	{
		systemValues.thePlayer.ActerSuperBaldePercent += 0.03f;
		systemValues.thePlayer.CActerSuperBaldePercent += 0.03f;
	}

	public override string wulingInformationForLearnOver()
	{
		return "+3%暴击率";
	}
}

