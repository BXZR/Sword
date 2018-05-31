using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plotController : MonoBehaviour {

	//剧本控制单元
	//因为剧本是线性的，所以直接会使用List就可以了

	public List<plotItem> thePlotItems = new List<plotItem> ();
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
		if (theItemNow) 
		{
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
		}
	}
}
