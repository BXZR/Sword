using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class systemValuesInStartScene : MonoBehaviour {

	//有些变量需要贯通整个游戏
	//例如剧情系统的标记
	//这种类型的标记是完全跨场景的，只有在startScene做唯一一次的刷新
	//所以需要一个特殊的脚本来控制这些变量

	public 
	void Start () 
	{
		systemValues.isInStory = false;
	}
	
	public void getToStoryInStartScene()
	{
		systemValues.isInStory = true;
	}
}
