using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class PlayerBasic : MonoBehaviour {

	//游戏人物属性类和游戏人物的数值操作类
	//有关最根本游戏的东西放在这里
	//最基本的计算方法处理

	//给出参考数值用于测试
	//究极人物类，完全就是属性和方法的记录脚本名称
	[HideInInspector]
	public string headPictureName;//人物头像的
	public string ActerName;//这个人物的名称

	//有关经验和等级
	[HideInInspector]
	public int playerLv = 1;
	[HideInInspector]
	public  float jingyanNow = 0;
	[HideInInspector]
	public  float jingyanMax = 100;
	[HideInInspector]
	public int LVMax= 18;//等级上限目前为18级，当然主人公也可以一些特殊方式来突破这一层天堑

	[HideInInspector]//为了保证设定面板的简洁，暂时隐藏之
	public bool isAlive = true;//是否生存，默认一定是存活的，除非死了

	//最基本的属性生命法力和名字
	public float ActerHpMax=1000f;//这个人物的生命上限
	public float ActerHp=1000f;//这个人物当前的生命值
	public float ActerSpMax=500f;//这个人物的法力上限
	public float ActerSp=200f;//这个人物当前的法力值
	public float ActerHpUp=0.5f;//人物生命恢复
	public float ActerSpUp=0.5f;//人物法力回复

	//特殊战斗属性
	public float ActerSuperBaldePercent=0f;//这个人物的暴击率
	public float ActerSuperBaldeAdder=2f;//暴击时伤害的倍数

	public float ActerAttackAtPercent = 1f;//这个人物的命中率
	public float ActerMissPercent=0f;//这个人物的闪避率
	
	public float ActerShielderPercent=0f;//这个人物的格挡率
	public float ActerShielderDamageMiuns=1;//格挡住的伤害值
	public float ActerShielderDamageMiunsPercent=0.1f;//格挡住的伤害百分比 (先计算固定格挡，然后计算百分比格挡)
	
	//物理战斗属性
	public float ActerWuliDamage=25f;//这个人物的物理攻击力
	public float ActerWuliReDamage=0f;//这个人物的物理反伤
	public float ActerWuliIner=0f;//这个人物的固定物理穿透
	public float ActerWuliInerPercent=0f;//这个人物百分比穿透  （先计算固定穿透，然后计算百分比穿透）

	public float ActerDamageMinusPercent = 0.0f;//一直存在的伤害减免百分比
	public float ActerDamageMinusValue = 0.0f;//一直存在的伤害减免数值
	
	//物理防御属性
	public float ActerWuliShield=150f;//这个人物的物理护甲

	//生命吸取属性
	public float ActerHpSuck=0f;//人物的固定生命偷取值
	public float ActerHpSuckPercent=0f;//根据所造成伤害的百分比生命吸取
	
	//法力吸取属性
	public float ActerSpSuck=0f;//人物的固定的法力偷取
	public float ActerSpSuckPercent=0f;//根据所造成伤害的百分比法力偷取

	//额外战斗属性(伤害增加)
	public float ActerDamageAdderPercent=0;//额外百分比伤害
	public float ActerDamageAdder=0;//额外真实加成
	//上面这些全都要放在RPC方法里面各种更新
	//人物等级提升后加成

	public float ActerMoveSpeedPercent = 1f;//移动速度百分比，在移动的时候会有这个速度百分比加成
	public float ActerAttackSpeedPercent = 1f;//攻击速度百分比，所有的动作的速度会受到这个限制
	public float ActerSpeedOverPervnet = 1f;//因为攻击速度和移动速度收到装备，负重等等因素的控制，所以很难受到外界控制，而这个参数就是这目标，最终加成

	public float ActerShieldHp = 0;//护盾的生命值
	public float ActerShieldHpSave = 10000;//护盾的生命值备份保存触发效果
	public float ActerShieldMaxPercent = 0.15f;//护盾针对最大生命值的上限

	public float theAttackAreaLength;//攻击范围（非常重要，同时这个是简化版本的每一种攻击招式分开计算范围的方式）
	public float theAttackAreaAngel = 20f;//攻击范围的角度，自身前方锥形范围内都是攻击范围
	public float theViewAreaLength = 4f;//视野长度，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
	public float theViewAreaAngel = 30f;//视野的角度，同样，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的

	//副本数据，在额外脚本计算的时候会有奇效------------------------------------------------------------------------
	//接下来是一些私有的战斗属性备份，用于计算值（作为例如护甲值提升10%这种的参数值计算）
	//因为是私有方法，所以还要给出获取这个值和修改这个值的方法
	//总体上讲，这些值是战斗属性的备份值，当有特殊计算方法的时候作为参数计算更新战斗属性
	//例如 战斗属性 = 备份值 *1.1f
	//顺带一提，之所以使用私有方法是因为不想让共有属性表太长，此外这些私有只会在特殊情况之下服务器才会使用

	//最基本的属性生命法力和名字
	[HideInInspector]
	public float CActerHpMax=1000f;//这个人物的生命上限
	[HideInInspector]
	public  float CActerSpMax=500f;//这个人物的法力上限
	[HideInInspector]
	public  float CActerHpUp=0.5f;//人物生命恢复
	[HideInInspector]
	public  float CActerSpUp=0.5f;//人物法力回复

	//特殊战斗属性
	[HideInInspector]
	public  float CActerSuperBaldePercent=0f;//这个人物的暴击率
	[HideInInspector]
	public  float CActerSuperBaldeAdder=2f;//暴击时伤害的倍数

	[HideInInspector]
	public  float CActerAttackAtPercent = 1f;//这个人物的命中率
	[HideInInspector]
	public  float CActerMissPercent=0f;//这个人物的闪避率

	[HideInInspector]
	public  float CActerShielderPercent=0f;//这个人物的格挡率
	[HideInInspector]
	public  float CActerShielderDamageMiuns=1;//格挡住的伤害值
	[HideInInspector]
	public  float CActerShielderDamageMiunsPercent=0.1f;//格挡住的伤害百分比 (先计算固定格挡，然后计算百分比格挡)
	

	//物理战斗属性
	[HideInInspector]
	public  float CActerWuliDamage=25f;//这个人物的物理攻击力
	[HideInInspector]
	public  float CActerWuliReDamage=0f;//这个人物的物理反伤
	[HideInInspector]
	public  float CActerWuliIner=0f;//这个人物的固定物理穿透
	[HideInInspector]
	public  float CActerWuliInerPercent=0f;//这个人物百分比穿透  （先计算固定穿透，然后计算百分比穿透）

	[HideInInspector]
	public float CActerDamageMinusPercent = 0.0f;//一直存在的伤害减免百分比
	[HideInInspector]
	public float CActerDamageMinusValue = 0.0f;//一直存在的伤害减免数值

	//物理防御属性
	[HideInInspector]
	public float CActerWuliShield=150f;//这个人物的物理护甲

	//生命吸取属性
	[HideInInspector]
	public  float CActerHpSuck=0f;//人物的固定生命偷取值
	[HideInInspector]
	public  float CActerHpSuckPercent=0f;//根据所造成伤害的百分比生命吸取

	//法力吸取属性
	[HideInInspector]
	public float CActerSpSuck=0f;//人物的固定的法力偷取
	[HideInInspector]
	public float CActerSpSuckPercent=0f;//根据所造成伤害的百分比法力偷取

	//额外战斗属性
	[HideInInspector]
	public float CActerDamageAdderPercent=0;//额外百分比伤害
	[HideInInspector]
	public float CActerDamageAdder=0;//额外真实加成

	[HideInInspector]
	public float CActerMoveSpeedPercent = 1f;//移动速度百分比，在移动的时候会有这个速度百分比加成
	[HideInInspector]
	public float CActerAttackSpeedPercent = 1f;//攻击速度百分比，所有的动作的速度会受到这个限制

	[HideInInspector]
	public float CtheAttackAreaLength;//攻击范围（非常重要，同时这个是简化版本的每一种攻击招式分开计算范围的方式）
	[HideInInspector]
	public float CtheAttackAreaAngel = 20f;//攻击范围的角度，自身前方锥形范围内都是攻击范围
	[HideInInspector]
	public float CtheViewAreaLength = 4f;//视野长度，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
	[HideInInspector]
	public float CtheViewAreaAngel = 30f;//视野的角度，同样，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
	[HideInInspector]
	public float CActerShieldMaxPercent = 0.15f;//护盾针对最大生命值的上限
///////////////////////////////////////////////////////////////////////////////////////////////////////	
//下面是游戏计算中的临时变量

	//[HideInInspector]//为了保证设定面板的简洁，暂时隐藏之
	public  float extraDamageForAnimation = 0;//设置为共有是为了传参数的时候方便，但是这个参数是不能够被主动在面板上设定的
	[HideInInspector]
	public bool isMainfighter = false;//是玩家控制的fighter

	public  GUISkin GUIShowStyleHP;//GUI显示的人物当前生命值
	public  GUISkin GUIShowStyleSP;//GUI显示的人物当前斗气值

	private bool isStarted =false;//是否已经开启

	//[HideInInspector]//为了保证设定面板的简洁，暂时隐藏之
	public string conNameToEMY ;//用于记录连招给出的额外buff
	//[HideInInspector]//为了保证设定面板的简洁，暂时隐藏之
	public string conNameToSELF;//用于记录连招给出的额外buff
	private float conNameCoolingTime = 1.5f;//如果这个特效脚本在1.5秒内还没有被使用，就认为无效
	//（否则准备一个脚本招式，然后等半天莫名其妙生效了不是好事）
	private float conNameCoolingTimeMax = 1.5f;

	//一个游戏人物的所有的武器拥有统一的冷却时间
	//冷却时间越短，造成伤害的时间间隔也就越短
	[HideInInspector]
	public  float timerForHit = 0.2f;
	[HideInInspector]
	public   float timerForHitMax = 0.2f;
	[HideInInspector]
	public  bool canHit =true;
 
	[HideInInspector]
	public  int winCountNow =0 ;//这个人物已经胜利的次数
	//有了静态的转换方法之后这个参数就不需要指定了，这样耦合性更低
	//public float TimePercent = 1;//武器冷却时间百分比，不同武器根据其动作可能会有不同的冷却时间
	//(这个属性在使用动画关键帧的时候被暂时弃用，但这个思路还是有的)
	private float DamageRead = 0;//记录已经收到的伤害，如果收到的伤害累积到一定数量就要播放受到攻击的动画
	//是否可以攻击命中
	public bool canAttack = true;

	//音效播放器
	[HideInInspector]
	public audioPlayer theAudioPlayer;//自己定义的音频播放器组件
	//值得注意的是声音的播放是在attackLink里面调用的，在这里保留引用是为了减少获取的次数

	//战斗状态标记，攻击或者受到攻击会刷新冷却时间
	public bool isFighting = false;
	public float fightingTimer = 7f;//战斗状态最少持续时间
	[HideInInspector]
	public float fightingTimerMax = 7f;//战斗状态持续时间上限

	//当前所有effect的attacklink模式
	[HideInInspector]
	public int EffectAttackLinkIndex = 0;

	//负重系数
	//是否负重参与计算
	public bool isWeightCanChangeSpeed = true;
	//负重越沉重，移动速度和攻击速度就会越慢
	[Range(0f,2f)]
	public float weightPercent = 0f;
	//此外一些额外的限制参数
	[HideInInspector]
	public float ActerAttackSpeedMaxPercent= 5f;//攻击速度百分比上限
	[HideInInspector]
	public float ActerMoveSpeedMaxPercent = 2.5f;//移动速度百分比上限

	//这是一个原始的功能，但是在发布之后没有使用，只是不断空转并且浪费了判断用的资源，应该注销以备后用
	[HideInInspector]
	public bool isShowingOnGUI = false;
	//不可遮挡的GUI血条
	// 这或许不是一个很好的方法
	// 因为不存在遮挡

	//网络控制节点=============================
	PhotonView photonView;
	//=========================================

	//增加经验的方法
	public void addJingYan(float adder  = 0)
	{
		jingyanNow += adder;
		int lvAdd= 0;
		if(jingyanNow > jingyanMax ) //经验足够准备升级
		{
			lvAdd = (int)  (jingyanNow / jingyanMax);
			jingyanNow =  jingyanNow % jingyanMax;
			jingyanMax += 50f;
		}
		for(int i = 0 ; i <lvAdd ; i++ )
		{
			if (this.playerLv < this.LVMax) 
			{
				this.playerLv++;
				OnLvIp ();
			}
		}
	}
	//升级的时候会发生什么效果放在这个里面
	private void OnLvIp()
	{
		ActerWuliDamage += 4f;
		CActerWuliDamage += 4f;
		float hpPercent = ActerHp / ActerHpMax;
		float spPercent = ActerSp / ActerSpMax;
		ActerHpMax += 20f;
		CActerHpMax += 20f;
		ActerSpMax += 5f;
		CActerSpMax += 5f;
		ActerHp = ActerHpMax * hpPercent;
		ActerSp = ActerSpMax * spPercent;
		//effect效果的钩子也因该发挥效用
		effectBasic [] theEffects = this.GetComponentsInChildren<effectBasic>();
		for (int i = 0; i < theEffects.Length; i++)
			theEffects [i].OnLvUp ();
		OnGUIStringForNAmeLV = this.ActerName + "(Lv." + this.playerLv + ")";//重新做一个GUI字符串
		systemValues.messageTitleBoxShow ("等级提升");
	}
	//有关经验和等级 OVER -----------------------------------------------------------------------------------------

	//进入到战斗状态
	public void getInFightState()
	{
		fightingTimer = fightingTimerMax;
		isFighting = true;
	}
	//战斗状态刷新
	public void fightStateUpdate()
	{
		if (isFighting) 
		{
			fightingTimer -= systemValues.updateTimeWait;
			if (fightingTimer < 0) 
			{
				fightingTimer = fightingTimerMax;
				isFighting = false;
				DamageRead /= 3f;//进入非战斗状态之后存储的伤害大幅度减少，这样就不会持续僵直
			}
		}
	}


	public void addDamageRead(float damage=0)//有些技能是带有额外伤害的，这个伤害也需要统计到这里面去
	{
		DamageRead += damage;
	}

	 
	//初始化备份的方法，只是在最开始的时候调用一次
	void startCValues()
	{
		//最基本的属性生命法力和名字
		CActerHpMax=ActerHpMax;//这个人物的生命上限
		CActerSpMax=ActerSpMax;//这个人物的法力上限
		CActerHpUp=ActerHpUp;//人物生命恢复
		CActerSpUp=ActerSpUp;//人物法力回复
		
		//特殊战斗属性
		CActerSuperBaldePercent=ActerSuperBaldePercent;//这个人物的暴击率
		CActerSuperBaldeAdder=ActerSuperBaldeAdder;//暴击时伤害的倍数

		CActerMissPercent = ActerMissPercent;//这个人物的闪避率
		CActerShielderPercent=ActerShielderPercent;//这个人物的格挡率
		CActerShielderDamageMiuns=ActerShielderDamageMiuns;//格挡住的伤害值
		CActerShielderDamageMiunsPercent=ActerShielderDamageMiunsPercent;//格挡住的伤害百分比 (先计算固定格挡，然后计算百分比格挡)
		
		
		//物理战斗属性
		CActerWuliDamage=ActerWuliDamage;//这个人物的物理攻击力
		CActerWuliReDamage=ActerWuliReDamage;//这个人物的物理反伤
		CActerWuliIner=ActerWuliIner;//这个人物的固定物理穿透
		CActerWuliInerPercent=ActerWuliInerPercent;//这个人物百分比穿透  （先计算固定穿透，然后计算百分比穿透）

		CActerDamageMinusValue = ActerDamageMinusValue;//百分比减伤
		CActerDamageMinusPercent = ActerDamageMinusPercent;//百分比减伤

		//物理防御属性
		CActerWuliShield=ActerWuliShield;//这个人物的物理护甲


		//生命吸取属性
		CActerHpSuck=ActerHpSuck;//人物的固定生命偷取值
		CActerHpSuckPercent=ActerHpSuckPercent;//根据所造成伤害的百分比生命吸取

		//移动速度百分比
		CActerMoveSpeedPercent = ActerMoveSpeedPercent;//人物的移动速度百分比

	}

	//真正的伤害方法组
	//所有的伤害都在这里计算
	//进行攻击方法组

 
	//在攻击命中的时候触发
	public void OnAttack(PlayerBasic thePlayerAim ,float extraDamage = 0 ,bool isSimple =false,bool isExtraAttack = false)
	{
		//也是原则，不对已经归西的目标附加伤害了
			if (!thePlayerAim.isAlive)
				return;
		//isExtraAttack表现为不用什么特殊动作直接造成伤害的条件
		//在这里似乎多做了一次判断
		//判断原因在于原先的攻击做法是连续的，而现在的攻击是离散的
			if(this.theAudioPlayer!= null)
			this.theAudioPlayer.playAttackSound ();//播放攻击音效
			//-------------------------------------------------------------------------------------
			float damage = 0;
			if(isSimple && isExtraAttack)//附加的真实伤害
				damage = extraDamage;
			if(isSimple && !isExtraAttack)//整体攻击真实伤害，附带额外真实伤害
				damage = extraDamage+extraDamageForAnimation+ActerWuliDamage;
			if(!isSimple && isExtraAttack)//附加的物理伤害
				damage = getTrueDamage (thePlayerAim, extraDamage,false);
			if(!isSimple && !isExtraAttack)//整体攻击物理伤害，附带额外物理伤害
				damage = getTrueDamage (thePlayerAim, extraDamage+extraDamageForAnimation);
			//-------------------------------------------------------------------------------------
		    float hpsuck =  makeHpSuck(  damage , thePlayerAim);//计算吸血
		    float spsuck =  makeSpSuck(damage , thePlayerAim);//计算吸蓝
		   float reDamage = makeReDamage(thePlayerAim);;//计算反伤
		   //单机动作控制
		   if(systemValues.theGameSystemMode == GameSystemMode.PC)
			thePlayerAim.OnBeAttack (damage);
		   //有些功能只在网络对战模式之下用就行
			if (systemValues.theGameSystemMode == GameSystemMode.NET) 
			{
			   if(this == systemValues.thePlayer) //游戏主人公的伤害时完全自治的
				thePlayerAim.photonView.RPC ("OnBeAttack", PhotonTargets.All, damage);
			   else if(this.gameObject.tag.Equals("AI")) //网络AI的伤害是各个客户端自己就计算了的，并没有强制的同步，所以还需要完善
				thePlayerAim.OnBeAttack (damage);
			}

		    //各种附加效果
			effectBasic[] Effects = this.GetComponentsInChildren<effectBasic> ();
			for (int i = 0; i < Effects.Length; i++) 
			{
				Effects [i].OnAttack ();
				Effects [i].OnAttack (thePlayerAim);
				Effects [i].OnAttack (thePlayerAim,damage);
			    Effects [i].OnHpUp (hpsuck);
			    Effects [i].OnSpUp (spsuck);
				Effects [i].OnBeAttack (reDamage);
			}
		   effectBasic[] EffectAim = thePlayerAim.GetComponentsInChildren<effectBasic> ();
		    for (int i = 0; i < EffectAim.Length; i++)
			    EffectAim [i].OnBeAttack (this);
           //额外的状态标记改变
		   getInFightState();
		  // makeValueUpdate();//为了保证网络效果完全同步，属性还是更新一下吧

		//print (this.ActerName +"攻击"+thePlayerAim.ActerName);
	}
		
	//有些攻击不想触发特效也不希望靠判断防止递归，就调用下面这两个方法
	//只有一些被标记为特殊效果的才会在这里显示效果出来
	public void OnAttackWithoutEffect(PlayerBasic thePlayerAim ,float extraDamage = 0 ,bool isSimple =false,bool isExtraAttack = false)
	{
			if(this.theAudioPlayer!= null)
			this.theAudioPlayer.playAttackSound ();//播放攻击音效
		    //-------------------------------------------------------------------------------------
			float damage = 0;
		    if(isSimple && isExtraAttack)//附加的真实伤害
			    damage = extraDamage;
		    if(isSimple && !isExtraAttack)//整体攻击真实伤害，附带额外真实伤害
			    damage = extraDamage+extraDamageForAnimation+ActerWuliDamage;
			if(!isSimple && isExtraAttack)//附加的物理伤害
			   damage = getTrueDamage (thePlayerAim, extraDamage,false);
			if(!isSimple && !isExtraAttack)//整体攻击物理伤害，附带额外物理伤害
			    damage = getTrueDamage (thePlayerAim, extraDamage+extraDamageForAnimation);
		   //-------------------------------------------------------------------------------------
		    float hpsuck =  makeHpSuck(  damage , thePlayerAim);//计算吸血
		    float spsuck =  makeSpSuck(damage , thePlayerAim);//计算吸蓝
		    float reDamage = makeReDamage(thePlayerAim);;//计算反伤
		//单机动作控制
		if(systemValues.theGameSystemMode == GameSystemMode.PC)
			thePlayerAim.OnBeAttack (damage);
		//有些功能只在网络对战模式之下用就行
		if (systemValues.theGameSystemMode == GameSystemMode.NET) 
		{
			if(this == systemValues.thePlayer) //游戏主人公的伤害时完全自治的
				thePlayerAim.photonView.RPC ("OnBeAttack", PhotonTargets.All, damage);
			else if(this.gameObject.tag.Equals("AI")) //网络AI的伤害是各个客户端自己就计算了的，并没有强制的同步，所以还需要完善
				thePlayerAim.OnBeAttack (damage);
		}
		
	     	//只有特殊的一类效果了才能够有效果，其余特效无效，所以是WithoutEffect
			effectBasic[] Effects = this.GetComponentsInChildren<effectBasic> ();
			for (int i = 0; i < Effects.Length; i++) 
			{
				if (Effects [i].isExtraUse ()) 
				{
					Effects [i].OnAttack ();
					Effects [i].OnAttack (thePlayerAim);
					Effects [i].OnAttack (thePlayerAim, damage);
				    Effects [i].OnHpUp (hpsuck);
				    Effects [i].OnSpUp (spsuck);
				    Effects [i].OnBeAttack (reDamage);
				}
			}
			effectBasic[] EffectAim = thePlayerAim.GetComponentsInChildren<effectBasic> ();
			for (int i = 0; i < EffectAim.Length; i++) 
			{
				if(EffectAim [i].isExtraUse())
				  EffectAim [i].OnBeAttack (this);
			}
		   //额外的状态标记改变
		   getInFightState();
		   //makeValueUpdate();//为了保证网络效果完全同步，属性还是更新一下吧
	}


	[PunRPC]	
	public void OnBeAttack(float damage)
	{
		//this.gameObject .tag != "AI"说明是小兵
		//只有英雄才需要被start

		if ( !isStarted )
			return;
		
		if (this.isAlive )//只有在活着的时候才可以被攻击
		{
			//计算减伤
			damage -= ActerDamageMinusValue;
			damage *= (1 - ActerDamageMinusPercent);
			damage = damage < 0 ? 0 : damage;

			DamageRead += damage;//累计伤害

			//如果护盾比较厚，就吸收所有的伤害
			if (ActerShieldHp > damage)
			{
				ActerShieldHp -= damage;
				damage = 0;
			} 
			else//如果护盾不够厚，护盾消失
			{
				damage -= ActerShieldHp;
				ActerShieldHp = 0;
			}
			this.ActerHp -= damage;

			effectBasic[] Effects = this.GetComponentsInChildren<effectBasic> ();
			for (int i = 0; i < Effects.Length; i++)
				Effects [i].OnBeAttack (damage);


			//如果收受到了较重的伤害，那么就取消当前的攻击动作，强制转为受到攻击的动作，并且攻击力转化为0
			//此外beHit状态已经在systemValues里面标注为无法造成伤害的状态之一
			//在这里将额外攻击力取消掉是因为这个是强制转换的，可能会有动作做到一半的情况，也许会有额外的伤害误差
			//此外，重击伤害暂定为一次受到最大生命值5%以上的伤害，但这是有误差的，因为有些攻击时存在效果伤害的
			//初步的思路是在效果中额外添加受到伤害的动作使之成为一种必然发生的动画

			if (damage > this.ActerHpMax * 0.25f || DamageRead > this.ActerHpMax*0.45f) 
			{ //伤害范围需要界定
				extraDamageForAnimation /= 2;//消除自身攻击效果
				DamageRead  = 0;//重新统计伤害
				for (int i = 0; i < Effects.Length; i++) 
					Effects [i].OnShowText ("僵直，伤害减半");
			}
			//额外的状态标记改变
			getInFightState();

			//print (this.ActerName + " is being attacked!");
		}
	}


	float getTrueDamage(PlayerBasic thePlayerAim, float extraDamage =0,bool withBasicDamageCanculate = true )//真正的计算伤害的方法，这个方法被“攻击”的时候调参数为攻击者经过计算的伤害
	{
		//根本就没有命中
		if (Random.value > this.ActerAttackAtPercent) 
		{
			effectBasic [] effectsGet = this.GetComponentsInChildren<effectBasic> ();
			for (int i = 0; i < effectsGet.Length; i++)
				effectsGet [i].OnDoNotAttackAt (thePlayerAim);

			return  0 ; 
		}
		
		if (Random.value < thePlayerAim.ActerMissPercent)
		{
			//攻击者暴击之后的特殊效果
			effectBasic[] Effects = thePlayerAim.GetComponentsInChildren<effectBasic> ();
			for (int i = 0; i < Effects.Length; i++)
				Effects[i].OnMiss(this);
			
			return 0f;//伤害整个被无视，也就是被闪避了
		}
		float damageMake = extraDamage;
		if (withBasicDamageCanculate)
			damageMake += this.ActerWuliDamage   - this. ActerWuliIner;

		//护甲伤害减免计算------------------------
		float hujiaGet = thePlayerAim .ActerWuliShield*(1- this.ActerWuliInerPercent);
		//山寨英雄联盟的伤害减免计算公式...
		//还是应该经常计算，因为护甲和穿透都不一定
		float hujiaRato = Mathf.Clamp( (1f- hujiaGet / (hujiaGet + 900f)) , 0f, 1f);
		//damageMake *= 1 - (hujiaGet  / 1500);//纯粹线性的方法，似乎娱乐性不是很够
		damageMake *=  hujiaRato ;
		//-----------------------------------------

		damageMake += this. ActerWuliIner;
		damageMake = damageMake * (1 + this.ActerDamageAdderPercent) + ActerDamageAdder;

		if (Random.value < thePlayerAim.ActerShielderPercent)//目标发生格挡
		{
			float damageMinus = damageMake;
			damageMake -= thePlayerAim.ActerShielderDamageMiuns;
			damageMake = Mathf.Clamp (damageMake, 0, ActerWuliDamage);//防止格挡过多变成负数
			damageMake *= (1- thePlayerAim.ActerShielderDamageMiunsPercent);
			damageMake = Mathf.Clamp (damageMake, 0, ActerWuliDamage);//防止百分比格挡过多变成负数

			damageMinus = damageMinus - damageMake;//计算最后格挡掉的伤害
			//攻击者暴击之后的特殊效果
			effectBasic[] Effects = thePlayerAim.GetComponentsInChildren<effectBasic> ();
			for (int i = 0; i < Effects.Length; i++)
				Effects[i].OnShield(this,damageMinus);
		}

		if (Random.value < ActerSuperBaldePercent)//攻击者暴击
		{
			damageMake*= ActerSuperBaldeAdder;
			//攻击者暴击之后的特殊效果
			effectBasic[] Effects = this.GetComponentsInChildren<effectBasic> ();
			for (int i = 0; i < Effects.Length; i++)
				Effects[i].OnSuperBlade(thePlayerAim,damageMake);
		}

		return damageMake;
	}

	//计算吸血，反伤等等最后计算之后的生命收益（可以为正也可以为负）
	float makeHpSuck(float damageMake , PlayerBasic thePlayerAim)
	{
		float hpChanger = damageMake * ActerHpSuckPercent + ActerHpSuck;
		this.ActerHp += hpChanger;//关于反伤和吸血的制作比较简单
		return  hpChanger;
	}
	float makeReDamage(PlayerBasic thePlayerAim)
	{
		float damge = thePlayerAim.ActerWuliReDamage * (1 - this.ActerWuliShield / 1500);
		this.ActerHp -= damge;
		return damge;
	}
	//计算吸蓝
	float makeSpSuck(float damageMake , PlayerBasic thePlayerAim)
	{
		float spChanger = damageMake * ActerSpSuckPercent + ActerSpSuck;
		this.ActerSp += spChanger;//关于反伤和吸血的制作比较简单
		return spChanger;
	}

	public void flashConNameTimer()
	{
		conNameCoolingTime = conNameCoolingTimeMax;
	}


	public string getPlayerInformation(bool showHpMax = true)
	{
		StringBuilder theString = new StringBuilder ();
		theString.Append ("生命值上限  ");
		theString.Append ((int)this.ActerHpMax);
		theString.Append ("");
		theString.Append ("斗气值上限  ");
		theString.Append ((int)this.ActerSpMax) ;
		theString.Append ("\n");
		theString.Append ("生命恢复  " );
		theString.Append (this.ActerHpUp .ToString ("f2"));
		theString.Append ("/秒   ");
		theString.Append ("斗气回复  ");
		theString.Append (this.ActerSpUp .ToString ("f2"));
		theString.Append ("/秒\n");

		theString.Append ("护甲  ");
		theString.Append (this.ActerWuliShield .ToString ("f0"));
		theString.Append ("(减免");
		float hujiaRato = 1f - Mathf.Clamp( (1f- ActerWuliShield / (ActerWuliShield  + 900f)) , 0f, 1f);
		theString.Append ((hujiaRato * 100).ToString("f1"));
		theString.Append ("%)");

		theString.Append ("   ");
		theString.Append ("伤害  ");
		theString.Append (this.ActerWuliDamage .ToString("f0"));
		theString.Append ("   ");
		theString.Append ("反伤  ");
		theString.Append (this.ActerWuliReDamage.ToString("f0"));
		theString.Append ("\n");

		theString.Append ("暴击率  ");
		theString.Append ((this.ActerSuperBaldePercent *100).ToString("f1"));
		theString.Append ("%   ");
		theString.Append ("暴击伤害加成  ");
		theString.Append ((this.ActerSuperBaldeAdder*100).ToString("f1"));
		theString.Append ("%\n闪避率  ");
		theString.Append ((this.ActerMissPercent *100).ToString("f0"));
		theString.Append ("%   命中率"+(this.ActerAttackAtPercent*100).ToString("f0"));
		theString.Append ("%   格挡率");
		theString.Append ((this.ActerShielderPercent *100).ToString("f0"));
		theString.Append ("%   \n");
		theString.Append ("格挡伤害减免  ");
		theString.Append (this.ActerShielderDamageMiuns.ToString ("f1"));
		theString.Append ("+");
		theString.Append ((this. ActerShielderDamageMiunsPercent *100).ToString("f0"));
		theString.Append ("%   ");
		theString.Append ("护盾上限  ");
		theString.Append ((this.ActerShieldMaxPercent*100));
		theString.Append ("%\n");
		theString.Append ("护甲穿透  ");
		theString.Append (this. ActerWuliIner.ToString("f1"));
		theString.Append ("+");
		theString.Append ((this.ActerWuliInerPercent*100).ToString("f1"));
		theString.Append ("%\n");
		theString.Append ("伤害/生命转化  ");
		theString.Append (this.ActerHpSuck.ToString("f1"));
		theString.Append ("+");
		theString.Append ((this.ActerHpSuckPercent*100).ToString("f0"));
		theString.Append ("%\n");
		theString.Append ("伤害/斗气转化  ");
		theString.Append (this.ActerSpSuck.ToString("f1"));
		theString.Append ("+");
		theString.Append ((this.ActerSpSuckPercent*100).ToString("f0"));
		theString.Append ("%\n");
		theString.Append ("额外伤害  ");
		theString.Append ((this.ActerDamageAdder).ToString("f1"));
		theString.Append ("+");
		theString.Append ((this.ActerDamageAdderPercent*100).ToString("f1"));
		theString.Append ("%\n");
		theString.Append ("额外减伤  ");
		theString.Append ((this.ActerDamageMinusValue).ToString("f1"));
		theString.Append ("+");
		theString.Append ((this.ActerDamageMinusPercent*100).ToString("f1"));
		theString.Append ("%\n");

		string attackLengthShow =  (this.theAttackAreaLength >0) ? this.theAttackAreaLength*100+"%   ": "【特殊】    ";
		string attackAreaShow = (this.theAttackAreaAngel >0) ? this.theAttackAreaAngel.ToString ("f1")  : "【特殊】";

		theString.Append ("攻击距离  ");
		theString.Append (attackLengthShow);
		theString.Append ("   攻击范围  ");
		theString.Append (attackAreaShow);
		theString.Append ("\n");
		theString.Append ("攻击速度  ");
		theString.Append ((ActerAttackSpeedPercent * 100).ToString ("f0"));
		theString.Append ("%");
		//theString.Append ("/");
		//theString.Append ((ActerAttackSpeedMaxPercent * 100).ToString ("f0"));
		//theString.Append ("%");
		theString.Append ("   移动速度  ");
		theString.Append ((ActerMoveSpeedPercent*100).ToString("f0"));
		theString.Append ("%");
		//theString.Append ("/");
		//theString.Append ((ActerMoveSpeedMaxPercent*100).ToString("f0"));
		//theString.Append ("%");
		theString.Append ("\n负重  ");
		theString.Append ((weightPercent * 100).ToString ("f0"));
		theString.Append ("%");

		return theString.ToString ();

		//下面这些内容是上面文本的排版
		//因为string相加的开销有点大，所以用了StringBuilder来做这件事，可以减少不少的GC
//		string information = "";
//		//information += "=======" + this.ActerName+"=======\n\n";
//		if (showHpMax)//因为有些时候生命值上限这种显示特殊用slider来做，就没有必要多次显示了
//		{
//			information += "生命值上限  " + (int)this.ActerHpMax + "   ";
//			information += "斗气值上限  " + (int)this.ActerSpMax + "\n";
//		}
//		information += "生命恢复  " + this.ActerHpUp .ToString ("f2")+"/秒   ";
//		information += "斗气回复  " + this.ActerSpUp .ToString ("f2") + "/秒\n";
//		information += "护甲  " + this.ActerWuliShield .ToString ("f1")+"   ";
//		information += "伤害  " + this.ActerWuliDamage .ToString("f1")+"   ";
//		information += "反伤  "+this.ActerWuliReDamage.ToString("f1")+"\n";
//		return information;

//下面这些内容是上面文本的排版
//因为string相加的开销有点大，所以用了StringBuilder来做这件事，可以减少不少的GC
//		string information = "";
//		information += "暴击率  "+(this.ActerSuperBaldePercent *100).ToString("f1")+"%   ";
//		information += "暴击伤害加成  "+(this.ActerSuperBaldeAdder*100).ToString("f1")+"%\n";
//		information += "闪避率  "+(this.ActerMissPercent *100).ToString("f0")+"%   命中率"+(this.ActerAttackAtPercent*100).ToString("f0")+"%   ";
//		information += "格挡率  "+(this.ActerShielderPercent *100).ToString("f0")+"%   \n";
//		information += "格挡伤害减免  " + this.ActerShielderDamageMiuns.ToString ("f1")+ "+"+(this. ActerShielderDamageMiunsPercent *100).ToString("f0")+"%   ";
//		information += "护盾上限  " + (this.ActerShieldMaxPercent*100)+"%\n";
//		information += "护甲穿透  "+this. ActerWuliIner.ToString("f1")+"+"+(this.ActerWuliInerPercent*100).ToString("f1")+"%\n";
//		information += "伤害/生命转化  "+this.ActerHpSuck.ToString("f1")+"+"+(this.ActerHpSuckPercent*100).ToString("f0")+"%\n";
//		information += "伤害/斗气转化  "+this.ActerSpSuck.ToString("f1")+"+"+(this.ActerSpSuckPercent*100).ToString("f0")+"%\n";
//		information += "额外伤害  "+(this.ActerDamageAdder).ToString("f1")+"+"+(this.ActerDamageAdderPercent*100).ToString("f1")+"%\n";
//		information += "额外减伤  "+(this.ActerDamageMinusValue).ToString("f1")+"+"+(this.ActerDamageMinusPercent*100).ToString("f1")+"%\n";
//		string attackLengthShow =  (this.theAttackAreaLength >0) ? this.theAttackAreaLength*100+"%   ": "[特殊]    ";
//		string attackAreaShow = (this.theAttackAreaAngel >0) ? this.theAttackAreaAngel.ToString ("f1")  : "[特殊]";
//		information += "攻击距离  "+attackLengthShow ;
//		information += "   攻击范围  " + attackAreaShow +"\n";
//		information += "攻击速度  " + (ActerAttackSpeedPercent * 100).ToString ("f0") + "%";
//		information += "   移动速度  " + (ActerMoveSpeedPercent*100).ToString("f0")+"%";
//			
//		return information;
	}
		
	/*****************************************************************************************/
	//这个方法每一帧都会调用，刷新任务的属性
	//并没有使用update方法而是使用invoke方法
	//inovke的调用时间间隔由systemValues类进行统一配置


	public List<effectBasic> Effects = new List<effectBasic> ();
	List<effectBasic> adder = new List<effectBasic> ();

	void flashEffects()
	{
		adder.Clear ();
		for (int i = 0; i < Effects.Count; i++) 
			if (Effects [i] != null)
				adder.Add (Effects[i]);

		Effects.Clear ();
		for (int i = 0; i < adder.Count; i++)
			Effects.Add (adder[i]);
	}

	public void updateValue()
	{
		if (isStarted && isAlive) 
		{
			//这一次循环可以调用的效果都在这里
			//effectBasic[] Effects = this.GetComponentsInChildren<effectBasic> ();
			flashEffects();
			//护盾是有上限的 
			float theMaxOfShield = ActerHpMax * ActerShieldMaxPercent;
			ActerShieldHp = Mathf.Clamp(ActerShieldHp , 0f ,theMaxOfShield ) ;
			//默认机制就是每一次恢复每秒钟的生命恢复再检查是否死亡
			if (ActerHp < ActerHpMax) 
			{
				float hpupValue = ActerHpUp * systemValues.updateTimeWait;
				ActerHp += hpupValue;
				for (int i = 0; i < Effects.Count; i++) 
				{
					Effects [i].OnHpUp ();
					Effects [i].OnHpUp (hpupValue);
				}
				if (ActerHp > ActerHpMax) 
					ActerHp = ActerHpMax; //最后一个修正
			}
			if (ActerHp < 0)
			{
				ActerHp = 0;	
				isAlive = false;
				makeDeadEffect ();
			}

			if (ActerSp < ActerSpMax) 
			{
				float spupValue = ActerSpUp * systemValues.updateTimeWait;
				ActerSp += spupValue;
				for (int i = 0; i < Effects.Count; i++)
				{
					Effects [i].OnSpUp ();
					Effects [i].OnSpUp (spupValue);
				}
				ActerSp = Mathf.Clamp (ActerSp, 0, ActerSpMax);
			}

			if (!systemValues.isNullOrEmpty (conNameToEMY) || !systemValues.isNullOrEmpty(conNameToSELF)) 
			{
				conNameCoolingTime -= systemValues.updateTimeWait;
				if (conNameCoolingTime <= 0)
				{
					conNameCoolingTime = conNameCoolingTimeMax;
					conNameToEMY = "";
					conNameToSELF = "";
				}
			}
			else
				conNameCoolingTime = conNameCoolingTimeMax;//此处多次空转赋值实际上是一个很大的浪费但是这句话非常必要

			for (int i = 0; i < Effects.Count; i++)
				Effects [i] .effectOnUpdateTime ();
			
			//战斗状态的刷新管理
			fightStateUpdate ();

			//负重计算
			if (isWeightCanChangeSpeed) 
			{
				float trueSpeedPercnet = Mathf.Clamp ((0.85f + (1 - weightPercent) * 0.15f), 0.6f, 1f);
				ActerAttackSpeedPercent = CActerAttackSpeedPercent * trueSpeedPercnet;
				ActerMoveSpeedPercent = CActerMoveSpeedPercent * trueSpeedPercnet;
			}
			//最后再来一层整体的百分比加速
			ActerAttackSpeedPercent *= ActerSpeedOverPervnet;
			ActerMoveSpeedPercent *= ActerSpeedOverPervnet;
			ActerAttackSpeedPercent = Mathf.Clamp( ActerAttackSpeedPercent ,0f, ActerAttackSpeedMaxPercent);
			ActerMoveSpeedPercent = Mathf.Clamp( ActerMoveSpeedPercent ,0f, ActerMoveSpeedMaxPercent);
		}
	}

	//死毕竟就只有一次，耗费多一点就多一点吧
	private void makeDeadEffect()
	{
		//this.gameObject.tag = "dead";
		//Destroy (this.gameObject, 1.5f);
		try
		{
			effectBasic[] Effects = this.GetComponentsInChildren<effectBasic> ();
			for(int i = 0 ; i < Effects.Length; i++)
				Effects[i].OnDead();
			this.GetComponent <attackLinkController> ().enabled = false;
			this.GetComponent <move> ().enabled = false;
			this.enabled = false;
			this.GetComponent <BoxCollider> ().enabled = false;

			CharacterController CC = this.GetComponent <CharacterController> ();
			if(CC) Destroy(CC );
			//this.transform.position = new Vector3 (this.transform .position .x , 2f , this.transform .position .z);
			//if(!this.gameObject.GetComponent <Rigidbody>())
			//	this.gameObject.AddComponent<Rigidbody>();
			
			shutArea();

			FSMStage theFSM = this.GetComponent <FSMStage>();
			if( theFSM ) theFSM .enabled = false;
			if(systemValues.thePlayer == this)
				systemValues.makeGameEnd("胜败，不过是兵家常事,\n我们可以，还可以从头再来。");
			else
				Destroy(this.gameObject ,15f);
		}
		catch 
		{
			//print ("组件缺失或者不必存在这个组件");
		}
		if (systemValues.theGameSystemMode == GameSystemMode.NET && photonView!= null)
		{
			photonView.RPC ("plaDeadAnimations", PhotonTargets.All, "dead");
		}
		else if (systemValues.theGameSystemMode == GameSystemMode.PC)
		{
			plaDeadAnimations ("dead");
		}
	}

	//自添加脚本
	//这是对外方法，需要在这里分为两种
	//根据的单机模式和网络对战模式做两种操作，仅此而已
	public void addEffects(string effectName)
	{
		if (systemValues.theGameSystemMode == GameSystemMode.NET && photonView!= null)
		{
			photonView.RPC ("addEffectsForSelf", PhotonTargets.All, effectName);
		}
		else if (systemValues.theGameSystemMode == GameSystemMode.PC)
		{
			addEffectsForSelf(effectName);
		}
	}


	public void makeStart()//初始化方法，由总控单元统一进行初始化
	{
		//这个应该是最先初始化的，因为有一些声音可能需要提前使用
		theAudioPlayer = this.GetComponent <audioPlayer> ();
		startCValues();//因为只有服务器上面的英雄才会使用这些参数
		InvokeRepeating("updateValue",0,systemValues .updateTimeWait);//每隔一秒钟计算额外的计算脚本

		if (systemValues.theGameSystemMode == GameSystemMode.NET) 
		{
			photonView = PhotonView.Get (this);
		}

		isStarted = true;
	}

	//网络主控人物才会有的方法
	public void makeStartForPrivate()
	{
		//网络上强制更新各种状态
		InvokeRepeating("makeValueUpdate" , 0 , 10f);
	}

	private void makeGUIStart()
	{
		//只有有BloodBasic，也就是血条显示能力的Player才需要Load资源
		if (this.GetComponent<BloodBasic> ()) 
		{
			GUIShowStyleHP = (GUISkin)Resources.Load ("UI/hpGUISkin");
			GUIShowStyleSP = (GUISkin)Resources.Load ("UI/spGUISkin");
			OnGUIStringForNAmeLV = this.ActerName + "(Lv." + this.playerLv + ")";//重新做一个GUI字符串
		}
	}

	//随时都进行网络更新太费事而且还有网络延迟的问题所以这是一个很低频率的更新
	//强制刷新确实可以解决很多麻烦问题，但是开销不容小觑
	//或许可以找到一个更好的架构或者方法来解决这个问题
	//有些地方需要强制更新
	public  void makeValueUpdate()
	{
		//更新网络上其他客户端这个人物的属性
		if (systemValues.theGameSystemMode == GameSystemMode.NET && photonView != null) 
		{
			photonView.RPC ("updateOthersValue", PhotonTargets.All, 
				ActerHpMax, ActerHp, ActerSpMax, ActerSp , ActerHpUp, ActerSpUp,
				ActerSuperBaldePercent, ActerSuperBaldeAdder, ActerMissPercent ,ActerShielderPercent,
				ActerShielderDamageMiuns, ActerShielderDamageMiunsPercent,
				ActerWuliDamage, ActerWuliReDamage, ActerWuliIner, ActerWuliInerPercent,
				ActerWuliShield,  ActerHpSuck, ActerHpSuckPercent, ActerSpSuck , ActerSpSuckPercent,
				ActerDamageAdderPercent , ActerDamageAdder , ActerMoveSpeedPercent, ActerShieldHp , ActerDamageMinusValue ,
				ActerDamageAdderPercent ,ActerAttackSpeedPercent
			);
		}
	}
	public void addEffectUpdate(string theEffect)
	{
		if (systemValues.theGameSystemMode == GameSystemMode.NET) 
		{
			photonView.RPC ("addEffectRpc", PhotonTargets.All,theEffect);
		} 
		else 
		{
			addEffectRpc(theEffect);
		}
	}

	public void dropEffectUpdate(string theEffectName)
	{
		if (systemValues.theGameSystemMode == GameSystemMode.NET)
		{
			photonView.RPC ("dropEffectRpc", PhotonTargets.All,theEffectName);
		} 
		else 
		{
			dropEffectRpc(theEffectName);
		}
	}
	//----------------------------------------------------------------------------------------------------------------------------------------------------------------//
	public void updateWulingEffects()
	{
		wuling theWulingEffect = this.GetComponent <wuling> ();
		if (!theWulingEffect)
			return;
		//五灵阴阳赋值
		//风
		float wind1 = 0f;
		lingBasic windE1 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.wind ,1 );
		if (windE1 != null) wind1 = windE1.value;
		float wind2 = 0f;
		lingBasic windE2 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.wind ,2 );
		if (windE2 != null) wind2 = windE2.value;
		//雷
		float thunder1 = 0f;
		lingBasic thunderE1 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.thunder ,1 );
		if (thunderE1 != null) thunder1 = thunderE1.value;
		float thunder2 = 0f;
		lingBasic thunderE2 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.thunder ,2 );
		if (thunderE2 != null) thunder2 = thunderE2.value ;
		//水
		float water1 = 0f;
		lingBasic waterE1 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.water ,1 );
		if (waterE1 != null)  water1 = waterE1.value;
		float water2 = 0f;
		lingBasic waterE2 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.water ,2 );
		if (waterE2 != null)  water2 = waterE2.value;
		//火
		float fire1 = 0;
		lingBasic fireE1 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.fire ,1 );
		if (fireE1 != null)  fire1 = fireE1.value;
		float fire2 = 0;
		lingBasic fireE2 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.fire ,2 );
		if (fireE2 != null) fire2 = fireE2.value;
		//土
		float earth1 = 0f;
		lingBasic earthE1 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.earth ,1 );
		if (earthE1 != null) earth1 = earthE1.value;
		float earth2 = 0f;
		lingBasic earthE2 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.earth ,2 );
		if (earthE2 != null) earth2 = earthE2.value;

		if (systemValues.theGameSystemMode == GameSystemMode.NET)
		{
			photonView.RPC ("updateWuling", PhotonTargets.All, wind1,wind2,thunder1,thunder2,water1,water2,fire1,fire2,earth1,earth2);
		} 
		else 
		{
			//单机版这个似乎没有必要，暂时先不用
			//updateWuling (wind1,wind2,thunder1,thunder2,water1,water2,fire1,fire2,earth1,earth2);
		}
	}

	[PunRPC]
	private void updateWuling(float wind1,float wind2,float thunder1,float thunder2 , float water1 , float water2 , float fire1 , float fire2 , float earth1 , float earth2)
	{
		wuling theWulingEffect = this.GetComponent <wuling> ();
		if (!theWulingEffect)
			return;
		//五灵阴阳赋值
		//风
		lingBasic windE1 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.wind ,1 );
		if (windE1 != null) windE1.value = wind1;
		lingBasic windE2 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.wind ,2 );
		if (windE2 != null) windE2.value = wind2;
		//雷
		lingBasic thunderE1 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.thunder ,1 );
		if (thunderE1 != null) thunderE1.value = thunder1;
		lingBasic thunderE2 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.thunder ,2 );
		if (thunderE2 != null) thunderE2.value = thunder2;
		//水
		lingBasic waterE1 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.water ,1 );
		if (waterE1 != null) waterE1.value = water1;
		lingBasic waterE2 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.water ,2 );
		if (waterE2 != null) waterE2.value = water2;
		//火
		lingBasic fireE1 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.fire ,1 );
		if (fireE1 != null) fireE1.value = fire1;
		lingBasic fireE2 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.fire ,2 );
		if (fireE2 != null) fireE2.value = fire2;
		//土
		lingBasic earthE1 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.earth ,1 );
		if (earthE1 != null) earthE1.value = earth1;
		lingBasic earthE2 =  getthLingWithType(theWulingEffect.lingEffects, wulingType.earth ,2 );
		if (earthE2 != null) earthE2.value = earth2;
	}

	private lingBasic getthLingWithType(List<lingBasic>lingEffects, wulingType theTypeCheck, int yinyang)
	{
		for (int i = 0; i < lingEffects.Count; i++) 
		{
			if (lingEffects [i].theType == theTypeCheck && lingEffects [i].getYinYagType() == yinyang)
				return lingEffects [i];
		}
		return null;
	}


	//----------------------------------------------------------------------------------------------------------------------------------------------------------------//

	[PunRPC]
	private void dropEffectRpc(string theEffectName)
	{
		System.Type theType = System.Type.GetType (theEffectName);
		if (theType == null)
			return;

		effectBasic theEffect = (effectBasic)this.GetComponent (theType);
		if(theEffect)
			DestroyImmediate (theEffect);
	}
	[PunRPC]
	private void addEffectRpc(string theEffect)
	{
		System.Type theType = System.Type.GetType (theEffect);
		if (theType == null)
			return;

		//装备的注灵效果应该是可以叠加的，这部是唯一被动
		//顺带解决了多个具有相同注灵效果的装备同时装备，当卸下一个装备的时候所有效果都消失的问题

		//if (!thePlayer.gameObject.GetComponent ( theType))
		  gameObject.AddComponent (theType);
		//else
		//	((effectBasic)thePlayer.GetComponent (theType)).updateEffect ();
	}



	static GameObject TheAreaShowerObj;
	GameObject theAreaShower;
	private  void showArea()
	{
		if (!TheAreaShowerObj)
			TheAreaShowerObj = Resources.Load<GameObject> ("effects/AreaShow3D");
		theAreaShower = (GameObject)GameObject.Instantiate (TheAreaShowerObj );
		theAreaShower.transform.SetParent (this.transform);
		theAreaShower.transform.localPosition = Vector3.zero;
		theAreaShower.transform.localRotation = Quaternion.identity;
		areaShower3D shower = theAreaShower.GetComponentInChildren <areaShower3D> ();
		if (shower)
		{
			//print ("show");
			shower.thePlayer = this;
			shower.makeFlash ();
		}
		//else
			//print ("no shower");

	}

	private void shutArea()
	{
		Destroy (theAreaShower.gameObject);
	}

	//--------------------------回调方法---------------------------------------------------//
	//由于这个类是一个究极的父类，因此具体的工作是不做的，但是留下了调用各种方法的方式木板
	void Start () 
	{
		makeStart ();
		makeGUIStart ();

		showArea ();
	}


	//有些东西可以提前计算，之后直接引用啊！
	private Rect Rect1 = new Rect (25, 0, 100, 23);
	private Rect Rect2 = new Rect (10, 23, 127, 15);
	private Rect Rect3 = new Rect (10, 39, 127, 15);
	private Rect RectHP = new Rect (12, 24, 120, 13);
	private Rect RectSP = new Rect (12, 40, 120, 13);
	private Rect RectPosition = new Rect (0, 0, 155, 100);
	private string OnGUIStringForNAmeLV = "";

	void OnGUI()
	{ 
		if (!systemValues.isGamming)
			return;

		if (isShowingOnGUI  && !this.isMainfighter  &&  isAlive &&  GUIShowStyleHP!=null &&  GUIShowStyleSP!=null )
		{
			if (!systemValues.isSystemUIUsing ()) 
			{
				//print (this.ActerName + " is GUI showing");
				float rotoForHp = Mathf.Clamp ((this.ActerHp / this.ActerHpMax), 0f, 1f);
				float rotoForSp = Mathf.Clamp ((this.ActerSp / this.ActerSpMax), 0f, 1f);
				Vector2 c = Camera.main.WorldToScreenPoint (new Vector3 (this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z));

				RectPosition.x = c.x;
				RectPosition.y = Screen.height - c.y;
				RectHP.width = 120 * rotoForHp;
				RectSP.width = 120 * rotoForSp;

				GUI.BeginGroup (RectPosition);
				GUI.Box (Rect1 ,OnGUIStringForNAmeLV );
				GUI.Box (Rect2, "");
				GUI.Box (RectHP, "", GUIShowStyleHP.box);
				GUI.Box (Rect3, "");
				GUI.Box (RectSP, "", GUIShowStyleSP.box);
				GUI.EndGroup ();



			}
		}
	}


	//--------------------------网络调用方法---------------------------------------------------//
	//真正添加脚本的能力
	[PunRPC]
	public void  addEffectsForSelf(string nameForEffect)
	{
		this.gameObject.AddComponent (System.Type.GetType (nameForEffect));
	}

	//网络播放动画
	//实际上目前只播放死亡动画
	[PunRPC]
	public void plaDeadAnimations(string nameIn)
	{
		Animator theAnimator = this.GetComponentInChildren <Animator>();
		if(theAnimator)
			theAnimator.Play(nameIn);
	}

	//网络更新基础数值
	//因为网络上出了自己客户端控制的privateFighter之外，支部会start的，也不应该这样
	//因此需要一种技术数据保持同步的方法
	//因为本游戏的攻击方式使用动画和坐标进行触发，而坐标已经被封装好
	//所以只需要更新当前数值就好
	//之所以将更多的数据传过去是想保证在各个机器上的额外效果保持一致
	//例如一个附加当前护甲值10%伤害的效果，如果不保持同步就会出错
	[PunRPC]
	public void updateOthersValue
	(
		float ActerHpMaxIn, float ActerHpIn, float ActerSpMaxIn, float ActerSpIn , float ActerHpUpIn, float ActerSpUpIn,
		float ActerSuperBaldePercentIn, float ActerSuperBaldeAdderIn, float ActerMissPercentIn ,float ActerShielderPercentIn,
		float ActerShielderDamageMiunsIn ,float  ActerShielderDamageMiunsPercentIn,
		float ActerWuliDamageIn, float ActerWuliReDamageIn, float ActerWuliInerIn, float ActerWuliInerPercentIn,
		float ActerWuliShieldIn,  float ActerHpSuckIn, float  ActerHpSuckPercentIn, float ActerSpSuckIn , float ActerSpSuckPercentIn,
		float ActerDamageAdderPercentIn , float ActerDamageAdderIn , float ActerMoveSpeedPercentIn , float ActerShieldHpIn,
		float ActerDamageMinusValueIn , float ActerDamageMinusPercentIn , float ActerAttackspeedPercentIn
	)
	{
		//最基本的属性生命法力和名字
		ActerHpMax = ActerHpMaxIn;//这个人物的生命上限
		ActerHp = ActerHpIn;//这个人物当前的生命值
		ActerSpMax = ActerSpMaxIn;//这个人物的法力上限
		ActerSp =  ActerSpIn ;//这个人物当前的法力值
		ActerHpUp = ActerHpUpIn;//人物生命恢复
		ActerSpUp = ActerSpUpIn ;//人物法力回复

		//特殊战斗属性
		ActerSuperBaldePercent = ActerSuperBaldePercentIn;//这个人物的暴击率
		ActerSuperBaldeAdder = ActerSuperBaldeAdderIn;//暴击时伤害的倍数

		ActerMissPercent = ActerMissPercentIn;//这个人物的闪避率

		ActerShielderPercent = ActerShielderPercentIn;//这个人物的格挡率
		ActerShielderDamageMiuns =  ActerShielderDamageMiunsIn;//格挡住的伤害值
		ActerShielderDamageMiunsPercent = ActerShielderDamageMiunsPercentIn;//格挡住的伤害百分比 (先计算固定格挡，然后计算百分比格挡)

		//物理战斗属性
		ActerWuliDamage = ActerWuliDamageIn;//这个人物的物理攻击力
		ActerWuliReDamage = ActerWuliReDamageIn;//这个人物的物理反伤
		ActerWuliIner = ActerWuliInerIn;//这个人物的固定物理穿透
		ActerWuliInerPercent = ActerWuliInerPercentIn;//这个人物百分比穿透  （先计算固定穿透，然后计算百分比穿透）

		//物理防御属性
		ActerWuliShield = ActerWuliShieldIn;//这个人物的物理护甲

		//生命吸取属性
		ActerHpSuck =  ActerHpSuckIn;//人物的固定生命偷取值
		ActerHpSuckPercent = ActerHpSuckPercentIn;//根据所造成伤害的百分比生命吸取

		//法力吸取属性
		ActerSpSuck = ActerSpSuckIn;//人物的固定的法力偷取
		ActerSpSuckPercent = ActerSpSuckPercentIn;//根据所造成伤害的百分比法力偷取


		//额外战斗属性
		ActerDamageAdderPercent = ActerDamageAdderPercentIn;//额外百分比伤害
		ActerDamageAdder = ActerDamageAdderIn;//额外真实加成
		//上面这些全都要放在RPC方法里面各种更新
		//人物等级提升后加成

		ActerMoveSpeedPercent  = ActerMoveSpeedPercentIn;//移动速度百分比，在移动的时候会有这个速度百分比加成
		ActerAttackSpeedPercent =  ActerAttackspeedPercentIn; //攻击速度百分比

		ActerShieldHp = ActerShieldHpIn;//护盾的生命值

		ActerDamageMinusValue = ActerDamageMinusValueIn;//真实减伤
		ActerDamageMinusPercent = ActerDamageMinusPercentIn;//百分比减伤


	}
 
}
