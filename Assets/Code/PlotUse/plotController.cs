using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plotController : MonoBehaviour {

	//剧本控制单元
	//因为剧本是线性的，所以直接会使用List就可以了

	//用来显示剧本文本内容的Text
	public Text theTextForTalk;
	//人物头像
	public Image theHeadPicture1;
	public Image theHeadPicture2;

	//这个是非常重要的所有剧本帧
	[HideInInspector]
	public List<plotItem> thePlotItems = new List<plotItem> ();
	//当前选中的剧本帧
	[HideInInspector]
	public plotItem theItemNow = null;
	private int indexNow = 0;
	//是否正式开始了的标记
	private bool isStarted = false;

	//初始化工作在这里完成
	public void  makeStart()
	{
		thePlotItems = new List<plotItem>( this.GetComponentsInChildren<plotItem> ());
		if(thePlotItems.Count>0)
		   makePlay(thePlotItems[indexNow]);
		isStarted = true;
	}



	public void makePlay(plotItem theItem)
	{
		if (theItem)
		{
			theItemNow = theItem;
			//说起来这也就是两段的初始化了
			theItem.OnStart (theTextForTalk , theHeadPicture1 ,theHeadPicture2);
			theItem.OnPlaytheItem ();
		}
	}

	public void makeUpdate(float timer)
	{

	}


	public void makeSkip()
	{
		if (!isStarted)
			return;
		
		for (int i = 0; i < thePlotItems.Count - 1; i++)
			thePlotItems [i].theTimeForThisItem = 0.01f;
		
		//indexNow = thePlotItems.Count - 1;
		makePlay (thePlotItems [indexNow]);
	}
		
	void Start () 
	{
		Invoke ("makeStart", 3f);
	}

	void Update () 
	{
		if (!theItemNow) return;

		theItemNow.OnItemUpdate ();

		if (theItemNow.isEnd)
		{
			theItemNow.OnEndtheItem ();
			indexNow++;
			if (indexNow >= thePlotItems.Count) 
			{
				theItemNow = null;
				try{UnityEngine.SceneManagement.SceneManager.LoadScene (systemValues.getNextStoryScene());}
				catch{print ("not a scene");}
			}
			else
				makePlay (thePlotItems [indexNow]);
		}
			

		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0))&& !systemValues.isSystemUIUsing())
			theItemNow.OnControlEnd ();


	}
}
