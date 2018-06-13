using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSpeedUp : effectBasic{

	void Start () 
	{
		lifeTimerAll = 2.5f;
		timerForEffect = 2.5f;
		theEffectName = "疾行";
		theEffectInformation = "增加110%移动速度，持续2.5秒";
		makeStart ();
		this.thePlayer.ActerMoveSpeedPercent += 1.20f;
		this.thePlayer.CActerMoveSpeedPercent += 1.20f;
		Destroy (this , 3f);
	}
	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
	}

	void OnDestroy()
	{
		this.thePlayer.ActerMoveSpeedPercent -= 1.20f;
		this.thePlayer.CActerMoveSpeedPercent -= 1.20f;
	}
 
}
