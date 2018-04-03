using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixPosition : MonoBehaviour {
	//这个脚本在选人界面使用给游戏人物的模型
	private Vector3 theposition;//固定坐标
	float timer = 3f;//计算时间间隔

	void Start()
	{
		theposition = this.transform.position;
		InvokeRepeating ("fixPositionUse", 0f, timer);
	}

	void fixPositionUse()
	{
		this.transform.position = theposition;
	}
}
