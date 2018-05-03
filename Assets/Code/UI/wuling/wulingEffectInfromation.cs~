using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//五灵效果显示用的Text
using UnityEngine.UI;


public class wulingEffectInfromation : MonoBehaviour {

	//五灵信息分开吧，有些东西还是需要多写点的
	private Text theText ;
	void Start()
	{
		theText = this.GetComponent<Text> ();
	}

	//风
	public void makeEffectForWind()
	{
		if (!theText) return ;
		thetypeNow = wulingType.wind;
		theText.text = "风之肆拂，无阻不透";
		theText.text += "\n\n"+getInfromationFromPlayer(wulingType.wind);
	}

	//雷
	public void makeEffectForThunder()
	{
		if (!theText) return ;
		thetypeNow = wulingType.thunder;
		theText.text = "雷之肃敛，无坚不摧";
		theText.text += "\n\n"+getInfromationFromPlayer(wulingType.thunder);
	}

	//水
	public void makeEffectForWater()
	{
		if (!theText) return ;
		thetypeNow = wulingType.water;
		theText.text = "水之润下，无孔不入";
		theText.text += "\n\n"+getInfromationFromPlayer(wulingType.water);
	}

	//火
	public void makeEffectForFire()
	{
		if (!theText) return ;
		thetypeNow = wulingType.fire;
		theText.text = "火之炎上，无物不焚";
		theText.text += "\n\n"+getInfromationFromPlayer(wulingType.fire);
	}

	//土
	public void makeEffectForEarth()
	{
		if (!theText) return ;
		thetypeNow = wulingType.earth;
		theText.text = "土之养化，无物不融";
		theText.text += "\n\n"+getInfromationFromPlayer(wulingType.earth);
	}


	//引用保存
	private wuling theWuling;
	private wulingType thetypeNow;

    //私有分类方法
	private string getInfromationFromPlayer(wulingType thetype)
	{
		if (!theText)
			return "";
		string theinformation = "";
		if (!systemValues.thePlayer)
			return theinformation;
		if(!theWuling)
		    theWuling = systemValues.thePlayer.GetComponent <wuling> ();
		if (!theWuling)
			return theinformation;
		
		for (int i = 0; i < theWuling.lingEffects.Count; i++)
			if (theWuling.lingEffects [i].theType == thetype)
			{
				string information = "【" + theWuling.lingEffects [i].lingName + "】\n" + theWuling.lingEffects [i].wulingInformation () + "\n";
				if (theWuling.lingEffects [i].isLearned ())
					theinformation += systemValues.BESkillColor +  information + systemValues.colorEnd;
				else
					theinformation += information;
			}
		return theinformation;
	}
	public void makeFlash()
	{
		if (!theText)
			return;
		switch (thetypeNow) 
		{
			case wulingType.water:{makeEffectForWater ();}break;
			case wulingType.wind:{makeEffectForWind ();}break;
			case wulingType.fire:{makeEffectForFire();}break;
			case wulingType.thunder:{makeEffectForThunder ();}break;
			case wulingType.earth:{makeEffectForEarth ();}break;
		}
	}
}
