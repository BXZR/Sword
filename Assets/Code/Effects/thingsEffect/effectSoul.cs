using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSoul : effectBasic{


	void Start ()
	{
		//只有本地的唯一玩家才会真的把这个灵力收入囊中
		if(this.GetComponent<PlayerBasic> () == systemValues.thePlayer)
		    systemValues.soulCount += 30;
		//systemValues.messageTitleBoxShow ("获得30灵力");
		Destroy (this);
	}

}
