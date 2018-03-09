using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectBasic : MonoBehaviour {
	//游戏最灵动的地方在这里
	//模板方法模式
	//所有的效果都依附在游戏人物身体尚上，在具体事件发生的时候触发
	//这个地方是脑洞大开的地方，可以说这个模板类方法内容越多，这个游戏越灵活

	protected PlayerBasic thePlayer;//作用于（依附于）哪一个对象
	public string theEffectName = " ";//技能名称（用于信息查询）
	public  string theEffectInformation = " ";//技能效果
	public string theEffedctExtraInformation = "";//技能额外的特性
	virtual public void onAttackAction(){}//在攻击的起手阶段触发
	virtual public void OnAttack (){}//在攻击的时候触发
	virtual public void OnBeAttack(float damage = 0){}//在被攻击的时候触发
	virtual public void OnBeAttack(PlayerBasic attacker){}//在被攻击的时候触发
	virtual public void effectOnUpdateTime(){}//在update里面调用的效果
	virtual public void OnHpTowardHpMax(){}//在生命恢复到满血的时候使用
	virtual public void OnSpTowardSpMax(){}//在法力恢复到满的时候触发
	virtual public void OnHpUp(){}//在生命恢复的时候触发
	virtual public void OnHpUp(float upValue = 0){}//在生命恢复的时候触发
	virtual public void OnSpUp(){}//在法力恢复的时候触发
	virtual public void OnSpUp(float upValue = 0){}//在法力恢复的时候触发
	virtual public void OnAttack (PlayerBasic aim){}//带目标的攻击效果
	virtual public void OnAttack (PlayerBasic aim,float TrueDamage){}//带目标的攻击效果此外附带造成的真实伤害
	virtual public void OnUseSP(float spUse = 0){}//是消耗斗气的时候调用
	virtual public void OnDead(){}//死亡的时候调用
	virtual public void OnSuperBlade(PlayerBasic aim, float Damage = 0){}//暴击的时候调用
	virtual public void OnMiss(PlayerBasic attacker){}//闪避的时候调用
	virtual public void OnShield(PlayerBasic attacker,float damage = 0){}//格挡的时候调用
	virtual public void OnAddShieldHp(float theSheildHpAdd = 0){}//增加护盾的时候的额外效果
	//这个效果可以在中途更新，且更新方法每一种效果自己定义
	//例如更新加长持续时间等等
	virtual public void updateEffect(){}
	public virtual void Init(){}//初始化的方法
	public virtual void effectDestory(){}//销毁的方法
	public virtual void effectDestoryExtra(){}//手动调用的额外销毁方法
	public virtual void makeInformation(){}
	//返回这个效果的信息



	public float extraTimer = 0;//这个是给extraEffectMaker提供的时间
	public bool isEffecting = true;//因为使用的是脚本方式，因此身上有脚本未必就是在生效，也有可能是在冷却，所以需要添加一个标记
	public  virtual  string getInformation ()
	{
		if (!isShowing())
			return "";
		
		string theInformation = "（" + this.theEffectName + "）";
		if(isBE())
			theInformation +="[被动]";
		else
			theInformation +="[主动]";
		theInformation +="\n"+this.theEffectInformation;
		return theInformation;
	}//显示完全的信息
		
	public virtual string getExtraInformation()
	{
		return theEffedctExtraInformation;
	}

	public   string getEffectInformation(){return this.theEffectInformation;}//只显示内容

	public string getEffectName()
	{

		string theInformation =  this.theEffectName ;
		if(isBE())
			theInformation +="\n[被动]";
		else
			theInformation +="\n[主动]";
		return theInformation;
	}

	//特殊用途不被认为是技能
	virtual public bool isExtraUse()
	{
		return false;
	}

	//一般都是主动技能，此处为默认为false
	virtual public bool isBE()
	{
		return false;//默认返回false
	}
		
	//是不是公有主动技能（这种技能所有人都会）
	virtual public bool isPublic()
	{
		return false;//默认返回false
	}

	//有些技能没有必要显示出来
	virtual public bool isShowing()
	{
		return true;
	}

	public void makeStart()
	{
		thePlayer = this.GetComponentInChildren<PlayerBasic> ();
	}

	//void OnDestroy()
	//{
		
	//}

}
