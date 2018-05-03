using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//阴阳点击按钮，用于五灵的阴阳修炼
using UnityEngine.UI;

public class theYinYangButton : MonoBehaviour {

	//用于当做进度条的Image
	public Image theSliderImage;
    //阴阳属性保存
	private lingBasic theWuling;
	//信息刷新
	public wulingEffectInfromation theInformation;

	public void setLing(lingBasic theLingIn)
	{
		theWuling = theLingIn;
		theSliderImage.fillAmount = theLingIn.getLearningPercent ();
	}

	//按钮按下的修炼
	public  void  makeYinYangButtonUse()
	{
		theWuling.learnWuling ();
		theSliderImage.fillAmount = theWuling.getLearningPercent ();
		theInformation.makeFlash ();
	}
}
