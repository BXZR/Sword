﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectHead : MonoBehaviour {

	//选择人物的时候点选的按钮
	private string theFightName = "";//对应的战士的名称
	//显示游戏人物信息的文本
	private Text playerInformationText;
	//显示游戏人物信息的文本
	private Text playerTitleText;
	//目标位置
	private Transform thetransForShow;
	//引用
	public   static GameObject therPlayer;
	//系统配置文件类的标记
	private int  indexForSystemValues;
	public void makeStart(string playerName , Text theTitleText , Text theDetailText , Transform thetransForShowIn , int indexForSystemValuesIn)
	{
		theFightName = playerName;
		playerInformationText = theDetailText;
		playerTitleText = theTitleText;
		thetransForShow = thetransForShowIn;
		indexForSystemValues = indexForSystemValuesIn;
	}


	public void makePlayer()
	{
		if(therPlayer!=null)
			Destroy(therPlayer);
		
		GameObject theProfab = Resources.Load<GameObject> ("fighters/"+theFightName);
		therPlayer = GameObject.Instantiate (theProfab );
		therPlayer.transform.position = thetransForShow .position;
		therPlayer.transform.localScale = new Vector3 (4,4,4);
		therPlayer.AddComponent<extraRotate> ();
		PlayerBasic thePlayerB = therPlayer.GetComponent<PlayerBasic> ();
		playerInformationText.text = thePlayerB.getPlayerInformation () + thePlayerB.getPlayerInformationExtra ();
		playerTitleText.text = "<color=#00FF00>"  + thePlayerB.ActerName + "</color>\n<color=#FF2400>" + systemValues.getTitleForPlayer (indexForSystemValues)+"</color>";
		systemValues.setIndexForPlayer (indexForSystemValues);
	}




}