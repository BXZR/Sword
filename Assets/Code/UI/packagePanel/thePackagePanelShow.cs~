using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class thePackagePanelShow : MonoBehaviour {

	//背包界面的总控制单元
	public Text theEquipMinusShowText ;
	public equipShowingButton theHeadButton;
	public equipShowingButton theBodyButton;
	public equipShowingButton theWeaponButton;
	public equipShowingButton theShowButton;
	public equipShowingButton theExtraButton1;
	public equipShowingButton theExtraButton2;
	//静态变量保存
	static Text theTextForStatic;
	public static equipBasics theEquip;
	static equipPackage theEquipPackage;
	static equipShowingButton staticHeadButton;
	static equipShowingButton staticBodyButton;
	static equipShowingButton staticWeaponButton;
	static equipShowingButton staticShoeButton;
	static equipShowingButton staticExtraButton1;
	static equipShowingButton staticExtraButton2;

	static Text staticHeadButtonText ;
	static Text  staticBodyButtonText ;
	static Text  staticWeaponButtonText ;
	static Text  staticShoeButtonText ;
	static Text  staticExtraButton1Text ;
	static Text  staticExtraButton2Text ;


	//因为背包的东西可能会经常换，所以暂时还真需要适时刷新
	//等待后面的事件机制完全了或许就好了
	void Start()
	{
		theEquipPackage = systemValues.thePlayer.GetComponent < equipPackage> ();

		theTextForStatic = theEquipMinusShowText ;
		staticHeadButton = theHeadButton;
		staticBodyButton = theBodyButton;
		staticWeaponButton = theWeaponButton;
		staticShoeButton = theShowButton;
		staticExtraButton1 = theExtraButton1;
		staticExtraButton2 = theExtraButton2;

		staticHeadButtonText = theHeadButton.GetComponentInChildren<Text>();
		staticBodyButtonText = staticBodyButton.GetComponentInChildren<Text>();
		staticWeaponButtonText = staticWeaponButton.GetComponentInChildren<Text>();
		staticShoeButtonText = staticShoeButton.GetComponentInChildren<Text>();
		staticExtraButton1Text = staticExtraButton1.GetComponentInChildren<Text>();
		staticExtraButton2Text = staticExtraButton2.GetComponentInChildren<Text>();

		makeFlash ();
	}
		
	public static void makeFlash()
	{
		staticHeadButton.theEquip = theEquipPackage .thEquipForHeadUsed;
		staticBodyButton.theEquip = theEquipPackage.thEquipForBodyUsed;
		staticWeaponButton.theEquip  = theEquipPackage.thEquipForWeaponUsed;
		staticShoeButton.theEquip = theEquipPackage.thEquipForShoeUsed;
		staticExtraButton1.theEquip = theEquipPackage.thEquipForExtraUsed1;
		staticExtraButton2.theEquip = theEquipPackage.thEquipForExtraUsed2;

		staticHeadButtonText.text = staticHeadButton .theEquip != null ? staticHeadButton .theEquip.equipName : "[尚未装备]";
		staticBodyButtonText .text =  staticBodyButton .theEquip != null ? staticBodyButton .theEquip.equipName : "[尚未装备]";
		staticWeaponButtonText.text  = staticWeaponButton .theEquip != null ? staticWeaponButton .theEquip.equipName : "[尚未装备]";
		staticShoeButtonText .text = staticShoeButton.theEquip != null ? staticShoeButton.theEquip.equipName : "[尚未装备]";
		staticExtraButton1Text .text = staticExtraButton1.theEquip != null ? staticExtraButton1.theEquip.equipName : "[尚未装备]";
		staticExtraButton2Text.text  = staticExtraButton2.theEquip != null ? staticExtraButton2.theEquip.equipName : "[尚未装备]";
		theTextForStatic.text = "";
	}


	public static void setCanculateInformation(string informationIn)
	{
		theTextForStatic.text = informationIn;
	}

	public static void setNewEquip(equipBasics theEquipIn)
	{
		theEquip = theEquipIn;
		makeTrast ();
	}

	private static void makeTrast()
	{
		//theTextForStatic.text = equipBasics.equipTrast (theEquip , null);//样例方法调用
		switch (theEquip.theEquipType) 
		{
		case equiptype.body:
			theTextForStatic.text = equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForBodyUsed);
			break;
		case equiptype.head:
			theTextForStatic.text =  equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForHeadUsed);
			break;
		case equiptype.weapon:
			theTextForStatic.text =  equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForWeaponUsed);
			break;
		case equiptype.shoe:
			theTextForStatic.text =  equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForShoeUsed);
			break;
		case equiptype.extra:
			{
				if(theEquipPackage.thEquipForExtraUsed1 != null)
					theTextForStatic.text =  equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForExtraUsed1);
				else
					theTextForStatic.text =  equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForExtraUsed2);
			}
			break;

		}

	}

	public static void wearEquip(equipBasics theEquip)
	{
		string oldEquipName = "";
		//这是一件已经装备上的装备，所以需要卸下
		if (theEquip.isUsing) 
		{
			//theTextForStatic.text = equipBasics.equipTrast (theEquip , null);//样例方法调用
			switch (theEquip.theEquipType) 
			{
			case equiptype.body:
				theEquipPackage.thEquipForBodyUsed = null;
				break;
			case equiptype.head:
				theEquipPackage.thEquipForHeadUsed = null;
				break;
			case equiptype.weapon:
				theEquipPackage.thEquipForWeaponUsed = null;
				break;
			case equiptype.shoe:
				theEquipPackage.thEquipForShoeUsed = null;
				break;
			case equiptype.extra:
				{
					if (theEquipPackage.thEquipForExtraUsed1 == theEquip)
						theEquipPackage.thEquipForExtraUsed1 = null;
					else
						theEquipPackage.thEquipForExtraUsed2 = null;
				}
				break;
			}
			theEquip.DropThisThing (systemValues.thePlayer);
		}
		//替换当前的装备
		else
		{
			//theTextForStatic.text = equipBasics.equipTrast (theEquip , null);//样例方法调用
			switch (theEquip.theEquipType) 
			{
			case equiptype.body:
				{
					if (theEquipPackage.thEquipForBodyUsed != null) 
					{
						oldEquipName = theEquipPackage.thEquipForBodyUsed.equipName;
						theEquipPackage.thEquipForBodyUsed.DropThisThing (systemValues.thePlayer);
					}
					theEquipPackage.thEquipForBodyUsed = theEquip;
					theEquip.GetThisThing (systemValues.thePlayer);
				}
				break;
			case equiptype.head:
				{
					if (theEquipPackage.thEquipForHeadUsed != null) 
					{
						oldEquipName = theEquipPackage.thEquipForHeadUsed.equipName;
						theEquipPackage.thEquipForHeadUsed.DropThisThing (systemValues.thePlayer);
					}
					theEquipPackage.thEquipForHeadUsed = theEquip;
					theEquip.GetThisThing (systemValues.thePlayer);
				}
				break;
			case equiptype.weapon:
				{
					if (theEquipPackage.thEquipForWeaponUsed != null) 
					{
						oldEquipName = theEquipPackage.thEquipForWeaponUsed.equipName;
						theEquipPackage.thEquipForWeaponUsed.DropThisThing (systemValues.thePlayer);
					}
					theEquipPackage.thEquipForWeaponUsed = theEquip;
					theEquip.GetThisThing (systemValues.thePlayer);
				}
				break;
			case equiptype.shoe:
				{
					if (theEquipPackage.thEquipForShoeUsed != null)
					{					
						oldEquipName = theEquipPackage.thEquipForShoeUsed.equipName;
						theEquipPackage.thEquipForShoeUsed.DropThisThing (systemValues.thePlayer);
					}
					theEquipPackage.thEquipForShoeUsed = theEquip;
					theEquip.GetThisThing (systemValues.thePlayer);
				}
				break;
			case equiptype.extra:
				{
					if (theEquipPackage.thEquipForExtraUsed1 == null) 
					{
						theEquipPackage.thEquipForExtraUsed1 = theEquip;
						theEquip.GetThisThing (systemValues.thePlayer);
					}
					else if (theEquipPackage.thEquipForExtraUsed2 == null) 
					{
						theEquipPackage.thEquipForExtraUsed2 = theEquip;
						theEquip.GetThisThing (systemValues.thePlayer);
					} 
					else 
					{
						oldEquipName = theEquipPackage.thEquipForExtraUsed1.equipName;
						theEquipPackage.thEquipForExtraUsed1.DropThisThing (systemValues.thePlayer);
						theEquipPackage.thEquipForExtraUsed1 = theEquip;
						theEquip.GetThisThing (systemValues.thePlayer);
					}
				}
				break;
			}
		}
		makeFlash ();

//		if(string.IsNullOrEmpty(oldEquipName))
//			systemValues.messageBoxShow ("装备装载" ,theEquip.equipName+"已经投入使用" );
//		else
//			systemValues.messageBoxShow ("装备替换" ,"["+oldEquipName+"]替换为["+theEquip.equipName+ "]");
	
	}

}
