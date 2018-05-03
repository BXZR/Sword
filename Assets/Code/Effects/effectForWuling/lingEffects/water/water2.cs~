using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water2 : lingBasic {


	float spAdd = 0.04f;

	public override void makeStart ()
	{
		lingName = "水•阴  雨恨云愁";
		theType = wulingType.water;
	}

	public override void OnAttack (PlayerBasic user, PlayerBasic aim)
	{
		aim.ActerSp -= aim.ActerSpMax * spAdd;
	}

	public override string wulingInformation ()
	{
		return "攻击命中额外消耗目标" + spAdd*100 +"%最大斗气";
	}

	public override int getYinYagType ()
	{
		return 2;
	}
}
