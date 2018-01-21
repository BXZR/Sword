using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSlash :effectBasic{

	float hpsuckAdd = 0.05f;
	int ranNumber = 4; 
	float addPercent = 0.10f;

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
		theEffectName = "刺客之道";
		//注意的是，最大生命值每回合都会更新的，这个最大生命值的削弱仅仅限制于本回合(如果削减最大斗气值就太变态了)
		theEffectInformation ="额外获得"+hpsuckAdd*100+"%的生命偷取\n攻击时拥有"+ranNumber*10 +"%机会使最终伤害提升"+addPercent *100+"%，同时恢复额外伤害等额的斗气";
		makeStart ();

		thePlayer.ActerHpSuckPercent += hpsuckAdd;
		thePlayer.CActerHpSuckPercent += hpsuckAdd;
	}

	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		int RN = Random.Range (0, 10);
		if (RN < ranNumber) 
		{
			float makeDamage = TrueDamage * addPercent;
			this.thePlayer.OnAttackWithoutEffect (aim,makeDamage,true,true);
			this.thePlayer.ActerSp += makeDamage;
		}
	}


}
