using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killerGeter : effectBasic
{

	private static GameObject theSoulProfab = null;
	//这个脚本挂在人身上用来记录击杀者
	PlayerBasic thePlayerKilledThis = null;
	 

	//这个效果用来记录击败这个单位的单位
	public override bool isExtraUse (){return true;}
	public override bool isBE (){return true;}
	public override bool isShowing (){return false;}
	public override bool isPublic (){return true;}

	void Start()
	{
		makeStart ();
	}


	public override void OnBeAttack (PlayerBasic attacker)
	{
		if( (this.thePlayer.ActerHp < 0 || this.thePlayer.isAlive == false) && thePlayerKilledThis == null )
		{
			//print ("dead");
			if (!theSoulProfab)
				theSoulProfab = Resources.Load<GameObject> ("effects/theSoul");
			
			thePlayerKilledThis = attacker;
			GameObject theSoul = GameObject.Instantiate<GameObject>(theSoulProfab);
			theSoul.transform.position = this.transform.position;
			int soulCount = systemValues.soulGet (this.thePlayer);
			theSoul.GetComponent <popSoulMove> ().makeSTART (attacker , soulCount);
		}
	}
    
}
