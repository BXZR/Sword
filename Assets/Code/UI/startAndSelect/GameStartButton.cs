using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour {

	//选人界面的开始按钮
	public netModeOptions NetOption;
	public GameObject forwardImage;//黑屏需要的图片
	Button theButtonOfthis;//最开始的时候是不可以直接按下的
	public selectHeaderMaker theSelectMaker;//选人控制
	Text showLabel ;//显示网络连接模式的信息
	private bool isPrepareOver = false;//准备完成才能按按钮

	public void makeStart()
	{
		if (systemValues.theGameSystemMode == GameSystemMode.PC) 
		{
			if(NetOption)
			    NetOption.singleModeShow ();
			showLabel = this.GetComponentInChildren<Text> ();
			showLabel.text = "开始游戏";
			isPrepareOver = true;
		}
		if (systemValues.theGameSystemMode == GameSystemMode.NET) 
		{
			if(NetOption)
				NetOption.netModeShow ();
			PhotonNetwork.ConnectUsingSettings ("1.0");
			showLabel = this.GetComponentInChildren<Text> ();
			showLabel.text = "正在连接";
		}

		if(theSelectMaker)
		    theSelectMaker.makeFirstFighter ();
	}

	public void gotoPlay()
	{
		if (!isPrepareOver)
			return;

		//跳转的时候消息框自动关闭
		systemValues.messageBoxClose ();

		if (systemValues.theGameSystemMode == GameSystemMode.PC) 
		{
			if(selectHead.therPlayer)
			   Destroy (selectHead.therPlayer.gameObject);
			if (selectHeadForStory.therPlayer)
				Destroy (selectHeadForStory.therPlayer.gameObject);
			
			forwardImage.SetActive (true);

		 	if(selectHead.theStaticSelectedImage)
			  selectHead.theStaticSelectedImage.SetActive (false);
			if (selectHeadForStory.theStaticSelectedImage)
				selectHeadForStory.theStaticSelectedImage.SetActive (false);

			UnityEngine.SceneManagement.SceneManager.LoadScene (systemValues.getScnenForSystem());

		}
		if (systemValues.theGameSystemMode == GameSystemMode.NET)
		{
			Destroy (selectHead.therPlayer.gameObject);
			PhotonNetwork.JoinOrCreateRoom (NetOption.theInputForRoom.text, new RoomOptions { MaxPlayers = 16 }, null);
			forwardImage.SetActive (true);
			selectHead.theStaticSelectedImage.SetActive (false);
			UnityEngine.SceneManagement.SceneManager.LoadScene (systemValues.getScnenForSystem());

		}
	}


	public void getoPlayStory()
	{
		if(selectHead.therPlayer)
			Destroy (selectHead.therPlayer.gameObject);
		if (selectHeadForStory.therPlayer)
			Destroy (selectHeadForStory.therPlayer.gameObject);

		forwardImage.SetActive (true);

		if(selectHead.theStaticSelectedImage)
			selectHead.theStaticSelectedImage.SetActive (false);
		if (selectHeadForStory.theStaticSelectedImage)
			selectHeadForStory.theStaticSelectedImage.SetActive (false);

		UnityEngine.SceneManagement.SceneManager.LoadScene (systemValues.getScnenForStory());

	}


	public void endGame()
	{
		if (systemValues.theGameSystemMode == GameSystemMode.NET) 
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
		if (systemValues.theGameSystemMode == GameSystemMode.NET && PhotonNetwork.connected )
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
