using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleTip : MonoBehaviour {

	// 这是一个标记，只有在特殊的模式之下才会生存
	//这是用于初始化的一个小功能
	//参照的是systemValues的 modeIndex 
	public int modeIndexDoNotDestory = 0;

	void Start () 
	{
		if (systemValues.modeIndex == modeIndexDoNotDestory)
			Destroy (this);
		else
			Destroy (this.gameObject);
	}

}
