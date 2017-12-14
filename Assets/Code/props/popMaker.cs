using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popMaker : MonoBehaviour {

	//生成道济的控制单元
	//这个可以有很多，分散在地图各地

	//道具数组，每一个种类的道具在这里存一个预设物
	public GameObject[] theProps;
	//当前产生的道具
	private GameObject thePropNow = null ;
	//当前生产的道具为空，冷进入冷却，这个事件表示冷却时间
	public float coolingTimer = 30f;
	//时间备份
	public float coolingTimerMax = 30f;
	//多客户端随机控制，现在还没有做好，因此这只是index方法
	int indexNow = 0;

	//当前道具为空，就进入冷却
	//这个方法也是用InvokeRepeat来做
	void WhenThePropNowNull()
	{
		if (thePropNow == null) 
		{
			coolingTimer -= systemValues.updateTimeWait;
			if (coolingTimer < 0) 
			{
				coolingTimer = coolingTimerMax;
				if (theProps.Length > 0) 
				{
					thePropNow = GameObject.Instantiate<GameObject>(theProps[indexNow]);
					thePropNow.transform.position = this.transform.position;
					indexNow++;
					if (indexNow >= theProps.Length)
						indexNow = 0;//顺序生成
				}
			}
		}
	}

	void Start () 
	{
		InvokeRepeating ("WhenThePropNowNull",1f,systemValues.updateTimeWait);	
	}

}
