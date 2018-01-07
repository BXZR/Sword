using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSpup : effectBasic{

	void Start ()
	{
		makeStart ();
		this.thePlayer.ActerSp += 30;
		Destroy (this);
	}
}

