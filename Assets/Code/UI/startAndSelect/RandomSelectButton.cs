
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSelectButton : MonoBehaviour {

	//在选择界面开启/关闭随机数选择功能的按钮
	public Transform theFather;
	private  selectHead [] theSelectHeads;
	bool isWorking = false;//是否正在开启随机选择
	Text theShowText;//显示的文本
	Color theTextColorSave;

	private bool isStarted = false;//是否开启的标记，标识是否可以使用

	void Start () 
	{
		theShowText = this.GetComponentInChildren<Text> ();
		theTextColorSave = theShowText.color;
		Invoke ("makeSTART" , 0.5f);
	}


	//为了防止因为没有构造完全产生引用丢失，这个部分还是先延迟一下
	void makeSTART()
	{
		theSelectHeads = theFather.GetComponentsInChildren<selectHead> ();
		isStarted = true;
	}

	//按钮方法
	public void  operateRandomSelect()
	{
		if (!isStarted)
			return;
		
		if (isWorking == false) 
		{
			InvokeRepeating ("RandomWork" , 0 , 1.5f);
			theShowText.color = Color.magenta;
			isWorking = true;
		}
		else
		{
			CancelInvoke ();
			theShowText.color = theTextColorSave;
			isWorking = false;
		}
	}

	private void RandomWork()
	{
		int indexSelect = Random.Range (0, theSelectHeads.Length);
		theSelectHeads [indexSelect].makePlayer ();
	}
}
