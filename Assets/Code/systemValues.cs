using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class systemValues : MonoBehaviour {
	//程序面板单元
	public static float updateTimeWait = 0.1f;
	public static bool isAttacking(Animator theAnimator)
	{
		//如果是移动状态说明没有攻击
		//如果不是移动状态就说明正在攻击
		//所以要加上“非”
		//此外因为所有的攻击动作都在第1层，所以层的选择需要是1
		return  !theAnimator.GetCurrentAnimatorStateInfo (1).IsName ("moveMent");
	}

	public static string[] playerNames = { "归海一刀", "郭靖" ,"花木兰" };
	//head picture 被保存在里面与图片是对应的
	public static string [] playerHeadNames = {"knifeHead"  , "guojingHead", "mulanHead"};
	public static string [] playerNamesInGame = {"theFightrSword"  , "theFightrguojing", "theFighterMulan"};
	public static string [] playerTitleInformation = {"重剑无锋，大巧不工"  , "多段攻击，招式连发", "高速攻击，疯狂输出"};


	//选人界面的逻辑
	private static int indexNow = 0;
	public static  void setIndexForPlayer(int indexIn)
	{
		indexNow = indexIn;
	}
	public static string getNextPlayer()
	{
		indexNow++;
		if (indexNow > playerNamesInGame.Length-1)
			indexNow = 0;

		return playerNamesInGame [indexNow];
	}

	public static string getProPlayer()
	{
		indexNow--;
		if (indexNow < 0)
			indexNow = playerNamesInGame.Length - 1;
		return playerNamesInGame [indexNow];
	}
	public static string getNowPlayer()
	{
		return playerNamesInGame [indexNow];
	}

	public static string getTitleForPlayer(int indexNow)
	{
		return playerTitleInformation [indexNow];
	}

	public static string getHeadPictureName (string playerNameIn)
	{
		for (int i = 0; i < playerNames.Length; i++) 
		{
			if (playerNames [i] == playerNameIn)
				return playerHeadNames [i];
		}
		return  "";
	}
	//当前控制的游戏人物，留一个引用方便使用
	public static PlayerBasic thePlayer;
	//当前播放的背景音乐名
	public static string theBackMusicNameNow = "";

	//非常重要的参数，游戏模式
	//0 单机模式
	//1 网络模式
	public static int modeIndex = 0;
}
