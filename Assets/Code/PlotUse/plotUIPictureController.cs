using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class plotUIPictureController : MonoBehaviour , plotActions  {

	//每一个剧本帧的音效控制单元
	public ImageInOut theShowImage;
	public bool isIn;

	//没办法，接口默认pulic
	//开始的时候统一调用
	public  void OnStart (plotItem theItem)
	{
		if (!theShowImage)
			return;
		
		theShowImage.timer = theItem.theTimeForThisItem; 
		if (isIn)
			theShowImage.makeStart ();
		else
			theShowImage.makeEnd ();
	}

	//结束的时候统一调用
	public  void OnEnd()
	{
	}

	//每一帧更新的时候统一调用
	public  void OnUpdate()
	{
	}

	//强制到达结束状态
	public void OnControlEnd()
	{

	}

}
