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
		theEffectInformation = "剑气围绕周身，获得" + shieldPercentAdd * 100 + "%格挡率\n非战斗状态下斗气恢复效率提升"+ spUpWhenNotFightingPercnet*100+"%\n每过" + lifeTimerAll + "秒的下一次攻击转为范围攻击\n目标周围"+area+"米的所有敌人受到额外" + extraDamagePercent * 100 + "%真实伤害";
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
		//print (this.thePlayer.ActerName + " make area attack");
		float damageUse = this.thePlayer.ActerWuliDamage * extraDamagePercent;
		Collider [] emys = Physics.OverlapSphere (theAim .transform .position, area );
		List<PlayerBasic> aimListWithOnly = new List<PlayerBasic> ();
		for (int i = 0; i < emys.Length; i++)
		{
			PlayerBasic theAimNow = emys [i].GetComponent <PlayerBasic> ();
			if ( theAimNow && theAimNow!= this.thePlayer  && aimListWithOnly.Contains (theAimNow) == false)
				aimListWithOnly.Add (theAimNow);
		}
		for (int i = 0; i < aimListWithOnly.Count; i++)
		{
			//print ( aimListWithOnly[i].ActerName +" is being extra damage");
			this.thePlayer.OnAttackWithoutEffect (aimListWithOnly[i],damageUse,true,true);
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
		{
			if (thePlayer.isFighting == false)
				return this.theEffectName + "积累";
			return this.theEffectName ;
		} 
		else
		{
			if (thePlayer.isFighting == false)
				return this.theEffectName + "积累\n[充能中]";
			return this.theEffectName + "\n[充能中]";
		}
	}

	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
	}
		

}
