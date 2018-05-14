using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire2 : lingBasic {

	private PlayerBasic theAim = null;
	float damageMax = 30f;
	float damageAll = 30f;
	float damageSave = 0f;
	float timer = 0f;
	float timerAll = 0.5f;

	public override void makeStart ()
	{
		lingName = "火•阴";
		lingEffectName = "鬼狱幽焰";
		theType = wulingType.fire;
		coolingTimerMax = 7f;
		coolingTimer = 7f;
	}

	public override void OnAttack (PlayerBasic user, PlayerBasic aim)
	{
		if (isCooled) 
		{
			isCooled = false;
			theAim = aim;
			damageAll = damageMax;
			damageSave = 0f;
		}
	}

	public override void effectOnUpdateTime (PlayerBasic user)
	{
		makeCool ();
		if (theAim) 
		{
			timer += systemValues.updateTimeWait;
			if (timer >= timerAll) 
			{
				timer = 0f;
				float damage = damageAll*timerAll;
				user.OnAttackWithoutEffect (theAim, damage, false, true);
				damageSave += damage;
				if (damageSave >= damageMax)
					theAim = null;
			}
		}
	}
	public override string wulingInformation ()
	{
		return "灼烧目标造成共"+damageMax+"物理伤害并触发受击特效\n灼烧持续1秒,冷却时间"+coolingTimerMax+"秒";
	}

	public override int getYinYagType ()
	{
		return 2;
	}

	//学成奖励 --------------------------------------------------------------------------------------------//
	public override void learnedOverGet ()
	{
		systemValues.thePlayer.ActerWuliReDamage += 3f;
		systemValues.thePlayer.CActerWuliReDamage += 3f;
	}

	public override string wulingInformationForLearnOver()
	{
		return "+3反伤";
	}
}

