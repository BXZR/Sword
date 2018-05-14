using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏玩法模式的特殊加成
//这些加成是放在游戏主人公身上的
public class playModeEffect2  : effectBasic {

	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{
		theEffectName = "超加速";
		theEffectInformation = "攻速移速最终加成增加20%";
		makeStart ();
		if(thePlayer)
		    thePlayer.ActerSpeedOverPervnet += 0.2f;
	}
}
