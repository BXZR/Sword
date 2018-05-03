using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用来描述五灵阴阳效果的总的panel
using UnityEngine.UI;


public class theWulingYinYangpanel : MonoBehaviour {

	//当前选择的五灵类型
	wulingType theEypeNow;
	//slider用的Image

	public theYinYangButton theYangButton = null;
	public theYinYangButton theYinButton = null;

	public void setWuling(wulingType theType)
	{
		theEypeNow = theType;
		lingBasic yinWuling = null;
		lingBasic YangWuling = null; 
		wuling theWuling = systemValues.thePlayer.GetComponent <wuling> ();

		for (int i = 0; i < theWuling.lingEffects.Count; i++)
		{
			if (theWuling.lingEffects [i].theType == theEypeNow && theWuling.lingEffects [i].getYinYagType () == 1)
				YangWuling = theWuling.lingEffects [i];
			if (theWuling.lingEffects [i].theType == theEypeNow && theWuling.lingEffects [i].getYinYagType () == 2)
				yinWuling = theWuling.lingEffects [i];
		}
		theYangButton.setLing (YangWuling);
		theYinButton.setLing (yinWuling);
	}
		
}
