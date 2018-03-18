using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectDragonMiss : effectBasic{

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
		lifeTimerAll = 3f;
		timerForEffect = 3f;
		theEffectName = "龙战于野";
		theEffectInformation = "攻击命中回复"+spupOnAttack+"斗气,每1%斗气提高伤害0.15%\n此外，所有生命回复效果提升"+hpupPercent *100+"%，持续"+lifeTimerAll+"秒";
		makeStart ();
		Destroy (this,lifeTimerAll);
	}

	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
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

	public override string getOnTimeFlashInformation ()
	{
		int percent = (int)(100 * this.thePlayer.ActerSp * 0.15f / this.thePlayer.ActerSpMax);
		if(isEffecting)
			return this.theEffectName +"\n("+percent+"%提升)";
		return this.theEffectName + "\n[失效]";
	}
	 
}
