﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldWoodRandomMove : MonoBehaviour {

	//太极梅花桩地图中的梅花桩是可以移动的
	public float speed = 12f;
	private Vector3 theRotateCenter;//太极图的中心

	void Start()
	{
		theRotateCenter = GameObject.Find ("/theWorld/center").transform .position;
	}


	void OnTriggerEnter(Collider collisioner)
	{ 
		PlayerBasic PB = collisioner.gameObject.GetComponent<PlayerBasic> ();
		if (PB)
			PB.OnBeAttack (40f);
	}
	void Update () 
	{
		this.transform.RotateAround (theRotateCenter , Vector3.up , speed  *Time .deltaTime);
	}
}