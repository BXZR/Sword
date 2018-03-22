using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectKOKnife : effectBasic{

	int attackCount = 0;
	int attackCountMax = 5;
	float attackPercent= 0.02f;
	float beAttackBackTimer = 0.1f;
	void Start ()
	{
		Init ();
	}


	public override bool isBE ()
	{
		return true;
	}

	public override void Init ()
	{
		theEffectName = "重剑无锋";
		theEffectInformation ="每第"+ attackCountMax +"攻击命中附加目标最大生命值"+attackPercent*100+"%物理伤害\n若目标生命百分比高于自己，则击退目标"+beAttackBackTimer+"秒";
		makeStart ();
	}

	public override void OnAttack (PlayerBasic aim)
	{
		attackCount++;
		if (attackCount >= attackCountMax)
		{
			float damage = aim.ActerHpMax * attackPercent;
			this.thePlayer.OnAttackWithoutEffect (aim,damage,false,true);
			attackCount = 0;
			if ((aim.ActerHp / aim.ActerHpMax) > (this.thePlayer.ActerHp / this.thePlayer.ActerHpMax))
			{
				aim.gameObject.AddComponent<monsterBeAttack> ();
				Destroy (aim.gameObject.GetComponent<monsterBeAttack> (), beAttackBackTimer);
			}
		}
	 
	}
	public override string getOnTimeFlashInformation ()
	{
		return this.theEffectName + "\n(" + attackCount + "/" + (attackCountMax-1) + "充能)";
	}


}
