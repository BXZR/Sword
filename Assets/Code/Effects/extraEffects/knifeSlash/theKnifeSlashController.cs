﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theKnifeSlashController : effectBasic {

	MeleeWeaponTrail theEffectForKnife = null;
	public float keepTimer =0.3f;//持续时间
	private float keepTimerMax = 0.3f;//刷新用的时间备份
	private Animator theAnimator = null;

	void Start ()
	{
		isShowing = false;
	}
	public override void onAttackAction ()//timeLength是持续时间
	{
		if (theEffectForKnife == null)
			theEffectForKnife = this.GetComponentInChildren<MeleeWeaponTrail> ();
		if (theAnimator == null)
			theAnimator = this.GetComponentInChildren<Animator> ();
		
		keepTimer = keepTimerMax;//每一次激活都增加一点时间
		theEffectForKnife.Use = true;
	}

	void Update ()
	{
		if (theEffectForKnife != null && theEffectForKnife.enabled) 
		{
			keepTimer -= Time.deltaTime;
			//从这里来看最少也要持续 keepTimerMax的时长
			if (keepTimer < 0 && systemValues.isAttacking(theAnimator) == false)
			{
				keepTimer = keepTimerMax;
				theEffectForKnife.Use = false;
			}
		}
	}
}