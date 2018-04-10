using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillPanelController : MonoBehaviour {

	//显示技能信息的面板

	//来临时生成技能按钮
	public GameObject theSkillButtonProfab;
	//生成的按钮摆放的位置
	public Transform theButtonFather;
	//显示信息的Text
	public Text theInformationText;
	//魂元数量显示
	public Text SoulCountText ;
	//是否已经建立建立一次就可以了
	private bool isBuilt = false;

    //技能介绍按钮生成一次就可以
	void OnEnable()
	{
		
		makeStart ();
	}


	void Update()
	{
		//其实UI交互是需要开一帧的
		SoulCountText.text = "魂元数量："+systemValues.soulCount;
	}
	void  makeStart()
	{
		//单次初始化吧性能自然会好，但是初始对于动态增加效果的时候的灵活性或许不够强大
		//if (systemValues.thePlayer != null && !isBuilt) 
		if (systemValues.thePlayer != null) 
		{
			//简单的清理工作
			skillButton[] buttons = theButtonFather.GetComponentsInChildren<skillButton> ();
			for (int i = 0; i < buttons.Length; i++)
				Destroy (buttons[i].gameObject);
			
			theInformationText.text = "";
			attackLink[] theAttacklinks = systemValues.thePlayer.GetComponentsInChildren < attackLink > ();
			for (int i = 0; i < theAttacklinks.Length; i++) 
			{
				GameObject theButton = GameObject.Instantiate<GameObject> (theSkillButtonProfab);
				theButton.transform.SetParent (theButtonFather.transform);
				skillButton theSkillInformation = theButton.GetComponent <skillButton> ();
				theSkillInformation.attackLinkBasicInformation = theAttacklinks [i].getInformation ();
				theSkillInformation.basicEffect = getAttacklinkEffectInformation (theAttacklinks [i], theButton, out theSkillInformation.effectInformation);
				theSkillInformation.theShowText = theInformationText;
				theSkillInformation.theAttacklink = theAttacklinks[i];
				theButton.GetComponentInChildren<Text> ().text = theAttacklinks [i].skillName;
				theSkillInformation.makeStart ();
				theButton.transform.localPosition = new Vector3 (1,1,1);
				theButton.transform.localScale = new Vector3 (1,1,1);
			}

			//isBuilt = true;
		}
	}

	//获得连招的效果信息
	string getAttacklinkEffectInformation(attackLink theAttacklink,GameObject theButton ,out string theSkillInformation )
	{
		string information = "";
		string skillInformation = "";
		if (string.IsNullOrEmpty (theAttacklink.conNameToSELF) == false) 
		{
			System.Type theType = System.Type.GetType (theAttacklink.conNameToSELF);
		    theButton.gameObject.AddComponent (theType);
			//effectBasic theselfEffect = theButton.GetComponent <effectBasic> ();
			effectBasic theselfEffect = (effectBasic)theButton.GetComponent (theType);
			theselfEffect.Init ();
			information += "发动可以触发[" + systemValues.SkillColorForSelf + theselfEffect.theEffectName + systemValues.colorEnd +"]";
			skillInformation += systemValues.SkillColorForSelf+ theselfEffect.getInformation ()+systemValues.colorEnd;
			skillInformation += "\n";
			skillInformation += systemValues.SkillExtraColor + theselfEffect.getExtraInformation () + systemValues.colorEnd;
			//skillInformation += "\n";
			Destroy (theButton.GetComponent <effectBasic> ());
		}
		if (string.IsNullOrEmpty (theAttacklink.conNameToEMY) == false) 
		{
			System.Type theType = System.Type.GetType (theAttacklink.conNameToEMY);
			theButton.gameObject.AddComponent (theType);
			//effectBasic theEMYEffect = theButton.GetComponent <effectBasic> ();
			effectBasic theEMYEffect = (effectBasic)theButton.GetComponent (theType);
			theEMYEffect.Init ();
			if (string.IsNullOrEmpty (information) == false)
				information += "\n";
			information += "命中可以触发[" + systemValues.SkillColorForEnemy+ theEMYEffect.theEffectName + systemValues.colorEnd+"]";
		
			if (string.IsNullOrEmpty (skillInformation) == false)
				skillInformation += "\n";
			skillInformation +=  systemValues.SkillColorForEnemy+  theEMYEffect.getInformation () +systemValues.colorEnd;
			skillInformation += "\n";
			skillInformation += systemValues.SkillExtraColor + theEMYEffect.getExtraInformation () + systemValues.colorEnd;
			skillInformation += "\n\n";
			Destroy (theEMYEffect);
		}
		theSkillInformation = skillInformation;
		return information;
	}
}
