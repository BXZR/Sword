using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScaner : MonoBehaviour {

	//观察显示血条
	//主动搜索的策略而不是被动接受的策略
	//搜索会有开销，但是会减少整体显示的时间和内容，似乎更为合适
	//有这个脚本的单位以看到BloodBasic

	List<GameObject> theEMY;
	public float angle = 30f;
	public float distance = 4f;
	private PlayerBasic thePlayer;//这个脚本是依附于playerBasic上面的，没有玩家，就没有必要展示了

	void Start ()
	{
		thePlayer = this.GetComponent <PlayerBasic> ();
		if (thePlayer)
		{
			angle = thePlayer.theViewAreaAngel;
			distance = thePlayer.theViewAreaLength;
			InvokeRepeating ("searchAIMs", 2f, 0.5f); 
		} 
		else 
		{
			Destroy (this);
		}
	}
 
	//个人认为比较稳健的方法
	//传入的是攻击范围和攻击扇形角度的一半
	//选择目标的方法，这年头普攻都是AOE
	void searchAIMs()//不使用射线而是使用向量计算方法
	{
		//print ("systemValues.isGamming = "+ systemValues.isGamming );
		if (!systemValues.isGamming)
			return;
		//视野很有可能可以受到限制，而是这也许就是“致盲”效果的初始阶段了
		angle = thePlayer.theViewAreaAngel;
		distance = thePlayer.theViewAreaLength;
		theEMY = systemValues.searchAIMs (angle,distance,this.transform);
		foreach (GameObject B in theEMY) 
		{
			BloodBasic BS = B.GetComponent <BloodBasic> ();
			if(BS)
			BS.flashBloodShowTimer ();
		}

	}

}
