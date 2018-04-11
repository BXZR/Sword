using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipShowingButton : MonoBehaviour {

	//这个类用来描述在显示背包中内容的时候view里面的button功能
	public equipBasics theEquip;
	//按下的时候的功能
	public void makeClick()
	{
		string theShowString = theEquip.getEquipAdderInformation ();
		systemValues.messageBoxShow (theEquip.equipName , theShowString , autoSize: true );
	}

}
