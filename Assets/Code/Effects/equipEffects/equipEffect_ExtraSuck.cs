using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipEffect_ExtraSuck : effectBasic{

	int count = 0;
	int countMax = 3;
	float hpsuck = 20f;
	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{
		theEffectName = "三叠吸血";
		theEffectInformation = "第"+countMax +"次攻击额外偷取"+hpsuck+"生命";
		makeStart ();
	}

	public override void OnAttack (PlayerBasic aim)
	{
		count++;
		if (count == countMax) 
		{
			count = 0;
			thePlayer.ActerHp += hpsuck;
			effectBasic[] effects = this.thePlayer.GetComponents<effectBasic> ();
			foreach (effectBasic EF in effects)
				EF.OnHpUp (hpsuck);
		}
	}

	public override bool isBE ()
	{
		return true;
	}

	public override string getOnTimeFlashInformation ()
	{
		return this.theEffectName + "\n(" + count + "/" + (countMax-1) + "充能)";
	}
		

}
