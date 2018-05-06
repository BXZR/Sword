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

	//五灵阴阳信息介绍
	//说白了就是五灵界面的规则
	public  void getWulngYinYangInformation()
	{
		string informaiton = "天地五灵生生不息，为世间最强力量，而五灵又分阴阳，各有神通，修炼之可获强大效能\n\n点击五灵法阵中的太极阴阳按钮即可消耗当前"+systemValues.learnWulingSpPercent*100+"%的斗气修炼五灵阴阳，修炼完满即可获得额外的特效和灵力的自我增长";
		systemValues.messageBoxShow ( "五灵修炼", informaiton , true);
	}
		
}
