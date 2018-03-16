using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectHpUp : effectBasic{


	float hpupMax = 210f;
	float hpupPerSecond = 0;

	void Start ()
	{
		lifeTimerAll = 3f;
		timerForEffect = 3f;
		hpupPerSecond = hpupMax / lifeTimerAll;
		theEffectName = "生命回复";
		theEffectInformation = lifeTimerAll+"秒内回复共"+ hpupMax +"生命";

		makeStart ();
		Destroy (this, lifeTimerAll);
		InvokeRepeating ("makeHpUpEffect",0f,1f);
	}

	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
	}

	void makeHpUpEffect()
	{
		effectBasic  [] efs = this.thePlayer.GetComponents<effectBasic> ();
		for (int i = 0; i < efs.Length; i++)
			efs [i].OnHpUp (hpupPerSecond);
	}

	void OnDestroy()
	{
		CancelInvoke ();
	}

	void Update()
	{
		thePlayer.ActerHp += hpupPerSecond * Time.deltaTime;	
	}
	 
}
