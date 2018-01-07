using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectHpUp : effectBasic{

	void Start ()
	{
		makeStart ();
		this.thePlayer.ActerHp += 150;
		Destroy (this);
	}
	 
}
