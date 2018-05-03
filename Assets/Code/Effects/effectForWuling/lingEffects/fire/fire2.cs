using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire2 : lingBasic {

	private PlayerBasic theAim = null;
	float damageMax = 25f;
	float damageAll = 25f;
	float damageSave = 0f;
	float timer = 0f;
	float timerAll = 0.5f;

	public override void makeStart ()
	{
		lingName = "火•阴  鬼狱幽焰";
		theType = wulingType.fire;
	}

	public override void OnAttack (PlayerBasic user, PlayerBasic aim)
	{
		theAim = aim;
		damageAll = damageMax;
		damageSave = 0f;
	}

	public override void effectOnUpdateTime (PlayerBasic user)
	{
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
		return "持续灼烧目标，造成"+damageMax+"物理伤害并触发受击特效";
	}

	public override int getYinYagType ()
	{
		return 2;
	}
}

