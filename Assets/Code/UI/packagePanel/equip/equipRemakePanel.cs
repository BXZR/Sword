using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipRemakePanel : MonoBehaviour {

	//这个脚本用来管理装备的装备，卸下，熔铸等等的子界面
	//这个脚本用来记录装备的操作
	//装备，卸下，合成，熔锻......
	public Button theEquipButton;//装备/卸下的按钮

    //静态信息保存
	private static equipBasics theEquip;
	private static Button theEquipButtonStatic;
	private static Text theEquipButtonText;
	private panelSoundController theSoundController;

	void Start()
	{
		theEquipButtonStatic = theEquipButton  ;
		theEquipButtonText = theEquipButton.GetComponentInChildren<Text> ();
		theSoundController = this.GetComponentInChildren<panelSoundController> ();
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

	//熔锻这个装备
	//这个装备被消熔之后，将会转化为灵力
	public void soulTheEquip()
	{
		if (!theEquip) 
		{
			systemValues.messageTitleBoxShow ("尚未选定装备");
			return;
		}
		systemValues.choiceMessageBoxShow ("熔锻装备？", "熔铸装备将会获得一些灵力，但是这个装备会永远消失。\n是否熔锻？", true, new MesageOperate (makeTheEquipToSoul));

	}

	//委托方法
	void makeTheEquipToSoul()
	{
		theSoundController.makeSoundShow (1);
		
		if (theEquip.isUsing)
			theEquip.DropThisThing (systemValues.thePlayer);

		int soulGet = 20 + systemValues.getSoulCountForEquipLvUp (theEquip , true);
		systemValues.soulCount += soulGet;
		systemValues.messageTitleBoxShow ("【"+theEquip.equipName+"】熔为"+soulGet+"灵力");
		DestroyImmediate(theEquip.gameObject);
		//theEquip = null;
		equipSelectTypeButton.flashThePanel ();
		thePackagePanelShow.makeFlash ();
		equipInformationPanel.makeFlash ();
		ShowMake ();

	}

	//装备的升级
	public void makeEquipLvUp()
	{
		if (!theEquip || !theEquip.checkCanLvUp ()) 
		{
			systemValues.messageTitleBoxShow ("尚未选定装备");
			return;
		}
		int cost = systemValues.getSoulCountForEquipLvUp (theEquip);
		if (systemValues.soulCount < cost)
		{
			systemValues.messageTitleBoxShow ("升级装备所需的灵力不足，还需要"+(cost - systemValues.soulCount)+"灵力");
			return;
		}
		systemValues.choiceMessageBoxShow ("升级装备？", "本次升级需要消耗"+cost+"灵力，效果为提升"+(theEquip.equipLvUpRate*100).ToString("f0")+"%的当前加成效果。\n是否升级？", true, new MesageOperate (makeEquipLvupForUse));
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
