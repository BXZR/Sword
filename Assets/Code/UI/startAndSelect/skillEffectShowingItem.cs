using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillEffectShowingItem : MonoBehaviour {

	//选人界面的技能效果介绍界面的子项目
	//连击信息存储单元
	attackLinkInformation theAttackLinkInformation;

	//获取控制单元
	private attackLinkController theAttackLinkContrtoller;


	public Button theButtonFoirAttackLink;//连击用按钮
	public Button theEffectForSelfButton;//自己BUFF按钮
	public Button theEffectForEMYButton;//敌人BUFF按钮
	public Button theEffectForShow;//展示连击用按钮
	private informationMouseShow SelfButtonShow;//显示用
	private informationMouseShow EMYButtonShow;//显示用

	//点击发动连招，这个由子按钮进行控制
	public void showAttackLink()
	{
		//print ("the attacklink string = " +  theAttackLinkInformation .attackLinkString);
		theAttackLinkContrtoller.makeAttackLink(theAttackLinkInformation .attackLinkString ,true);
	}

	//构建方法
	public void maketheItem(attackLinkInformation theAttackLinkInformationIn)
	{
		if (string.IsNullOrEmpty (theAttackLinkInformationIn.attackLinkString) == false)
		{
			theAttackLinkContrtoller = theAttackLinkInformationIn.thePlayer.GetComponentInChildren<attackLinkController> ();
			theAttackLinkContrtoller.makeStart ();
		}
		else 
		{
			Destroy (theButtonFoirAttackLink.gameObject);
		}

		theAttackLinkInformation = theAttackLinkInformationIn;
		SelfButtonShow = theEffectForSelfButton.GetComponent <informationMouseShow> ();
		EMYButtonShow = theEffectForEMYButton.GetComponent<informationMouseShow> ();

		theButtonFoirAttackLink.GetComponentInChildren<Text> ().text = theAttackLinkInformation.attackLinkName;
		theButtonFoirAttackLink.GetComponent <informationMouseShow> ().showText = theAttackLinkInformation.attackLinkInformationText;
		if (string.IsNullOrEmpty (theAttackLinkInformation.theEffectForSelfName))
		{
			Destroy (theEffectForSelfButton.gameObject);
		}
		else
		{
			theEffectForSelfButton.GetComponentInChildren<Text> ().text = theAttackLinkInformation.theEffectForSelfName;
			SelfButtonShow.showText = theAttackLinkInformation.theEffectForSelfInformaion;
		}

		if (string.IsNullOrEmpty (theAttackLinkInformation.theEffectForEMYName))
		{
			Destroy (theEffectForEMYButton.gameObject);
		}
		else
		{
			theEffectForEMYButton.GetComponentInChildren<Text> ().text = theAttackLinkInformation.theEffectForEMYName;
			EMYButtonShow.showText = theAttackLinkInformation.theEffectForEMYInformaion;
		}
		if (string.IsNullOrEmpty (theAttackLinkInformationIn.attackLinkString))
			Destroy (theEffectForShow.gameObject);
	}
		
}
