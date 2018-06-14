using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipEffect_mojian : effectBasic {
	
	void Start ()
	{
		Init ();
	}

	float damageMax = 2000f;
	float damageNow = 0f;
	float damageAddPerMax = 5f;

	public override void Init ()
	{
		theEffectName = "魔剑残影";
		theEffectInformation = "每造成"+damageMax+"伤害，永久增加"+damageAddPerMax+"攻击力";

		makeStart ();
	}
		
	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		damageNow += TrueDamage;
		if (damageNow > damageMax) 
		{
			damageNow = 0f;
			thePlayer.ActerWuliDamage += damageAddPerMax;
			thePlayer.CActerWuliDamage += damageAddPerMax;
		}
	}

	public override string getOnTimeFlashInformation ()
	{
		return this.theEffectName + "\n(" + (damageNow *100 /damageMax).ToString("f0") +"%充能)";
	}


}
