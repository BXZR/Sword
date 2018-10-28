using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSuperSuck  : effectBasic {

	private float suckPercentAdder = 0.002f;
	private float addAll = 0f;

	float timerForAddSave = 0f;

	void Start ()
	{
		Init ();
	}


	public override void Init ()
	{
		lifeTimerAll = 60f;//每一个段时间才能够使用这个伤害
		timerForEffect = 0.5f; 
		theEffectName = "百炼天吸";
		theEffectInformation = "每一次攻击起手均可永久获得"+suckPercentAdder*100 +"%的生命偷取、斗气偷取和最终伤害";
		makeStart ();
		Destroy (this,lifeTimerAll);

	}


	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		base.OnAttack (aim, TrueDamage);
	}

	public override string getOnTimeFlashInformation ()
	{
		return this.theEffectName + "\n" + (addAll * 100).ToString ("f0")+"%";
	}

	public override void onAttackAction ()
	{
		if (!thePlayer)
			return;
		if (!isEffecting)
			return;
		
		thePlayer.ActerHpSuckPercent += suckPercentAdder;
		thePlayer.CActerHpSuckPercent += suckPercentAdder;
		thePlayer.ActerSpSuckPercent += suckPercentAdder;
		thePlayer.CActerSpSuckPercent += suckPercentAdder;
		thePlayer.ActerDamageAdderPercent += suckPercentAdder;
		thePlayer.CActerDamageAdderPercent += suckPercentAdder;
		addAll += suckPercentAdder;

		isEffecting = false;

	}

	public override void addTimer ()
	{
		if (!isEffecting) 
		{
			timerForAdd += systemValues.updateTimeWait;
			if (timerForAdd > timerForEffect )
			{
				timerForAdd = 0;
				lifeTimerAll -= timerForAdd;
				isEffecting = true;
			}
		}
		timerForAddSave  += systemValues.updateTimeWait;
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
