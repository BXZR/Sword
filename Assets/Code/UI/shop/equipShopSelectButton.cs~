using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipShopSelectButton : equipShowingButton {


	//这个类用来描述在显示背包中内容的时候view里面的button功能

	public Text theValueText;


	void Start()
	{
		theValueText .text = this.theEquip.theSoulForThisEquip.ToString();
	}
		

	//按下的时候的功能
	public void makeClickForSelect()
	{
		//消息框显示的时候这个功能无效
		if (systemValues.isMessageBoxShowing) 
		{
			print ("isMessageBoxing");
			return;
		}
		if (theEquip) 
		{
			systemValues.theEquipNowInShop = this.theEquip;
			//print (systemValues.theEquipNow.equipName +"is lated selected");
			equipInformationPanelInShop.changeEquipToIntroduct (this.theEquip);
			trastPanelForShop.makeTrast (this.theEquip);
			makePictureShow(1);
		}
		else 
		{
			print ("d");
			//因为有UI穿透的问题，这个消息框实现不用了
			//systemValues.messageBoxShow ("" , "尚且没有装备",1f);
		}
	}

}