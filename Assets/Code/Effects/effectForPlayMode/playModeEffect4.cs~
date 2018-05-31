using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playModeEffect4 : effectBasic {


	private float percent = 10f;

	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{
		theEffectName = "一击必杀";
		theEffectInformation = "攻击力已经毫无意义，敌我均是一击必杀";
		makeStart ();
	}

	//严酷
	public override void OnBeAttack (float damage = 0)
	{
		if (damage <= 0)
			return;

		this.thePlayer.ActerHp -= this.thePlayer.ActerHpMax * percent;
	}

	//训诫
	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		if (TrueDamage <= 0)
			return;
		aim.ActerHp -= this.thePlayer.ActerHpMax * percent;
	}
}
