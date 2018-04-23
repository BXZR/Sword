using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

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
	//保存下来的引用
	private List<attackLink> saved;
    //技能介绍按钮生成一次就可以
	void OnEnable()
	{
		bool changed = IsChanged ();
		if(changed)
		     makeStart ();
		//print ("ischanged = "+changed);
	}
		

	//如果连招没有变化，那就没有必要重新加载重建连招信息
	bool IsChanged()
	{
		if (saved == null)
			return true;
		attackLink[] theAttacklinks = systemValues.thePlayer.GetComponentsInChildren < attackLink > ();
		if (theAttacklinks.Length != saved.Count)
			return true;
		for (int i = 0; i < saved.Count; i++)
			if (saved [i] != theAttacklinks [i])
				return true;
		return false;
		
	}

	void  makeStart()
	{
		//单次初始化吧性能自然会好，但是初始对于动态增加效果的时候的灵活性或许不够强大
		//if (systemValues.thePlayer != null && !isBuilt) 
		if (systemValues.thePlayer != null) 
		{
			saved = new List<attackLink> ();
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
				saved.Add (theAttacklinks[i]);
			}

			//isBuilt = true;
		}
	}

	//获得连招的效果信息
	string getAttacklinkEffectInformation(attackLink theAttacklink,GameObject theButton ,out string theSkillInformation )
	{
		StringBuilder theInformationString = new StringBuilder ();
		StringBuilder theSkillInformationString = new StringBuilder ();

		if (systemValues.isNullOrEmpty (theAttacklink.conNameToSELF) == false) 
		{
			System.Type theType = System.Type.GetType (theAttacklink.conNameToSELF);
		    theButton.gameObject.AddComponent (theType);
			//effectBasic theselfEffect = theButton.GetComponent <effectBasic> ();
			effectBasic theselfEffect = (effectBasic)theButton.GetComponent (theType);
			theselfEffect.Init ();

			theInformationString.Append ("发动可以触发【");
			theInformationString.Append (systemValues.SkillColorForSelf);
			theInformationString.Append (theselfEffect.theEffectName);
			theInformationString.Append (systemValues.colorEnd);
			theInformationString.Append ("】");

			theSkillInformationString.Append (systemValues.SkillColorForSelf);
			theSkillInformationString.Append (theselfEffect.getInformation ());
			theSkillInformationString.Append (systemValues.colorEnd);

			theSkillInformationString.Append ("\n");
			theSkillInformationString.Append (systemValues.SkillExtraColor );
			theSkillInformationString.Append (theselfEffect.getExtraInformation ());
			theSkillInformationString.Append (systemValues.colorEnd);

			Destroy (theselfEffect);
		}
		if (systemValues.isNullOrEmpty (theAttacklink.conNameToEMY) == false) 
		{
			System.Type theType = System.Type.GetType (theAttacklink.conNameToEMY);
			theButton.gameObject.AddComponent (theType);
			//effectBasic theEMYEffect = theButton.GetComponent <effectBasic> ();
			effectBasic theEMYEffect = (effectBasic)theButton.GetComponent (theType);
			theEMYEffect.Init ();
			if ( (theInformationString.Length)!= 0 )
				theInformationString.Append( "\n");
			
			theInformationString.Append ("命中可以触发【");
			theInformationString.Append (systemValues.SkillColorForEnemy);
			theInformationString.Append (theEMYEffect.theEffectName);
			theInformationString.Append (systemValues.colorEnd);
			theInformationString.Append ("】");

		
			if (theSkillInformationString.Length != 0)
				theSkillInformationString.Append("\n");

			theSkillInformationString.Append (systemValues.SkillColorForEnemy);
			theSkillInformationString.Append ( theEMYEffect.getInformation ());
			theSkillInformationString.Append (systemValues.colorEnd);
			theSkillInformationString.Append ("\n");
			theSkillInformationString.Append (systemValues.SkillExtraColor);
			theSkillInformationString.Append (theEMYEffect.getExtraInformation () );
			theSkillInformationString.Append (systemValues.colorEnd);
			theSkillInformationString.Append ("\n\n");

			Destroy (theEMYEffect);
		}
		theSkillInformation = theSkillInformationString .ToString();
		return theInformationString.ToString();
	}
}
