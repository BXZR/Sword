using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectHpUp : effectBasic{

	float timer = 0f;
	float timerMax = 3f;
	float hpupMax = 210f;
	float hpupPerSecond = 0;

	void Start ()
	{
		hpupPerSecond = hpupMax / timerMax;
		makeStart ();
		Destroy (this, timerMax);
		InvokeRepeating ("makeHpUpEffect",0f,1f);
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
