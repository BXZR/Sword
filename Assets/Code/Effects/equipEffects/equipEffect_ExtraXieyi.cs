using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipEffect_ExtraXieyi  : effectBasic{


	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{
		theEffectName = "谢衣灵韵";
		theEffectInformation = "斗气消耗减少5%";
		makeStart ();
	}

	public override void OnUseSP (float spUse = 0)
	{
		this.thePlayer.ActerSp += spUse * 0.05f;
	}

	public override bool isBE ()
	{
		return true;
	}
		
}
