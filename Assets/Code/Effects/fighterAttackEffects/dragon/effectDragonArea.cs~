using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectDragonArea :  effectBasic 
{

	public float hpupOnBeAttack = 7f;
	public float damageInPercentForUp = 0.1f;
	public float spUseOnBeAttackPercent = 0.1f;
	public float spUse = 5f;
	public int countMax = 5;
	public float timerForLife = 25f;
	GameObject theEffect;//特效

	void Start () 
	{
		Init ();//进行初始化
	}

	public override bool isBE ()
	{
		return false;
	}

	void OnDestroy()
	{
		Destroy (theEffect);
	}

	public override void Init ()
	{
		try
		{
			theEffectName = "密云不雨";
			theEffectInformation = "聚集内力保护自身，在受到攻击时花费("+spUse +"+"+spUseOnBeAttackPercent*100+"%)当前斗气恢复("+hpupOnBeAttack +"+" +damageInPercentForUp*100+"%伤害)的生命\n";
			theEffectInformation += "此效果存在"+timerForLife+"秒且不可叠加，持续时间内最多生效"+countMax +"次";
			makeStart ();
			Destroy(this,timerForLife);

			if (this.thePlayer) 
			{
				theEffect = GameObject.Instantiate<GameObject> (Resources.Load<GameObject> ("effects/dragonHPShield"));
				theEffect.transform.SetParent (this.thePlayer.transform);
				theEffect.transform.localPosition = new Vector3 (0, 1.25f, 0);
			}

		}
		catch(System.Exception X)
		{
			print (X.Message);
		}
	}


	//这是一个主动的技能
	//附加给目标敌人的脚本
	public override void OnBeAttack (float damage = 0)
	{
		if (this.thePlayer && countMax >=0 ) 
		{
			countMax--;
			this.thePlayer.ActerSp *= (1 - spUseOnBeAttackPercent);
			this.thePlayer.ActerSp -= spUse;
			this.thePlayer.ActerHp += hpupOnBeAttack + damage* damageInPercentForUp;
			//附加的各种效果
			effectBasic [] effects = this.thePlayer.GetComponents<effectBasic> ();
			foreach (effectBasic EF in effects)
				EF.OnHpUp (hpupOnBeAttack);

			if(theEffect && countMax <0)
				Destroy (theEffect);
		}
	}

}
