using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soulShowText : MonoBehaviour {

	private Text SoulCountText;

	void Start()
	{
		SoulCountText = this.GetComponent <Text> ();
	}
	void Update () 
	{
		//其实UI交互是需要开一帧的
		if(SoulCountText)
		SoulCountText.text = ""+systemValues.soulCount;
	}
}
