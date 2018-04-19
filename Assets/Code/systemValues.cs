using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class systemValues : MonoBehaviour {
	//程序面板单元
	//也可以理解为这是每一个客户端的计分板

	#region系统记录静态信息
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
	//0 单机模式
	//1 网络模式
	public static int modeIndex = 0;
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
	private static int indexNow = 3 ;
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
	public static string []  attackLinkChinese= {"上", "下", "左" , "右" , "击"};
	public static string []  attackLinkEnglish= {"W", "S", "A" , "D" , "J"};
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
				theInformation.theEffectForSelfName=  efs[i].getEffectName();
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
				theInformation.attackLinkInformationText = ak.getInformationSimple (false);//获取简略的信息就足够了

				//if (string.IsNullOrEmpty (ak.conNameToEMY) == false) 
				if (!isNullOrEmpty(ak.conNameToEMY)) 
				{
					//初始化一下效果
					System.Type theType = System.Type.GetType (ak.conNameToEMY);
					thePlayer.gameObject.AddComponent (theType);
					effectBasic theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
					buffer.Add (theEffect);

					if(theEffect.isShowing())
					{
						theEffect.Init ();
						theInformation.theEffectForEMYName = theEffect.getEffectName();
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
					thePlayer.gameObject.AddComponent (theType);
					effectBasic theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
					buffer.Add (theEffect);

					if(theEffect.isShowing())
					{
						theEffect.Init ();
						theInformation.theEffectForSelfName = theEffect.getEffectName();
						showString = theEffect.getInformation (false);
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
					thePlayer.gameObject.AddComponent (theType);
					effectBasic theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
					buffer.Add (theEffect);
				}
				if (!isNullOrEmpty(ak.conNameToSELF )) 
				{
					//初始化一下效果
					System.Type theType = System.Type.GetType (ak.conNameToSELF);
					thePlayer.gameObject.AddComponent (theType);
					effectBasic theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
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
				skillsInformation += ef.getInformation () + "\n"+ ef.getExtraInformation();
		}
		return (BESkillColor  + skillsInformation + colorEnd);
	}

	//使用名字获得effectBasic效果信息
	//重用行很好的方法
	public static string getEffectInfromationWithName(string nameIn,GameObject theGameOBJ)
	{
		string information = "";
		try
		{
			System.Type thetype = System.Type.GetType (nameIn);
			theGameOBJ.AddComponent (thetype);
			effectBasic theEf =  (effectBasic)theGameOBJ.GetComponent(thetype);
			theEf.Init ();
			information = theEf.getInformation ();
			Destroy (theEf);
			return information;
		}
		catch
		{
			return "";
		}
	}
	//使用名字获得effectBasic效果信息
	//重用行很好的方法
	public static string getEffectNameWithName(string nameIn , GameObject theGameOBJ)
	{
		string information = "";
		try
		{
			System.Type thetype = System.Type.GetType (nameIn);
			theGameOBJ.AddComponent (thetype);
			effectBasic theEf =  (effectBasic)theGameOBJ.GetComponent(thetype);
			theEf.Init ();
			information = theEf.theEffectName;
			Destroy (theEf);
			return information;
		}
		catch
		{
			return "";
		}
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
		return  systemValues.IsSystemPanelOpened; 
	}
	#endregion

	#region 颜色编码
	//所有的颜色标签都在这里设置
	public static string normalColor = "<color=#000000>";//什么都不加成的颜色 黑色
	public static string BESkillColor  = "<color=#FFFF8F>";//黄色
	public static string SkillColorForSelf = "<color=#FFF809>";//橙色
	public static string SkillColorForEnemy = "<color=#92FFFF>";//绿色
	public static string SkillExtraColor = "<color=#AFFF3A>";//标记淡绿色
	public static string playerNameColor = "<color=#00FF00>" ;//其实也是黄色
	public static string playerIntroductionColor = "<color=#FF2400>";//应该是绿色
	public static string colorEnd = "</color>";
	#endregion

	#region 灵力计算
	//灵力相关的计算------------------------------------------------------------------------------------------------------
	//收集的灵力数量
	//灵力可以通过击杀目标来获取
	public static int soulCount = 3;
	//全球唯一计算灵力获取量的方法
	public static int soulGet(PlayerBasic thePlayerIn)
	{
		return (int)(thePlayerIn.ActerHpMax / 100);
	}
	//升级这个装备需要的灵力数量
	public static  int getSoulCountForEquipLvUp(equipBasics theEquip, bool withLv1 = false)
	{
		if(withLv1)
			return 8 * (theEquip.EquipLvNow-1);

		return 8 * theEquip.EquipLvNow;
	}
	#endregion

	#region 消息框操作
	//消息框的操作---------------------------------------------------------------------------------------------------------
	public static void  messageBoxShow(string showTitle , string  showText , bool autoSize = false)
	{
		GameObject theMessageBox = GameObject.Instantiate<GameObject>( Resources.Load<GameObject> ("UI/MessageBox"));
		theMessageBox.transform .localScale =  new Vector3 (2,2,2);
		theMessageBox.transform.localPosition = Vector3.zero;
		theMessageBoxPanel theMesage = theMessageBox.GetComponent <theMessageBoxPanel> ();
		theMesage.isAutoResize = autoSize;
		theMesage.setInformation (showTitle, showText);
	}
	public static void  messageBoxShow(string showTitle , string  showText , float timer , bool autoSize = false )
	{
		GameObject theMessageBox = GameObject.Instantiate<GameObject>( Resources.Load<GameObject> ("UI/MessageBox"));
		theMessageBox.transform .localScale =  new Vector3 (2,2,2);
		theMessageBox.transform.localPosition = Vector3.zero;
		theMessageBoxPanel theMesage = theMessageBox.GetComponent <theMessageBoxPanel> ();
		theMesage.setInformation (showTitle, showText);
		theMesage.isAutoResize = autoSize;
		theMesage.setTimer (timer);
	}

	public static void messageTitleBoxShow(string information)
	{
		GameObject theMessageBox = GameObject.Instantiate<GameObject>( Resources.Load<GameObject> ("UI/MessageBoxForTitle"));
		theMessageBox.transform .localScale =  new Vector3 (2,2,2);
		theMessageBox.transform.localPosition = Vector3.zero;
		titleMessageBox theMesage = theMessageBox.GetComponent <titleMessageBox> ();
		theMesage.setInformation (information);
	}

	public static void  choiceMessageBoxShow(string showTitle , string  showText , bool autoSize = false , MesageOperate theOperateMethod = null)
	{
		GameObject theMessageBox = GameObject.Instantiate<GameObject>( Resources.Load<GameObject> ("UI/MessageBoxForChoice"));
		theMessageBox.transform .localScale =  new Vector3 (2,2,2);
		theMessageBox.transform.localPosition = Vector3.zero;
		choiceMessageBox theMesage = theMessageBox.GetComponent <choiceMessageBox> ();
		theMesage.isAutoResize = autoSize;
		theMesage.setInformation (showTitle, showText,theOperateMethod );
	}

	public static void messageBoxClose()
	{
		if (theMessageBoxPanel.theMessageSave)
			Destroy (theMessageBoxPanel.theMessageSave.gameObject);
		if (titleMessageBox.theMessageSave)
			Destroy (titleMessageBox.theMessageSave.gameObject);
		if (choiceMessageBox.theMessageSave)
			Destroy (choiceMessageBox.theMessageSave.gameObject);
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

	#region通用静态方法
	//加载图像全局工具方法
	public static  Sprite makeLoadSprite(string textureName)
	{
		//textureName = "people/noOne";
		Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
		return Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
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
		GridLayoutGroup theGroup = theViewFather.GetComponent<GridLayoutGroup> ();
		RectTransform theFatherRect = theViewFather.GetComponent<RectTransform> ();

		int countPerLine = (int)( ( theFatherRect.rect.width - theFatherRect.rect.xMin ) / theGroup.cellSize.x);
		int lines = countPerLine != 0 ?  count / countPerLine + 1 : 1;
		//print ("CL = "+ countPerLine);
		//print ("lines = "+ lines);
		//print ("heightPerLine = "+theGroup.cellSize.y);
		float height = 30 + (int)(theGroup.cellSize.y)  * lines ;
		//print ("height = "+ height);

		Rect newRect = new Rect (0,0,theFatherRect.rect.width , height);
		theFatherRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical,  height);
		//额外增加一点点数值以备不测
	}

	#endregion

	//GM的初始化==============================================================================
	void Start()
	{
		transStatic = this.transform;
	}

}
