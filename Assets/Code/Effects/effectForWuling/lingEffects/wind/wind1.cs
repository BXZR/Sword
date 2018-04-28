using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//阳属性风灵效果
public class wind1 : lingBasic {


	private float addPercent = 0.1f;
	public override void makeStart ()
	{
		lingName = "风•阳  暖雾";
		theType = wulingType.wind;
	}

	public override void OnHpUp (PlayerBasic user, float upValue = 0)
	{
		user.ActerHp += upValue * addPercent;
	}


	public override string wulingInformation ()
	{
		return "生命恢复效果提升"+(addPercent*100).ToString("f0")+"%，不触发其他生命回复特效";
	}
}
