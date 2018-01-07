using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectZiying :effectBasic{

	float shieldPercentAdd = 0.09f;
	float extraDamagePercent = 0.02f;
	float timer = 7f;
	float timerMax = 7f;
	bool canExtraDamage = true;


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
		theEffectName = "千方残光剑";
		theEffectInformation = "剑气围绕周身，获得" + shieldPercentAdd * 100 + "%格挡率\n每过" + timerMax + "秒可以在攻击命中的第一个目标周围造成自身当前攻击力" + extraDamagePercent * 100 + "%真实伤害";
		makeStart ();
		this.thePlayer.ActerShielderPercent += shieldPercentAdd;
		this.thePlayer.CActerShielderPercent += shieldPercentAdd;
	}
		
	public override void OnAttack (PlayerBasic aim)
	{
		//print ("effext using");
		if (canExtraDamage)
		{
			canExtraDamage = false;
			makeAreaAttack (aim.transform);
		}
	 
	}


	void  makeAreaAttack (Transform theAim )
	{
		float damageUse = this.thePlayer.ActerWuliDamage * extraDamagePercent;
		Collider [] emys = Physics.OverlapSphere (theAim .transform .position, 3);
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


	public override void effectOnUpdate ()
	{
		if (canExtraDamage == false)
		{
			timer -= systemValues .updateTimeWait;
			if (timer < 0)
			{
				timer = timerMax;
				canExtraDamage = true;
			}
		}
	}
}
