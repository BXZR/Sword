using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipShowingButton : MonoBehaviour {

	//这个类用来描述在显示背包中内容的时候view里面的button功能
	public equipBasics theEquip;
	//按下的时候的功能
	public void makeClick()
	{
		equipInformationPanel.changeEquipToIntroduct (this.theEquip);
		//返回两个装备对比的结果（可能会很长，需要控制）
		thePackagePanelShow.setCanculateInformation (this.theEquip.equipName +"与当前装备比较：\n攻击 + 12\n攻击速度 - 7%");
		equipRemakePanel.getEquipForOperate (theEquip);
	}

}
