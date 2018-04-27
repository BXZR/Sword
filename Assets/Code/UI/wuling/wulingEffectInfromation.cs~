using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//五灵效果显示用的Text
using UnityEngine.UI;


public class wulingEffectInfromation : MonoBehaviour {

	//五灵信息分开吧，有些东西还是需要多写点的

	//风
	public void makeEffectForWind()
	{
		this.GetComponent<Text> ().text = "风之肆拂，无阻不透";
		this.GetComponent<Text> ().text += "\n\n"+getInfromationFromPlayer(wulingType.wind);
	}

	//雷
	public void makeEffectForThunder()
	{
		this.GetComponent<Text> ().text = "雷之肃敛，无坚不摧";
		this.GetComponent<Text> ().text += "\n\n"+getInfromationFromPlayer(wulingType.thunder);
	}

	//水
	public void makeEffectForWater()
	{
		this.GetComponent<Text> ().text = "水之润下，无孔不入";
		this.GetComponent<Text> ().text += "\n\n"+getInfromationFromPlayer(wulingType.water);
	}

	//火
	public void makeEffectForFire()
	{
		this.GetComponent<Text> ().text = "火之炎上，无物不焚";
		this.GetComponent<Text> ().text += "\n\n"+getInfromationFromPlayer(wulingType.fire);
	}

	//土
	public void makeEffectForEarth()
	{
		this.GetComponent<Text> ().text = "土之养化，无物不融";
		this.GetComponent<Text> ().text += "\n\n"+getInfromationFromPlayer(wulingType.earth);
	}


    //私有分类方法
	private string getInfromationFromPlayer(wulingType thetype)
	{
		string theinformation = "";
		if (!systemValues.thePlayer)
			return theinformation;
		wuling theWuling = systemValues.thePlayer.GetComponent <wuling> ();
		if (!theWuling)
			return theinformation;
		for (int i = 0; i < theWuling.lingEffects.Count; i++)
			if (theWuling.lingEffects [i].theType == thetype)
				theinformation += "【"+theWuling.lingEffects[i].lingName+"】\n"+theWuling.lingEffects [i].wulingInformation ()+"\n";
		return theinformation;
	}
}
