using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSuck : effectBasic {


	int attackCount = 0;
	int attackCountMax =3;
	float speednow = 0f;
	float speedMax = 0.15f;
	float addCount = 0;
	float addMax = 80;

	void Start ()
	{
		Init ();
	}


	public override void Init ()
	{
		lifeTimerAll = 90f;//每一个段时间才能够使用这个伤害
		timerForEffect = 90f; 
		theEffectName = "吸神诀";
		theEffectInformation = "闪避、格挡触发时回复40%最大斗气\n攻击起手吸取身边所有目标1%最大生命值\n每第4次攻击附加以下额外效果：\n吸取目标2%最大生命值，暴击时吸收双倍\n永久吸取目标3%攻击力和护甲(上限80次)\n攻速和移速加成效果提升1%(上限15%)";
		theEffedctExtraInformation = "特性：吸神之意，受到攻击时将伤害的7%转化为护盾\n特性：吸海无涯，升级时额外增加2%护盾上限";
		makeStart ();
		Destroy (this,lifeTimerAll);

		if (thePlayer && thePlayer.playerLv >= 3) 
		{
			thePlayer.ActerSpSuckPercent += 0.06f;
			thePlayer.CActerSpSuckPercent += 0.06f;
		}

	}

	public override string getEffectAttackLinkLVExtra ()
	{
		return "等级奖励\n获得吸神诀时等级超过2级，额外永久获得6%斗气偷取";
	}

	public override string getOnTimeFlashInformation ()
	{
		return this.theEffectName +"\n"+addCount+"|"+(speednow*100).ToString("f0")+"%";
	}


	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		attackCount++;
		if (attackCount == attackCountMax)
		{
			attackCount = 0;

			if ((addCount +1) <= addMax)
			{
				addCount++;
				thePlayer.ActerWuliDamage += aim.ActerWuliDamage * 0.03f;
				thePlayer.ActerWuliShield += aim.ActerWuliShield * 0.03f;
				thePlayer.CActerWuliDamage += aim.ActerWuliDamage * 0.03f;
				thePlayer.CActerWuliShield += aim.ActerWuliShield * 0.03f;
			}

			if ((speednow + 0.01f) < speedMax)
			{
				thePlayer.ActerSpeedOverPervnet += 0.01f;
				speednow +=  0.01f;
			}
			makeSuck (aim , 0.02f);
		}
	}

	public override void OnSuperBlade (PlayerBasic aim, float Damage = 0)
	{
		makeSuck (aim , 0.02f);
	}

	public override void onAttackAction ()
	{
		//print ("aa");
		List<GameObject> theEMY = systemValues.searchAIMs (180f, 1.75f,thePlayer.transform);
		//print (theEMY.Count);
		float hpup = 0;
		for(int i = 0 ; i  < theEMY.Count ; i++)
		{
			PlayerBasic aim = theEMY [i].GetComponent <PlayerBasic> ();
			if(aim)
				hpup += aim.ActerHpMax * 0.01f;

		}

		thePlayer.ActerHp += hpup;
		for (int i = 0; i < thePlayer.Effects.Count; i++)
			thePlayer.Effects [i].OnHpUp (hpup);
	}

	public override void OnBeAttack (float damage = 0)
	{
		thePlayer.ActerShieldHp += damage * 0.07f;
	}

	public override void OnLvUp ()
	{
		thePlayer.ActerShieldMaxPercent += 0.02f;
		thePlayer.CActerShieldMaxPercent += 0.02f;
	}


	public override void OnMiss (PlayerBasic attacker)
	{
		thePlayer.ActerSp += 0.4f * thePlayer.ActerSpMax;
	}

	public override void OnShield (PlayerBasic attacker, float damageMinus = 0)
	{
		thePlayer.ActerSp += 0.4f * thePlayer.ActerSpMax;
	}

 
 
	public override void effectOnUpdateTime ()
	{
		addTimer ();
		if (thePlayer && systemValues.theGameSystemMode == GameSystemMode.NET)
		{
			thePlayer.makeValueUpdate ();
		}
	}

	void makeSuck(PlayerBasic aim ,float suckPercent = 0.02f)
	{
		float hpup =  aim.ActerHpMax * suckPercent;
		thePlayer.ActerHp += hpup;
		for (int i = 0; i < thePlayer.Effects.Count; i++)
			thePlayer.Effects [i].OnHpUp (hpup);
	}
}
