using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plotController : MonoBehaviour {

	//剧本控制单元
	//因为剧本是线性的，所以直接会使用List就可以了

	//用来显示剧本文本内容的Text
	public Text theTextForTalk;
	//这个是非常重要的所有剧本帧
	[HideInInspector]
	public List<plotItem> thePlotItems = new List<plotItem> ();
	//当前选中的剧本帧
	[HideInInspector]
	public plotItem theItemNow = null;
	private int indexNow = 0;

	//初始化工作在这里完成
	public void  makeStart()
	{
		Time.timeScale = 1f;//预防出现速度问题
		thePlotItems = new List<plotItem>( this.GetComponentsInChildren<plotItem> ());
		if(thePlotItems.Count>0)
		   makePlay(thePlotItems[indexNow]);
	}



	public void makePlay(plotItem theItem)
	{
		if (theItem)
		{
			theItemNow = theItem;
			//说起来这也就是两段的初始化了
			theItem.OnStart (theTextForTalk);
			theItem.OnPlaytheItem ();
		}
	}

	public void makeUpdate(float timer)
	{

	}
		
	void Start () 
	{
		makeStart ();
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
				theItemNow = null;
			else
				makePlay (thePlotItems [indexNow]);
		}

		//下面是一些操作
		if (Input.GetKeyDown (KeyCode.Escape))
			Time.timeScale = Time.timeScale == 0 ? 1 : 0;

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0))
			theItemNow.OnControlEnd ();


	}
}
