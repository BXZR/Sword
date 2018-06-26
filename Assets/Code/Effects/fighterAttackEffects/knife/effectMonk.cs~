using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectMonk :effectBasic{

	float basicDamage = 15f;//每一个单位能够给出的伤害
	float hpupRate = 0.7f;//生命回复百分比
	int maxEMYCountForUse = 6;//最多触发层数
	int EMYCount  = 0;
	float beAttackBackTimer = 0.15f;

	void Start ()
	{
		Init ();
	}
 
	int getCount ()
	{
		Init ();
		int count = 0;
		if (this.thePlayer) 
		{
			//接下来需要的就是对坐标进行审查
			Collider[] emys = Physics.OverlapSphere (this.thePlayer.transform.position, 2);
			for (int i = 0; i < emys.Length; i++) 
			{//开始对相交球体探测物体进行排查
				//相交球最大的问题就是如果自身有碰撞体，自己也会被侦测到
				//print (emys [i].gameObject.name);
				PlayerBasic theP =  emys [i].GetComponent <PlayerBasic> ();
				if (theP && theP.isAlive && emys [i].GetComponent <Collider> ().gameObject != this.thePlayer.gameObject)
					count++;
			}
		}
		return count;
	}

	public override void OnAttack (PlayerBasic aim)
	{
		//特殊伤害只能够生效一次
		if (isEffecting) 
		{
			EMYCount = Mathf.Clamp(getCount (),0,maxEMYCountForUse);
			float damage =  EMYCount  * basicDamage;
			aim.ActerHp -= damage;
			this.thePlayer.OnAttackWithoutEffect (aim, damage, true, true);

			damage *= hpupRate;
			thePlayer.ActerHp += damage;
			//附加的各种效果
			effectBasic [] effects = this.thePlayer.GetComponents<effectBasic> ();
			foreach (effectBasic EF in effects)
				EF.OnHpUp ( damage);
			
			isEffecting = false;//标记，已经失效


			Destroy (aim.gameObject.AddComponent<monsterBeAttack> (), beAttackBackTimer);
		}
	}

	public override void Init ()
	{
		//print ("灭却浮屠发动");
		lifeTimerAll = 5f;//每一个段时间才能够使用这个伤害
		timerForEffect = 5f; 
		theEffectName = "灭却浮屠";
		theEffectInformation ="攻击追加(身边敌人数×"+basicDamage+")真实伤害\n击退目标"+beAttackBackTimer+"秒,恢复额外伤害七成的生命值\n额外伤害最多"+maxEMYCountForUse+"层，冷却时间"+  (lifeTimerAll) +"秒";
		makeStart ();
		Destroy (this,lifeTimerAll);
	}
	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
	}

	public override string getOnTimeFlashInformation ()
	{
		return this.theEffectName +"\n("+EMYCount+"层)";
	}

	//招式等级额外特效 ====================================================================
	public override void SetAttackLink (attackLink attackLinkIn)
	{
		if (attackLinkIn && attackLinkIn.theAttackLinkLv >= 3)
		{
			//print ("ad");
			basicDamage *= 1.15f;
		}
	}
	public override string getEffectAttackLinkLVExtra ()
	{
		return "等级奖励：等级超过3级的招式触发此效果时\n每一层的治疗效果提升15%";
	}

}
 