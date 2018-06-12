using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class HitText : MonoBehaviour {

	//显示当前连击次数的TEXT
	//这个text是自主控制的

	Text theTextForShow;
	void Start () 
	{
		theTextForShow = this.GetComponent <Text> ();
		InvokeRepeating ("makeUpdate" , 0f , 0.2f);
	}
	
	void makeUpdate()
	{
		if (systemValues.hitCount == 0)
			theTextForShow.text = "";
		else
			theTextForShow.text = systemValues.hitCount + "Hit";

		systemValues.updateHit (0.2f);
	}
}
