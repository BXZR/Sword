using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectMonk :effectBasic{

	float basicDamage = 15f;//每一个单位能够给出的伤害
	float timer = 4f;//每一个段时间才能够使用这个伤害
	bool isUsed = false;
	int  EMYCoutExtraEffect = 2;//多于这些敌人触发额外效果
	int maxEMYCountForUse = 5;//最多触发层数
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
			int EMYCount = Mathf.Clamp(getCount (),0,maxEMYCountForUse);
			float damage =  EMYCount  * basicDamage;
			aim.ActerHp -= damage;
			this.thePlayer.OnAttackWithoutEffect (aim, damage, true, true);

			if (EMYCount > EMYCoutExtraEffect) 
			{
				thePlayer.ActerHp += damage;
				//附加的各种效果
				effectBasic [] effects = this.thePlayer.GetComponents<effectBasic> ();
				foreach (effectBasic EF in effects)
					EF.OnHpUp ( damage);
			}

			isUsed = true;
		}
	}

	public override void Init ()
	{
		//print ("灭却浮屠发动");
		theEffectName = "灭却浮屠";
		theEffectInformation ="根据身边敌人数量追加下一击的伤害，每存在一个敌人追加"+basicDamage+"真实伤害\n如果身边敌人超过2个，额外真实伤害将转化为生命值\n额外伤害最多"+maxEMYCountForUse+"层，冷却时间"+ timer +"秒";
		makeStart ();
 
	}
}
 