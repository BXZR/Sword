using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plotTextController : MonoBehaviour, plotActions {

	//剧本文本对话Text的文字显示
    

	public string speakerName = "";//说话的人
	public string speakerTalk = "";//说出来的话

	private plotItem thePlotItemForThis;//这个剧本帧
	private string theShowString = "";
	private int indexNow = 0;
	private float timerWait =1f;

	static List<Sprite> theImageBuff = new List<Sprite> ();

	//没办法，接口默认pulic
	//开始的时候统一调用
	public  void OnStart (plotItem theItem)
	{
		thePlotItemForThis = theItem;

		if (string.IsNullOrEmpty (speakerTalk))
			thePlotItemForThis.theTextForTalk.transform.parent.gameObject.SetActive (false);
		else
			thePlotItemForThis.theTextForTalk.transform.parent.gameObject.SetActive (true);

		timerWait = speakerTalk.Length == 0 ? 0.01f : theItem.theTimeForThisItem * 0.75f / speakerTalk.Length;
		theShowString = speakerName + "\n";
		thePlotItemForThis.theTextForTalk.text = theShowString;
		InvokeRepeating ("showTheTextWithUpdate", 0f, timerWait);

		thePlotItemForThis.HeadImage1.sprite = loadHeadImage (speakerName);
		thePlotItemForThis.HeadImage2.sprite = loadHeadImage ("noOne");
	}


	//结束的时候统一调用
	public  void OnEnd()
	{
		if(thePlotItemForThis.theTextForTalk)
			thePlotItemForThis.theTextForTalk.transform.parent.gameObject.SetActive (false);
		
		CancelInvoke ();
	}

	//每一帧更新的时候统一调用
	public  void OnUpdate()
	{

	}

	//强制到达结束状态
	public void OnControlEnd()
	{
		thePlotItemForThis.theTextForTalk.text = speakerName + "\n" +speakerTalk;
		indexNow = speakerTalk.Length;
	}

	//私有内部方法------------------------------------------

	private Sprite loadHeadImage(string theChineseName)
	{
		//检查缓冲区
		for (int i = 0; i < theImageBuff.Count; i++)
			if (theImageBuff[i] && theImageBuff [i].name == theChineseName)
				return theImageBuff [i];

		//需要加载
		int index = -1;
		for (int i = 0; i < systemValues.playerNames.Length; i++) {
			if (systemValues.playerNames [i] == theChineseName) {
				index = i;
				break;
			}
		}
		if (index >= 0)
		{
			Sprite theSprite = systemValues.makeLoadSprite ("playerHeadPicture/" + systemValues.playerHeadNames [index]);
			theSprite.name = theChineseName;
			theImageBuff.Add (theSprite);
			return theSprite;
		}
		else 
		{
			Sprite theSprite = systemValues.makeLoadSprite ("playerHeadPicture/noOne");
			theSprite.name = theChineseName;
			theImageBuff.Add (theSprite);
			return theSprite;
		}

	}

	private void showTheTextWithUpdate()
	{
		if (indexNow < speakerTalk.Length)
		{
			//print ("show the text");
			theShowString += speakerTalk [indexNow];
			indexNow++;
			thePlotItemForThis.theTextForTalk.text = theShowString;
		}
	}
}
