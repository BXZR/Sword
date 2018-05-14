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
		theButtonForControllerImage = this.GetComponent<Image> ();
		if (systemValues.modeIndex == 0) 
		{
			theInformations = systemValues.getGameModeWithMove ();
			makeShow ();
		} 
		else 
		{
			theButtonForControllerImage.sprite = systemValues.makeLoadSprite ("gameMode/NoMode");
			modeNameText.text = "";
		}
	}
		
	public void newMode(int adder)
	{
		if (systemValues.modeIndex == 0) 
		{
			theInformations = systemValues.getGameModeWithMove (adder);
			makeShow ();
		}
		else 
		{
			systemValues.messageTitleBoxShow ("对战模式无法选择游戏玩法");
		}

	}


	private void makeShow()
	{
		if (theInformations.Count <= 0)
			return;

		string gameModeName = theInformations [0];
		string gameModeInformation = theInformations [1];
		string gameModePicture = theInformations [2];

		modeNameText.text = systemValues.rowStringToColumn ("☵" + gameModeName + "☲☯");
		theButtonForControllerImage.sprite = systemValues.makeLoadSprite ("gameMode/" + gameModePicture);

	}

	public void showInformation()
	{
		if (systemValues.modeIndex == 0) 
		{
			if (theInformations.Count > 0)
			{
				string informationShow = theInformations [1];
				if(!string.IsNullOrEmpty(theInformations [3]))
				    informationShow += "\n\n模式特效：" + systemValues.getEffectInfromationWithName (theInformations [3] , this.gameObject);
				systemValues.messageBoxShow (theInformations [0],informationShow, true);
			}
			else
				systemValues.messageTitleBoxShow ("无法获取信息");
		} 
		else 
		{
			systemValues.messageTitleBoxShow ("对战模式无特殊游戏玩法");
		}
	}
}
