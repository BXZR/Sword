using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playModeEffect5 : effectBasic {

float percent = 0.1f;
void Start ()
{
	Init ();
}

public override void Init ()
{
	theEffectName = "天助圣光";
	theEffectInformation = "每秒额外恢复最大生命值和最大斗气值的"+percent *100+"%";
	makeStart ();
}

public override void effectOnUpdateTime ()
{
	if (!thePlayer)
		return;

	thePlayer.ActerHp += thePlayer.ActerHpMax * percent * systemValues.updateTimeWait;
	thePlayer.ActerSp += thePlayer.ActerSpMax * percent * systemValues.updateTimeWait;
}
}



