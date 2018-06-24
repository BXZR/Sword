using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillButton : MonoBehaviour {

  //技能效果界面的选择按钮
  //也是生成的过程中保存各种显示信息的地方

	//招式名称
	public string theAttackLinkName = "";
	//招式升级信息
	public string theAttackkLinkLvUpInformation = "";
	//基础的连招信息
	public string attackLinkBasicInformation = "";
	//是否触发特效
	public string basicEffect = "";
	//特效信息
	public string effectInformation = "";
	//招式等级特效奖励
	public string effectExtraWithAttackLinkLV =  "";
	//用来显示的文本
	public skillInformationPanel theInformationPanel;
	//记录连招引用
	public attackLink theAttacklink;

	//可能有一些特殊的初始化需要按照一定顺序进行
	public void makeStart()
	{
		makeFlash ();
	}

	public void makeFlash()
	{
		theAttackLinkName = theAttacklink.skillName;
		if (theAttacklink.canLvup)
			theAttackLinkName += "\nLV." + theAttacklink.theAttackLinkLv;
		else
			theAttackLinkName += "\n[不可升级]";

		attackLinkBasicInformation = theAttacklink.getInformation (false);
		theAttackkLinkLvUpInformation = theAttacklink.getLvUpInfotrmation ();

	}

	public void makeShow()
	{
		makeFlash ();
		theInformationPanel.SetAttackLink (this.theAttacklink , this);
	}
}
