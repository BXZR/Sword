using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMode4 : playModeBasic {

	//这个任务需要的时间

	private string theGameEndInformation = "练习模式";

	public override void OnGameStart ()
	{
		systemValues.soulCount = 88888;
		modeName = systemValues.getGameModeWithMove () [0];

		theShowRect = new Rect(Screen.width*0.85f , 0 , Screen.width*0.15f,Screen.height*0.03f)  ;
		rectShowString = modeName;

		InvokeRepeating ("flashGUI", 0f, 5f);
	}

	public override void flashGUI ()
	{
		theShowRect = new Rect(Screen.width*0.85f , 0 , Screen.width*0.15f,Screen.height*0.03f)  ;
		rectShowString = modeName;
	}
	void OnGUI()
	{ 
		if (systemValues.isGamming  && !systemValues.isSystemUIUsing ()) 
		{
			GUI.Box (theShowRect,rectShowString);
		}
	}
}
