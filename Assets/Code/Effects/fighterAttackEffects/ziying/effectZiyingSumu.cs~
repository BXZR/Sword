using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectZiyingSumu :effectBasic {

	float timer = 20f;//总持续时间，其实也就是冷却时间
	float effectTime = 0f;//累加计时器，超过max就无效了
	float effectTimerMax = 10f;//最长生效时间上限
	float damage = 10f;//回敬的物理伤害
	float attackAdd = 15f;//增加的攻击力
	GameObject theEffect;//特效

	void Start () 
	{
		Init ();
		Destroy (this,timer);
	}

	public override void OnBeAttack (PlayerBasic attacker)
	{
		if (effectTime < effectTimerMax)
		{
			this.thePlayer.OnAttackWithoutEffect (attacker,damage,false,true);
		}
	}
	public override void  effectOnUpdateTime ()
	{
		effectTime += systemValues.updateTimeWait;
		if (effectTime > effectTimerMax && theEffect) 
		{
			thePlayer.ActerWuliDamage -= attackAdd;
			thePlayer.CActerWuliDamage -= attackAdd;
			Destroy (theEffect);
		}
	}

	public override void OnAddShieldHp (float theSheildHpAdd = 0)
	{
		this.thePlayer.ActerSp += theSheildHpAdd * 0.17f;
	}

	public override void Init ()
	{
		theEffectName = "四方肃穆";
		theEffectInformation ="将剑气交错于己身，增加"+attackAdd+"攻击力，受到攻击时回敬"+damage+"物理伤害\n增加任何护盾时恢复护盾值17%的斗气\n持续"+effectTimerMax+"秒，冷却时间"+ timer+"秒";
		makeStart ();
		if (thePlayer) 
		{
			theEffect = GameObject.Instantiate<GameObject> (Resources.Load<GameObject> ("effects/ziyingShield"));
			theEffect.transform.SetParent (this.thePlayer.transform);
			theEffect.transform.localPosition = new Vector3 (0, 1.5f, 0);
			thePlayer.ActerWuliDamage += attackAdd;
			thePlayer.CActerWuliDamage += attackAdd;
		}
	} 
}
