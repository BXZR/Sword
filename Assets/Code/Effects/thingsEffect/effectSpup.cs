using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSpup : effectBasic{

	float spupPerSecond = 5f;
	void Start ()
	{
		lifeTimerAll = 3f;
		timerForEffect = 3f;
		theEffectName = "斗气恢复";
		theEffectInformation = "立刻回复10斗气值，之后每秒恢复5斗气，持续3秒";
		makeStart ();
		this.thePlayer.ActerSp += 10;
		InvokeRepeating ("makeHpUpEffect",0f,1f);
		Destroy (this,	lifeTimerAll );
	}

	void makeHpUpEffect()
	{
		effectBasic  [] efs = this.thePlayer.GetComponents<effectBasic> ();
		for (int i = 0; i < efs.Length; i++)
			efs [i].OnSpUp (spupPerSecond);
	}

	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
	}

	void OnDestroy()
	{
		CancelInvoke ();
	}

	void Update()
	{
		thePlayer.ActerSp += spupPerSecond * Time.deltaTime;	
	}
}

