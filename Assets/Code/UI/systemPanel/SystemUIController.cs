using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemUIController : MonoBehaviour {

	//这个脚本是总UI控制单元
	//可以用来控制所有的界面
	//同时所有的界面都会在这里面保存引用


	public GameObject[] theChildPanels;//所有最上层的界面的引用
	//同时，下标为0的就是开始主界面了


	//开启系统选项
	public void startSystems()
	{
		if (systemValues.modeIndex == 0) 
		{
			//单机模式下算是福利，各一个时间暂停
			Time.timeScale = 0;
		}
		if(theChildPanels.Length >0)
		   theChildPanels [0].gameObject.SetActive (true);
	}

	//关闭系统选项
	public void shutSystems()
	{
		for (int i = 0; i < theChildPanels.Length; i++)
		{
			theChildPanels [i].gameObject.SetActive (false);
		}
		Time.timeScale = 1f;//这里控制时间，所以有可能会有其他功能有冲突，务必注意
	}



	void Start () 
	{
		shutSystems ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (systemValues.IsSystemPanelOpened == false)
			{
				startSystems ();
				systemValues.IsSystemPanelOpened  = true;
			}
			else
			{
				shutSystems ();
				systemValues.IsSystemPanelOpened  = false;
			}

		}
	}
}
