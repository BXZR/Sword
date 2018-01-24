using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectMonk :effectBasic{

	float basicDamage = 15f;//每一个单位能够给出的伤害
	float timer = 4f;//每一个段时间才能够使用这个伤害
	bool isUsed = false;
	void Start ()
	{
		Init ();
		Destroy (this,timer);
	}
 
	int getCount ()
	{
		Init ();
		int count = 0;
		if (this.thePlayer) 
		{
			//接下来需要的就是对坐标进行审查
			Collider[] emys = Physics.OverlapSphere (this.thePlayer.transform.position, 2);
			for (int i = 0; i < emys.Length; i++) 
			{//开始对相交球体探测物体进行排查
				//相交球最大的问题就是如果自身有碰撞体，自己也会被侦测到
				//print (emys [i].gameObject.name);
				if (emys [i].GetComponent <PlayerBasic> () && emys [i].GetComponent <Collider> ().gameObject != this.thePlayer.gameObject)
					count++;
			}
		}
		return count;
	}

	public override void OnAttack (PlayerBasic aim)
	{
		if (isUsed == false) 
		{
			float damage = getCount () * basicDamage;
			aim.ActerHp -= damage;
			this.thePlayer.OnAttackWithoutEffect (aim, damage, true, true);
			isUsed = true;
		}
	}

	public override void Init ()
	{
		//print ("灭却浮屠发动");
		theEffectName = "灭却浮屠";
		theEffectInformation ="根据身边敌人数量追加下一击的伤害，每存在一个敌人追加"+basicDamage+"真实伤害，冷却时间为"+timer+"秒";
		makeStart ();
 
	}
}
 