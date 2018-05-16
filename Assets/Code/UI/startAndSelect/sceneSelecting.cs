using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneSelecting : MonoBehaviour {

	//选择副本（场景）的控制单元，其实也是也个按钮

	public Text sceneNameText;
	private Image theButtonForControllerImage;
	void Start()
	{
		Time.timeScale = 1f;
		systemValues.sceneSelectFlash ();
		theButtonForControllerImage = this.GetComponent <Image> ();
		theButtonForControllerImage.sprite = systemValues.makeLoadSprite ("ScenePicture/"+ systemValues.getNowScene());
		makeText (systemValues.getSceneInformaiton () );
	}

	public void showSceneInformation()
	{
		string information = systemValues.getSceneInformaiton ();
		systemValues.messageBoxShow ("场景介绍", information , true );
		makeText (systemValues.getSceneInformaiton () );
	}

	public void makeNextScene()
	{
		if (systemValues.theGameSystemMode == GameSystemMode.NET)
			systemValues.messageTitleBoxShow ("对战模式不可以选择地图");
		else 
		{
			string information = systemValues.getNextScene ();
			theButtonForControllerImage.sprite = systemValues.makeLoadSprite ("ScenePicture/" +information );
			makeText (systemValues.getSceneInformaiton ());
		}
	}

	public void makePreScene()
	{
		if (systemValues.theGameSystemMode == GameSystemMode.NET)
			systemValues.messageTitleBoxShow ("对战模式不可以选择地图");
		else 
		{
			string information = systemValues.getPreScene ();
			theButtonForControllerImage.sprite = systemValues.makeLoadSprite ("ScenePicture/" +information );
			makeText (systemValues.getSceneInformaiton ());
		}
	}


	private void makeText(string information)
	{
		if (sceneNameText)
			sceneNameText.text = systemValues.rowStringToColumn(  "☯☲" + information.Split ('\n') [0] +"☵");
	}
}
