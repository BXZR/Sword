using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectLastingDamage : effectBasic{


	int count = 3;
	public float theDamage = 60f;
	void Start () 
	{
		makeStart ();
		InvokeRepeating ("makeDamage",0f,1f);
	}

	private  void  makeDamage()
	{
		theEffectName = "剧毒";
		theEffectInformation = count +"秒内每秒减少"+ theDamage +"生命值";

		this.thePlayer.ActerHp -= theDamage;
		//附加的各种效果
		effectBasic [] effects = this.thePlayer.GetComponents<effectBasic> ();
		foreach (effectBasic EF in effects)
			EF.OnBeAttack(theDamage);
		
		count--;
		if (count < 0)
			Destroy (this);
	}
}
