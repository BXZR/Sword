using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillButton : MonoBehaviour {

  //技能效果界面的选择按钮
  //也是生成的过程中保存各种显示信息的地方

	//基础的连招信息
	public string attackLinkBasicInformation = "";
	//是否触发特效
	public string basicEffect = "";
	//特效信息
	public string effectInformation = "";
	//用来显示的文本
	public Text theShowText;

	public void makeShow()
	{
		theShowText.text = attackLinkBasicInformation + "\n" + basicEffect + "\n\n" + effectInformation;
	}
}
