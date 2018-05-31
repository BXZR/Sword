using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plotItem : MonoBehaviour {

	//剧本信息
	//每一步的剧本真正的操作在这里完成
	//没有回环的选择错误的问题，目前还是先抛下有些冗余的东西做一个单纯的东西
	//因为是完全顺序的，在GameObject的排布上就比较简单了
	//直接使用线性排列方式就可以了

	public string thePlotIteTitle = "";//阶段性小标题
	public string thePlotItemExtraInformation = "";//这个剧本帧的额外说明
	public  float theTimeForThisItem = 5f;//这个剧本帧的持续时间
	public AudioClip [] theSounds;//这个剧本帧需要发出来的音效组

	public bool isEnd = false;

	private void makeTimer()
	{
		theTimeForThisItem -= Time.deltaTime;
		//print ("theItem is playing last："+theTimeForThisItem);
		if (theTimeForThisItem < 0)
			isEnd = true;
	}


    //由控制单元同一控制更新
	//在刷新的时候的效果 
	public void OnItemUpdate()
	{
		makeTimer ();
	}

	//在刚开始这个剧本帧的时候的效果
	public void OnPlaytheItem()
	{
		print (thePlotIteTitle);
	}

	//在结束这个剧本帧的时候的效果吧
	public void OnEndtheItem()
	{
		
	}
}
