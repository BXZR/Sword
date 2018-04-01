using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killerGeter : effectBasic
{
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
			thePlayerKilledThis = attacker;
			GameObject theSoul = GameObject.Instantiate<GameObject>( Resources.Load<GameObject>("effects/theSoul"));
			theSoul.transform.position = this.thePlayer.transform.position;
			int soulCount = (int)(this.thePlayer.ActerHpMax / 100);
			theSoul.GetComponent <popSoulMove> ().makeSTART (attacker.transform , soulCount);
		}
	}
    
}
