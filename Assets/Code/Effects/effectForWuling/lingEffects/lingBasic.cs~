using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum wulingType {wind, thunder , water ,earth , fire}
public class lingBasic 
{

	//为了方便管理，就是用继承来做
	//这个是五灵效果的基类
	//阴阳五灵的效果的丰富性也就是在依靠这个类的脑洞了

	public string lingName = "";
	public float lingNow = 0 ;//需要100 灵力才能修炼完成
	public float lingNeed = 100 ;//需要100 灵力才能修炼完成
	public wulingType theType = wulingType.wind;//五灵的类型，用于查询

	public virtual void makeStart(){}//通用初始化方法
	public virtual string wulingInformation(){return "";}//专门的五灵信息说明

	virtual public void onAttackAction(PlayerBasic user){}//在攻击的起手阶段触发
	virtual public void OnAttack (PlayerBasic user){}//在攻击的时候触发
	virtual public void OnBeAttack(PlayerBasic user,float damage = 0){}//在被攻击的时候触发
	virtual public void OnDoNotAttackAt(PlayerBasic user,PlayerBasic aim = null){}//没有命中的时候调用表
	virtual public void OnBeAttack(PlayerBasic user,PlayerBasic attacker){}//在被攻击的时候触发
	virtual public void effectOnUpdateTime(PlayerBasic user){}//在update里面调用的效果
	virtual public void OnHpTowardHpMax(PlayerBasic user){}//在生命恢复到满血的时候使用
	virtual public void OnSpTowardSpMax(PlayerBasic user){}//在法力恢复到满的时候触发
	virtual public void OnHpUp(PlayerBasic user){}//在生命恢复的时候触发
	virtual public void OnHpUp(PlayerBasic user,float upValue = 0){}//在生命恢复的时候触发
	virtual public void OnSpUp(PlayerBasic user){}//在法力恢复的时候触发
	virtual public void OnSpUp(PlayerBasic user,float upValue = 0){}//在法力恢复的时候触发
	virtual public void OnAttack (PlayerBasic user,PlayerBasic aim){}//带目标的攻击效果
	virtual public void OnAttack (PlayerBasic user,PlayerBasic aim,float TrueDamage){}//带目标的攻击效果此外附带造成的真实伤害
	virtual public void OnUseSP(PlayerBasic user,float spUse = 0){}//是消耗斗气的时候调用
	virtual public void OnKill(PlayerBasic user){}//玩家杀死一个单位的时候触发
	virtual public void OnDead(PlayerBasic user){}//死亡的时候调用
	virtual public void OnSuperBlade(PlayerBasic user,PlayerBasic aim, float Damage = 0){}//暴击的时候调用
	virtual public void OnMiss(PlayerBasic user,PlayerBasic attacker){}//闪避的时候调用
	virtual public void OnShield(PlayerBasic user,PlayerBasic attacker,float damageMinus = 0){}//格挡的时候调用
	virtual public void OnAddShieldHp(PlayerBasic user,float theSheildHpAdd = 0){}//增加护盾的时候的额外效果
	virtual public void OnLvUp(PlayerBasic user){}//玩家升级的时候触发
	virtual public void OnAddSoul(PlayerBasic user,int soulCount){}//当玩家收集到一个魂元的时候

	//判断阴阳的标记
	virtual public int getYinYagType() {return 1;}//返回阴阳标记 1是阳 2是阴


	//有关修为
	public float value = 0f;
	public float valueMax = 200f;
	public void learnWuling()
	{
		if (!systemValues.thePlayer)
			return;
		if ((valueMax - value) > systemValues.thePlayer.ActerSp) 
		{
			value += systemValues.thePlayer.ActerSp;
			systemValues.thePlayer.ActerSp = 0;
		} 
		else 
		{
			value = valueMax;
			systemValues.thePlayer.ActerSp -= (valueMax - value);
		}
	}

	public bool isLearned()
	{
		return  value == valueMax;
	}

	public float getLearningPercent()
	{
		return  value / valueMax;
	}

}
