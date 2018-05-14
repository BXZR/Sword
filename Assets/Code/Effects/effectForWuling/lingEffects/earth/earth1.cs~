using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth1  : lingBasic {


	private float hpup  = 7f;
	public override void makeStart ()
	{
		lingName = "土•阳  承天载物";
		theType = wulingType.earth;
	}

	public override void OnBeAttack (PlayerBasic user, PlayerBasic attacker)
	{
		user.ActerHp += hpup;
		effectBasic[] efs = user.GetComponents<effectBasic> ();
		foreach (effectBasic  a in efs)
			a.OnHpUp (hpup);
	}
		
	public override string wulingInformation ()
	{
		return "受到攻击时恢复" + hpup + "生命并触发生命恢复特效";
	}

	//学成奖励 --------------------------------------------------------------------------------------------//
	public override void learnedOverGet ()
	{
		systemValues.thePlayer.ActerWuliShield += 25f;
		systemValues.thePlayer.CActerWuliShield += 25f;
	}

	public override string wulingInformationForLearnOver()
	{
		return "初成奖励：25护甲";
	}
}

