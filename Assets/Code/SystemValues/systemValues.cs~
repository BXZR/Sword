using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameSystemMode { NET , PC }
public class systemValues : MonoBehaviour {
	//程序面板单元
	//也可以理解为这是每一个客户端的计分板

	#region系统记录静态参数信息
	//用来记录这个客户端的相关内容
	//统一invokeRepeat的调用时间
	public static float updateTimeWait = 0.1f;
	//当前播放的背景音乐名
	public static string theBackMusicNameNow = "";
	//当前控制的游戏人物，留一个引用方便使用
	public static PlayerBasic thePlayer;
	//当前控制的游戏人物，留一个引用方便使用
	public static Animator thePlayerAnimator;
	//非常重要的参数，游戏模式
	public static GameSystemMode theGameSystemMode = GameSystemMode .PC;//默认PC
	//游戏是否正在进行标记，如果没有再进行，各种东西都没有必要进行计算了
	public static bool isGamming = true;

	//游戏特殊的位置
	//各种模式都用得上所以还是留一个出来
	public Transform theSpecialTransform;
	public static Transform theSpecialTransformStatic;

	//网络控制节点=============================
	static PhotonView photonView;
	//=========================================
	public static float learnWulingSpPercent = 0.75f;//修炼五灵需要消耗的当前的斗气百分比
	public static equipBasics theEquipNow = null;//当前选中的注灵效果或者装备
	public static equipBasics theEquipNowInShop = null;//当前选中的注灵效果或者装备(商店)
	//当前游戏模式
	public static playModeBasic playModeNow ;
	#endregion

	#region游戏角色信息设定查询
	//游戏角色相关信息设定和查询
	public static string[] playerNames = { "归海一刀", "郭靖" ,"花木兰" , "慕容紫英"};
	//head picture 被保存在里面与图片是对应的
	public static string [] playerHeadNames = {"knifeHead"  , "guojingHead", "mulanHead", "ziyingHead"};
	public static string [] playerNamesInGame = {"theFightrSword"  , "theFightrguojing", "theFighterMulan", "theFightrZiying"};
	public static string [] playerTitleInformation = {"重剑无锋，大巧不工"  , "多段攻击，招式连发", "高速攻击，疯狂输出" , "剑气冲霄，高攻减伤"};
	public static string [] playerBackMusic = {"bahuangfu"  , "kai", "fightMusic", "canglangjianfu"};

	//选人界面的逻辑
	private static int indexNow = 2 ;
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

	//获得背景音乐名称
	public  static string getBackMusicName()
	{
		return playerBackMusic [indexNow];
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
	#endregion

	#region连招状态检查
	//动画层1只有在这个状态之下才可以运行战斗动画
	public static string [] canAttackStateInBasicLayer = {"moveMent","rotatePoseBack" ,"rotatePoseForward","jump"};
	public static string canAttackStateInAttackLayer = "moveMent";
	//是否在战斗状态
	public static int theNotAttackLayerIndex = 0;//不攻击的状态全放在这一个动画层
	public static int theAttackLayerIndex  = 1;//攻击状态都放在这个层里面
	public static bool isAttacking(Animator theAnimator)
	{
		//如果是移动状态说明没有攻击
		//如果不是移动状态就说明正在攻击
		//所以要加上“非”
		//此外因为所有的攻击动作都在第1层，所以层的选择需要是1
		return  !theAnimator.GetCurrentAnimatorStateInfo (1).IsName ("moveMent");
	}
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

	public static bool checkCanAttackAct(Animator theAnimator)
	{
		//非攻击状态下的才可以转换，否则不行
		//默认只有在攻击层的状态转换才能判断这个
		//因为分层，这里有一些内容已经需要有改变了，人物能够进入攻击动作的条件：
		//层1处于moveMent状态，也就是没有异常状态例如beHit
		//层2处于moveMent状态，也就是上一个攻击动作已经完成
		if (theAnimator == null)
			return false;

		//有一些异常的状态（例如击倒）是没有办法攻击的
		for (int i = 0; i < canAttackStateInBasicLayer.Length; i++)
		{
			if (theAnimator.GetCurrentAnimatorStateInfo (theNotAttackLayerIndex).IsName (canAttackStateInBasicLayer[i]) &&
				theAnimator.GetCurrentAnimatorStateInfo (theAttackLayerIndex).IsName (canAttackStateInAttackLayer)) //在层1中只有在移动状态下才可以进行着各种战斗动作
				return  true;
		}

		return  false;
	}
	#endregion

	#region连招基本信息
	//获取连招出招表（中文）
	public static string []  attackLinkChinese= {"上", "下", "左" , "右" , "击" , "换" , "止"};
	public static string []  attackLinkEnglish= {"W", "S", "A" , "D" , "J" , "Q"  , "E"};
	//这是一个非常天真的方法
	public static string getAttacklinkInformationTranslated(string attacklink)
	{
		char[] attacklinkChar = attacklink.ToCharArray ();
		string information = "";
		for (int i = 0; i < attacklinkChar.Length; i++) 
		{
			bool findChar = false;
			for (int j = 0; j < attackLinkEnglish.Length; j++) 
			{
				if (attacklinkChar [i].ToString() == attackLinkEnglish [j] && !findChar)
				{
					information += attackLinkChinese [j];
					findChar = true;
				}
			}
			if(!findChar)
				information += "-";
		}
		return information;
	}
	#endregion

	#region获取连招、效果信息
	//工具方法，更为复杂的方法
	//用于连招的显示按钮等等信息的全部获取
	public static List < attackLinkInformation>  getEffectInformationsMore(GameObject thePlayer,bool withAttackLinkEffect = false)
	{
		List < attackLinkInformation> theAttackLinkInformaitons = new List<attackLinkInformation> ();
		List<effectBasic> buffer = new List<effectBasic> ();
		string showString = "";
		string showExtra = "";
		//被动没有连招，但是也应该显示
		effectBasic [] efs = thePlayer.GetComponentsInChildren<effectBasic>();
		for (int i = 0; i < efs.Length; i++)
		{
			buffer.Add (efs[i]);
			efs [i].Init ();
			if (efs [i].isBE () && efs[i].isShowing()) 
			{
				attackLinkInformation theInformation = new attackLinkInformation ();
				theInformation.attackLinkName = "";
				theInformation.attackLinkString = "";
				theInformation.theEffectForSelfName=  efs[i].getEffectName(false,false);
				theInformation.theEffectForSelfInformaion =  efs[i].getInformation(false) +"\n"+efs[i].getExtraInformation();
				theAttackLinkInformaitons.Add (theInformation);
			}
		}
		//展示连招中触发的各种效果
		if (withAttackLinkEffect) 
		{
			attackLink[] attacklinks = thePlayer.GetComponentsInChildren<attackLink> (); 
			//因为有顺序和统一调用的问题，建议建立之后统一进行销毁，因此建立一个预存。

			foreach (attackLink ak in attacklinks)
			{
				ak.makeStart ();
				attackLinkInformation theInformation = new attackLinkInformation ();
				theInformation.attackLinkName = ak.skillName;
				theInformation.attackLinkString = ak.attackLinkString.Split(';')[0];
				theInformation.thePlayer = thePlayer.GetComponentInChildren<PlayerBasic>();
				theInformation.attackLinkInformationText = ak.getInformation ( true ,false);//获取简略的信息就足够了

				//if (string.IsNullOrEmpty (ak.conNameToEMY) == false) 
				if (!isNullOrEmpty(ak.conNameToEMY)) 
				{
					//初始化一下效果
					System.Type theType = System.Type.GetType (ak.conNameToEMY);
					//thePlayer.gameObject.AddComponent (theType);
					//effectBasic theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
					effectBasic theEffect = thePlayer.gameObject.AddComponent (theType) as effectBasic;
					buffer.Add (theEffect);

					if(theEffect.isShowing())
					{
						theEffect.Init ();
						theInformation.theEffectForEMYName = theEffect.getEffectName(true,true);
						showString = theEffect.getInformation (false);
						showExtra = theEffect.getExtraInformation ();
						if (isNullOrEmpty  (showExtra) == false)
							showString += "\n" + showExtra;
						theInformation.theEffectForEMYInformaion =  showString;
					}
				}
				//if (string.IsNullOrEmpty (ak.conNameToSELF) == false) 
				if (!isNullOrEmpty(ak.conNameToSELF)) 
				{
					//初始化一下效果
					System.Type theType = System.Type.GetType (ak.conNameToSELF);
					//thePlayer.gameObject.AddComponent (theType);
					//effectBasic theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
					effectBasic theEffect = thePlayer.gameObject.AddComponent (theType) as effectBasic;
					buffer.Add (theEffect);

					if(theEffect.isShowing())
					{
						theEffect.Init ();
						theInformation.theEffectForSelfName = theEffect.getEffectName(true,true);
						showString =  theEffect.getInformation (false);
						showExtra = theEffect.getExtraInformation ();
						if (isNullOrEmpty (showExtra) == false)
							showString += "\n" + showExtra;
						theInformation.theEffectForSelfInformaion =  showString;
					}
				}
				theAttackLinkInformaitons.Add (theInformation);
			}

			//清空预存
			for (int i = 0; i < buffer.Count; i++){
				DestroyImmediate (buffer [i]);}
		}
		return theAttackLinkInformaitons;
	}


	//工具方法，获得所有可显示的技能效果等等的信息
	public static string getEffectInformations(GameObject thePlayer,bool withAttackLinkEffect = false)
	{
		List<effectBasic> buffer = new List<effectBasic> ();
		if (withAttackLinkEffect) 
		{
			attackLink[] attacklinks = thePlayer.GetComponentsInChildren<attackLink> (); 
			//因为有顺序和统一调用的问题，建议建立之后统一进行销毁，因此建立一个预存。
			foreach (attackLink ak in attacklinks)
			{
				if (!isNullOrEmpty(ak.conNameToEMY)) 
				{
					//初始化一下效果
					System.Type theType = System.Type.GetType (ak.conNameToEMY);
					//thePlayer.gameObject.AddComponent (theType);
					//effectBasic theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
					effectBasic theEffect = thePlayer.gameObject.AddComponent (theType) as effectBasic;
					buffer.Add (theEffect);
				}
				if (!isNullOrEmpty(ak.conNameToSELF )) 
				{
					//初始化一下效果
					System.Type theType = System.Type.GetType (ak.conNameToSELF);
					//thePlayer.gameObject.AddComponent (theType);
					//effectBasic theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
					effectBasic theEffect = thePlayer.gameObject.AddComponent (theType) as effectBasic;
					buffer.Add (theEffect);
				}
			}
		}
		string skillsInformation = "\n";
		string skillsNames = "\n";
		effectBasic[] effects = thePlayer.GetComponents<effectBasic> ();
		foreach (effectBasic ef in effects) 
		{
			ef.Init ();
			string showString = ef.getInformation ();
			string showExtra = ef.getExtraInformation ();

			if(ef.isShowing())
				skillsNames += ef.getEffectName(false) +"\n";

			if (string.IsNullOrEmpty (showExtra) == false)
				showString += "\n" + showExtra;
			skillsInformation += showString;
			if(string.IsNullOrEmpty(showString) == false)
			   skillsInformation += "\n\n";
		}
		//清空预存
		if (withAttackLinkEffect)
			for (int i = 0; i < buffer.Count; i++)
				DestroyImmediate (buffer [i]);
		//return skillsInformation;
		return skillsNames;
	}

	//只是获取最基本的战斗被动技能
	public static string getBasicBEEffectInformation()
	{
		string skillsInformation = "\n";
		effectBasic[] effects = thePlayer.GetComponents<effectBasic> ();
		foreach (effectBasic ef in effects) 
		{
			//ef.Init ();
			if(ef.isBE())
				skillsInformation += ef.getInformation () + "\n"+ ef.getExtraInformation() +"\n";
		}
		return (BESkillColor  + skillsInformation + colorEnd);
	}

	//使用名字获得effectBasic效果信息
	//重用行很好的方法
	public static string getEffectInfromationWithName(string nameIn,GameObject theGameOBJ = null)
	{
		if (theGameOBJ == null)
			theGameOBJ = transStatic.gameObject;

		string information = "";

		if (string.IsNullOrEmpty (nameIn))
			return "没有效果";

		System.Type thetype = System.Type.GetType (nameIn);
		effectBasic  theEf =  (effectBasic)theGameOBJ.GetComponent(thetype);
		if (theEf == null)
		{
			theEf = theGameOBJ.AddComponent (thetype) as effectBasic;
			theEf.Init ();
		}
		information = theEf.getInformation ();
		Destroy (theEf);
		return information;
	}
	//使用名字获得effectBasic效果信息
	//重用行很好的方法
	public static string getEffectNameWithName(string nameIn , GameObject theGameOBJ= null)
	{
		if (theGameOBJ == null)
			theGameOBJ = transStatic.gameObject;

		string information = "";

		System.Type thetype = System.Type.GetType (nameIn);
		effectBasic  theEf =  (effectBasic)theGameOBJ.GetComponent(thetype);
		if (theEf == null)
		{
			theEf = theGameOBJ.AddComponent (thetype) as effectBasic;
			theEf.Init ();
		}
		information = theEf.theEffectName;
		Destroy (theEf);
		return information;

	}

	//最扯淡的弥补方法
	private  void flashRubbish()
	{
		effectBasic []efs = this.gameObject.GetComponents<effectBasic> ();
		for (int i = 0; i < efs.Length; i++)
			Destroy (efs[i]);
	}
	#endregion

	#region系统状态检查
	//UI界面选项是否已经开启
	//这是一个通用的全局检查用标记
	private  static  bool isSystemPanelOpened = true;
	public  static bool IsSystemPanelOpened
	{
		get {return isSystemPanelOpened; }
		set 
		{   isSystemPanelOpened = value ;
			Cursor.visible = value ;//附加额外的控制操作
		}
	}

	//当一些特殊的功能发动的时候，有一些功能应该暂停
	//为此需要一组标记，而这组标记就在这个方法里面统一处理
	public static bool isSystemUIUsing() 
	{
		return  systemValues.IsSystemPanelOpened && systemValues.isGamming; 
	}
	#endregion

	#region 颜色编码
	//所有的颜色标签都在这里设置
	public static string normalColor = "<color=#000000>";//什么都不加成的颜色 黑色
	public static string BESkillColor  = "<color=#FFFF8F>";//黄色
	public static string SkillColorForSelf = "<color=#FFF809>";//橙色
	public static string SkillColorForEnemy = "<color=#92FFFF>";//绿色
	public static string SkillExtraColor = "<color=#AFFF3A>";//标记淡绿色
	public static string SkillColorForLink = "<color=#EE55DD>";//粉色
	public static string playerNameColor = "<color=#00FF00>" ;//其实也是黄色
	public static string playerIntroductionColor = "<color=#FF2400>";//应该是绿色
	public  static string equipAddColor = "<color=#009100>";//装备相比增加的内容
	public static string equipMinusColor = "<color=#FF0000>";//装备相比减少的内容
	public static string colorEnd = "</color>";
	#endregion

	#region 副本选择
	//所有的副本都在这里统一设定吧
	//后期处理可以考虑用文本将副本介绍等等内容写到文件里面去

	//当前选择的场景名字的下标
	private static int indexNowForScene = 0;
	//真正的系统场景资源名字
	private static string  [] theSceneNameForSystem = {"theFight2" , "theFight3" , "theFight4"};
	//场景名字，同时也是场景的图片的资源名字
	private static string [] theSceneNames = {"map1" , "map2" , "map3"};
	//这个场景（副本）的说明
	//注意这个说明的地名和真正的说明之间有'\n'
	private static string[]   theSceneInformations = 
	{
		"空积城\n存在于著名游戏“荣耀”中的一个城池。传说一叶知秋和大漠孤烟两位高手初次碰面就是在这里,后来此地就成了游戏玩家的圣地。传说只要在此地击败隐藏魔兽就可以获得至尊秘宝，至今不知真假。",
		"迷踪峡谷\n以如迷宫一般的道路闻名，并且栖息着各种各样诡异而强大的怪物，即便是高手也必须小心谨慎而入。但也正因如此，此处有着各种百年难遇的奇宝，非常有益于修行。",
		"太极梅花阵\n一个很小的方寸之地上遍布梅花桩，这些梅花桩按照太极阵法缓缓移动，这是一个适合切磋的场地，很多豪杰都相约来这里一较高下。"
	};
	//前后获取sceneName的方法，分别是向前和向后选择
	public static string getNowScene()
	{
		return theSceneNames [indexNowForScene];
	}
	public static string getNextScene()
	{
		indexNowForScene++;
		if (indexNowForScene >= theSceneNames.Length)
			indexNowForScene = 0;
		return theSceneNames [indexNowForScene];
	}
	public static string getPreScene()
	{
		indexNowForScene--;
		if (indexNowForScene < 0)
			indexNowForScene = theSceneNames.Length - 1;
		return theSceneNames [indexNowForScene];
	}

	public static  void sceneSelectFlash()
	{
		indexNowForScene = 0;
	}
	//获得说明
	public static string getSceneInformaiton ()
	{
		return theSceneInformations [indexNowForScene];
	}
	//获得系统场景ID
	public static  string getScnenForSystem()
	{
		return theSceneNameForSystem[indexNowForScene];
	}
	#endregion

	#region 游戏模式选择
	//游戏模式数组，其实就是在游戏的开始的时候在GM上面再加上一个脚本
	//这种方法非常动态，但是同时在设计上就有了一些难度
	//搞不好还需要弄一个全球的基类来做这件事
	private static string [] gameModeAdders = {"playMode1" , "playMode2" , "playMode3" , "playMode1", "playerMode4" };
	private static string[] gameModeName = {"限时击杀" , "限时挑战","极限生存"  , "一击必杀" , "训练模式"};
	private static string[] gameModeInformation = {"在指定时间内击杀目标即可完成任务" , "60秒限时挑战最大击杀数量" , "争取生存240秒并获得最大击杀数"  , "在指定时间内击杀目标即可完成任务","没有限制，可以一直练习下去"};
	private static string[] gameModePicture = {"playMode1", "playMode2", "playMode3" , "playMode4" , "playMode5"};
	private static string[] gameModeAdderEffects = {"", "playModeEffect2","playModeEffect3","playModeEffect4","playModeEffect5"};
	private static int gameModeIndexNow = 0;
	public static List<string>  getGameModeWithMove(int adder = 0)
	{
		List<string> theStrings = new List<string> ();
		gameModeIndexNow += adder;
		if (gameModeIndexNow >= gameModeAdders.Length)
			gameModeIndexNow = 0;
		if (gameModeIndexNow < 0)
			gameModeIndexNow = gameModeAdders.Length - 1;

		theStrings.Add (gameModeName[gameModeIndexNow]);
		theStrings.Add (gameModeInformation[gameModeIndexNow]);
		theStrings.Add (gameModePicture[gameModeIndexNow]);
		theStrings.Add (gameModeAdderEffects[gameModeIndexNow]);

		return theStrings;
	}

	public static string getGameMode()
	{
		if (systemValues.theGameSystemMode == GameSystemMode.NET)
			return "";//对战模式不附加脚本

		return gameModeAdders[gameModeIndexNow];
	}

	public static string getGameModeExtraEffect()
	{
		if (systemValues.theGameSystemMode == GameSystemMode.NET)
			return "";//对战模式不附加脚本

		return gameModeAdderEffects[gameModeIndexNow];
	}
	#endregion

	#region 灵力计算
	//灵力相关的计算------------------------------------------------------------------------------------------------------
	//收集的灵力数量
	//灵力可以通过击杀目标来获取
	public static int soulCount = 7;
	//全球唯一计算灵力获取量的方法
	public static int soulGet(PlayerBasic thePlayerIn)
	{
		//一次最多10000灵力，不会是负数，做一下限制吧，要不然溢出了多尴尬
		return Mathf.Clamp( (int)(thePlayerIn.ActerHpMax / 100) , 0, 10000);
	}
	//升级这个装备需要的灵力数量
	public static  int getSoulCountForEquipLvUp(equipBasics theEquip, bool withLv1 = false)
	{
		int theSoulGet = 0;
		if(withLv1)
			theSoulGet +=8 * (theEquip.EquipLvNow-1);
		else
			theSoulGet +=8 * (theEquip.EquipLvNow);
		return theSoulGet;
	}

	public static int getSoulInForDestroyTheEquip(equipBasics theEquip)
	{
		int theSoulGet = (int)(theEquip.theSoulForThisEquip * 0.2f);
		theSoulGet += 8 * (theEquip.EquipLvNow-1);
		theSoulGet += theEquip.theEffectNames.Count * 17;
		theSoulGet += 15;
		return theSoulGet;
	}

	//灵力转换斗气
	public static bool canLingLiToSP()
	{
		return (systemValues.soulCount >= 2);
	}
	public static void LingLiToSP()
	{
		if (systemValues.soulCount >=2) 
		{
			systemValues.soulCount-=2;
			if (systemValues.thePlayer)
			{
				float spAdd = systemValues.thePlayer.ActerSpMax * 0.25f;
					
				systemValues.thePlayer.ActerSp += Mathf.Clamp (spAdd , 0f , 30f);
				systemValues.thePlayer.makeValueUpdate ();
			}
		}
	}

	//斗气转灵力
	public static bool canSpToLing()
	{
		return  (systemValues.thePlayer.ActerSp >= 40);
	}

	public static void SpToLing()
	{
		if (systemValues.thePlayer.ActerSp >= 40) 
		{
			systemValues.thePlayer.ActerSp -= 40;
			systemValues.thePlayer.makeValueUpdate ();
			systemValues.soulCount++;
		}
	}
	//完全透支转化介绍
	public static string HpSpToLinginformation(int extraLing , out bool canchange)
	{
		int spChange = (int )systemValues.thePlayer.ActerSp / 40;
		float spUse = 0f;
		if (spChange >= extraLing) 
		{
			canchange = true;
			spUse = extraLing * 40;
			return "灵力：" + spUse;
		}
		else
		{
			spUse = spChange * 40;
			int hpExtraCount = extraLing - spChange;
			float HpUse = hpExtraCount * 35;
			if (HpUse > systemValues.thePlayer.ActerHp) 
			{
				canchange = false;
				return "到达透支极限，不可转化";
			}
			canchange = true;
			return   "灵力：" + spUse +"\n生命： "+ HpUse;
		}
	}
	//真正的转化过程
	public static void HpSpToLing(int extraLing)
	{
		systemValues.soulCount += extraLing;

		int spChange = (int)systemValues.thePlayer.ActerSp / 40;

		//开始透支
		float spUse = extraLing * 40;
		if (spChange >= extraLing) 
		{
			systemValues.thePlayer.ActerSp -= spUse;
		}
		else
		{
			int hpExtraCount = extraLing - spChange;
			float HpUse =hpExtraCount * 35;
			systemValues.thePlayer.ActerHp -= HpUse;
		}
	}

	#endregion

	#region 消息框操作
	//消息框的操作---------------------------------------------------------------------------------------------------------
	private static GameObject theMessageProfab;
	private static GameObject theTitleMessageBoxProfab;
	private static GameObject theChoiceMessageBoxProfab;
	//一些引用保留，没有必要每一次都重建一个本就唯一的预设物
	private static titleMessageBox theTitleMessageBoxSave;
	private static theMessageBoxPanel theMessageBoxSave;
	private static choiceMessageBox theChoiceMessageBoxSave;
	public static bool isMessageBoxShowing = false;//消息框打开的时候有些功能需要被限制，否则就会有UI穿透的问题
	public static void  messageBoxShow(string showTitle , string  showText , bool autoSize = false)
	{
		if (!isGamming)
			return;
		
		if (!theMessageProfab)
			theMessageProfab = Resources.Load<GameObject> ("UI/MessageBox");

		if (!theMessageBoxSave) 
		{
			GameObject theMessageBox = GameObject.Instantiate<GameObject> (theMessageProfab);
			theMessageBox.transform.SetParent (transStatic);
			//theMessageBox.transform .localScale =  new Vector3 (2,2,2);
			//theMessageBox.transform.localPosition = Vector3.zero;
			theMessageBoxSave = theMessageBox.GetComponent <theMessageBoxPanel> ();
			theMessageBoxSave.isAutoResize = autoSize;
			theMessageBoxSave.setInformation (showTitle, showText);
		} 
		else
		{
			theMessageBoxSave.enabled = true;
			theMessageBoxSave.isAutoResize = autoSize;
			theMessageBoxSave.setInformation (showTitle, showText);
		}
	}
	public static void  messageBoxShow(string showTitle , string  showText , float timer , bool autoSize = false )
	{
		if (!isGamming)
			return;
		
		if (!theMessageProfab)
			theMessageProfab = Resources.Load<GameObject> ("UI/MessageBox");

		if (!theMessageBoxSave) 
		{
			GameObject theMessageBox = GameObject.Instantiate<GameObject> (theMessageProfab);
			theMessageBox.transform.SetParent (transStatic);
			//theMessageBox.transform .localScale =  new Vector3 (2,2,2);
			//theMessageBox.transform.localPosition = Vector3.zero;
			theMessageBoxSave = theMessageBox.GetComponent <theMessageBoxPanel> ();
			theMessageBoxSave .setInformation (showTitle, showText);
			theMessageBoxSave .isAutoResize = autoSize;
			theMessageBoxSave .setTimer (timer);
		}
		else
		{
			theMessageBoxSave.enabled = true;
			theMessageBoxSave.isAutoResize = autoSize;
			theMessageBoxSave.setInformation (showTitle, showText);
		}
	}

	public static void messageTitleBoxShow(string information)
	{
		//if (!isGamming)
		//	return;
		
		if (!theTitleMessageBoxProfab)
			theTitleMessageBoxProfab = Resources.Load<GameObject> ("UI/MessageBoxForTitle");

		if (!theTitleMessageBoxSave) 
		{
			GameObject theMessageBox = GameObject.Instantiate<GameObject> (theTitleMessageBoxProfab);
			theMessageBox.transform.SetParent (transStatic);
			//theMessageBox.transform .localScale =  new Vector3 (2,2,2);
			//theMessageBox.transform.localPosition = Vector3.zero;
			theTitleMessageBoxSave = theMessageBox.GetComponent <titleMessageBox> ();
			theTitleMessageBoxSave.setInformation (information);
		} 
		else
		{
			theTitleMessageBoxSave.enabled = true;
			theTitleMessageBoxSave.setInformation (information);
		}
	}

	public static void  choiceMessageBoxShow(string showTitle , string  showText , bool autoSize = false , MesageOperate theOperateMethod = null)
	{
		if (!isGamming)
			return;
		
		if (!theChoiceMessageBoxProfab)
			theChoiceMessageBoxProfab = Resources.Load<GameObject> ("UI/MessageBoxForChoice");

		if (!theChoiceMessageBoxSave) 
		{
			GameObject theMessageBox = GameObject.Instantiate<GameObject> (theChoiceMessageBoxProfab);
			//theMessageBox.transform .localScale =  new Vector3 (2,2,2);
			//theMessageBox.transform.localPosition = Vector3.zero;
			theMessageBox.transform.SetParent (transStatic);
			theChoiceMessageBoxSave = theMessageBox.GetComponent <choiceMessageBox> ();
			theChoiceMessageBoxSave.isAutoResize = autoSize;
			theChoiceMessageBoxSave.setInformation (showTitle, showText, theOperateMethod);
		} 
		else
		{
			//print ("make choice");
			theChoiceMessageBoxSave.enabled = true;
			theChoiceMessageBoxSave.isAutoResize = autoSize;
			theChoiceMessageBoxSave.setInformation (showTitle, showText, theOperateMethod);
		}
	}

	public static void messageBoxClose()
	{
		//print (" close message box");
		if (theTitleMessageBoxSave)
			theTitleMessageBoxSave.makeEnd ();
		if (theMessageBoxSave) 
		{
			theMessageBoxSave.makeEnd ();
			theMessageBoxSave.makeEndTrue ();
		}
		if (theChoiceMessageBoxSave) 
		{
			//两段的end操作
			theChoiceMessageBoxSave.makeEnd ();
			theChoiceMessageBoxSave.makeEndTrue ();
		}
	}
		
	//消息框的操作OVER---------------------------------------------------------------------------------------------------------
	#endregion

	#region 3DText显示对象池
	//3D bloodText和其他显示3D Text的对象池============================================================================
	private static List<GameObject> theShowingTexts = new List<GameObject> ();
	private static GameObject theShowTextProfab;//显示文本预设物需要加载一次
	private static Transform  transStatic ;
	public static void addIntoTheTextPool(GameObject theTextIn)
	{
		//保留一个引用
		if(!theShowingTexts.Contains(theTextIn))
		    theShowingTexts.Add (theTextIn);
		theTextIn.SetActive (false);
		//print (" text pool count = "+theShowingTexts.Count);
	}

	public static GameObject getTextFromTextPool()
	{
//		//这是对象池的另一种实习方案，因为有循环查找，暂时先不用
//		GameObject findOne = null;
//		for (int i = 0; i < theShowingTexts.Count; i++) 
//		{
//			if (!theShowingTexts [i].activeInHierarchy) 
//			{
//				findOne = theShowingTexts [i];
//				break;
//			}
//		}
//		if (!findOne) 
//		{
//			if(!theShowTextProfab)//实际上加载一次就可以了
//			    theShowTextProfab = Resources.Load <GameObject>("effects/bloodText");
//			findOne = GameObject.Instantiate (theShowTextProfab);
//			theShowingTexts.Add (findOne);
//		}
//		findOne.SetActive (true);
//		return findOne;

		if (theShowingTexts.Count > 0) 
		{
			//print ("move pre -> have "+ theShowingTexts.Count  );
			GameObject theTextOut = theShowingTexts [0];
			theTextOut.SetActive (true);
			theShowingTexts.RemoveAt(0);
			//print ("move out -> have "+ theShowingTexts.Count );
			//print (theTextOut.name);
			return theTextOut;
		}
		else
		{
			//print ("new");
			if(!theShowTextProfab)//实际上加载一次就可以了
			    theShowTextProfab = Resources.Load <GameObject>("effects/bloodText");
			GameObject  theShowText = GameObject.Instantiate (theShowTextProfab);
			if(transStatic)
			  theShowText.transform.SetParent (transStatic);
			return  theShowText;
		}
	}

	//3D bloodText和其他显示3D Text的对象池============================================================================
	#endregion

	#region 图片池与图像加载

	static List<Sprite> theSavedSprite = new List<Sprite> ();
	//加载图像全局工具方法
	public static  Sprite makeLoadSprite(string textureName)
	{

		Sprite theSprite = theSavedSprite.Find (x => x.name == textureName);
	    if (theSprite != null)
			return theSprite;

		Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
		theSprite = Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
		theSprite.name = textureName;
		theSavedSprite.Add (theSprite);
		//print (textureName);
		return theSprite;
	}
	#endregion

	#region 灵力效果池(其实就是保存一下引用)
	public static List<popSoulMove> thePopSoul = new List<popSoulMove> ();
	private static GameObject theSoulProfab = null;

	public static popSoulMove getPopSoul()
	{
		if (!theSoulProfab)
			theSoulProfab = Resources.Load<GameObject> ("effects/theSoul");

		if (thePopSoul.Count <= 0)
		{
			GameObject theSoul = GameObject.Instantiate<GameObject> (theSoulProfab);
			theSoul.transform.SetParent (transStatic);
			return theSoul.GetComponent<popSoulMove> ();
		}
		else 
		{
			popSoulMove A = thePopSoul [0];
			A.gameObject.SetActive (true);
			thePopSoul.RemoveAt(0);
			return A;
		}

	}

	public static void savePopSoul(popSoulMove A)
	{
		thePopSoul.Add (A);
		A.gameObject.SetActive (false);
	}
	#endregion

	#region通用静态方法


	private static float change(float angle)//角度转弧度的方法
	{
		return( angle * Mathf.PI / 180);
	}

	public static List<GameObject> theEMY = new List<GameObject> ();
	//个人认为比较稳健的方法
	//传入的是攻击范围和攻击扇形角度的一半
	//选择目标的方法，这年头普攻都是AOE
	public static List<GameObject> searchAIMs(float angle , float distance ,Transform theSearcherTransform)//不使用射线而是使用向量计算方法
	{
		//这个方法的正方向使用的是X轴正方向
		//具体使用的时候非常需要注意正方向的朝向
		theEMY.Clear();
		//以自己为中心进行相交球体探测
		//实际上身边一定圆周范围内的所有具有碰撞体的单位都会被被这一步探测到
		//接下来需要的就是对坐标进行审查
		Collider [] emys = Physics.OverlapSphere (theSearcherTransform.position, distance);
		//使用cos值进行比照，因为在0-180角度范围内，cos是不断下降的
		//具体思路就是，判断探测到的物体的cos值如果这个cos值大于标准值，就认为这个单位的角度在侦查范围角度内。
		float angleCosValue = Mathf.Cos (change(angle));//莫认真侧角度的cos值作为计算标准
		//print ("angleCosValue-"+angleCosValue);
		for (int i = 0; i < emys.Length; i++)//开始对相交球体探测物体进行排查
		{ 
			//用alive标记减少在这里参与计算的单位数量
			if (emys [i].gameObject != theSearcherTransform.gameObject) //相交球最大的问题就是如果自身有碰撞体，自己也会被侦测到
			{
				//print ("name-"+ emys [i].name);
				Vector3 thisToEmy = emys [i].transform.position - theSearcherTransform.transform.position;//目标坐标减去自身坐标
				Vector2 theVectorToSearch = (new Vector2 (thisToEmy.x, thisToEmy.z)).normalized;//转成2D坐标，高度信息在这个例子中被无视
				//同时进行单位化，简化计算向量cos值的时候的计算
				Vector2 theVectorForward = (new Vector2 (theSearcherTransform.transform.forward.x, theSearcherTransform.transform.forward.z)).normalized;//转成2D坐标，高度信息在这个例子中被无视
				//同时进行单位化，简化计算向量cos值的时候的计算
				float cosValue = (theVectorForward.x * theVectorToSearch.x + theVectorForward.y * theVectorToSearch.y);//因为已经单位化，就没必要再进行求模计算了
				//print ("cosValue-" + cosValue);
				/*
				    先求出两个向量的模
					再求出两个向量的向量积
					|a|=√[x1^2+y1^2]
					|b|=√[x2^2+y2^2]
					a*b=(x1,y1)(x2,y2)=x1x2+y1y2
					cos=a*b/[|a|*|b|]
					=(x1x2+y1y2)/[√[x1^2+y1^2]*√[x2^2+y2^2]]
				*/
				if (cosValue >= angleCosValue)//如果cos值大于基准值，认为这个就是应该被探测的目标
				{
					if (theEMY .Contains (emys [i].gameObject) == false) //不重复地放到已找到的列表里面
					{
						theEMY.Add (emys [i].gameObject);
						//print ("SeachFind "+emys [i].GetComponent<Collider> ().gameObject.name);//找到目标
					}
				}
			}
		}
		return theEMY;
	}



	//让某一个物体出现到指定位置
	public static  void setPositionForGameOject(string playerName , string thingName , float x , float y , float z)
	{
		if (systemValues.theGameSystemMode == GameSystemMode.NET) 
		{
			photonView.RPC ("setPositionForGameOjectRPC", PhotonTargets.All,playerName ,  thingName, x , y , z);
		} 
		else 
		{
			GameObject thePlayer = GameObject.Find (playerName);
			if (!thePlayer)
				return;

			equipPackage thePackage = thePlayer.GetComponent <equipPackage> ();
			if (!thePackage)
				return;

			equipBasics theThing = thePackage.allEquipsForSave.Find (s => s.equipName == thingName);
			if (!theThing)
				return;

			thePackage.allEquipsForSave.Remove (theThing);
			GameObject theOBJ = theThing.gameObject;
			theOBJ.SetActive (true);
			theOBJ.transform.position = new Vector3 (x,y,z);
		}

	}

	[PunRPC]
	public   void  setPositionForGameOjectRPC(string playerName , string thingName , float x , float y , float z)
	{
		GameObject thePlayer = GameObject.Find (playerName);
		if (!thePlayer)
			return;

		equipPackage thePackage = thePlayer.GetComponent <equipPackage> ();
		if (!thePackage)
			return;

		equipBasics theThing = thePackage.allEquipsForSave.Find (s => s.equipName == thingName);
		if (!theThing)
			return;

		thePackage.allEquipsForSave.Remove (theThing);
		GameObject theOBJ = theThing.gameObject;
		theOBJ.SetActive (true);
		theOBJ.transform.position = new Vector3 (x,y,z);
	}

	//检查一个字符串是不是空的
	public static bool isNullOrEmpty(string value)
	{
		return (value + "").Length == 0;
	}
	public static void quickSort(List<int> theP, int low, int high)
	{
		if (low >= high)
			return;

		int first = low;
		int last = high;
		int keyValue = theP[low];
		while (low < high)
		{
			while (low < high && theP[high] >= keyValue)
				high--;
			theP[low] = theP[high];
			while (low < high && theP[low] <= keyValue)
				low++;
			theP[high] = theP[low];
		}
		theP[low] = keyValue;
		quickSort(theP, first, low - 1);
		quickSort(theP, low + 1, last);
	}

	//根据数组长度修改content的height
	public  static void makeFather(int count , Transform theViewFather)
	{
		//print ("make height: "+ theViewFather.name);
		GridLayoutGroup theGroup = theViewFather.GetComponent<GridLayoutGroup> ();
		RectTransform theFatherRect = theViewFather.GetComponent<RectTransform> ();

		int countPerLine = (int)( ( theFatherRect.rect.width - theFatherRect.rect.xMin ) / (theGroup.cellSize.x + theGroup.spacing.x));
		int lines = countPerLine != 0 ?  count / countPerLine + 1 : 1;
		//print ("CL = "+ countPerLine);
		//print ("lines = "+ lines);
		//print ("heightPerLine = "+theGroup.cellSize.y);
		float height = 30 + (int)(theGroup.cellSize.y+ theGroup.spacing.y)  * lines ;
		//print ("height = "+ height);

		Rect newRect = new Rect (0,0,theFatherRect.rect.width , height);
		theFatherRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical,  height);
		//额外增加一点点数值以备不测
	}


	//将横版字符串修改为竖版
	//startFromLeft右边对齐还是左边对齐
	//theCountPerColumn 一列有多少个字
	public static  string rowStringToColumn(string information , int theCountPerColumn = 7 , bool startFromLeft = true)
	{
		string returnString = "";
		theCountPerColumn = theCountPerColumn <= 0 ? 1 : theCountPerColumn;
		int rount = information.Length / theCountPerColumn;
		int thisRount = 0;

		for (int k = 0; k < theCountPerColumn; k++) 
		{
			string clip = "";
			for (int i = 0; i < information.Length; i++) 
			{
				if (i % theCountPerColumn == k)
				{
					clip += information [i];
				}
				//print ("clip = "+ clip);
			}
			while(clip.Length < rount+1)
				clip += "    ";
			returnString += startFromLeft? clip : ArrayReverse(clip);
			returnString += "\n";
		}

		//Debug.Log (returnString);
		return returnString;
	}
	//字符串倒转
	public static string ArrayReverse(string text)
	{
		char[] charArray = text.ToCharArray();
		System.Array.Reverse(charArray);

		return new string(charArray);
	}
	#endregion

	#region系统清理
	//反复跳转场景应该做一些清理工作
	public static void makeSystemClean()
	{
		if(thePlayer)
		    thePlayer.Effects.Clear ();
		playModeNow = null;
		gameModeIndexNow = 0;
		sceneSelectFlash ();
		soulCount = 7;//回到初始，给7个灵力
		Cursor.visible = true;
		Time.timeScale = 1f;
		isGamming = true;
		theShowingTexts.Clear ();
		theSavedSprite.Clear ();
		thePopSoul.Clear ();
		messageBoxClose ();
		theAreaRenders.Clear ();
		wulingButton.isStarted = false;
		isGamming = true;

	}
	#endregion

	#region 死亡处理，也算是游戏的收尾工作
	//死亡的面板
	public static GameObject theHoverPanel;
	public static void  makeGameEnd(string theInformation)
	{
		if (theHoverPanel)
		{
			isGamming = false;

			theHoverPanel.SetActive (true);
			if (!isInStory) 
			{ 
				//free模式之下直接返回开始界面就可以了
				theHoverPanel.GetComponent <theHoverPanel> ().makeTheEndMode(theInformation, checkisOver ());
			} 
			else 
			{
				//人物传记模式之下需要判断输赢
				//输了就重新来过（自动）
				//赢了就先返回开始界面
				bool isWin = checkisOver ();
				if (!isWin)
					messageTitleBoxShow ("胜败乃兵家常事，大侠请重新来过");
				string thisSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
				theHoverPanel.GetComponent <theHoverPanel> ().makeTheEndMode(theInformation, isWin ,thisSceneName , getNextStoryScene() );
			}
		}
			
	}
    //检查任务是否完成了
	private static bool checkisOver()
	{
		if (systemValues.thePlayer && systemValues.playModeNow)
		{
			if (systemValues.thePlayer.isAlive = false)
				return false;
			return systemValues.playModeNow.isOvered;
		}
		else 
		{
			return false;
		}
	}
	#endregion



	#region 范围信息储存
	//死亡的面板
	public static List< GameObject > theAreaRenders = new List<GameObject> ();

	private static void changeShowArea()
	{
		if (Input.GetKeyDown (KeyCode.Tab))
			foreach (GameObject A in theAreaRenders)
				if (A)
					A.gameObject.SetActive (!A.activeInHierarchy);
	}
	#endregion

	#region 游戏人物传记

	//是否在人物传记中的标记
	//这个标记会影响每一关结束之后的走向
	//这个的刷新在systemValuesInStartScene中
	//并且只是在开始界面进行的
	public static bool isInStory = false;

	//当前这个人物的剧本场景
	public static int indexForStory = 0;

	private static string[] storySummaryName = 
	{
		"knife_summary",
		"guojing_summary" ,
		"mulan_summary" ,
		"ziying_summary"
	};
	private static string[][] storiesScene = new string[][]{
		new string []{ "storyKnife" , "theFight2" ,  "storyKnife" , "theFight2"},
		new string []{ "storyGuojing" , "theFight2" ,"storyGuojing" , "theFight2"  },
		new string []{ "storyMulan" , "theFight2" ,"storyMulan" , "theFight2"},
		new string []{ "storyZiying" , "theFight2" , "storyZiying" , "theFight2" }
	};

	public static string getNextStoryScene()
	{
		indexForStory++;
		if(indexForStory <storiesScene[indexNow].Length )
		  return storiesScene [indexNow][indexForStory];

		//人物传记模式在全完事的时候才会彻底完事
		//systemValues.isGamming = false;
		return  "allStartScene";
	}
	//获得系统场景ID
	public static  string getScnenForStory()
	{
		return storiesScene [indexNow][ indexForStory];
	}
	public static void flashStoryErrorMove()
	{
		indexForStory -- ;
	}

	public static string  getStorySimpleInformation(int index)
	{
		return Resources.Load<TextAsset>("Stories/"+ storySummaryName [index]).text; 
	}

	#endregion

	#region 统计信息
	//连击---------------------------------------
	public static int hitCount = 0;
	private static float hitTimer = 1f;
	private static float hitTimerMax = 1f;
	public  static void updateHit(float TimerUse)
	{
		hitTimer -= TimerUse * 0.6f;;
		if (hitTimer < 0) 
		{
			hitTimer = hitTimerMax;
			hitCount = 0;
		}
	}
	public static void addHitCount( int count = 1)
	{
		hitCount += count;
		hitTimer = hitTimerMax;
	}
	//-----------------------------------------------
	#endregion


	//GM的初始化==============================================================================
	void Start()
	{
        //print (theGameSystemMode);
		Application.targetFrameRate = 50;//默认FPS是50，没有必要太高不是.....
		transStatic = this.transform;
		theSpecialTransformStatic = theSpecialTransform;
		if (theGameSystemMode == GameSystemMode.NET)
		{
			photonView = PhotonView.Get (this);
			return;
		}
		Invoke("makeGameMode" , 4f);

		theHoverPanel = GameObject.Find("/CanvasForFight/theHoverPanel");
	
	}

	void makeGameMode()
	{
		string modePlayerString = getGameMode ();
		//print ( "The GameMode IS "+ modePlayerString);
		try
		{
			if (!string.IsNullOrEmpty (modePlayerString))
				this.gameObject.AddComponent (System.Type.GetType(modePlayerString));
		}
		catch(System.Exception C){print (C.Message);}
		playModeNow = this.GetComponent <playModeBasic> ();
		if(playModeNow ) playModeNow .OnGameStart ();

		string effectForAdd = getGameModeExtraEffect ();
		//print (effectForAdd +" is for add" );
		if(!string.IsNullOrEmpty(effectForAdd))
			systemValues.thePlayer.gameObject.AddComponent (System.Type.GetType( effectForAdd));
	}

	void Update()
	{
		changeShowArea ();
	}
	void OnDestroy()
	{
		makeSystemClean ();
	}

}
