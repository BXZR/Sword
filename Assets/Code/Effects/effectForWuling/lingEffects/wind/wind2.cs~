using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind2 : lingBasic {


	private float addPercent = 0.10f;

	public override void makeStart ()
	{
		lingName = "风•阴  乱刃罡风";
		theType = wulingType.wind;
	}

	public override void OnSuperBlade (PlayerBasic user, PlayerBasic aim, float Damage = 0)
	{
		float damage = Damage * addPercent;
		aim.ActerHp -= damage;
		aim.OnAttackWithoutEffect (aim, damage, true, true);
	}

	public override string wulingInformation ()
	{
		return "暴击时最终伤害额外提高"+(addPercent*100).ToString("f0")+"%";
	}

	public override int getYinYagType ()
	{
		return 2;
	}
}
