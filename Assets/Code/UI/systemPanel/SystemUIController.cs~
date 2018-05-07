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
		//theChildPanels [0]是基础背景面板，这一点可以参考风之幻想三的UIO设计
		//但是估计这个面板不会比风三面板复杂，因为这个游戏的游戏乐趣或许在于复杂的装备组合和效果
		//更加在于身临其境的打击感，算是KO和风系列的组合尝试吧
	}

	//关闭系统选项
	//有的时候是不关闭菜单项目的，这里需要做一个标记
	public void shutSystems(bool all = true)
	{
		for (int i = 0; i < theChildPanels.Length; i++)
		{
			theChildPanels [i].gameObject.SetActive (false);
		}
		if (all)
		{
		    Time.timeScale = 1f;//这里控制时间，所以有可能会有其他功能有冲突，务必注意
			systemValues.IsSystemPanelOpened = false;
			Cursor.visible = true;
			systemValues.IsSystemPanelOpened  = false;
		}
		else
		{
			theChildPanels [0].gameObject.SetActive (true);
		}

	}

	//切换选择的时候，只有菜单项和当前选择不动
	public void shutOtherPanels(int index = 0)
	{
		//主菜单默认是0
		for (int i = 1; i < theChildPanels.Length; i++)
		{
			if(i != index)
			theChildPanels [i].gameObject.SetActive (false);
		}
	}

	//打开或者挂关闭某一个界面
	public void controlPanelWithIndex(int index = 0)
	{
		shutOtherPanels(index);
		if (index < theChildPanels.Length && theChildPanels [index] != null) 
		{
			theChildPanels [index].gameObject.SetActive (!theChildPanels [index].gameObject.activeInHierarchy);
		}
		systemValues.messageBoxClose ();//消息框强制关闭
	}


	void Start () 
	{
		shutSystems ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && systemValues.thePlayer && systemValues.isGamming) 
		{
			if (systemValues.IsSystemPanelOpened == false)
			{
				startSystems ();
				systemValues.IsSystemPanelOpened  = !systemValues.IsSystemPanelOpened;
			}
			else
			{
				shutSystems ();
			}
		}
	}
}
