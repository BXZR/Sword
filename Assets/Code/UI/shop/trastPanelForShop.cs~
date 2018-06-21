using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trastPanelForShop : MonoBehaviour {

	//商店的对比界面
	//文字显示而已
	//但是显示的内容并不完全相同

	private static Text theShowText;

	void Start()
	{
		Text theText = this.GetComponentInChildren<Text> ();
		theShowText = theText;
	}
	public static void makeTrast(equipBasics theEquip)
	{
		if (!theShowText) 
		{
			print ("no text");
			return;
		}

		if (!systemValues.thePlayer) 
		{
			print ("no player");
			return;
		}
		equipPackage thePackage = systemValues.thePlayer.GetComponentInChildren<equipPackage> ();
		if (!thePackage)
		{
			print ("no package");
			return;
		}
		theShowText .text = getTextForTrast(theEquip , thePackage);
	}



	static string getTextForTrast(equipBasics theEquip , equipPackage theEquipPackage)
	{
			switch (theEquip.theEquipType) 
			{
			case equiptype.body:
				return equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForBodyUsed);
				break;
			case equiptype.head:
				return  equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForHeadUsed);
				break;
			case equiptype.weapon:
				return  equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForWeaponUsed);
				break;
			case equiptype.shoe:
				return  equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForShoeUsed);
				break;
			case equiptype.extra:
				{
					if (theEquipPackage.thEquipForExtraUsed1 != null)
					{
						if (theEquipPackage.thEquipForExtraUsed2 != null) //两个饰品都装备了就跟第一个做对比
							return equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForExtraUsed1);
						else //第二件饰品如果没有装备那么直接就不用对比
							return equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForExtraUsed2);
					} 
					else 
					{    //第一件饰品已经有了空缺就直接用这个空缺就好了
						return  equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForExtraUsed1);
					}
				}
				break;
			case equiptype.god:
				return  equipBasics.equipTrast (theEquip, theEquipPackage.thEquipForGod);
				break;
		    case equiptype.equipSkill:
				return  "注灵效果各异，或许不应该只靠数值进行比较";
				break;
			}
		return "无法比较";

	}

}
