using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectZiying :effectBasic{

	float shieldPercentAdd = 0.05f;
	float extraDamagePercent = 0.10f;
	float spUpWhenNotFightingPercnet = 0.15f;
	float area = 1f;//范围

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
		lifeTimerAll = 15f;
		timerForEffect = 15f;
		theEffectName = "剑气";
		theEffectInformation = "剑气围绕周身，获得" + shieldPercentAdd * 100 + "%格挡率\n非战斗状态下斗气恢复效率提升"+ spUpWhenNotFightingPercnet*100+"%\n每过" + lifeTimerAll + "秒可以在攻击命中的第一个目标时对目标周围"+area+"米的所有敌人造成自身当前攻击力" + extraDamagePercent * 100 + "%真实伤害";
		makeStart ();
		this.thePlayer.ActerShielderPercent += shieldPercentAdd;
		this.thePlayer.CActerShielderPercent += shieldPercentAdd;
	}
		
	public override void OnAttack (PlayerBasic aim)
	{
		//print ("effext using");
		if (isEffecting)
		{
			isEffecting = false;
			makeAreaAttack (aim.transform);
		}
	 
	}


	void  makeAreaAttack (Transform theAim )
	{
		float damageUse = this.thePlayer.ActerWuliDamage * extraDamagePercent;
		Collider [] emys = Physics.OverlapSphere (theAim .transform .position, area );
		for (int i = 0; i < emys.Length; i++)
		{
			PlayerBasic theAimNow = emys [i].GetComponent <PlayerBasic> ();
			if ( theAimNow && theAimNow!= this.thePlayer )
			{
				//print (emys[i].name +" is being extra damage");
				this.thePlayer.OnAttackWithoutEffect (theAimNow,damageUse,true,true);
			}
		}
	}

	public override void OnSpUp (float upValue = 0)
	{
		if (thePlayer.isFighting == false)
			thePlayer.ActerSp += upValue * spUpWhenNotFightingPercnet;
	}

	public override void addTimer ()
	{
		if (!isEffecting) 
		{
			timerForAdd += systemValues.updateTimeWait;
			if (timerForAdd > lifeTimerAll)
			{
				timerForAdd = 0;
				isEffecting = true;
			}
		}
	}

	public override string getOnTimeFlashInformation ()
	{
		if (isEffecting)
			return this.theEffectName;
		return this.theEffectName + "\n(充能中)";
	}

	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
	}
		

}
