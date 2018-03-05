using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectQianfang : effectBasic {

	GameObject Arrow;//弹矢引用保存
	Vector3 forward;
	float arrowLife = 0.15f;// 弹矢生存时间
	float lastingTime =0.3f;//根据规则产生的脚本覆盖时间间隔，这个时间越短，但是发射频率越高，也就是攻速越快
	public int arrowCounts =3;//发射的剑气数量
	public float hpup = 0.05f;//吸收的生命值百分比
	public float hpupTrueUseExtra = 3f;//吸收的生命值
	float angleForArrow = 20;//剑气角度
	GameObject theArrow ;//真正的弹矢

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
		
		theEffectName = "千方残光剑";
		theEffectInformation ="向前方锥形发射"+arrowCounts+"束特殊剑气\n技能触发攻击效果并有额外（"+hpup*100+"%+"+hpupTrueUseExtra+"）生命偷取\n每束剑气最多对三个目标造成伤害，持续"+arrowLife+"秒 ";
		makeStart ();
		//print ("气剑指");
		//没有控制者就不发
		if (this.thePlayer) 
		{
			for (int i = 0; i < arrowCounts ; i++)
			{
				//四元数的方法在这里似乎不是很好用
				//forward = Quaternion.AngleAxis((float)(45*i), new Vector3(0,1,0)) *this.thePlayer.transform.forward ;
				//print ("forward = "+ forward);
				Arrow = (GameObject)Resources.Load ("effects/ziyingarrow2");

				theArrow = (GameObject)GameObject.Instantiate (Arrow);
				theArrow.GetComponentInChildren <extraWeapon> ().setPlayer (this.thePlayer);

				Vector3 positionNew = thePlayer.transform.position + new Vector3 (0, 0.8f * thePlayer.transform.localScale.y + 0.2f, forward.normalized.z * 0.1f);
				theArrow.transform.localScale *= thePlayer.transform.localScale.y;
				theArrow.transform.position = positionNew;

				theArrow.transform.forward = this.thePlayer.transform.forward ;
				theArrow.transform.Rotate (new Vector3(0,(float)(-angleForArrow *(arrowCounts/2) +angleForArrow*i) ,0));
				theArrow.transform.forward = theArrow.transform .forward;
				Destroy (theArrow, arrowLife);
				Destroy (this.GetComponent (this.GetType ()), lastingTime);
			}
		}

	} 
	public override void OnAttack (PlayerBasic aim, float TrueDamage)
	{
		float hpupHP = TrueDamage * hpup + hpupTrueUseExtra;
		this.thePlayer.ActerHp += hpupHP;
		//附加的各种效果
		effectBasic [] effects = this.thePlayer.GetComponents<effectBasic> ();
		foreach (effectBasic EF in effects)
			EF.OnHpUp ( hpupHP);
	}


	//public override void onAttackAction ()
	//{

	//}

}
