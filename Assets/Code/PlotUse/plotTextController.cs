using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plotTextController : MonoBehaviour, plotActions {

	//剧本文本对话Text的文字显示
    

	public string speakerName = "";//说话的人
	public string speakerTalk = "";//shuochulaidehua 
	private Text theText;
	private string theShowString = "";
	private int indexNow = 0;
	private float timerWait =1f;

	//没办法，接口默认pulic
	//开始的时候统一调用
	public  void OnStart (plotItem theItem)
	{
		theText = theItem.theTExtForTalk;

		timerWait = speakerTalk.Length == 0 ? 0.01f : theItem.theTimeForThisItem * 0.75f / speakerTalk.Length;
		theShowString = speakerName + "\n";
		theText.text = theShowString;
		InvokeRepeating ("showTheTextWithUpdate", 0f, timerWait);
	}

	//结束的时候统一调用
	public  void OnEnd()
	{
		if(theText)
		    theText.text = "";
		CancelInvoke ();
	}

	//每一帧更新的时候统一调用
	public  void OnUpdate()
	{

	}

	//私有内部方法------------------------------------------

	void showTheTextWithUpdate()
	{
		if (indexNow < speakerTalk.Length)
		{
			//print ("show the text");
			theShowString += speakerTalk [indexNow];
			indexNow++;
			theText.text = theShowString;
		}
	}
}
