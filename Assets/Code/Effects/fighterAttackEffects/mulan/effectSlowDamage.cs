using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSlowDamage  :effectBasic {

	float timer = 15f;//总持续时间，其实也就是冷却时间
	float effectTimer = 0;//生效时间
	float effectTimeMax = 7f;//生效时间上限
	float   damageMinus = 0.1f;//减少的伤害百分比
	float attackAtMinus = 0.2f;
	float hpmaxMinus = 100f;//暂时减少的生命上限
	void Start () 
	{
		Init ();
		Destroy (this,timer);
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
		effectTimer += systemValues.updateTimeWait;
	}

	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		if(effectTimer < effectTimeMax)
			aim.ActerHp += TrueDamage * damageMinus;
	}

	public override void Init ()
	{
		theEffectName = "封禁";
		theEffectInformation ="目标输出伤害减少"+damageMinus*100+"%，命中率减少"+attackAtMinus*100+"%,生命上限减少"+hpmaxMinus+"\n持续"+effectTimeMax+"秒，冷却时间"+ timer+"秒";
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
