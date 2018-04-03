using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectShield: effectBasic{

	private int shieldAdd = 100;
	void Start ()
	{
		theEffectName = "护盾";
		theEffectInformation = "增加100护盾生命值";
		makeStart ();
		this.thePlayer.ActerShieldHp += shieldAdd ;

		//套护盾的额外效果
		effectBasic [] Effects = thePlayer.GetComponentsInChildren<effectBasic>();
		for (int i = 0; i < Effects.Length; i++)
			Effects [i] .OnAddShieldHp(shieldAdd );

		Destroy (this);
	}
}

