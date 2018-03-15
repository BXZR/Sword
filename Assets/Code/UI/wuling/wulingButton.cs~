using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wulingButton : MonoBehaviour {

	//五灵元素的按钮
	//功能很多，首先是五灵法阵的旋转
	public float aimRotationForZ = 0;//五灵法阵目标旋转角度
	public wulingRotation thePanel ;//五灵法阵
	//五灵珠的图像
	public Image theImageForLingZhu;
	//为了记录的静态图片
	public static Image theImageForLingzhuSave;

	//所有按钮初始化的时候都做一次
	//这是为了保证初始的效果能够完整
	void Start()
	{
		WulingButtonOperate ();
	}

	//对于这个按钮来说的main方法
	public void  WulingButtonOperate()
	{
		thePanel.rotateFixed (this.aimRotationForZ);
		changeLingzhuImage ();
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
