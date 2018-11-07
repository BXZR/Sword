using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectQianfang : effectBasic {

	static GameObject Arrow;//弹矢引用保存
	static GameObject Arrow2;//弹矢引用保存
	Vector3 forward;
	float arrowLife = 0.25f;// 弹矢生存时间
	public int arrowCounts = 5;//发射的剑气数量
	public float hpup = 0.04f;//吸收的生命值百分比
	public float hpupTrueUseExtra = 4f;//吸收的生命值
	float angleForArrow = 13;//剑气角度

	static List<extraWeapon> theArrows = new List<extraWeapon> ();
	static extraWeapon updatedArrow;

	void Start ()
	{
		Init ();
	}

	//手动调用的额外销毁方法
	public override void effectDestoryExtra ()
	{
	}
	public override void Init ()
	{
		lifeTimerAll = 5.5f;
		timerForEffect = 0.2f;
		theEffectName = "千方残光剑";
		theEffectInformation ="向前方锥形发射"+arrowCounts+"束特殊剑气\n攻击命中额外追加（"+hpup*100+"%+"+hpupTrueUseExtra+"）生命偷取\n每束剑气最多对三个目标造成伤害，持续"+arrowLife+"秒" +
			"\n冷却时间为" + (lifeTimerAll-timerForEffect) +"秒，冷却中使用此技可释放普通剑气";
		makeStart ();
		if(!Arrow)
			Arrow = (GameObject)Resources.Load ("effects/ziyingarrow2");
		if(!Arrow2)
			Arrow2 = (GameObject)Resources.Load ("effects/ziyingarrow");
		makeFlashList ();
		
		//print ("气剑指");
		//没有控制者就不发
		if (this.thePlayer) 
		{
			if (isEffecting)
			{
				GameObject theArrow ;
				for (int i = 0; i < arrowCounts; i++)
				{
					//四元数的方法在这里似乎不是很好用
					//forward = Quaternion.AngleAxis((float)(45*i), new Vector3(0,1,0)) *this.thePlayer.transform.forward ;
					//print ("forward = "+ forward);
					if (theArrows.Count < i + 1 || theArrows [i] == null) {
						theArrow = (GameObject)GameObject.Instantiate (Arrow);
						extraWeapon A = theArrow.GetComponentInChildren <extraWeapon> ();
						A.setPlayer (this.thePlayer);
						theArrows.Add (A);
					} 
					else 
					{
						theArrows [i].gameObject.SetActive (true);
						theArrow = theArrows [i].gameObject;
					}

					Vector3 positionNew = thePlayer.transform.position + new Vector3 (0, 0.8f * thePlayer.transform.localScale.y + 0.2f, forward.normalized.z * 0.07f);
					theArrow.transform.localScale *= thePlayer.transform.localScale.y;
					theArrow.transform.position = positionNew;

					theArrow.transform.forward = this.thePlayer.transform.forward;
					theArrow.transform.Rotate (new Vector3 (0, (float)(-angleForArrow * (arrowCounts / 2) + angleForArrow * i), 0));

					if(arrowCounts %2 == 0)
						theArrow.transform.Rotate (new Vector3 (0, (float)(angleForArrow /2), 0));
					
					theArrow.transform.forward = theArrow.transform.forward;
					//Destroy (theArrow, arrowLife);
					Destroy (this, lifeTimerAll);
					Invoke ("makeArrowOver", arrowLife);
					Invoke ("shutEffecting" , arrowLife);
				}
			}
		}

	} 


	void makeFlashList()
	{
		List<extraWeapon > toDelete = new List<extraWeapon> ();
		for (int i = 0; i < theArrows.Count; i++)
			if (!theArrows [i])
				toDelete.Add (theArrows[i]);

		for (int i = 0; i < toDelete.Count; i++)
			theArrows.Remove (toDelete[i]);
	}

	void makeArrowOver()
	{
		for (int i = 0; i < theArrows.Count; i++)
			if (theArrows [i].gameObject.activeInHierarchy) 
			{
				theArrows [i].GetComponentInChildren <extraWeapon> ().makeFlash ();
				theArrows [i].gameObject.SetActive (false);
			}
	}

	void makeUpdatedArrowOver()
	{
		updatedArrow.GetComponentInChildren <extraWeapon> ().makeFlash ();
		updatedArrow.gameObject.SetActive (false);
	}


	public override void effectOnUpdateTime ()
	{
		addTimer ();
	}

	void shutEffecting()
	{
		isEffecting = false;
	}

	public override void updateEffect ()
	{
		if (isEffecting)
			return;
		
		forward = this.thePlayer.transform.forward;

		if (!updatedArrow) 
		{
			updatedArrow = ((GameObject)GameObject.Instantiate (Arrow2)).GetComponentInChildren <extraWeapon> ();
			updatedArrow.setPlayer (this.thePlayer);
		} 
		else
		{
			updatedArrow.gameObject.SetActive (true);
		}

		Vector3 positionNew = thePlayer.transform.position + new Vector3 (0, 0.8f * thePlayer.transform.localScale.y + 0.2f, forward.normalized.z * 0.1f);
		updatedArrow.transform.localScale *= thePlayer.transform.localScale.y;
		updatedArrow.transform.position = positionNew;

		updatedArrow.transform.forward = thePlayer.transform.forward;
		//Destroy (updatedArrow, arrowLife);
		Invoke("makeUpdatedArrowOver" , arrowLife);
	}


	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		//只有特殊剑气才会有这些效果
		if (isEffecting) 
		{
			float hpupHP = TrueDamage * hpup + hpupTrueUseExtra;
			this.thePlayer.ActerHp += hpupHP;
			//附加的各种效果
			effectBasic[] effects = this.thePlayer.GetComponents<effectBasic> ();
			foreach (effectBasic EF in effects)
				EF.OnHpUp (hpupHP);
		}
	}


	//招式等级额外特效 ====================================================================
	public override void SetAttackLink (attackLink attackLinkIn)
	{
		if (attackLinkIn && attackLinkIn.theAttackLinkLv >= 7)
		{
			hpup += 0.03f;
		}

		if (attackLinkIn && attackLinkIn.theAttackLinkLv >= 12)
		{
			arrowCounts += 2;
		}
	}
	public override string getEffectAttackLinkLVExtra ()
	{
		return "招式等级奖励\n7级招式: 追加3%生命偷取效果\n12级招式: 特殊剑气增加两束";
	}



	//public override void onAttackAction ()
	//{

	//}

}
