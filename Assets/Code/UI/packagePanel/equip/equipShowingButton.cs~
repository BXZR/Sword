using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipShowingButton : MonoBehaviour {

	//这个类用来描述在显示背包中内容的时候view里面的button功能
	public equipBasics theEquip;
	//按下的时候的功能
	public void makeClick()
	{
		if (theEquip)
		{
			//三个静态方法传递对象
			//于是区域自治（伪观察者模式）
			equipInformationPanel.changeEquipToIntroduct (this.theEquip);
			//返回两个装备对比的结果（可能会很长，需要控制）
			thePackagePanelShow.setNewEquip (this.theEquip);
			equipRemakePanel.getEquipForOperate (theEquip);
		}
	}

}
