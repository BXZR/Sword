using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class wulingButton : MonoBehaviour {

	//五灵元素的按钮
	//功能很多，首先是五灵法阵的旋转
	public wulingType theWulingType;//五灵的标记
	public float aimRotationForZ = 0;//五灵法阵目标旋转角度
	public wulingRotation thePanel ;//五灵法阵
	//五灵珠的图像
	public Image theImageForLingZhu;
	//为了记录的静态图片
	public static Image theImageForLingzhuSave;

	//五灵显示和修炼界面引用
	public theWulingYinYangpanel wulingExciesePanel;

	private  static  bool isStarted = false;

	//所有按钮初始化的时候都做一次
	//这是为了保证初始的效果能够完整
	void Start()
	{
		theImageForLingZhu.enabled = false;

		if (isStarted || !systemValues.thePlayer)
  			return;
		 
		isStarted = true;

		//遮掩做是为了保证第一次打开五灵界面的时候能偶一个事先就被选中了
		PointerEventData theData = new PointerEventData (EventSystem.current );//创建事件数据
		//传值：大概理解是：目标Gameobject ，事件数据 ， 类型（与那边接收的时候做匹配（大概））
		ExecuteEvents .Execute<IPointerClickHandler> ( this.gameObject , theData ,ExecuteEvents.pointerClickHandler);
		//WulingButtonOperate ();
		//changeLingzhuImage ();
	}

	//对于这个按钮来说的main方法
	public void  WulingButtonOperate()
	{
		thePanel.rotateFixed (this.aimRotationForZ);
		changeLingzhuImage ();
		if (wulingExciesePanel)
			wulingExciesePanel.setWuling (this.theWulingType);
	}

	//更换灵珠图片
	private void changeLingzhuImage()
	{
		if(theImageForLingzhuSave != null)
			theImageForLingzhuSave.enabled = false;
		theImageForLingzhuSave = theImageForLingZhu;
		theImageForLingzhuSave.enabled = true;
	}

   
}
