using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipRemakePanel : MonoBehaviour {

	//这个脚本用来管理装备的装备，卸下，熔铸等等的子界面
	//这个脚本用来记录装备的操作
	//装备，卸下，合成，熔锻......
	public Button theEquipButton;//装备/卸下的按钮
	public Transform theViewFather;
	public Text equipSkillAddInformation;
	public GameObject theShowingButtonProfab;

    //静态信息保存
	private static equipBasics theEquip;
	public static equipBasics equipSkillAdderNow  = null ;
	private static Button theEquipButtonStatic;
	private static Text theEquipButtonText;
	private panelSoundController theSoundController;
	private static Text equipSkillAddInformationStatic;
	private static Transform equipSkillAdderViewFatherStatic;
	private static GameObject theShowingButtonProfabStatic;

	private bool isStarted = false;
	void Start()
	{
		theEquipButtonStatic = theEquipButton  ;
		theEquipButtonText = theEquipButton.GetComponentInChildren<Text> ();
		theSoundController = this.GetComponentInChildren<panelSoundController> ();
		equipSkillAddInformationStatic = equipSkillAddInformation;
		equipSkillAdderViewFatherStatic = theViewFather;
		theShowingButtonProfabStatic = theShowingButtonProfab;
		isStarted = true;
		flashThePanel ();
	}

	void OnEnable()
	{
		if (isStarted == false)
			return;
		//界面刷新

		equipSelectTypeButton.flashThePanel ();
		thePackagePanelShow.makeFlash ();
		equipInformationPanel.makeFlash ();
		ShowMake ();
		flashThePanel ();
	}

	//展示这个注灵的效果
	public static void showEquipSkillAdderGet(equipBasics theEquip)
	{
		equipSkillAdderNow = theEquip;
		equipSkillAddInformationStatic.text = systemValues.getEffectInfromationWithName (theEquip.equipName);
		equipSkillAddInformationStatic.text += "\n【潜力要求：" + theEquip.skillValueCountAll +"】";

	}

	public static void getEquipForOperate(equipBasics theEquipIn)
	{
		theEquip = theEquipIn;
		ShowMake ();
	}

    //开始界面操作
	public static void ShowMake()
	{
		if (!theEquip)
			theEquipButtonText.text = "无效";
	
		else if(theEquip.isUsing)
			theEquipButtonText.text = "卸下";

		else 
			theEquipButtonText.text = "装备";
	}

	public static  void flashThePanel()
	{
		//因为所有的显示都是针对本机角色的
		if (!systemValues.thePlayer )
			return;
		//前期清理工作
		equipShowingButton []  es = equipSkillAdderViewFatherStatic.GetComponentsInChildren<equipShowingButton>();
		List<equipBasics> eqs = systemValues.thePlayer.GetComponent <equipPackage> ().allEquipsForSave.FindAll (x => x!=null && x.theEquipType == equiptype.equipSkill);
		//直接用lambda表达式查询吧还是
		eqs.Sort((a,b) => {return (a.equipName[0] - b.equipName[0] + a.equipName.Length - b.equipName.Length);} );//linq排序
		systemValues. makeFather (eqs.Count , equipSkillAdderViewFatherStatic);

		if (eqs.Count > es.Length) 
		{
			int i = 0;
			for (; i < es.Length; i++) 
			{
				es [i].theEquip = eqs [i];
				//es [i].GetComponentInChildren<Text>().text = "";
				//print ("equipPicture/" + equiptype.equipSkill + "/" + eqs [i].equipPictureName);
				es[i].GetComponent <Image> ().sprite = systemValues.makeLoadSprite ("equipPicture/" + equiptype.equipSkill + "/" + eqs [i].equipPictureName);
			}
			for (; i < eqs.Count; i++) 
			{
				GameObject theButton = GameObject.Instantiate<GameObject> (theShowingButtonProfabStatic);
				theButton.transform.SetParent (equipSkillAdderViewFatherStatic.transform);
				theButton.GetComponent <equipShowingButton> ().theEquip = eqs [i];
				//theButton.GetComponentInChildren<Text>().text = "";
				//print ("equipPicture/" + equiptype.equipSkill + "/" + eqs [i].equipPictureName);
				theButton.GetComponent <Image> ().sprite = systemValues.makeLoadSprite ("equipPicture/" + equiptype.equipSkill + "/" + eqs [i].equipPictureName);
				//因为有grid控件，所以这些都没有必要使用了
			}
			//print ("重建次数："+( i- es.Length));
		}
		else
		{
			//print ("deletes343434");
			int i = 0;
			for (; i < eqs.Count; i++) 
			{
				es [i].theEquip = eqs [i];
				//es [i].GetComponentInChildren<Text>().text = "";
				es[i].GetComponent <Image> ().sprite = systemValues.makeLoadSprite ("equipPicture/" + equiptype.equipSkill + "/" + eqs [i].equipPictureName);
			}
			for (; i>=0 && i < es.Length; i++) 
			{
				Destroy (es [i].gameObject);
			}
		}
		equipSkillAddInformationStatic.text = "暂无注灵效果信息";
	}

	//装备或者替换这个装备
	public void wearThEquip()
	{
		if (theEquip)
		{
			theSoundController.makeSoundShow (2);
			systemValues.messageTitleBoxShow ("装备【" + theEquip.equipName + "】");
			thePackagePanelShow.wearEquip (theEquip);
			equipSelectTypeButton.flashThePanel ();
			ShowMake ();
		}
		else 
		{
			systemValues.messageTitleBoxShow ("尚未选定装备");
		}
	}


	//UI调用方法和回调方法=================================================================================================
	//将装备扔出出来给别人用
	//这个在网络游戏中应该是比较需要的
	public void dropTheEquip()
	{
		if (!theEquip && !equipSkillAdderNow) 
		{
			systemValues.messageTitleBoxShow ("尚未选定装备或者注灵材料");
			return;
		}

		equipBasics theEquipUse = theEquip != null ? theEquip : equipSkillAdderNow;
		string nameUse = theEquip != null ? theEquip.equipName : equipSkillAdderNow.equipExtraName;

		if (theEquipUse.isUsing) 
			thePackagePanelShow.wearEquip (theEquip);

		Vector3 position = systemValues.thePlayer.transform.position + systemValues.thePlayer.transform.forward*2.5f;
		systemValues.setPositionForGameOject(systemValues.thePlayer.gameObject.name, theEquipUse.equipName , position.x , position.y , position.z);

		//界面刷新
		equipSelectTypeButton.flashThePanel ();
		thePackagePanelShow.makeFlash ();
		equipInformationPanel.makeFlash ();
		ShowMake ();
		flashThePanel ();
	}


	public void makeTheEquipGetSkill()
	{
		if (!theEquip || !equipSkillAdderNow) 
		{
			systemValues.messageTitleBoxShow ("尚未选定装备或者注灵材料");
			return;
		}

		if (theEquip.theEffectNames.Contains (equipSkillAdderNow.equipName)) 
		{
			systemValues.messageTitleBoxShow ("此装备已经注有同类型的灵力，无法叠加");
			return;
		}
		if ((theEquip.skillValueCountAll - theEquip.skillValueCount) < equipSkillAdderNow.skillValueCountAll) 
		{
			systemValues.messageTitleBoxShow ("此装备已经没有足够的天赋继续开发了");
			return;
		}
			
		string theEffectName = equipSkillAdderNow.equipExtraName;// systemValues.getEffectNameWithName (equipSkillAdderNow.equipName);
		string theEffectInformation = equipSkillAdderNow .theEquipStroy;//systemValues.getEffectInfromationWithName (equipSkillAdderNow.equipName);
		systemValues.choiceMessageBoxShow ("是否注灵？", "注灵将会使【"+theEquip.equipName+"】获得“"+theEffectName+"”效果，需要永久消耗《"+theEffectName+"》图谱\n\n效果："+theEffectInformation +"\n\n是否注灵？", true, new MesageOperate (getTheSkill));
	}
		
	void getTheSkill()
	{
		theSoundController.makeSoundShow (3);
		theEquip.makeEquipAddSkill (equipSkillAdderNow);
		DestroyImmediate(equipSkillAdderNow.gameObject);
		equipInformationPanel.changeEquipToIntroduct (theEquip);
		thePackagePanelShow.setNewEquip (theEquip);
		equipRemakePanel.getEquipForOperate (theEquip);
		flashThePanel();
		equipShowingButton.flashPicture ();
	}




	//熔锻这个装备
	//这个装备被消熔之后，将会转化为灵力
	public void soulTheEquip()
	{
		if (!theEquip && !equipSkillAdderNow) 
		{
			systemValues.messageTitleBoxShow ("尚未选定目标");
			return;
		}
		equipBasics theEq;
		if (systemValues.theEquipNow!= null)
			theEq = systemValues.theEquipNow;
	    else	
		    theEq =  theEquip != null ? theEquip : equipSkillAdderNow;

		if (theEq == null)
			return;

		string nameUse = theEq.theEquipType != equiptype.equipSkill ? theEq.equipName : theEq.equipExtraName;
		int soulGet = 20 + systemValues.getSoulInForDestroyTheEquip(theEq);
		systemValues.choiceMessageBoxShow ("熔锻？", "熔锻【"+nameUse+"】将会获得"+ soulGet +"灵力，但是这个物品会永远消失。\n\n是否熔锻？", true, new MesageOperate (makeTheEquipToSoul));

	}

	//委托方法
	void makeTheEquipToSoul()
	{
		theSoundController.makeSoundShow (1);

		equipBasics theEquipUse;
		if (systemValues.theEquipNow!= null)
			theEquipUse= systemValues.theEquipNow;
		else	
			theEquipUse =  theEquip != null ? theEquip : equipSkillAdderNow;

		if (theEquipUse == null)
			return;
		
		//equipBasics theEquipUse = theEquip != null ? theEquip : equipSkillAdderNow;
		string nameUse =  theEquipUse.theEquipType != equiptype.equipSkill ? theEquipUse.equipName : theEquipUse.equipExtraName;

		if (theEquipUse.isUsing)
			theEquipUse.DropThisThing (systemValues.thePlayer);

		int soulGet = 20 + systemValues.getSoulInForDestroyTheEquip (theEquipUse );
		systemValues.soulCount += soulGet;
		systemValues.messageTitleBoxShow ("【"+nameUse+"】熔为"+soulGet+"灵力");
		DestroyImmediate(theEquipUse.gameObject);
		//theEquip = null;
		equipSelectTypeButton.flashThePanel ();
		thePackagePanelShow.makeFlash ();
		equipInformationPanel.makeFlash ();
		ShowMake ();
		flashThePanel ();
	}

	//装备的升级
	public void makeEquipLvUp()
	{
		if (!theEquip ) 
		{
			systemValues.messageTitleBoxShow ("尚未选定装备");
			return;
		}
		if( !theEquip.checkCanLvUp ())
		{
			systemValues.messageTitleBoxShow ("装备已经满级");
			return;
		}
		int cost = systemValues.getSoulCountForEquipLvUp (theEquip);
		if (systemValues.soulCount < cost)
		{
			systemValues.messageTitleBoxShow ("升级装备所需的灵力不足，还需要"+(cost - systemValues.soulCount)+"灵力");
			return;
		}
		systemValues.choiceMessageBoxShow ("升级装备？", "本次升级需要消耗"+cost+"灵力，效果为提升"+(theEquip.equipLvUpRate*100).ToString("f0")+"%的当前加成效果。\n\n是否升级？", true, new MesageOperate (makeEquipLvupForUse));
	}
	//委托方法
	void makeEquipLvupForUse()
	{
		theSoundController.makeSoundShow (0);
		int cost = systemValues.getSoulCountForEquipLvUp (theEquip);
		systemValues.soulCount -= cost;
		theEquip.makeEquipLvUp ();
		//数值改变了，还是刷新一下比较好
		equipInformationPanel.changeEquipToIntroduct (theEquip);
		thePackagePanelShow.setNewEquip (theEquip);
		equipRemakePanel.getEquipForOperate (theEquip);
	}
}
