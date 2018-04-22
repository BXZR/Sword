using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipEffect_ExtraDamage : effectBasic {

	int count = 0;
	int countMax = 5;
	float damage = 30f;
	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{
		theEffectName = "青丘狐刃";
		theEffectInformation = "第"+countMax +"次攻击造成额外"+damage+"物理伤害";

		makeStart ();
	}
		
	public override void OnAttack (PlayerBasic aim)
	{
		count++;
		if (count == countMax) 
		{
			count = 0;
			this.thePlayer.OnAttackWithoutEffect (aim,damage,false,true);
		}
	}

	public override bool isBE ()
	{
		return true;
	}

	public override string getOnTimeFlashInformation ()
	{
		return this.theEffectName + "\n(" + count + "/" + (countMax-1) + "充能)";
	}


}
