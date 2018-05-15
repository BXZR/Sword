using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playModeEffect3 : effectBasic {


	private float percent = 0.15f;

	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{
		theEffectName = "严酷训诫";
		theEffectInformation = "敌我攻击都会额外带走目标最大生命值的"+percent*100+"%\n(此效果不会触发任何额外特效)";
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

