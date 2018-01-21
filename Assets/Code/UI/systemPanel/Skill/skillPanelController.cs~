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

	//是否已经建立建立一次就可以了
	private bool isBuilt = false;

    //技能介绍按钮生成一次就可以
	void OnEnable()
	{
		
		makeStart ();
	}

	void  makeStart()
	{
		if (systemValues.thePlayer != null && isBuilt == false) 
		{
			attackLink[] theAttacklinks = systemValues.thePlayer.GetComponentsInChildren < attackLink > ();
			for (int i = 0; i < theAttacklinks.Length; i++) 
			{
				GameObject theButton = GameObject.Instantiate<GameObject> (theSkillButtonProfab);
				theButton.transform.SetParent (theButtonFather.transform);
				skillButton theSkillInformation = theButton.GetComponent <skillButton> ();
				theSkillInformation.attackLinkBasicInformation = theAttacklinks [i].getInformation ();
				theSkillInformation.basicEffect = getAttacklinkEffectInformation (theAttacklinks [i], theButton, out theSkillInformation.effectInformation);
				theSkillInformation.theShowText = theInformationText;
				theButton.GetComponentInChildren<Text> ().text = theAttacklinks [i].skillName;
				theButton.transform.localPosition = new Vector3 (1,1,1);
				theButton.transform.localScale = new Vector3 (1,1,1);
			}
			isBuilt = true;
		}
	}

	//获得连招的效果信息
	string getAttacklinkEffectInformation(attackLink theAttacklink,GameObject theButton ,out string theSkillInformation )
	{
		string information = "";
		string skillInformation = "";
		if (string.IsNullOrEmpty (theAttacklink.conNameToSELF) == false) 
		{
			theButton.gameObject.AddComponent (System.Type.GetType (theAttacklink.conNameToSELF) );
			effectBasic theselfEffect = theButton.GetComponent <effectBasic> ();
			theselfEffect.Init ();
			information += "发动可以触发[" + systemValues.importantColor + theselfEffect.theEffectName + systemValues.colorEnd +"]";
			skillInformation += theselfEffect.getInformation ();
			Destroy (theselfEffect);
		}
		if (string.IsNullOrEmpty (theAttacklink.conNameToEMY) == false) 
		{
			theButton.gameObject.AddComponent (System.Type.GetType (theAttacklink.conNameToEMY) );
			effectBasic theEMYEffect = theButton.GetComponent <effectBasic> ();
			theEMYEffect.Init ();
			if (string.IsNullOrEmpty (information) == false)
				information += "\n";
			information += "命中可以触发[" + systemValues.importantColor+ theEMYEffect.theEffectName + systemValues.colorEnd+"]";
		
			if (string.IsNullOrEmpty (skillInformation) == false)
				skillInformation += "\n";
			skillInformation += theEMYEffect.getInformation ();

			Destroy (theEMYEffect);
		}
		theSkillInformation = skillInformation;
		return information;
	}
}
