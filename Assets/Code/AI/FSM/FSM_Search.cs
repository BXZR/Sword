using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Search : FSMBasic {

	List<PlayerBasic> theEMYGet   = null;
	public float angle = 125;//视野角度范围的一半
	public float distance = 2;//视野长度
	PlayerBasic theMainEMY = null;


	float change(float angle)//角度转弧度的方法
	{
		return( angle * Mathf.PI / 180);
	}

	//个人认为比较稳健的方法
	//传入的是攻击范围和攻击扇形角度的一半
	//选择目标的方法，这年头普攻都是AOE
	void searchAIMs()//不使用射线而是使用向量计算方法
	{
		//这个方法的正方向使用的是X轴正方向
		//具体使用的时候非常需要注意正方向的朝向
		theEMYGet = new List<PlayerBasic> ();
		//以自己为中心进行相交球体探测
		//实际上身边一定圆周范围内的所有具有碰撞体的单位都会被被这一步探测到
		//接下来需要的就是对坐标进行审查
		Collider [] emys = Physics.OverlapSphere (theThis.transform .position, distance);
		//使用cos值进行比照，因为在0-180角度范围内，cos是不断下降的
		//具体思路就是，判断探测到的物体的cos值如果这个cos值大于标准值，就认为这个单位的角度在侦查范围角度内。
		float angleCosValue = Mathf.Cos (change(angle));//莫认真侧角度的cos值作为计算标准
		//print ("angleCosValue-"+angleCosValue);
		for (int i = 0; i < emys.Length; i++)//开始对相交球体探测物体进行排查
		{ 
			//Debug.Log(emys [i].gameObject.name);
			if (emys[i].tag != "AI" && emys[i].GetComponent <PlayerBasic>() && emys [i].GetComponent <Collider>().gameObject != theThis.gameObject) //相交球最大的问题就是如果自身有碰撞体，自己也会被侦测到
			{
				//Debug.Log(emys [i].gameObject.name);
				//print ("name-"+ emys [i].name);
				Vector3 thisToEmy = emys [i].transform.position - theThis.transform.position;//目标坐标减去自身坐标
				Vector2 theVectorToSearch = (new Vector2 (thisToEmy.x, thisToEmy.z)).normalized;//转成2D坐标，高度信息在这个例子中被无视
				//同时进行单位化，简化计算向量cos值的时候的计算
				Vector2 theVectorForward = (new Vector2 (theThis.transform.forward.x, theThis.transform.forward.z)).normalized;//转成2D坐标，高度信息在这个例子中被无视
				//同时进行单位化，简化计算向量cos值的时候的计算
				float cosValue = (theVectorForward.x * theVectorToSearch.x + theVectorForward.y * theVectorToSearch.y);//因为已经单位化，就没必要再进行求模计算了
				//Debug.Log("cosValue-" + cosValue);
				/*
				 * 先求出两个向量的模
					再求出两个向量的向量积
					|a|=√[x1^2+y1^2]
					|b|=√[x2^2+y2^2]
					a*b=(x1,y1)(x2,y2)=x1x2+y1y2
					cos=a*b/[|a|*|b|]
					=(x1x2+y1y2)/[√[x1^2+y1^2]*√[x2^2+y2^2]]
				 * 
				*/
				if (cosValue >= angleCosValue)//如果cos值大于基准值，认为这个就是应该被探测的目标
				{
					PlayerBasic theAIM = emys [i].GetComponent<Collider> ().gameObject.GetComponent<PlayerBasic> ();
					//Debug.Log("AIM "+emys [i].GetComponent<Collider> ().gameObject.name);//找到目标
					if ( theEMYGet .Contains (theAIM) == false) //不重复地放到已找到的列表里面
					{
						theEMYGet.Add (theAIM);
						//Debug.Log("SeachFind "+emys [i].GetComponent<Collider> ().gameObject.name);//找到目标
					}
				}
			}

		}
		theMainEMY = getMainEMY ();

	}


    //找到的目标很多，排序找到最终的目标
	private PlayerBasic getMainEMY()
	{
		//Debug.Log ( "theEMYGet.Count = "+ theEMYGet.Count);
		//这只是一个概念的做法，后期一定要扩充一下
		if ( theEMYGet.Count > 0)
			return theEMYGet [0];
		return null;
	}

//-------------------------------------------------------------------------//
	public override int geID ()
	{
		return 4;
	}

	public override void actInThisState ()
	{
		//Debug.Log ("search!");
		searchAIMs ();
	}

	public override FSMBasic moveToNextState ()
	{
		//找不到目标就继续找
		if (theMainEMY == null)
			return this;
		//找到了目标就转到下一个状态
		else 
		{
			Debug.Log ("search to attack");
			FSM_Attack attack = new FSM_Attack ();
			attack.makeState (this.theMoveController, this.theAttackLlinkController, this.theAnimator,this.theThis,this.theMainEMY);
			return attack;
		}
 
	}
}
