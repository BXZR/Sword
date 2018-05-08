using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameModeSelecting : MonoBehaviour {

	//选择游戏模式，代码与选择游戏场景非常相似

	public Text modeNameText;
	private Image theButtonForControllerImage;
	List<string> theInformations  = new List<string> ();

	void Start()
	{
		theInformations = systemValues.getGameModeWithMove();
		theButtonForControllerImage = this.GetComponent<Image> ();
		makeShow ();
	}
		
	public void newMode(int adder)
	{
		theInformations = systemValues.getGameModeWithMove(adder);
		makeShow ();
	}


	private void makeShow()
	{
		if (theInformations.Count <= 0)
			return;

		string gameModeName = theInformations [0];
		string gameModeInformation = theInformations [1];
		string gameModePicture = theInformations [2];

		modeNameText.text = systemValues.rowStringToColumn( "☵" + gameModeName+"☲☯");
		theButtonForControllerImage.sprite = systemValues.makeLoadSprite ("gameMode/"+gameModePicture);
	}

	public void showInformation()
	{
		if (theInformations.Count > 0)
			systemValues.messageBoxShow (theInformations [0], theInformations [1], true);
		else
			systemValues.messageTitleBoxShow ("无法获取信息");
	}
}
