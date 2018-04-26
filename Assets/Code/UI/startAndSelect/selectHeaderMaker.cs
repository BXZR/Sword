using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectHeaderMaker : MonoBehaviour {

	//制作基础的选人界面的按钮
	//自动化读取“配置文件类”做

	//按钮的预设物
	public GameObject theButtonProfab;
	//按钮的预设物的父物体
	public Transform theButtonContant;
	//显示游戏人物信息的文本
	public Text playerInformationText;
	//显示游戏人物信息的文本
	public Text playerTitleText;
	//显示游戏人物技能的item
	public GameObject  theAttackEffectItemProfab;
	//显示游戏人物技能的位置
	public Transform theshowContantFortheAttackEffectItem;

	//按钮的预设物的父物体
	public Transform modePosition;

	//多边形状态图(父物体)
	public GameObject theStateImage;

	//为了展示最开始的一个战士，需要保留一个引用
	private selectHead theFirstHead = null;

	void Start ()
	{
		makeButtons ();

	}

	public void makeButtons()
	{
		for (int i = 0; i < systemValues.playerNamesInGame.Length; i++)
		{
			GameObject theButton = GameObject.Instantiate<GameObject> (Resources.Load<GameObject>("UI/SelectButton"));
			theButton.transform.SetParent (theButtonContant.transform);
			theButton.transform.localPosition = new Vector3 (0,0,0);
			theButton.transform.localScale = new Vector3 (1,1,1);
			selectHead theHead = theButton.GetComponent <selectHead> ();
			theHead.makeStart ( systemValues.playerNamesInGame[i] , playerTitleText , playerInformationText ,  theAttackEffectItemProfab,
			theshowContantFortheAttackEffectItem ,modePosition , i);
			theHead.setStateImage (theStateImage);
			theButton.GetComponent <Image> ().sprite = systemValues.makeLoadSprite ("playerHeadPicture/"+ systemValues.playerHeadNames[i]);
			if (theFirstHead == null)
				theFirstHead = theHead;
		}
	}
		
	//加载第一个战士
	public void makeFirstFighter()
	{
		//print ("make first fighter");
		if (theFirstHead)
			theFirstHead.makePlayer ();
	}
}
