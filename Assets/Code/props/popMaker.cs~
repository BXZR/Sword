using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class popMaker : PunBehaviour  {

	//生成道济的控制单元
	//这个可以有很多，分散在地图各地

	//道具数组，每一个种类的道具在这里存一个预设物
	public GameObject[] theProps;
	//当前产生的道具
	private GameObject thePropNow = null ;
	//当前生产的道具为空，冷进入冷却，这个事件表示冷却时间
	public float coolingTimer = 1000f;//最开始的时间会比较长，作为等待时间
	//时间备份
	public float coolingTimerMax = 30f;
	//多客户端随机控制，现在还没有做好，因此这只是index方法
	int indexNow = 0;
	//网络控制节点这些是需要同步的
    //一个简单的策略就是直接刷新时间
	public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer) 
	{
		makeFlash ();
	}

	void makeFlash()
	{
		if(thePropNow)
		Destroy (thePropNow.gameObject);
		coolingTimer = coolingTimerMax;
	}

	//当前道具为空，就进入冷却
	//这个方法也是用InvokeRepeat来做
	void WhenThePropNowNull()
	{
		if (thePropNow == null) 
		{
			coolingTimer -= 3f;//一秒计算一次就可以了
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

	//这个只是一个道具，玩家对这个的时间观察似乎也不是很严厉，所以直接每一秒计算一次就好了
	//没有必要很短的时间计算一次
	void Start () 
	{
		InvokeRepeating ("WhenThePropNowNull",1f,3f);	
	}

}
