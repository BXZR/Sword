using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipRemakePanel : MonoBehaviour {

	//这个脚本用来管理装备的装备，卸下，熔铸等等的子界面

	public Button theEquipButton;//装备/卸下的按钮

    //静态信息保存
	private static equipBasics theEquip;
	private static Button theEquipButtonStatic;
	private static Text theEquipButtonText;

	void Start()
	{
		theEquipButtonStatic = theEquipButton  ;
		theEquipButtonText = theEquipButton.GetComponentInChildren<Text> ();
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
}
