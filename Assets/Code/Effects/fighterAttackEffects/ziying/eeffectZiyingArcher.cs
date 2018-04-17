using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class eeffectZiyingArcher : effectBasic {

	GameObject Arrow;//弹矢引用保存
	Vector3 forward;
	float arrowLife = 0.2f;// 弹矢生存时间

	GameObject theArrow ;//真正的弹矢

	void Start ()
	{
		Init ();
	}

	//手动调用的额外销毁方法
	public override void effectDestoryExtra ()
	{
		if (theArrow)
		{
			try
			{
				Destroy (theArrow );
			}
			catch(Exception d)
			{
				//print (d.ToString());
			}
		}
	}
	public override void Init ()
	{
		lifeTimerAll = 0.5f;
		timerForEffect = 0.5f;
		theEffectName = "气剑指";
		theEffectInformation ="将剑气凝于手指激射而出用作普攻\n可对所有剑气命中目标造成普攻物理伤害\n剑气最多对三个目标造成伤害，持续"+arrowLife+"秒\n同一时刻只能存在一束剑气";
		makeStart ();
		//print ("气剑指");
		//没有控制者就不发
		if (this.thePlayer) 
		{
			forward = this.thePlayer.transform.forward;
			if (!Arrow) //加载资源仅仅需要一次，后面的引用就好了
				Arrow = (GameObject)Resources.Load ("effects/ziyingarrow");
			
			//考虑到多种连发的情况，暂时还是不做弹矢的对象池子，后期优化吧
			theArrow = (GameObject)GameObject.Instantiate (Arrow);
			theArrow.GetComponentInChildren <extraWeapon> ().setPlayer (this.thePlayer);

			theArrow.transform.forward = thePlayer.transform.forward;

			float extraX = Camera.main.transform.rotation.eulerAngles.x;
			extraX = extraX > 180 ? extraX - 360 : extraX;
			extraX = Mathf.Clamp (extraX , -10f,3f);
			//print ("theExtraX = "+ extraX);
			theArrow.transform.Rotate (new Vector3 ( extraX, 0, 0), Space.Self);

			Vector3 positionNew = thePlayer.transform.position + new Vector3 (0, 0.8f * thePlayer.transform.localScale.y + 0.3f, forward.normalized.z * 0.1f);
			theArrow.transform.localScale *= thePlayer.transform.localScale.y;
			theArrow.transform.position = positionNew;

			Destroy (theArrow, arrowLife);
			Destroy (this.GetComponent (this.GetType ()), lifeTimerAll);
		}

	} 

	public override void effectOnUpdateTime ()
	{
		addTimer ();
		//print ("timer add = "+ timerForAdd);
	}

	//public override void onAttackAction ()
	//{

	//}

}
