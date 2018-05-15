using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏玩法2
//无尽模式
//一直战斗下去，知道不想玩了或者....挂了
public class playMode3 : playModeBasic {

	//这个任务需要的时间
	public float timerForKill = 240f;//这个任务需要在60秒之内完成
	private string theGameEndInformation = "极限生存";
	private bool ended = false;

	private int count = 0;
	private static  int maxCount = 0;


	public override void OnGameStart ()
	{
		count = GameObject.FindGameObjectsWithTag ("AI").Length;
		//systemValues.thePlayer.ActerSpeedOverPervnet += 0.2f; //世界BUFF，这个过后要用脚本实现
	}


	public override void OnGameUpdate ()
	{
		timerForKill -= Time .deltaTime;
	}

	public override bool isGameOver ()
	{
		if (timerForKill < 0) 
		{
			return true;
		}
		return false;
	}

	public override void OnGameEnd ()
	{
		int countNow = 0;
		GameObject[] AIS = GameObject.FindGameObjectsWithTag ("AI");
		for (int i = 0; i < AIS.Length; i++) 
		{
			PlayerBasic PB = AIS [i].GetComponent <PlayerBasic> ();
			if (PB && PB.isAlive)
				countNow++;
		}
		count = count - countNow;
		if (count > maxCount)
			maxCount = count;
		theGameEndInformation = "限时击杀："+count +"  最佳战绩："+maxCount;
		systemValues.makeGameEnd(theGameEndInformation);
	}

	void OnGUI()
	{ 
		if ( systemValues.isGamming )
		{
			if (!systemValues.isSystemUIUsing ()) 
			{
				GUI.Box (new Rect(Screen.width*0.85f , 0 , Screen.width*0.15f,Screen.height*0.03f) , "剩余时间："+ timerForKill.ToString("f0")+"s");
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
