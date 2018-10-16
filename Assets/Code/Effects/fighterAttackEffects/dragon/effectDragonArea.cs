using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectDragonArea :  effectBasic 
{
	public float shieldAdd = 20f;
	public float hpupOnBeAttack = 5f;
	public float damageInPercentForUp = 0.03f;
	public float spUseOnBeAttackPercent = 0.02f;
	public float spUse = 3f;
	public int countMax = 4;
	private GameObject theEffectProfab;
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
			lifeTimerAll = 22f;
			timerForEffect = 22f;
			theEffectName = "密云不雨";
			theEffectInformation = "立即获得"+shieldAdd+"护盾，并获得额外特效：\n";
			theEffectInformation += "受到攻击时自动消耗("+spUse +"+"+spUseOnBeAttackPercent*100+"%当前斗气)\n这些斗气将用于恢复("+hpupOnBeAttack +"+" +damageInPercentForUp*100+"%已损生命)\n";
			theEffectInformation += "此效果存在"+lifeTimerAll+"秒且不可叠加\n持续时间内最多生效"+countMax +"次";


			makeStart ();
			Destroy(this,timerForEffect);
			Destroy (theEffect,timerForEffect);

			if (this.thePlayer) 
			{
				if(!theEffectProfab)
					theEffectProfab = Resources.Load<GameObject> ("effects/dragonHPShield");
				
				theEffect = GameObject.Instantiate<GameObject> (theEffectProfab);
				theEffect.transform.SetParent (this.thePlayer.transform);
				theEffect.transform.localPosition = new Vector3 (0, 1.25f, 0);
				thePlayer.ActerShieldHp += shieldAdd;
			}

		}
		catch(System.Exception X)
		{
			print (X.Message);
		}
	}


	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
	}

	//这是一个主动的技能
	//附加给目标敌人的脚本
	public override void OnBeAttack (float damage = 0)
	{
		if (this.thePlayer && countMax >=0 ) 
		{
			countMax--;
			float hpup  = hpupOnBeAttack + (thePlayer.ActerHpMax - thePlayer.ActerHp) * damageInPercentForUp;
			this.thePlayer.ActerSp *= (1 - spUseOnBeAttackPercent);
			this.thePlayer.ActerSp -= spUse;
			this.thePlayer.ActerHp += hpup;
			//附加的各种效果
			effectBasic [] effects = this.thePlayer.GetComponents<effectBasic> ();
			foreach (effectBasic EF in effects)
				EF.OnHpUp (hpup);

			if (theEffect && countMax < 0) 
			{
				Destroy (theEffect);
				isEffecting = false;//标记，已经失效
				//timerForAdd = timerForEffect;//计时器直接失效
			}
		}
	}

	//图标显示信息
	public override string getOnTimeFlashInformation ()
	{
		if(isEffecting)
			return this.theEffectName+"\n("+countMax+"次)";
		return this.theEffectName+"\n[失效]";
	}

	//招式等级额外特效 ====================================================================
	public override void SetAttackLink (attackLink attackLinkIn)
	{
		if (attackLinkIn && attackLinkIn.theAttackLinkLv >= 3)
		{
			//print ("ad");
			countMax ++;
		}
	}
	public override string getEffectAttackLinkLVExtra ()
	{
		return "等级奖励：等级超过3级的招式触发此效果时\n额外增加一次触发效果";
	}

}
