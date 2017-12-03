using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour {

	//选人界面的开始按钮
	public InputField TextIn;
	public selectPlayerButton nextPlayerButton;
	public GameObject forwardImage;//黑屏需要的图片
	Button theButtonOfthis;//最开始的时候是不可以直接按下的

	public void makeStart()
	{
		PhotonNetwork.ConnectUsingSettings("1.0");
		nextPlayerButton.getProPlayer ();
		theButtonOfthis = this.GetComponent <Button> ();
		theButtonOfthis.enabled = false;
	}

	public void gotoPlay()
	{
		PhotonNetwork.JoinOrCreateRoom(TextIn.text, new RoomOptions {MaxPlayers = 16}, null);
		nextPlayerButton.DestroyPlayerMode ();
		forwardImage.SetActive (true);
		UnityEngine.SceneManagement.SceneManager.LoadScene ("theFight2");


	}
	public void endGame()
	{
		PhotonNetwork.LeaveRoom();
	}

	void Update ()
	{
		if (PhotonNetwork.connected && theButtonOfthis.enabled== false )
		{
			theButtonOfthis.enabled = true;
		}
	}

}
