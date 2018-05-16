using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneSkipButton : MonoBehaviour {
	//最单纯的场景跳转按钮，单独列出来防止耦合

	public string aimsceneName = "start";
	public GameSystemMode theMode;
	public void MoveToScene()
	{
		//时间始终要控制为最基本的状态
		Time.timeScale = 1f;
		systemValues.theGameSystemMode  =  theMode;
		Cursor.visible = true;
		systemValues.messageBoxClose ();
		UnityEngine.SceneManagement.SceneManager.LoadScene (aimsceneName);

	}

	public void GamingToStart()
	{
		systemValues.choiceMessageBoxShow ("返回？", "返回初始则当前游戏进度将会随风而逝，是否返回？", true, new MesageOperate (MoveToScene));
	}
	public void GamingToEnd()
	{
		systemValues.choiceMessageBoxShow ("结束？", "是否希望推出整个游戏？", true, new MesageOperate (endTheGame));
	}

	//结束整个游戏
	//也算是换一个场景吧.....
	public void endTheGame()
	{
		systemValues.messageBoxClose ();
		Application.Quit ();
	}
}
