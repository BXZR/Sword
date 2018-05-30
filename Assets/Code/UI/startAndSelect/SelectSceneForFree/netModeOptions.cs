using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class netModeOptions : MonoBehaviour {

	//选择人物界面的网络部分的面板

	public InputField  theInputForRoom;
	public Text theExtraShowText;

	public void netModeShow()
	{
		Destroy (theExtraShowText.gameObject);
	}

	public void singleModeShow()
	{
		Destroy ( theInputForRoom.gameObject);
		theExtraShowText.text = "星沉月落夜闻香 素手出锋芒 前缘再续新曲 心有意 爱无伤 \n江湖远 碧空长 路茫茫 闲愁滋味 多感情怀 无限思量";

	}

}
