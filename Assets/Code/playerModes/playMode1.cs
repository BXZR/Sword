﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏玩法1
//定时杀人
//在规定的时间之内需要杀掉某一个目标，被杀或者时间都算是输了
using UnityEngine.AI;


public class playMode1 : playModeBasic {

	//这个任务需要的时间
	public float timerForKill = 50f;//这个任务需要在900秒之内完成
	public string theBossProfabName = "AISoldierBoss";//为了保证灵活配置，这里就用字符串联系资源进行加载
	private PlayerBasic theBoss;//留一个boss引用
	private string theGameEndInformation = "恭喜你完成了这项艰巨的试炼。";
	private bool ended = false;

	public override void OnGameStart ()
	{
		GameObject Boss = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("fighters/" + theBossProfabName));
		Boss.GetComponent <NavMeshAgent> ().enabled = false;
		Boss.transform.position = systemValues.theSpecialTransformStatic.position;
		theBoss = Boss.GetComponent <PlayerBasic> (); 
		Boss.GetComponent <NavMeshAgent> ().enabled = true;
	}

 
	public override void OnGameUpdate ()
	{
		timerForKill -= Time .deltaTime;
	}

	public override bool isGameOver ()
	{
		if (theBoss) 
		{
			if (!theBoss.isAlive) 
			{
				theGameEndInformation = "恭喜你完成了这项艰巨的试炼。";
				return true;
			} 
			else if (timerForKill < 0) 
			{
				theGameEndInformation = "挑战失败。";
				return true;
			}
		}
		return false;
	}

	public override void OnGameEnd ()
	{
		systemValues.makeGameEnd(theGameEndInformation);
	}

	void OnGUI()
	{ 
		if ( systemValues.isGamming )
		{
			if (!systemValues.isSystemUIUsing ()) 
			{
				GUI.Box (new Rect(Screen.width*0.9f , 0 , Screen.width*0.1f,Screen.height*0.1f) , "剩余时间："+ timerForKill.ToString("f0"));
			}
		}
	}
		
	
	// Update is called once per frame
	void Update () 
	{
		if (!ended )
		{
			OnGameUpdate ();
			if (isGameOver ())
			{
				OnGameEnd ();
				ended = true;
			}
		}
	   
	}
}