using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipShowingButton : MonoBehaviour {

	//这个类用来描述在显示背包中内容的时候view里面的button功能
	public equipBasics theEquip;
	public static Image selectedEffectPictureSave;
	public static Image selectedEffectPictureForSkill;
	public Image selectedEffectPicture;
	[HideInInspector]
	public Image theEquipImage = null;

	public void makeStart()
	{
		if(theEquipImage == null)
		theEquipImage = this.GetComponent <Image> ();
	}

	//按下的时候的功能
	public void makeClickForSelect()
	{
		//消息框显示的时候这个功能无效
		if (systemValues.isMessageBoxShowing) 
		{
			//print ("isMessageBoxing");
			return;
		}
		if (theEquip) 
		{
			//三个静态方法传递对象
			//于是区域自治（伪观察者模式）
			systemValues.theEquipNow = this.theEquip;
			//print (systemValues.theEquipNow.equipName +"is lated selected");
			if (theEquip.theEquipType != equiptype.equipSkill)
			{
				
				equipInformationPanel.changeEquipToIntroduct (this.theEquip);
				//返回两个装备对比的结果（可能会很长，需要控制）
				thePackagePanelShow.setNewEquip (this.theEquip);
				equipRemakePanel.getEquipForOperate (this.theEquip);
				makePictureShow(1);
			} 
			else 
			{
				equipRemakePanel.showEquipSkillAdderGet(theEquip);
				makePictureShow(2);
			}
		}
		else 
		{
			//因为有UI穿透的问题，这个消息框实现不用了
			//systemValues.messageBoxShow ("" , "尚且没有装备",1f);
		}
	}

	public void makePictureShow(int type)
	{
		//额外显示内容
		switch (type) 
		{
		  case 2:
			{
				if (selectedEffectPictureForSkill)
					selectedEffectPictureForSkill.enabled = false;
				selectedEffectPictureForSkill = this.selectedEffectPicture;
				if(selectedEffectPictureForSkill)
				selectedEffectPictureForSkill.enabled = true;
			}
			break;
		case 1:
			{
				if (selectedEffectPictureSave)
					selectedEffectPictureSave.enabled = false;
				selectedEffectPictureSave = this.selectedEffectPicture;
				if (selectedEffectPictureSave) 
				{
					selectedEffectPictureSave.enabled = true;
					this.selectedEffectPicture.enabled = true;
					//print ("show");
				}
			}
			break;
		}

	}

	public static void flashPicture(bool half = false)
	{
		if(selectedEffectPictureSave && !half)
			selectedEffectPictureSave.enabled = false;
		if (selectedEffectPictureForSkill)
			selectedEffectPictureForSkill.enabled = false;
	}

}
