using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class props : MonoBehaviour {

	//道具的效果其实也是通过effectBasic的方法实现的
	//字符串不同，效果自然也就不同
	public string nameForEffectOfProp;
	private bool isUsed = false;//简单的标记
	//道具效果在某一个物体上面生效
	//其实就是添加一个脚本
	//难点就是怎么做网络上的同步
	//目前是一个非常简单的思路，就是依靠trandform的同步
	//直接用碰撞在每一个客户端自行解决
	//但是就怕不完全同步的情况
	void gameEffect(GameObject aim)
	{
		aim.gameObject.AddComponent (System.Type.GetType (nameForEffectOfProp));
		Destroy (this.gameObject);
	}


	void OnTriggerEnter(Collider collisioner)
	//void OnCollisionEnter(Collision collisioner)
	{
		if (collisioner.gameObject.GetComponent <PlayerBasic> ())
		{
			gameEffect(collisioner.gameObject);
		}
	}
}
