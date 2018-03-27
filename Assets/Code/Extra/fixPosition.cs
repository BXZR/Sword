using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixPosition : MonoBehaviour {

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
