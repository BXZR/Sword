using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectDragonExtraDamage : effectBasic 
{

	float damageAddPercent = 0.12f;//额外真实伤害百分比
	float damageCount = 3f;//生效次数

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
		try
		{
			//print("<或跃在渊>正在初始化");
			lifeTimerAll = 20f;//持续时间，同时也是针对一个目标的冷却时间
			timerForEffect = 20f;
			theEffectName = "损则有孚";
			theEffectInformation = "标记目标，使之接下来受到的" +  damageCount  + "次攻击伤害提升" + damageAddPercent * 100 + "%\n对同一目标持续"+ (lifeTimerAll)+"秒";
			makeStart ();
			Destroy (this,lifeTimerAll);
		}
		catch(System.Exception X)
		{
			print ("或跃在渊错误:"+X.Message);
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
		if (this.thePlayer) 
		{
			//print ("count");
			damageCount--;
			if (damageCount >= 0)
			{
				//print ("dragon extra damage");
				float damageTrue = damage * damageAddPercent;
				this.thePlayer.ActerHp -= damageTrue;
				effectBasic[] effects = this.thePlayer.GetComponents<effectBasic> ();
				foreach (effectBasic ef in effects)
				{
					if (ef != this)
						ef.OnBeAttack (damageTrue);
				}
			} 
			else 
			{
				isEffecting = false;//标记，已经失效
			}
		}
	}

	public override string getOnTimeFlashInformation ()
	{
		if (damageCount >= 0)
			return this.theEffectName +"\n("+damageCount+"次)";
		return this.theEffectName + "\n[失效]";
	}

	//招式等级额外特效 ====================================================================
	public override void SetAttackLink (attackLink attackLinkIn)
	{
		if (attackLinkIn && attackLinkIn.theAttackLinkLv >= 8)
		{
			//print ("ad");
			damageAddPercent += 0.06f;
		}
	}
	public override string getEffectAttackLinkLVExtra ()
	{
		return "招式等级奖励\n8级招式: 伤害追加效果提升6%";
	}

}
