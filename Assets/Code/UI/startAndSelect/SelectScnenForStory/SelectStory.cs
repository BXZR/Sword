using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStory : MonoBehaviour {

	//按钮的预设物
	public GameObject theButtonProfab;
	//按钮的预设物的父物体
	public Transform theButtonContant;
	//显示游戏人物信息的文本
	public Text StoryInformationText;
	//按钮的预设物的父物体
	public Transform modePosition;

	//为了展示最开始的一个战士，需要保留一个引用
	private selectHeadForStory theFirstHead = null;

	void Start ()
	{
		Time.timeScale = 1f;
		makeButtons ();
		makeFirstFighter ();
	}

	public void makeButtons()
	{
		for (int i = 0; i < systemValues.playerNamesInGame.Length; i++)
		{
			GameObject theButton = GameObject.Instantiate<GameObject> (Resources.Load<GameObject>("UI/SelectButtonForStory"));
			theButton.transform.SetParent (theButtonContant.transform);
			theButton.transform.localPosition = Vector3.zero;
			theButton.transform.localScale = Vector3.one;
			selectHeadForStory theHead = theButton.GetComponent <selectHeadForStory> ();
			theHead.makeStart ( systemValues.playerNamesInGame[i] ,modePosition , StoryInformationText , i);
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
