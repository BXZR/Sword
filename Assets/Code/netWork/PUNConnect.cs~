using UnityEngine;
using System.Collections;

public class PUNConnect : MonoBehaviour {


    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
    }
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 75, 20, 150, 20), PhotonNetwork.connectionStateDetailed.ToString());
        if (GUI.Button(new Rect(Screen.width/2-50,50,100,30),"加入游戏房间" ))
        {
            if (PhotonNetwork.connected)
            {
				PhotonNetwork.JoinOrCreateRoom("fight", new RoomOptions {MaxPlayers = 16}, null);
				Invoke ("makeStart", 2);
            }

        }

        if (GUI.Button(new Rect(Screen.width / 2 - 50, 150, 100, 30), "退出游戏房间"))
        {
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.LeaveRoom();
            }

        }
    }

	void makeStart()
	{
		this.GetComponent <gameStarter> ().makeStart ();
	}
}
