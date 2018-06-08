using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water1 : lingBasic {

	float spAddValue = 2f;
	float spAdd = 0.03f;

	public override void makeStart ()
	{
		lingName = "水•阳";
		lingEffectName = "五气连波";
		theType = wulingType.water;
	}


	public override void OnAttack (PlayerBasic user)
	{
		user.ActerSp += (user.ActerSpMax - user.ActerSp) * spAdd;
		user.ActerSp += spAddValue;
	}


	public override string wulingInformation ()
	{
		return "招式命中额外恢复(已损失" + spAdd*100 +"% + "+spAddValue+")斗气";
	}

	//学成奖励 --------------------------------------------------------------------------------------------//
	public override void learnedOverGet ()
	{
		systemValues.thePlayer.ActerSpSuckPercent += 0.01f;
		systemValues.thePlayer.CActerSpSuckPercent += 0.01f;
	}

	public override string wulingInformationForLearnOver()
	{
		return "+1%斗气偷取";
	}
}

