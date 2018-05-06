using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth2  : lingBasic {


	private float reDamagePercent  = 0.006f;
	public override void makeStart ()
	{
		lingName = "土•阴  泰山压顶";
		theType = wulingType.earth;
	}

	public override void OnBeAttack (PlayerBasic user, PlayerBasic attacker)
	{
		float damage = attacker.ActerHp * reDamagePercent;
		user.OnAttackWithoutEffect (attacker, damage, false, true);
	}

	public override string wulingInformation ()
	{
		return "受到攻击时回敬攻击者当前生命"+reDamagePercent*100+"%物理伤害";
	}

	public override int getYinYagType ()
	{
		return 2;
	}
}

