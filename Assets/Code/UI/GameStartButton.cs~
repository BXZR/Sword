using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour {

	//选人界面的开始按钮
	public InputField TextIn;
	public GameObject forwardImage;//黑屏需要的图片
	Button theButtonOfthis;//最开始的时候是不可以直接按下的
	public selectHeaderMaker theSelectMaker;//选人控制

	public void makeStart()
	{
		if (systemValues.modeIndex == 0) 
		{
			Destroy (TextIn.gameObject);
		}
		if (systemValues.modeIndex == 1) 
		{
			PhotonNetwork.ConnectUsingSettings ("1.0");
			theButtonOfthis = this.GetComponent <Button> ();
			theButtonOfthis.enabled = false;
		}
		theSelectMaker.makeFirstFighter ();
	}

	public void gotoPlay()
	{
		if (systemValues.modeIndex == 0) 
		{
			Destroy (selectHead.therPlayer.gameObject);
			forwardImage.SetActive (true);
			UnityEngine.SceneManagement.SceneManager.LoadScene ("theFight2");
		}
		if (systemValues.modeIndex == 1)
		{
			Destroy (selectHead.therPlayer.gameObject);
			PhotonNetwork.JoinOrCreateRoom (TextIn.text, new RoomOptions { MaxPlayers = 16 }, null);
			forwardImage.SetActive (true);
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

	void Update ()
	{
		if (systemValues.modeIndex == 1 && PhotonNetwork.connected && theButtonOfthis.enabled== false )
		{
			theButtonOfthis.enabled = true;
		}
	}

}
