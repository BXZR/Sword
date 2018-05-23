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
		if (!theWuling.isLearned ())
			systemValues.choiceMessageBoxShow ("五灵修炼", "修炼五灵需要最多消耗当前" + systemValues.learnWulingSpPercent * 100 + "%的斗气，是否修炼？", false, new MesageOperate (trueLearn));
		else
			systemValues.messageTitleBoxShow (theWuling.lingName + "已经领悟" );
	}

	private void trueLearn()
	{
		theWuling.learnWuling ();
		theSliderImage.fillAmount = theWuling.getLearningPercent ();
		theInformation.makeFlash ();
		if (theWuling.isLearned ())
		{
			systemValues.thePlayer.addZizhi ();//资质增加
			systemValues.messageBoxShow ("五灵修炼突破", "灵脉"+theWuling.lingName +"突破成功\n【提升资质并获得五灵特效和初成奖励】\n\n"+theWuling.lingName +"特效："+theWuling.lingEffectName + "\n" + theWuling.wulingInformation() +"\n\n初成奖励："+theWuling.wulingInformationForLearnOver() , true);
			this.GetComponent <buttonWithSound> ().makeSoundShow ();
		}
	}
}
