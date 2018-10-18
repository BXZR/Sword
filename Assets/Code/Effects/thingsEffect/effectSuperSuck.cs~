using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSuperSuck  : effectBasic {

	private float suckPercentAdder = 0.002f;
	private float addAll = 0f;
	void Start ()
	{
		Init ();
	}


	public override void Init ()
	{
		lifeTimerAll = 60f;//每一个段时间才能够使用这个伤害
		timerForEffect = 60f; 
		theEffectName = "百炼天吸";
		theEffectInformation = "每一次攻击起手均可永久获得"+suckPercentAdder*100 +"%的生命偷取和斗气偷取";
		makeStart ();
		Destroy (this,lifeTimerAll);

	}


	public override string getOnTimeFlashInformation ()
	{
		return this.theEffectName + "\n" + (addAll * 100).ToString ("f0")+"%";
	}

	public override void onAttackAction ()
	{
		if (!thePlayer)
			return;

		thePlayer.ActerHpSuckPercent += suckPercentAdder;
		thePlayer.CActerHpSuckPercent += suckPercentAdder;
		thePlayer.ActerSpSuckPercent += suckPercentAdder;
		thePlayer.CActerSpSuckPercent += suckPercentAdder;
		addAll += suckPercentAdder;
	}

	public override void effectOnUpdateTime ()
	{
		addTimer ();
		if (thePlayer && systemValues.theGameSystemMode == GameSystemMode.NET)
		{
			thePlayer.makeValueUpdate ();
		}
	}

}
