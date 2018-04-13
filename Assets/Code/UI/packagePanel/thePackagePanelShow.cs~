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
	static equipBasics theEquip;
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

	void OnEnable()
	{
		if (!theEquipPackage)
			return;
		
		makeFlash ();
	}

	public void makeFlash()
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

}
