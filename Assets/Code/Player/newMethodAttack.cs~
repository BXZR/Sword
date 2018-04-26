using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class newMethodAttack : MonoBehaviour {

	//动画时间点伤害脚本
	//这个是非常严格的攻击脚本
	//这个是在动画中插入关键帧的方法，没有使用碰撞检测的方法制作的
	//但同时也有限制，就是只能用于这种唯一目标的游戏而没有办法很好地模拟武器AOE

	private List<GameObject> theEMY= new List<GameObject> ();//为了打得爽，其实所有攻击都是AOE，AOE获取方式为自制的扇形检测
	private PlayerBasic thePlayer;//自身
	float theDistance = 0;//距离中间变量

	private void extraEffectSELF()
	{
		if (systemValues.isNullOrEmpty (thePlayer . conNameToSELF) == false) //效果不可叠加
		{
			System.Type theType = System.Type.GetType (thePlayer . conNameToSELF);
			effectBasic theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
			if (!theEffect)
			{
				try
				{
					thePlayer.gameObject.AddComponent (theType);
					theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
					theEffect.SetAttackLinkIndex(thePlayer.EffectAttackLinkIndex);
					thePlayer.EffectAttackLinkIndex = 0;//刷新为初始数值
				}
				catch (Exception E)
				{
					print (E.ToString());
					//无法添加这个效果
					//那么就转换成恢复效果，恢复2生命
					thePlayer.ActerHp += 2f;
					//print ("canNotAddSELF");
				}
			} 
			else
			{
				theEffect.updateEffect ();
				theEffect.SetAttackLinkIndex(thePlayer.EffectAttackLinkIndex);
			}
			thePlayer . conNameToSELF = "";
		}
	}
		
	private void extraDamageEffect(PlayerBasic playerAim)//额外添加挂在的计算脚本
	{
		if (systemValues.isNullOrEmpty (thePlayer . conNameToEMY) == false)//效果不可叠加
		{
			System.Type theType = System.Type.GetType (thePlayer.conNameToEMY);
			effectBasic theEffect = playerAim.gameObject.GetComponent (theType) as effectBasic;
			if(!theEffect)
			{
				try
				{
					playerAim.gameObject.AddComponent (theType );
					//print("makeEffect");
				}
				catch(Exception E)
				{
					print (E.ToString());
					//无法添加这个效果
					//那么就转换成伤害，造成2点真实伤害
					thePlayer.OnAttack (playerAim,2,true);
					//print ("canNotAddEMY");
				}
			}
			else
			{
				theEffect.updateEffect ();
				theEffect.SetAttackLinkIndex(thePlayer.EffectAttackLinkIndex);
				//print ("update");
			}
			thePlayer .conNameToEMY= "";
		}
	}

	//makeDamage 有些动画是不造成伤害的
	//吐槽，这个方法的调用限制有点多，实时上传如多个类型的参数就会报错......（到手的动画资源大多数只读，所以关键帧方法还是要用这种的）
	//这个是这个机制不够灵活的地方
	/*
	 *makeDamage参数的多种用途
	 *1如果非正则可以造成伤害
	 *2 如果为负数则要根据这个值减小攻击距离
	 *3 如果是整数就无法造成伤害
	 */
	public  void attackForAnimation( float makeDamage)//攻击方法（带伤害）
	{
		//print ("prepareToAttack");
			//防止空引用
			try
			{
			if (!thePlayer)
				thePlayer = this.GetComponentInParent <PlayerBasic> ();
			//if (!theEMY)
				//theEMY = systemValues.getEMY (this.thePlayer.transform).GetComponent <PlayerBasic> ();
			}
			catch
			{
				//因为这个方法与动画播放的绑定比较紧密，因此在查看的界面中有可能会出问题
				//如果没有获取到引用就说明是某些特殊的调用方式
			}
			if(thePlayer)
			{
			    extraEffectSELF ();//添加自身特效
			}
		  
		    if (thePlayer.canAttack) 
			{
				Attack (makeDamage);
				//print ("attack!");
			}
	}



	List<GameObject> toDelete  = new List<GameObject> ();
	List<PlayerBasic> toUSePlayerBasic = new List<PlayerBasic> ();
	//如果是AI需要加入额外的检查
	private void check()
	{
		toDelete.Clear ();
		toUSePlayerBasic.Clear ();

		//AI之间不会自己攻击自己 
		if (this.thePlayer.gameObject.tag == "AI") 
		{
			for (int i = 0; i < theEMY.Count; i++) 
			{
				if (theEMY [i].tag == "AI")
				{
					toDelete.Add (theEMY [i]);
				}
			}
			for (int i = 0; i < toDelete.Count; i++) 
			{
				theEMY.Remove (toDelete[i]);
			}
		}
		//获取playerBasic引用用来真正地攻击

		for (int i = 0; i < theEMY.Count; i++)
		{
			PlayerBasic PB = theEMY [i].GetComponent<PlayerBasic> ();
			if (PB)
				toUSePlayerBasic.Add (PB );
		}
	}

	private List<PlayerBasic> EMYUse = new List<PlayerBasic> ();
	//真正的攻击方法
	private void Attack( float makeDamage)
	{
		//如果makeDamage是负数，就说明制作获得效果，但是不攻击
		float theDistanceCheck = thePlayer.theAttackAreaLength;
		if (makeDamage >= 0) 
		{
			EMYUse.Clear ();
			theDistanceCheck += makeDamage;
			theEMY = systemValues.searchAIMs (thePlayer.theAttackAreaAngel, theDistanceCheck,thePlayer.transform);
			check ();

			//print ("theEMY.count " + theEMY.Count);
			for (int i = 0; i < toUSePlayerBasic.Count; i++) 
			{
				theDistance = Vector3.Distance (thePlayer.transform.position, toUSePlayerBasic [i].transform.position);
				//print (theDistanceCheck);
				if (theDistance <= theDistanceCheck)
				{
					if (thePlayer && toUSePlayerBasic [i]) 
					{
						if (makeDamage >= 0)
						{//有些时候仅仅是增加脚本，例如“斗气爆发”不具备攻击效果
							thePlayer.OnAttack (toUSePlayerBasic [i], 0, false);//造成直接的伤害
							extraDamageEffect (toUSePlayerBasic [i]);//添加额外的计算脚本，每个脚本的效果由脚本自己决定
							//print(theEMY[i].name+" is being attacked");
						}
					}
				}
			}
		}
	}

}
