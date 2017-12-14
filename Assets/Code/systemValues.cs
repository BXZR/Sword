using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class systemValues : MonoBehaviour {
	//程序面板单元
	//统一invokeRepeat的调用时间
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
	//当前控制的游戏人物，留一个引用方便使用
	public static Animator thePlayerAnimator;
	//动画层1只有在这个状态之下才可以运行战斗动画
	public static string [] canAttackStateInBasicLayer = {"moveMent","rotatePoseBack" ,"rotatePoseForward","jump"};
	public static string canAttackStateInAttackLayer = "moveMent";
	//是否在战斗状态
	public static int theNotAttackLayerIndex = 0;//不攻击的状态全放在这一个动画层
	public static int theAttackLayerIndex  = 1;//攻击状态都放在这个层里面

	public static bool checkCanAttackAct()
	{
		//非攻击状态下的才可以转换，否则不行
		//默认只有在攻击层的状态转换才能判断这个
		//因为分层，这里有一些内容已经需要有改变了，人物能够进入攻击动作的条件：
		//层1处于moveMent状态，也就是没有异常状态例如beHit
		//层2处于moveMent状态，也就是上一个攻击动作已经完成
		if (thePlayerAnimator == null)
			return false;

		//有一些异常的状态（例如击倒）是没有办法攻击的
		for (int i = 0; i < canAttackStateInBasicLayer.Length; i++)
		{
			if (thePlayerAnimator.GetCurrentAnimatorStateInfo (theNotAttackLayerIndex).IsName (canAttackStateInBasicLayer[i]) &&
				thePlayerAnimator.GetCurrentAnimatorStateInfo (theAttackLayerIndex).IsName (canAttackStateInAttackLayer)) //在层1中只有在移动状态下才可以进行着各种战斗动作
			return  true;
		}

		return  false;
	}


	//当前播放的背景音乐名
	public static string theBackMusicNameNow = "";

	//非常重要的参数，游戏模式
	//0 单机模式
	//1 网络模式
	public static int modeIndex = 0;
}
