using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class thePackagePanelShow : MonoBehaviour {

	//背包界面的总控制单元
	public Text theEquipMinusShowText ;
	static Text theTextForStatic;
	static equipBasics theEquip;
	static equipPackage theEquipPackage;
	//因为背包的东西可能会经常换，所以暂时还真需要适时刷新
	//等待后面的事件机制完全了或许就好了
	void Start()
	{
		theTextForStatic = theEquipMinusShowText ;
	}

	public static void setCanculateInformation(string informationIn)
	{
		theTextForStatic.text = informationIn;
	}

	public static void setNewEquip(equipBasics theEquipIn)
	{
		theEquip = theEquipIn;
		theEquipPackage = systemValues.thePlayer.GetComponent < equipPackage> ();
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
