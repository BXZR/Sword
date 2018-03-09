using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSpeedUp : effectBasic{

	void Start () 
	{
		theEffectName = "疾行";
		theEffectInformation = "增加75%移动速度，持续3秒";
		makeStart ();
		this.thePlayer.ActerMoveSpeedPercent += 0.75f;
		this.thePlayer.CActerMoveSpeedPercent += 0.75f;
		Destroy (this , 3f);
	}

	void OnDestroy()
	{
		this.thePlayer.ActerMoveSpeedPercent -= 0.75f;
		this.thePlayer.CActerMoveSpeedPercent -= 0.75f;
	}
 
}
