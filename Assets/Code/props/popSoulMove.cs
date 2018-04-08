using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popSoulMove : MonoBehaviour {

	//死亡的时候的灵魂
	//玩家通过收集这些“魂元”来获得升级连击招数的技能点
   
	//这个魂元容纳的魂魄数量
	public int soulCount = 1;
	//魂元飞向的目标
	public Transform theAim;
	//开始标记，也就是一个延迟效果的开始
	bool isStart = false;

	public void makeSTART(Transform theAimIn , int soulC = 1)
	{
		this.theAim = theAimIn;
		soulCount = soulC;
		Invoke ("theStart" , 5f);

	}

	private void theStart()
	{
		isStart = true;
	}

	void Update ()
	{
		if (isStart && theAim) 
		{
			this.transform.position = Vector3.Lerp (this.transform.position, theAim.transform.position, 3f * Time.deltaTime);
			if (Vector3.Distance (this.transform.position, theAim.transform.position) < 1f) 
			{
				systemValues.soulCount += soulCount;
				theAim.GetComponent<PlayerBasic> ().addJingYan (soulCount * 2 );
				effectBasic[] theEffects = theAim.GetComponentsInChildren<effectBasic> ();
				for (int i = 0; i < theEffects.Length; i++)
					theEffects [i].OnAddSoul (soulCount);
				
				Destroy (this.gameObject);
			}
		}
		else 
		{
			this.transform.Translate (new Vector3 (0,1,0) *0.4f*Time .deltaTime);
		}
	}
}
