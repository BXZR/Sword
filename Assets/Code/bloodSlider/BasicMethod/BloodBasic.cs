﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBasic : MonoBehaviour {

	//非主要任务的头顶血条只有在被看到的时候才会真正地被激活
	//需要meshrenderer
	PlayerBasic thePlayer;
	Renderer theRender;

	//显示有时间限制
	float showHpTimer = 1f;
	float showHPTimerMax = 1f;

	void Start ()
	{
		thePlayer = this.transform.root.GetComponent<PlayerBasic> ();
		theRender = this.GetComponentInChildren <Renderer> ();
		InvokeRepeating("showBlood" , 3f, 0.2f);
	}

	public void flashBloodShowTimer()
	{
		showHpTimer = showHPTimerMax;
		thePlayer.isShowing = true;
	}

	void showBlood()
	{
		//方法1，GUI方法，因为不存在遮挡，有点不想用
		if (theRender.isVisible)
		{
			if (thePlayer.isShowing) 
			{
				showHpTimer -= 0.2f;
				if (showHpTimer < 0) 
				{
					thePlayer.isShowing = false;
				}
			}
		}
		else 
		{
			thePlayer.isShowing = false;
		}
	}
	void Update () 
	{

	}
}