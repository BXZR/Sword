using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipInformationPanel : MonoBehaviour {

	//这个脚本用来控制显示装备基本信息的界面

	public Button theButton;//现实的图片，同时也是也个按钮
	public Text theBasicText;//基本加成信息
	public Text theSkillText;//装备技能信息
	public Text theEquipLvText;//装备的当前等级
	//装备的故事被封装到按钮点击messageBox中
	private Image theButtonImage;//装备他图

	//静态保存
	static equipBasics theEquipStatic = null;//装备静态保存
	static Button theButtonStatic;//现实的图片，同时也是也个按钮
	static Text theBasicTextStatic;//基本加成信息
	static Text theSkillTextStatic;//装备技能信息
	static Image theButtonImageStatic ;//装备他图
	static Text theEquipLvTextStatic;//装备的当前等级

	void makeSTART()
	{
		theButtonStatic = theButton;
		theBasicTextStatic = theBasicText;
		theSkillTextStatic = theSkillText;
		theButtonImageStatic = theButtonImage;
		theEquipLvTextStatic = theEquipLvText;
	}

	public static void changeEquipToIntroduct(equipBasics theEquipIn)
	{
		theEquipStatic = theEquipIn;
		showEquipInformation ();
	}

	 static void showEquipInformation()
	{
		if (theEquipStatic)
		{
			theButtonImageStatic.sprite = systemValues.makeLoadSprite ("equipPicture/"+ theEquipStatic.theEquipType + "/"+ theEquipStatic.equipPictureName);
			theBasicTextStatic.text = theEquipStatic.getEquipName() +"\n"+theEquipStatic.getEquipAdderInformation ();
			theEquipLvTextStatic.text = theEquipStatic.EquipLvNow.ToString();
			//技能介绍
			if (theEquipStatic.theEffectNames.Length == 0)
				theSkillTextStatic.text = "此物品没有附加效果";
			else 
			{
				theSkillTextStatic.text = "";
				for (int i = 0; i < theEquipStatic.theEffectNames.Length; i++)
					theSkillTextStatic.text += systemValues.getEffectInfromationWithName (theEquipStatic.theEffectNames [i]) +"\n";
			}
		}
		else
		{
			theSkillTextStatic.text = "";
			theBasicTextStatic.text = "";
			theEquipLvTextStatic.text = "";
		}
	}

	public static void makeFlash()
	{
		theSkillTextStatic.text = "";
		theBasicTextStatic.text = "";
		theEquipLvTextStatic.text = "1";
		theButtonImageStatic.sprite = null;
	}

	public void showEquipStory()
	{
		string theShowString = theEquipStatic != null ?  theEquipStatic.theEquipStroy : "目前尚未选定任何装备";
		string theShowTitle = theEquipStatic != null ?  theEquipStatic.equipName : "";
		systemValues.messageBoxShow ( theShowTitle , theShowString , autoSize: true );
	}

	void Start () 
	{
		theButtonImage = theButton.GetComponent <Image> ();
		//下面两个方法的顺序不可以变化
		makeSTART ();
		showEquipInformation ();
	}
	
// Update is called once per frame
//	void Update () {
//		
//	}
}
