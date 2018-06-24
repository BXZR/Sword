using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillInformationPanel : MonoBehaviour {

	//这是用来介绍招式信息的面板脚本

	private attackLink theLinkNow = null;
	private skillButton theShowButton;
	public Text theSkillNameText;
	public Text theSkillBasicInformationText;
	public Text theSkillEffectInformationText;
	public Text theSkillLvUpInformationText;
	public Button theSkillLvupButton;

	public void SetAttackLink(attackLink theLink , skillButton theButton)
	{
		theLinkNow = theLink;
		theShowButton = theButton;
		makeShow ();
		theSkillLvupButton.interactable = theLinkNow.canLvup;
	}


	//升级按钮对应的方法
	public void makeLvUp()
	{
		if (theLinkNow)
		{
			theLinkNow.canculateCost ();
			theLinkNow.makeAttackLinkUp ();
			theShowButton.attackLinkBasicInformation = theLinkNow.getInformation (false);
			makeShow ();//重新显示内容
		}
	}


	void makeShow()
	{
		if (!theShowButton ) 
		{
			print ("no");
			return;
		}

		if (!theLinkNow ) 
		{
			print ("no2");
			return;
		}

		
		theSkillNameText.text = theShowButton.theAttackLinkName;
		theSkillBasicInformationText.text = theShowButton.attackLinkBasicInformation;
		theSkillEffectInformationText.text = theShowButton.basicEffect + "\n\n" + theShowButton.effectInformation;
		theSkillLvUpInformationText.text = theShowButton.theAttackkLinkLvUpInformation;

		if (string.IsNullOrEmpty (theSkillEffectInformationText.text))
			theSkillEffectInformationText.text = "此招式无可触发特效";
		//theAttacklink.canculateCost ();
		//attackLinkBasicInformation = theAttacklink.getInformation ();
		//theShowText.text = attackLinkBasicInformation + "\n" + 

	}
}
