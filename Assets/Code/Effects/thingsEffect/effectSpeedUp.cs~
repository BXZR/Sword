using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSpeedUp : effectBasic{

	void Start () 
	{
		lifeTimerAll = 3f;
		timerForEffect = 3f;
		theEffectName = "疾行";
		theEffectInformation = "增加75%移动速度，持续3秒";
		makeStart ();
		this.thePlayer.ActerMoveSpeedPercent += 0.75f;
		this.thePlayer.CActerMoveSpeedPercent += 0.75f;
		Destroy (this , 3f);
	}
	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
	}

	void OnDestroy()
	{
		this.thePlayer.ActerMoveSpeedPercent -= 0.75f;
		this.thePlayer.CActerMoveSpeedPercent -= 0.75f;
	}
 
}
