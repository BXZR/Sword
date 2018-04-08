using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class thePackagePanelShow : MonoBehaviour {

	//背包界面的总控制单元
	public Text theBasicShowText ;


	
	//因为背包的东西可能会经常换，所以暂时还真需要适时刷新
	//等待后面的事件机制完全了或许就好了
	void Update ()
	{
		if (systemValues.thePlayer)
			theBasicShowText.text = "当前负重: "+ systemValues.thePlayer.weightPercent *100 +"%";
	}
}
