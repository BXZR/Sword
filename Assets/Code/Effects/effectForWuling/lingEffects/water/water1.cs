using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water1 : lingBasic {

	float spAddValue = 2f;
	float spAdd = 0.04f;

	public override void makeStart ()
	{
		lingName = "水•阳  五气连波";
		theType = wulingType.water;

	}

	public override void onAttackAction (PlayerBasic user)
	{
		user.ActerSp += (user.ActerSpMax - user.ActerSp) * spAdd;
		user.ActerSp += spAddValue;
	}
		
	public override string wulingInformation ()
	{
		return "攻击起手额外恢复(已损失" + spAdd*100 +"% + "+spAddValue+")斗气";
	}
}

