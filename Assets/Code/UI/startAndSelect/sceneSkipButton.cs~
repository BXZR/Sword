using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneSkipButton : MonoBehaviour {
	//最单纯的场景跳转按钮，单独列出来防止耦合

	public string aimsceneName = "start";
	public int GameMode = 0;
	public void MoveToScene()
	{
		//时间始终要控制为最基本的状态
		Time.timeScale = 1f;
		systemValues.modeIndex = GameMode;
		Cursor.visible = true;
		systemValues.messageBoxClose ();
		UnityEngine.SceneManagement.SceneManager.LoadScene (aimsceneName);

	}

	//结束整个游戏
	//也算是换一个场景吧.....
	public void endTheGame()
	{
		systemValues.messageBoxClose ();
		Application.Quit ();
	}
}
