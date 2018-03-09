using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectDragonExtraDamage : effectBasic 
{

	float damageAddPercent = 0.15f;//额外真实伤害百分比
	float damageCount = 3f;//生效次数
	float timer = 20f;//持续时间，同时也是针对一个目标的冷却时间

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
			theEffectName = "损则有孚";
			theEffectInformation = "标记目标，使这个目标受到的下" +  damageCount  + "次攻击伤害提升" + damageAddPercent * 100 + "%，对同一目标有"+timer+"秒的冷却时间";
			makeStart ();
			Destroy (this,timer);
		}
		catch(System.Exception X)
		{
			print ("或跃在渊错误:"+X.Message);
		}
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

}
