﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour {

	//选人界面的开始按钮
	public InputField TextIn;
	public GameObject forwardImage;//黑屏需要的图片
	Button theButtonOfthis;//最开始的时候是不可以直接按下的
	public selectHeaderMaker theSelectMaker;//选人控制
	Text showLabel ;//显示网络连接模式的信息
	private bool isPrepareOver = false;//准备完成才能按按钮

	public void makeStart()
	{
		if (systemValues.modeIndex == 0) 
		{
			Destroy (TextIn.gameObject);
			showLabel = this.GetComponentInChildren<Text> ();
			showLabel.text = "开始游戏";
			isPrepareOver = true;
		}
		if (systemValues.modeIndex == 1) 
		{
			PhotonNetwork.ConnectUsingSettings ("1.0");
			showLabel = this.GetComponentInChildren<Text> ();
			showLabel.text = "正在连接";
		}

		theSelectMaker.makeFirstFighter ();
	}

	public void gotoPlay()
	{
		if (!isPrepareOver)
			return;
		
		if (systemValues.modeIndex == 0) 
		{
			Destroy (selectHead.therPlayer.gameObject);
			forwardImage.SetActive (true);
			selectHead.theStaticSelectedImage.SetActive (false);
			UnityEngine.SceneManagement.SceneManager.LoadScene ("theFight2");

		}
		if (systemValues.modeIndex == 1)
		{
			Destroy (selectHead.therPlayer.gameObject);
			PhotonNetwork.JoinOrCreateRoom (TextIn.text, new RoomOptions { MaxPlayers = 16 }, null);
			forwardImage.SetActive (true);
			selectHead.theStaticSelectedImage.SetActive (false);
			UnityEngine.SceneManagement.SceneManager.LoadScene ("theFight2");

		}



	}
	public void endGame()
	{
		if (systemValues.modeIndex == 1) 
		{
			PhotonNetwork.LeaveRoom ();
		}
	}

	void Start ()
	{
		makeStart ();
	}

	//连接上之后立即链接还是会出错
	int countWait = 0;
	int counTWaitMax =8;
	//所以间隔几帧作为保险

	void Update ()
	{
		if (systemValues.modeIndex == 1 && PhotonNetwork.connected )
		{
			if (!isPrepareOver) 
			{
				countWait++;
				if (countWait > counTWaitMax) 
				{
					isPrepareOver = true;
					showLabel.text = "开始游戏";
				}
			}
		}
	}

}