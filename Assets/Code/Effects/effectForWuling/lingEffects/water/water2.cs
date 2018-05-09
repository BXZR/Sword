using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water2 : lingBasic {


	float spAdd = 0.08f;

	public override void makeStart ()
	{
		lingName = "水•阴  雨恨云愁";
		theType = wulingType.water;
		coolingTimerMax = 4f;
		coolingTimer = 4f;
	}

	public override void OnAttack (PlayerBasic user, PlayerBasic aim)
	{
		if (isCooled) 
		{
			isCooled = false;
			aim.ActerSp -= aim.ActerSpMax * spAdd;
		}
	}
	public override void effectOnUpdateTime (PlayerBasic user)
	{
		makeCool ();
	}

	public override string wulingInformation ()
	{
		return "攻击命中额外消耗目标" + spAdd*100 +"%最大斗气\n冷却时间"+coolingTimerMax+"秒";
	}

	public override int getYinYagType ()
	{
		return 2;
	}
}
