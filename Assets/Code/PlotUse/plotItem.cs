using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//统一控制的接口
using UnityEngine.UI;


public interface plotActions 
{
	void OnStart (plotItem theItem);//开始的时候统一调用
	void OnEnd();//结束的时候统一调用
	void OnUpdate();//每一帧更新的时候统一调用
	//这个方法与OnEnd可以使两段控制
	void OnControlEnd();//强制到达结束状态,但是还没有结束
}


public class plotItem : MonoBehaviour {

	//剧本信息
	//每一步的剧本真正的操作在这里完成
	//没有回环的选择错误的问题，目前还是先抛下有些冗余的东西做一个单纯的东西
	//因为是完全顺序的，在GameObject的排布上就比较简单了
	//直接使用线性排列方式就可以了

	public string thePlotIteTitle = "";//阶段性小标题
	public string thePlotItemExtra= "";//这个剧本帧的额外说明
	public  float theTimeForThisItem = 5f;//这个剧本帧的持续时间
	public bool isEnd = false;
	//各种分项的控制单元
	public List<plotActions> Actions = new List<plotActions> ();

	//一些引用的保存
	public Text theTExtForTalk;
	//外部调用----------------------------------------------------------------------------------------
    //由控制单元同一控制更新


	//每一个剧本帧在运行之前应该初始化
	public  void  OnStart(Text textIn)
	{
		Actions = new List<plotActions> (this.GetComponentsInChildren<plotActions> ());
		//print ("Actions count = " + Actions.Count);
		theTExtForTalk = textIn;
	}


	//在刷新的时候的效果 
	public void OnItemUpdate()
	{
		makeTimer ();
		for (int i = 0; i < Actions.Count; i++)
			Actions [i].OnUpdate();
	}

	//在刚开始这个剧本帧的时候的效果
	public void OnPlaytheItem()
	{

		//print (thePlotIteTitle);
		for (int i = 0; i < Actions.Count; i++)
			Actions [i].OnStart (this);
	}

	//在结束这个剧本帧的时候的效果吧
	public void OnEndtheItem()
	{
		for (int i = 0; i < Actions.Count; i++)
			Actions [i].OnEnd ();
	}

	//强制控制，但是不是结束
	public void OnControlEnd()
	{
		for (int i = 0; i < Actions.Count; i++)
			Actions [i].OnControlEnd();
	}



	//内部实现----------------------------------------------------------------------------------------

	private void makeTimer()
	{
		theTimeForThisItem -= Time.deltaTime;
		//print ("theItem is playing last："+theTimeForThisItem);
		if (theTimeForThisItem < 0)
			isEnd = true;
	}

}
