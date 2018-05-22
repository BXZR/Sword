using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//五灵效果显示用的Text
using UnityEngine.UI;


public class wulingEffectInfromation : MonoBehaviour {

	//五灵信息分开吧，有些东西还是需要多写点的
	private Text theText ;
	//引用保存
	private wuling theWuling;
	private int  thetypeNow;
	public Text theTitleText;

	void Start()
	{
		theText = this.GetComponent<Text> ();
	}


	public void makeShow(int typeIn)
	{
		if (!theText) return ;
		switch (typeIn) 
		{
		case 1:
			{
				thetypeNow = typeIn;
				showInformation (wulingType.wind);
				makeEffectTitle("风之肆拂•无阻不透");
			}break;
		case 2:
			{
				thetypeNow = typeIn;
				showInformation (wulingType.thunder);
				makeEffectTitle("雷之肃敛•无坚不摧");
			}break;
		case 3:
			{
				thetypeNow = typeIn;
				showInformation (wulingType.water);
				makeEffectTitle("水之润下•无孔不入");
			}break;
		case 4:
			{
				thetypeNow = typeIn;
				showInformation (wulingType.fire);
				makeEffectTitle("火之炎上•无物不焚");
			}break;
		case 5:
			{
				thetypeNow = typeIn;
				showInformation (wulingType.earth);
				makeEffectTitle("土之养化•无物不融");
			}break;
		}
	}


	private void showInformation(wulingType theWulingTypeIn)
	{
		if (!theText) return ;
		theText.text = getInfromationFromPlayer(theWulingTypeIn);
	}

	private void makeEffectTitle(string information)
	{
		if (!theTitleText) return ;
		theTitleText.text = systemValues.rowStringToColumn (information , 15);
	}
    //私有分类方法
	private string getInfromationFromPlayer(wulingType thetype)
	{
		if (!theText) 
		{
			print ("There is no system text");
			return "";
		}
		string theinformation = "";
		if (!systemValues.thePlayer) 
		{
			print ("There is no system Player");
			return theinformation;
		}
		if(!theWuling)
		    theWuling = systemValues.thePlayer.GetComponent <wuling> ();
		if (!theWuling) 
		{
			print ("There is no wuling effect");
			return theinformation;
		}
		
		for (int i = 0; i < theWuling.lingEffects.Count; i++)
			if (theWuling.lingEffects [i].theType == thetype)
			{
				string information = "【" + theWuling.lingEffects [i].lingName  +"  "+theWuling.lingEffects [i].lingEffectName +"】\n" + theWuling.lingEffects [i].wulingInformation () + "\n";
				if (theWuling.lingEffects [i].isLearned ())
					theinformation += systemValues.BESkillColor +  information + systemValues.colorEnd;
				else
					theinformation += information;
				theinformation += "\n";
			}
		return theinformation;
	}
	public void makeFlash()
	{
		makeShow(thetypeNow);
	}

	//初次学成信息
	public void getLearnedOverGetInformation()
	{
		try
		{
			if(!theWuling)
				theWuling = systemValues.thePlayer.GetComponent <wuling> ();
			
			string information = "";
			for (int i = 0; i < theWuling.lingEffects.Count; i++) 
			{
				information += theWuling.lingEffects [i].lingName +" "+theWuling.lingEffects [i].wulingInformationForLearnOver();
				if(theWuling.lingEffects [i].isLearned())
					information += "【已获得】";
				information += "\n";
			}
			systemValues.messageBoxShow ("初成奖励" , information , true);
		}
		catch
		{
			systemValues.messageBoxShow ("出错" , "暂时无法查询得到五灵初成奖励信息" , false);
			print ("暂时无法查询得到五灵初成奖励信息");//顺带用输出在控制台定位一下这个错误
		}
	}
}
