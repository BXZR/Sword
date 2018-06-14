using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipEffect_ExtraLingYa : effectBasic {


	int attackCountP = 7;
	float damage = 5f;
	int count = 0;
	int countMax = 4;

	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{
		theEffectName = "灵牙";
		theEffectInformation = "第"+attackCountP +"层连击提供额外"+damage+"物理伤害(最多"+countMax+"层)";

		makeStart ();
	}

	public override void OnAttack (PlayerBasic aim)
	{
		count = Mathf.Clamp (systemValues.hitCount / attackCountP, 0, countMax);
		float damageUse = count * damage;
		this.thePlayer.OnAttackWithoutEffect (aim,damageUse,false,true);
	}


	public override string getOnTimeFlashInformation ()
	{
		count = Mathf.Clamp (systemValues.hitCount / attackCountP, 0, countMax);
		return this.theEffectName + "\n(" + count + "层)";
	}


}
