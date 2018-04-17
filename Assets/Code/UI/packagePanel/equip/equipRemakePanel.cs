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
	private buttonWithSound theSound;

	void Start()
	{
		theEquipButtonStatic = theEquipButton  ;
		theEquipButtonText = theEquipButton.GetComponentInChildren<Text> ();
		theSound = this.GetComponentInChildren<buttonWithSound> ();
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
			systemValues.messageTitleBoxShow ("装备【"+theEquip.equipName+"】");
			thePackagePanelShow.wearEquip (theEquip);
			equipSelectTypeButton.flashThePanel ();
			ShowMake ();
		}
	}

	//熔锻这个装备
	//这个装备被消熔之后，将会转化为灵力
	public void soulTheEquip()
	{
		if (!theEquip)
			return;
		systemValues.choiceMessageBoxShow ("熔铸装备？", "熔铸装备将会获得一些灵力，但是这个装备会永远消失，是否熔铸？", true, new MesageOperate (makeTheEquipToSoul));

	}

	void makeTheEquipToSoul()
	{
		theSound.makeSoundShow ();
		
		if (theEquip.isUsing)
			theEquip.DropThisThing (systemValues.thePlayer);

		int soulGet = 20 + systemValues.getSoulCountForEquipLvUp (theEquip , true);
		systemValues.soulCount += soulGet;
		systemValues.messageTitleBoxShow ("【"+theEquip.equipName+"】熔为"+soulGet+"灵力");
		DestroyImmediate(theEquip.gameObject);
		theEquip = null;
		equipSelectTypeButton.flashThePanel ();
		thePackagePanelShow.makeFlash ();
		equipInformationPanel.makeFlash ();
		ShowMake ();

	}

	//装备的升级
	public void makeEquipLvUp()
	{
		if (!theEquip || !theEquip.checkCanLvUp())
			return;

		int cost = systemValues.getSoulCountForEquipLvUp (theEquip);
		if (systemValues.soulCount < cost)
			return;
		
		systemValues.soulCount -= cost;
		theEquip.makeEquipLvUp ();
		//数值改变了，还是刷新一下比较好
		equipInformationPanel.changeEquipToIntroduct (theEquip);
		thePackagePanelShow.setNewEquip (theEquip);
		equipRemakePanel.getEquipForOperate (theEquip);

	}
}
