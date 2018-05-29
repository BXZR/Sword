using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class theSceneInformationPanel : MonoBehaviour {

	//场景信息面板
	//这个界面会用来介绍场景，加成等等各种游戏信息
	//算是游戏信息集中介绍面板
	//设计这个面板的时候税号更加分散一点，改动一定会不少啊

	public Text theTextForSceneSkill;
	public Text theTextForSceneInformation;
	public  Text theTextForScnerAim;
	public Image theScnenImage;
	public Image theAimImage;

	void Start () 
	{
		
	}

	void OnEnable()
	{
		try{flashThePanel ();}
		catch(System.Exception D) {print (D.Message);}
	}


	//界面信息刷新通用方法
	private void flashThePanel()
	{
		List<string> theSceneAimInformation = systemValues.getGameModeWithMove();
		if (theTextForScnerAim)
			theTextForScnerAim.text = "主线任务："+theSceneAimInformation [0] + "\n" + theSceneAimInformation [1];
		if (theScnenImage)
			theScnenImage.sprite = systemValues.makeLoadSprite ("ScenePicture/" +systemValues.getNowScene());
		if(theAimImage)
			theAimImage.sprite = systemValues.makeLoadSprite ("gameMode/" +theSceneAimInformation [2]);

		string theEffectName = systemValues.getGameModeExtraEffect ();
		if(theTextForSceneSkill)
			theTextForSceneSkill.text = "场景特效："+systemValues.getEffectInfromationWithName (theEffectName);

		string theSceneInformation = systemValues.getSceneInformaiton ();
		if (theTextForSceneInformation)
			theTextForSceneInformation.text = theSceneInformation;


	}


	void Update () 
	{
		
	}
}
