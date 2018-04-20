using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipRemakePanel : MonoBehaviour {

	//这个脚本用来管理装备的装备，卸下，熔铸等等的子界面
	//这个脚本用来记录装备的操作
	//装备，卸下，合成，熔锻......
	public Button theEquipButton;//装备/卸下的按钮
	public Transform theViewFather;
	public Text equipSkillAddInformation;
	public GameObject theShowingButtonProfab;

    //静态信息保存
	private static equipBasics theEquip;
	public static equipBasics equipSkillAdderNow  = null ;
	private static Button theEquipButtonStatic;
	private static Text theEquipButtonText;
	private panelSoundController theSoundController;
	private static Text equipSkillAddInformationStatic;
	private static Transform equipSkillAdderViewFatherStatic;
	private static GameObject theShowingButtonProfabStatic;

	private bool isStarted = false;
	void Start()
	{
		theEquipButtonStatic = theEquipButton  ;
		theEquipButtonText = theEquipButton.GetComponentInChildren<Text> ();
		theSoundController = this.GetComponentInChildren<panelSoundController> ();
		equipSkillAddInformationStatic = equipSkillAddInformation;
		equipSkillAdderViewFatherStatic = theViewFather;
		theShowingButtonProfabStatic = theShowingButtonProfab;
		flashThePanel ();
		isStarted = true;
	}


	//展示这个注灵的效果
	public static void showEquipSkillAdderGet(equipBasics theEquip)
	{
		equipSkillAdderNow = theEquip;
		equipSkillAddInformationStatic.text = systemValues.getEffectInfromationWithName (theEquip.equipName);
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

		if(equipShowingButton.selectedEffectPictureSave)
		equipShowingButton.selectedEffectPictureSave.enabled = false;
	}

	public static  void flashThePanel()
	{
		//因为所有的显示都是针对本机角色的
		if (!systemValues.thePlayer )
			return;
		//前期清理工作
		equipShowingButton []  es = equipSkillAdderViewFatherStatic.GetComponentsInChildren<equipShowingButton>();
		List<equipBasics> eqs = systemValues.thePlayer.GetComponent <equipPackage> ().allEquipsForSave.FindAll (x => x!=null && x.theEquipType == equiptype.equipSkill);
		//直接用lambda表达式查询吧还是
		systemValues. makeFather (eqs.Count , equipSkillAdderViewFatherStatic);

		if (eqs.Count > es.Length) 
		{
			int i = 0;
			for (; i < es.Length; i++) 
			{
				es [i].theEquip = eqs [i];
				es [i].GetComponentInChildren<Text>().text = "灵";
			}
			for (; i < eqs.Count; i++) 
			{
				GameObject theButton = GameObject.Instantiate<GameObject> (theShowingButtonProfabStatic);
				theButton.transform.SetParent (equipSkillAdderViewFatherStatic.transform);
				theButton.GetComponent <equipShowingButton> ().theEquip = eqs [i];
				theButton.GetComponentInChildren<Text>().text = "灵";
				//因为有grid控件，所以这些都没有必要使用了
			}
			//print ("重建次数："+( i- es.Length));
		}
		else
		{
			//print ("deletes343434");
			int i = 0;
			for (; i < eqs.Count; i++) 
			{
				es [i].theEquip = eqs [i];
				es [i].GetComponentInChildren<Text>().text = "灵";
			}
			for (; i>=0 && i < es.Length; i++) 
			{
				Destroy (es [i].gameObject);
			}
		}
		equipSkillAddInformationStatic.text = "";
	}

	//装备或者替换这个装备
	public void wearThEquip()
	{
		if (theEquip)
		{
			theSoundController.makeSoundShow (2);
			systemValues.messageTitleBoxShow ("装备【" + theEquip.equipName + "】");
			thePackagePanelShow.wearEquip (theEquip);
			equipSelectTypeButton.flashThePanel ();
			ShowMake ();
		}
		else 
		{
			systemValues.messageTitleBoxShow ("尚未选定装备");
		}
	}



	void OnEnable()
	{
		if(isStarted)
			flashThePanel ();
	}

	//UI调用方法和回调方法=================================================================================================
	public void makeTheEquipGetSkill()
	{
		if (!theEquip || !equipSkillAdderNow) 
		{
			systemValues.messageTitleBoxShow ("尚未选定装备或者注灵材料");
			return;
		}

		if (theEquip.theEffectNames.Contains (equipSkillAdderNow.equipName)) 
		{
			systemValues.messageTitleBoxShow ("此装备已经注有同类型的灵力，无法叠加");
			return;
		}
		string theEffectName = systemValues.getEffectNameWithName (equipSkillAdderNow.equipName);
		string theEffectInformation = systemValues.getEffectInfromationWithName (equipSkillAdderNow.equipName);
		systemValues.choiceMessageBoxShow ("是否注灵？", "注灵将会使【"+theEquip.equipName+"】获得“"+theEffectName+"”效果，需要永久消耗《"+theEffectName+"》图谱\n\n效果："+theEffectInformation +"\n\n是否注灵？", true, new MesageOperate (getTheSkill));
	}

	void getTheSkill()
	{
		theSoundController.makeSoundShow (3);
		theEquip.makeEquipAddSkill (equipSkillAdderNow.equipName);
		DestroyImmediate(equipSkillAdderNow.gameObject);
		equipInformationPanel.changeEquipToIntroduct (theEquip);
		thePackagePanelShow.setNewEquip (theEquip);
		equipRemakePanel.getEquipForOperate (theEquip);
		flashThePanel();
	}




	//熔锻这个装备
	//这个装备被消熔之后，将会转化为灵力
	public void soulTheEquip()
	{
		if (!theEquip) 
		{
			systemValues.messageTitleBoxShow ("尚未选定装备");
			return;
		}
		systemValues.choiceMessageBoxShow ("熔锻装备？", "熔铸装备将会获得一些灵力，但是这个装备会永远消失。\n\n是否熔锻？", true, new MesageOperate (makeTheEquipToSoul));

	}

	//委托方法
	void makeTheEquipToSoul()
	{
		theSoundController.makeSoundShow (1);
		
		if (theEquip.isUsing)
			theEquip.DropThisThing (systemValues.thePlayer);

		int soulGet = 20 + systemValues.getSoulCountForEquipLvUp (theEquip , true);
		systemValues.soulCount += soulGet;
		systemValues.messageTitleBoxShow ("【"+theEquip.equipName+"】熔为"+soulGet+"灵力");
		DestroyImmediate(theEquip.gameObject);
		//theEquip = null;
		equipSelectTypeButton.flashThePanel ();
		thePackagePanelShow.makeFlash ();
		equipInformationPanel.makeFlash ();
		ShowMake ();

	}

	//装备的升级
	public void makeEquipLvUp()
	{
		if (!theEquip || !theEquip.checkCanLvUp ()) 
		{
			systemValues.messageTitleBoxShow ("尚未选定装备");
			return;
		}
		int cost = systemValues.getSoulCountForEquipLvUp (theEquip);
		if (systemValues.soulCount < cost)
		{
			systemValues.messageTitleBoxShow ("升级装备所需的灵力不足，还需要"+(cost - systemValues.soulCount)+"灵力");
			return;
		}
		systemValues.choiceMessageBoxShow ("升级装备？", "本次升级需要消耗"+cost+"灵力，效果为提升"+(theEquip.equipLvUpRate*100).ToString("f0")+"%的当前加成效果。\n\n是否升级？", true, new MesageOperate (makeEquipLvupForUse));
	}
	//委托方法
	void makeEquipLvupForUse()
	{
		theSoundController.makeSoundShow (0);
		int cost = systemValues.getSoulCountForEquipLvUp (theEquip);
		systemValues.soulCount -= cost;
		theEquip.makeEquipLvUp ();
		//数值改变了，还是刷新一下比较好
		equipInformationPanel.changeEquipToIntroduct (theEquip);
		thePackagePanelShow.setNewEquip (theEquip);
		equipRemakePanel.getEquipForOperate (theEquip);
	}
}
