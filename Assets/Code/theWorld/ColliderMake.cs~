using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMake : MonoBehaviour {

	//为所有可以二段跳的墙壁打上标记
	//放在世界Root上面
	void Start ()
	{
		Collider[] theColliders = this.GetComponentsInChildren<Collider> ();
		foreach (Collider AS in theColliders)
			AS.tag = "Wall";
		Destroy (this);//这个对象可以销毁了
	}
	
 
}
