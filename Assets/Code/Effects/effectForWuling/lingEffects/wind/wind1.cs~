using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//阳属性风灵效果
public class wind1 : lingBasic {


	private float addPercent = 0.08f;
	public override void makeStart ()
	{
		lingName = "风•阳";
		lingEffectName = "暖雾春风";
		theType = wulingType.wind;
	}

	public override void OnHpUp (PlayerBasic user, float upValue = 0)
	{
		user.ActerHp += upValue * addPercent;
	}



	public override string wulingInformation ()
	{
		return "所有生命恢复效果提升"+(addPercent*100).ToString("f0")+"%";
	}

	//学成奖励 --------------------------------------------------------------------------------------------//
	public override void learnedOverGet ()
	{
		systemValues.thePlayer.ActerHpSuckPercent += 0.02f;
		systemValues.thePlayer.CActerHpSuckPercent += 0.02f;
	}

	public override string wulingInformationForLearnOver()
	{
		return "+2%生命偷取";
	}
}
