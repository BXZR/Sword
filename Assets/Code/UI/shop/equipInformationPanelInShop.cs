using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipInformationPanelInShop : MonoBehaviour {

	//这个脚本用来控制显示装备基本信息的界面

	public Button theButton;//现实的图片，同时也是也个按钮
	public Text theBasicText;//基本加成信息
	public Text theSkillText;//装备技能信息
	//装备的故事被封装到按钮点击messageBox中
	private Image theButtonImage;//装备他图

	//静态保存
	static equipBasics theEquipStatic = null;//装备静态保存
	static Button theButtonStatic;//现实的图片，同时也是也个按钮
	static Text theBasicTextStatic;//基本加成信息
	static Text theSkillTextStatic;//装备技能信息
	static Image theButtonImageStatic ;//装备他图

	void makeSTART()
	{
		theButtonStatic = theButton;
		theBasicTextStatic = theBasicText;
		theSkillTextStatic = theSkillText;
		theButtonImageStatic = theButtonImage;
	}

	public static void changeEquipToIntroduct(equipBasics theEquipIn)
	{
		theEquipStatic = theEquipIn;
		//print ("name = "+ theEquipStatic.equipName);
		showEquipInformation ();
	}

	static void showEquipInformation()
	{
		if (theEquipStatic)
		{
			theButtonImageStatic.sprite = systemValues.makeLoadSprite ("equipPicture/"+ theEquipStatic.theEquipType + "/"+ theEquipStatic.equipPictureName);
			if (theEquipStatic.theEquipType != equiptype.equipSkill) 
			{
				theBasicTextStatic.text = theEquipStatic.equipName + "\n" + theEquipStatic.getEquipAdderInformation ();
				theSkillTextStatic.text = theEquipStatic.theEquipStroy + "\n(潜力：" + theEquipStatic.skillValueCountAll + ")";
			}
			else 
			{
				string [] showing = systemValues.getEffectInfromationWithName (theEquipStatic.equipName).Split('\n');
				if (showing.Length < 2)
					return;
				
				theBasicTextStatic.text = showing [0] +"\n这是一个注灵图谱\n注灵能使装备拥有额外的战斗特效\n潜力要求: "+ theEquipStatic.skillValueCount;
				theSkillTextStatic.text ="附加效果:" +showing [0]  +"\n"+ showing [1] ;
			}
		}
		else
		{
			theSkillTextStatic.text = "";
			theBasicTextStatic.text = "";
		}
	}

	public static void makeFlash()
	{
		theSkillTextStatic.text = "";
		theBasicTextStatic.text = "";
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


}
