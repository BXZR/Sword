using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectZiying :effectBasic{

	float shieldPercentAdd = 0.1f;
	float damageToSavePercent  = 0.1f;
	float damageSave = 0;
	float damageSaveMax = 20;

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
		theEffectName = "五气朝元";
		theEffectInformation ="剑气围绕周身，获得"+ shieldPercentAdd*100+"%格挡率" +
			"\n受到攻击时储存"+damageToSavePercent*100+"%伤害\n攻击消耗储存量造成相应真实伤害\n储存量上限为"+damageSaveMax+",且每秒自减10%" ;
		makeStart ();
		this.thePlayer.ActerShielderPercent += shieldPercentAdd;
		this.thePlayer.CActerShielderPercent += shieldPercentAdd;
	}

	public override void OnBeAttack (float damage = 0)
	{
		damageSave += damage * 0.1f;
		damage = Mathf.Clamp (damageSave,0f,damageSaveMax);
	}

	public override void OnAttack (PlayerBasic aim)
	{
		aim.ActerHp -= damageSave;
		damageSave = 0;
	}
	public override void effectOnUpdate ()
	{
		damageSave *= 0.9f;
	}
}
