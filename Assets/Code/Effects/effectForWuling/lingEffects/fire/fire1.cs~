using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire1 : lingBasic {

	private PlayerBasic theAim = null;
	float shieldPercent = 0.40f;
	float damageMax = 80f;
	float damageAll = 80f;
	float damageSave = 0f;
	float timer = 0f;
	float timerAll = 0.5f;


	public override void makeStart ()
	{
		lingName = "火•阳";
		lingEffectName = "三味真火";
		theType = wulingType.fire;
		coolingTimerMax = 6f;
		coolingTimer = 6f;
	}

	public override void OnAttack (PlayerBasic user, PlayerBasic aim)
	{
		if (isCooled && theAim == null &&  aim != theAim ) 
		{
			isCooled = false;
			//这里应该用备份数值
			//永久增加效果是连C变量也会增加的
			//但是短暂的效果只是原变量增加
			//这里削减的仅仅是原有变量数值的百分比护甲
			damageMax = aim.CActerWuliShield * shieldPercent;
			damageAll = aim.CActerWuliShield * shieldPercent;
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
				float damage = damageAll * timerAll * 0.5f;
				theAim.ActerWuliShield -= damage;
				damageSave += damage;
				if (damageSave > damageMax)
				{
					theAim.ActerWuliShield += damageSave;
					theAim = null;
				}
			}
		}
	}
		
	public override string wulingInformation ()
	{
		return "灼烧目标最多"+shieldPercent*100+"%物理护甲\n灼烧持续2秒,冷却时间"+coolingTimerMax+"秒";
	}

	//学成奖励 --------------------------------------------------------------------------------------------//
	public override void learnedOverGet ()
	{
		systemValues.thePlayer.ActerWuliInerPercent += 0.05f;
		systemValues.thePlayer.CActerWuliInerPercent += 0.05f;
	}

	public override string wulingInformationForLearnOver()
	{
		return "+5%穿甲";
	}
}

