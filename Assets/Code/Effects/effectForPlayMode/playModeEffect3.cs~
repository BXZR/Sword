using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playModeEffect3 : effectBasic {

	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{
		theEffectName = "一刀致命";
		theEffectInformation = "自身受到攻击会被额外带走12%最大生命值";
		makeStart ();
	}

	public override void OnBeAttack (float damage = 0)
	{
		if (damage <= 0)
			return;
		
		this.thePlayer.ActerHp -= this.thePlayer.ActerHpMax * 0.12f;//一刀必死
	}
}

