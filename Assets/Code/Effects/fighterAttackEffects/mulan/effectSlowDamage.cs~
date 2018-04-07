using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSlowDamage  :effectBasic {

	float   damageMinus = 0.1f;//减少的伤害百分比
	float attackAtMinus = 0.2f;
	float hpmaxMinus = 100f;//暂时减少的生命上限
	void Start () 
	{
		Init ();
		Destroy (this , lifeTimerAll);
	}

	void OnDestroy()
	{
		if (thePlayer) 
		{
			thePlayer.ActerHpMax += hpmaxMinus;
			thePlayer.CActerHpMax += hpmaxMinus;
			thePlayer.ActerAttackAtPercent += attackAtMinus;
			thePlayer.CActerAttackAtPercent += attackAtMinus;
		}
	}

	public override void effectOnUpdateTime ()
	{
		if (timerForAdd < lifeTimerAll ) 
		{
			addTimer ();
		} 
		else if(timerForAdd > timerForEffect)
		{
			isEffecting = false;
		}
	}

	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		if (timerForAdd < timerForEffect) 
		{
			aim.ActerHp += TrueDamage * damageMinus;
		}
	}

	public override void Init ()
	{
		lifeTimerAll = 25f;
		timerForEffect = 7f;
		theEffectName = "禁咒封";
		theEffectInformation ="目标输出伤害减少"+damageMinus*100+"%，命中率减少"+attackAtMinus*100+"%\n削减目标"+hpmaxMinus+"生命上限\n持续"+timerForEffect+"秒，冷却时间"+lifeTimerAll+"秒";
		makeStart ();
		if (thePlayer) 
		{
			thePlayer.ActerHpMax -= hpmaxMinus;
			thePlayer.CActerHpMax -= hpmaxMinus;
			thePlayer.ActerAttackAtPercent -= attackAtMinus;
			thePlayer.CActerAttackAtPercent -= attackAtMinus;
		}
	} 
}
