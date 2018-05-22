using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wuling : effectBasic {

	//五灵效果脚本
	//风雷水火土五灵效果都在这个脚本里实现
	//是要是为了方便配置
	//五灵效果又分为阴阳两种
	//也就是类似阳雷阴雷的效果

	//五灵效果前期需要灵力进行修炼
	//每一种灵力修炼完成都会带来不同的效果，并且额外提灵力增长
   


	public List<lingBasic> lingEffects;

	void Start()
	{
		Init ();
	}

	public override void Init ()
	{
		theEffectName = "五灵";
		lingEffects = new List<lingBasic> ();
		makeLings ();
		makeStart ();
		makeInformation ();
	}
		
	void makeLings()
	{
		wind1 theWind1 = new wind1 ();
		theWind1.makeStart ();
		wind2 theWind2 = new wind2 ();
		theWind2.makeStart ();
		thunder1 theThunder1 = new thunder1 ();
		theThunder1.makeStart ();
		thunder2 theThunder2 = new thunder2 ();
		theThunder2.makeStart ();
		fire1 theFire1 = new fire1 ();
		theFire1.makeStart ();
		fire2 theFire2 = new fire2 ();
		theFire2.makeStart ();
		water1 theWater1 = new water1 ();
		theWater1.makeStart ();
		water2 theWater2 = new water2 ();
		theWater2.makeStart ();
		earth1 theEarth1 = new earth1 ();
		theEarth1.makeStart ();
		earth2 theEarth2 = new earth2 ();
		theEarth2.makeStart ();

		lingEffects.Add (theWind1);
		lingEffects.Add (theWind2);
		lingEffects.Add (theThunder1);
		lingEffects.Add (theThunder2);
		lingEffects.Add (theFire1);
		lingEffects.Add (theFire2);
		lingEffects.Add (theWater1);
		lingEffects.Add (theWater2);
		lingEffects.Add (theEarth1);
		lingEffects.Add (theEarth2);
	}

	void makeInformation()
	{
		theEffectInformation = "";
		for (int i = 0; i < lingEffects.Count; i++)
			theEffectInformation += lingEffects [i].wulingInformation ()+"\n";
	}

	//---------------------------------------------------------------//

	//在攻击的起手阶段触发
	override public void onAttackAction()
	{
		foreach (lingBasic ling in lingEffects)
		   if(ling.isLearned())
			ling.onAttackAction (this.thePlayer);
	}
	//在攻击的时候触发
	override public void OnAttack ()
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
			    ling.OnAttack (this.thePlayer);
	}
	//在被攻击的时候触发
	override public void OnBeAttack(float damage = 0)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnBeAttack (this.thePlayer, damage);
	}
	//没有命中的时候调用表
	override public void OnDoNotAttackAt(PlayerBasic aim = null)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnDoNotAttackAt (this.thePlayer, aim);
	}
	//在被攻击的时候触发
	override public void OnBeAttack(PlayerBasic attacker)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnBeAttack (this.thePlayer, attacker);
	}
	//在update里面调用的效果
	override public void effectOnUpdateTime()
	{
		foreach (lingBasic ling in lingEffects) 
			if (ling.isLearned ()) 
				ling.effectOnUpdateTime (this.thePlayer);
		makeSoulAddWithWuling(systemValues.updateTimeWait* 0.1f);
	}
	//-----------------------------------------------------------------------------------
	float soulGet = 0f;
	//额外增加灵力的方法
	private void makeSoulAddWithWuling(float adder)
	{
		foreach (lingBasic ling in lingEffects)
			if (ling.isLearned ())
				soulGet += adder;
		if (soulGet >= 1f) 
		{
			systemValues.soulCount += (int)soulGet;
			soulGet = 0f;
		}
		
	}
	//-----------------------------------------------------------------------------------

	//在生命恢复到满血的时候使用
	override public void OnHpTowardHpMax()
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnHpTowardHpMax(this.thePlayer);
	}
	//在法力恢复到满的时候触发
	override public void OnSpTowardSpMax()
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnSpTowardSpMax(this.thePlayer);
	}
	//在生命恢复的时候触发
	override public void OnHpUp()
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnHpUp(this.thePlayer);
	}
	//在生命恢复的时候触发
	override public void OnHpUp(float upValue = 0)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnHpUp(this.thePlayer , upValue);
	}
	//在法力恢复的时候触发
	override public void OnSpUp()
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnSpUp(this.thePlayer);
	}
	//在法力恢复的时候触发
	override public void OnSpUp(float upValue = 0)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnSpUp(this.thePlayer,upValue);
	}
	//带目标的攻击效果
	override public void OnAttack (PlayerBasic aim)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnAttack(this.thePlayer,aim);
	}
	//带目标的攻击效果此外附带造成的真实伤害
	override public void OnAttack (PlayerBasic aim,float TrueDamage)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnAttack(this.thePlayer,aim,TrueDamage);
	}
	//是消耗斗气的时候调用
	override public void OnUseSP(float spUse = 0)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnUseSP(this.thePlayer,spUse);
	}
	//玩家杀死一个单位的时候触发
	override public void OnKill()
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnKill(this.thePlayer);
	}
	//死亡的时候调用
	override public void OnDead()
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnDead(this.thePlayer);
	}
	//暴击的时候调用
	override public void OnSuperBlade(PlayerBasic aim, float Damage = 0)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnSuperBlade(this.thePlayer,aim,Damage);
	}
	//闪避的时候调用
	override public void OnMiss(PlayerBasic attacker)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnMiss(this.thePlayer,attacker);
	}
	//格挡的时候调用
	override public void OnShield(PlayerBasic attacker,float damageMinus = 0)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnShield(this.thePlayer,attacker,damageMinus);
	}
	//增加护盾的时候的额外效果
	override public void OnAddShieldHp(float theSheildHpAdd = 0)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnAddShieldHp(this.thePlayer,theSheildHpAdd);
	}
	//玩家升级的时候触发
	override public void OnLvUp()
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnLvUp(this.thePlayer);
	}
	//当玩家收集到一个魂元的时候
	override public void OnAddSoul(int soulCount)
	{
		foreach (lingBasic ling in lingEffects)
			if(ling.isLearned())
				ling.OnAddSoul(this.thePlayer,soulCount);
	}
}
