﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectLastingDamage : effectBasic{


	int count = 3;
	public float theDamage = 60f;
	void Start () 
	{
		makeStart ();
		InvokeRepeating ("makeDamage",0f,1f);
	}

	private  void  makeDamage()
	{
		this.thePlayer.ActerHp -= theDamage;
		count--;
		if (count < 0)
			Destroy (this);
	}
}