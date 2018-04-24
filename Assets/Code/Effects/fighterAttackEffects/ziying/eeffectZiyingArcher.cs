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


	public override void Init ()
	{
		lifeTimerAll = 0.25f;
		timerForEffect = 0.25f;
		theEffectName = "气剑指";
		theEffectInformation ="将剑气凝于手指激射而出用作普攻\n剑气可对命中的最多三个目标造成伤害\n"+lifeTimerAll +"秒内只能发射一束剑气\n无法发射剑气时触发可以恢复10斗气";
		//这个效果算是气剑指的特性，并且也是手速狂魔的福音，手速快的话在高攻速的时候就是无消耗输出
		makeStart ();
		if (!Arrow) //加载资源仅仅需要一次，后面的引用就好了
			Arrow = (GameObject)Resources.Load ("effects/ziyingarrow");
		makeArrow ();
	} 

	public override void updateEffect ()
	{
		//print ("up");
		if (isEffecting)
			makeArrow ();
		else
		    thePlayer.ActerSp += 10f;
	}

	public override void effectOnUpdateTime ()
	{

		if (!isEffecting) 
		{
			timerForAdd += systemValues.updateTimeWait;
			if (timerForAdd > lifeTimerAll)
			{
				timerForAdd = 0;
				isEffecting = true;
			}
		}
		//print ("timer add = "+ timerForAdd);
	}

	private  void makeArrow()
	{
		//print ("气剑指");
		//没有控制者就不发
		if (this.thePlayer && isEffecting) 
		{
			forward = this.thePlayer.transform.forward;
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

			Destroy (theArrow.gameObject, arrowLife);
			isEffecting = false;
		}
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
}
