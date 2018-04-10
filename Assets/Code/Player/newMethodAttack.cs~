using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class newMethodAttack : MonoBehaviour {

	//动画时间点伤害脚本
	//这个是非常严格的攻击脚本
	//这个是在动画中插入关键帧的方法，没有使用碰撞检测的方法制作的
	//但同时也有限制，就是只能用于这种唯一目标的游戏而没有办法很好地模拟武器AOE

	private List<PlayerBasic> theEMY;//为了打得爽，其实所有攻击都是AOE，AOE获取方式为自制的扇形检测
	private PlayerBasic thePlayer;//自身
	float theDistance = 0;//距离中间变量

	private void extraEffectSELF()
	{
		if (string.IsNullOrEmpty (thePlayer . conNameToSELF) == false) //效果不可叠加
		{
			System.Type theType = System.Type.GetType (thePlayer . conNameToSELF);
			if (!thePlayer.gameObject.GetComponent ( theType))
			{
				try
				{
					thePlayer.gameObject.AddComponent (theType);
					effectBasic theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
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
				effectBasic theEffect = thePlayer.gameObject.GetComponent (theType) as effectBasic;
				theEffect.updateEffect ();
				theEffect.SetAttackLinkIndex(thePlayer.EffectAttackLinkIndex);
			}
			thePlayer . conNameToSELF = "";
		}
	}
		
	private void extraDamageEffect(PlayerBasic playerAim)//额外添加挂在的计算脚本
	{
		if (string.IsNullOrEmpty (thePlayer . conNameToEMY) == false)//效果不可叠加
		{
			System.Type theType = System.Type.GetType (thePlayer.conNameToEMY);
			if(!playerAim.gameObject.GetComponent (theType))
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
				effectBasic theEffect = playerAim.gameObject.GetComponent (theType) as effectBasic;
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


	float change(float angle)//角度转弧度的方法
	{
		return( angle * Mathf.PI / 180);
	}


	//个人认为比较稳健的方法
	//传入的是攻击范围和攻击扇形角度的一半
	//选择目标的方法，这年头普攻都是AOE
	void searchAIMs(float angle , float distance)//不使用射线而是使用向量计算方法
	{
		//这个方法的正方向使用的是X轴正方向
		//具体使用的时候非常需要注意正方向的朝向
		theEMY = new List<PlayerBasic> ();
		//以自己为中心进行相交球体探测
		//实际上身边一定圆周范围内的所有具有碰撞体的单位都会被被这一步探测到
		//接下来需要的就是对坐标进行审查
		Collider [] emys = Physics.OverlapSphere (this.transform .position, distance);
		//使用cos值进行比照，因为在0-180角度范围内，cos是不断下降的
		//具体思路就是，判断探测到的物体的cos值如果这个cos值大于标准值，就认为这个单位的角度在侦查范围角度内。
		float angleCosValue = Mathf.Cos (change(angle));//莫认真侧角度的cos值作为计算标准
		//print ("angleCosValue-"+angleCosValue);
		for (int i = 0; i < emys.Length; i++)//开始对相交球体探测物体进行排查
		{ 
			//print (emys [i].gameObject.name);
			PlayerBasic thePlayerAim = emys[i].GetComponent <PlayerBasic>();
			//用alive标记减少在这里参与计算的单位数量
			if (thePlayerAim && thePlayerAim.isAlive && emys [i].GetComponent <Collider>().gameObject != this.gameObject) //相交球最大的问题就是如果自身有碰撞体，自己也会被侦测到
			{
				//print ("name-"+ emys [i].name);
				Vector3 thisToEmy = emys [i].transform.position - this.transform.position;//目标坐标减去自身坐标
				Vector2 theVectorToSearch = (new Vector2 (thisToEmy.x, thisToEmy.z)).normalized;//转成2D坐标，高度信息在这个例子中被无视
				//同时进行单位化，简化计算向量cos值的时候的计算
				Vector2 theVectorForward = (new Vector2 (this.transform.forward.x, this.transform.forward.z)).normalized;//转成2D坐标，高度信息在这个例子中被无视
				//同时进行单位化，简化计算向量cos值的时候的计算
				float cosValue = (theVectorForward.x * theVectorToSearch.x + theVectorForward.y * theVectorToSearch.y);//因为已经单位化，就没必要再进行求模计算了
				//print ("cosValue-" + cosValue);
				/*
				    先求出两个向量的模
					再求出两个向量的向量积
					|a|=√[x1^2+y1^2]
					|b|=√[x2^2+y2^2]
					a*b=(x1,y1)(x2,y2)=x1x2+y1y2
					cos=a*b/[|a|*|b|]
					=(x1x2+y1y2)/[√[x1^2+y1^2]*√[x2^2+y2^2]]
				*/
				if (cosValue >= angleCosValue)//如果cos值大于基准值，认为这个就是应该被探测的目标
				{
					PlayerBasic theAIM = emys [i].GetComponent<Collider> ().gameObject.GetComponent<PlayerBasic> ();
					if (theEMY .Contains (theAIM) == false) //不重复地放到已找到的列表里面
					{
						theEMY.Add (theAIM);
						checkIfISAI ();
						//print ("SeachFind "+emys [i].GetComponent<Collider> ().gameObject.name);//找到目标
					}
				}
			}

		}

	}


	//如果是AI需要加入额外的检查
	private void  checkIfISAI()
	{
		List<PlayerBasic> toDelete = new List<PlayerBasic> ();
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
	}
	//真正的攻击方法
	private void Attack( float makeDamage)
	{
		//如果makeDamage是负数，就说明制作获得效果，但是不攻击
		float theDistanceCheck = thePlayer.theAttackAreaLength;
		if (makeDamage >= 0) 
		{
			theDistanceCheck += makeDamage;

			searchAIMs (thePlayer.theAttackAreaAngel, theDistanceCheck);

			//print ("theEMY.count " + theEMY.Count);
			for (int i = 0; i < theEMY.Count; i++) 
			{

				theDistance = Vector3.Distance (thePlayer.transform.position, theEMY [i].transform.position);
				//print (theDistanceCheck);
				if (theDistance <= theDistanceCheck)
				{
					if (thePlayer && theEMY [i]) 
					{
						if (makeDamage >= 0)
						{//有些时候仅仅是增加脚本，例如“斗气爆发”不具备攻击效果
							thePlayer.OnAttack (theEMY [i], 0, false);//造成直接的伤害
							extraDamageEffect (theEMY [i]);//添加额外的计算脚本，每个脚本的效果由脚本自己决定
							//print(theEMY[i].name+" is being attacked");
						}
					}

				}
			}
		}
	}

}
