using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//阳属性风灵效果
public class wind1 : lingBasic {

	public override void makeStart ()
	{
		lingName = "风•阳";
	}

	public override void OnHpUp (PlayerBasic user, float upValue = 0)
	{
		user.ActerHp += upValue * 0.1f;
	}

	public override string wulingInformation ()
	{
		return "生命恢复效果提升10%，额外提升效果不在触发其他生命回复特效";
	}
}
