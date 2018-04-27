using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind2 : lingBasic {


	private float addPercent = 0.2f;
	private float spup = 25f;
	public override void makeStart ()
	{
		lingName = "风•阴  罡风";
		theType = wulingType.wind;
	}

	public override void OnSuperBlade (PlayerBasic user, PlayerBasic aim, float Damage = 0)
	{
		float damage = Damage * addPercent;
		aim.ActerHp -= damage;
		aim.OnAttackWithoutEffect (aim, damage, true, true);
		user.ActerSp += spup;
	}

	public override string wulingInformation ()
	{
		return "暴击时最终伤害额外提高"+(addPercent*100).ToString("f0")+"%，并额外恢复"+spup+"斗气";
	}
}
