using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectShield: effectBasic{

	void Start ()
	{
		makeStart ();
		this.thePlayer.ActerShieldHp += 100;
		Destroy (this);
	}
}

