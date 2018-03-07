using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectDragonMiss : effectBasic{

	float timer = 3f;
	float spupOnAttack = 4;
	float hpupPercent = 0.1f;
	void Start () 
	{
		Init ();//进行初始化
	}

	public override bool isBE ()
	{
		return false;
	}
		
	public override void Init ()
	{
		//print("<或跃在渊>正在初始化");
		theEffectName = "龙战于野";
		theEffectInformation = "攻击命中回复"+spupOnAttack+"斗气,每1%斗气提高伤害0.15%\n此外，所有生命回复效果提升"+hpupPercent *100+"%，持续"+timer+"秒";
		makeStart ();
		Destroy (this,timer);
	}

	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		if (thePlayer) 
		{
			this.thePlayer.ActerSp += spupOnAttack;
			float damage = TrueDamage * this.thePlayer.ActerSp * 0.15f / this.thePlayer.ActerSpMax;
			this.thePlayer.OnAttackWithoutEffect (aim, damage, false, true);
		}
	}

	public override void OnHpUp (float upValue = 0)
	{
		if(thePlayer)
		thePlayer.ActerHp += upValue * hpupPercent;
	}
	 
}
