using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class columnText : MonoBehaviour {

	//Text文本编程竖版

	void Start () 
	{
		Text theText = this.GetComponent <Text> ();
		if (theText)
			theText.text = systemValues.rowStringToColumn (theText.text , 35 , false);
		Destroy (this);
	}
	

}
