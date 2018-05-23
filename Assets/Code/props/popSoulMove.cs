using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popSoulMove : MonoBehaviour {

	//死亡的时候的灵魂
	//玩家通过收集这些“灵力”来获得升级连击招数的技能点
   
	//这个魂元容纳的灵力数量
	public int soulCount = 1;
	//魂元灵力飞向的目标
	public PlayerBasic theAim;
	//开始标记，也就是一个延迟效果的开始
	bool isStart = false;

	public void makeSTART(PlayerBasic theAimIn , int soulC = 1)
	{
		this.theAim = theAimIn;
		soulCount = soulC;
		Invoke ("theStart" , 4f);//转向运动

	}

	private void theStart()
	{
		isStart = true;
	}

	//获取灵力、和经验的方法
	void makeGet()
	{
		systemValues.soulCount += soulCount;
		theAim.addJingYan (soulCount * 2 );
		effectBasic[] theEffects = theAim.GetComponentsInChildren<effectBasic> ();
		for (int i = 0; i < theEffects.Length; i++)
			theEffects [i].OnAddSoul (soulCount);

		CancelInvoke ();
		isStart = false;
		systemValues.savePopSoul (this);
	}

	void Update ()
	{
		if (isStart && theAim) 
		{
			this.transform.position = Vector3.Lerp (this.transform.position, theAim.transform.position, 3f * Time.deltaTime);
			if (Vector3.Distance (this.transform.position, theAim.transform.position) < 1f)
				makeGet ();
		}
		else 
		{
			this.transform.Translate (Vector3.up * 0.4f * Time .deltaTime, Space.World);
		}
	}
}
