using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//物品装备的类别
using System.Text;

//五种装备类型分别对应 头部 身体 武器 鞋子 饰品
//equipSkill对应注灵，这不是真正的装备，而是装备的装备
public enum equiptype {head,body,weapon,shoe,extra,equipSkill}
public class equipBasics : MonoBehaviour {

	//这个类是装备，物品等等相关东西的基类
	//实际上对于一般带有一些加成信息的物品，直接用就可以了
	//特殊性在于不得不给所有的playerBasic增加一个属性

	public string equipName = "" ;//装备的名字
	public string equipExtraName = "";//装备因为技能而存在的额外附加名字
	public string equipPictureName = "";//装备的图片
	public float equipValue = 0;//装备的价值
	public equiptype theEquipType;//装备种类也是分类的标记
	public string theEquipStroy = "";//装备的故事
	public bool isUsing = false;//是否已经被装备了
	//加成属性如下-------------------------------------------------------------------------------------------------------------------------
	//最基本的属性生命法力和名字
	public float equipActerHpMax=0f;//这个人物的生命上限
	public float equipActerSpMax=0f;//这个人物的法力上限
	public float equipActerHpUp=0f;//人物生命恢复
	public float equipActerSpUp=0f;//人物法力回复
	//特殊战斗属性
	public float equipActerSuperBaldePercent=0f;//这个人物的暴击率
	public float equipActerSuperBaldeAdder=0f;//暴击时伤害的倍数
	//public float equipActerAttackAtPercent =0f;//这个人物的命中率
	public float equipActerMissPercent=0f;//这个人物的闪避率
	public float equipActerShielderPercent=0f;//这个人物的格挡率
	public float equipActerShielderDamageMiuns=0f;//格挡住的伤害值
	public float equipActerShielderDamageMiunsPercent=0f;//格挡住的伤害百分比 (先计算固定格挡，然后计算百分比格挡)
	//物理战斗属性
	public float equipActerWuliDamage=0f;//这个人物的物理攻击力
	public float equipActerWuliReDamage=0f;//这个人物的物理反伤
	public float equipActerWuliIner=0f;//这个人物的固定物理穿透
	public float equipActerWuliInerPercent=0f;//这个人物百分比穿透  （先计算固定穿透，然后计算百分比穿透）
	public float equipActerDamageMinusPercent = 0.0f;//一直存在的伤害减免百分比
	public float equipActerDamageMinusValue = 0.0f;//一直存在的伤害减免数值
	//物理防御属性
	public float equipActerWuliShield=0f;//这个人物的物理护甲
	//生命吸取属性
	public float equipActerHpSuck=0f;//人物的固定生命偷取值
	public float equipActerHpSuckPercent=0f;//根据所造成伤害的百分比生命吸取
	//法力吸取属性
	public float equipActerSpSuck=0f;//人物的固定的法力偷取
	public float equipActerSpSuckPercent=0f;//根据所造成伤害的百分比法力偷取
	//额外战斗属性
	public float equipActerDamageAdderPercent=0;//额外百分比伤害
	public float equipActerDamageAdder=0;//额外真实加成
	//上面这些全都要放在RPC方法里面各种更新
	//人物等级提升后加成
	public float equipActerMoveSpeedPercent = 0f;//移动速度百分比，在移动的时候会有这个速度百分比加成
	public float equipActerAttackSpeedPercent = 0f;//攻击速度百分比，所有的动作的速度会受到这个限制
	public float equipActerShieldMaxPercent = 0f;//护盾针对最大生命值的上限
	//范围百分比属性
	public float equiptheAttackAreaLength;//攻击范围（非常重要，同时这个是简化版本的每一种攻击招式分开计算范围的方式）
	public float equiptheAttackAreaAngel = 0f;//攻击范围的角度，自身前方锥形范围内都是攻击范围
	public float equiptheViewAreaLength = 0f;//视野长度，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
	public float equiptheViewAreaAngel = 0f;//视野的角度，同样，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
	//装备重量
	//是一个装备都会有重量，有重量就会对速度有影响
	//至于特性加攻速，不代表本身的重量也不会有影响
	public float theEquipWeight = 0.05f;
	//装备的潜力，也就是可以消耗的总的点数
	//如果这“装备”是注灵，则表示消耗的潜力
	public int skillValueCount = 0;
	//当前的总装备天赋
	public int skillValueCountAll = 1;
	//加成属性-------------------------------------------------------------------------------------------------------------------------
	//这件装备能够带来的特效名字
	//这些特效也会是effectBasic的脚本
	//当然，可以有多个被动技能
	//顺带一提，多个技能的情况非常的稀少
	public List< string>  theEffectNames = new List<string> ();//注意这个初始化，这是非常重要的
	//装备等级
	private int equipLvNow = 1;
	public int EquipLvNow{get{ return equipLvNow;}}//等级是只读的
	//装备等级上限
	public int equipLvMax = 10;
	//装备升级速率
	public float equipLvUpRate = 0.07f;//每一次升级各个属性增加当前的7%，可以滚雪球


	public string getEquipName()
	{
		if(isUsing)
			return equipName +equipExtraName +"[已装备]";
		return equipName +equipExtraName+"[未装备]";
	}
	//获得装备
	public void GetThisThing(PlayerBasic thePlayer)
	{
		//保存一些变动的数值的百分比
		float hpPercent = thePlayer.ActerHp / thePlayer.ActerHpMax;
		float spPercent = thePlayer.ActerSp / thePlayer.ActerSpMax;
		//最基本的属性生命法力和名字
		thePlayer.ActerHpMax += equipActerHpMax;//这个人物的生命上限
		thePlayer.ActerSpMax += equipActerSpMax;//这个人物的法力上限
		thePlayer.ActerHpUp += equipActerHpUp;//人物生命恢复
		thePlayer.ActerSpUp += equipActerSpUp;//人物法力回复
		//特殊战斗属性
		thePlayer.ActerSuperBaldePercent += equipActerSuperBaldePercent;//这个人物的暴击率
		thePlayer.ActerSuperBaldeAdder += equipActerSuperBaldeAdder;//暴击时伤害的倍数
		//thePlayer.ActerAttackAtPercent += equipActerAttackAtPercent;//这个人物的命中率
		thePlayer.ActerMissPercent += equipActerMissPercent;//这个人物的闪避率
		thePlayer.ActerShielderPercent += equipActerShielderPercent;//这个人物的格挡率
		thePlayer.ActerShielderDamageMiuns += equipActerShielderDamageMiuns;//格挡住的伤害值
		thePlayer.ActerShielderDamageMiunsPercent += equipActerShielderDamageMiunsPercent;//格挡住的伤害百分比 (先计算固定格挡，然后计算百分比格挡)
		//物理战斗属性
		thePlayer.ActerWuliDamage += equipActerWuliDamage;//这个人物的物理攻击力
		thePlayer.ActerWuliReDamage += equipActerWuliReDamage;//这个人物的物理反伤
		thePlayer.ActerWuliIner += equipActerWuliIner;//这个人物的固定物理穿透
		thePlayer.ActerWuliInerPercent += equipActerWuliInerPercent;//这个人物百分比穿透  （先计算固定穿透，然后计算百分比穿透）
		thePlayer.ActerDamageMinusPercent += equipActerDamageMinusPercent;//一直存在的伤害减免百分比
		thePlayer.ActerDamageMinusValue += equipActerDamageMinusValue;//一直存在的伤害减免数值
		//物理防御属性
		thePlayer.ActerWuliShield += equipActerWuliShield;//这个人物的物理护甲
		//生命吸取属性
		thePlayer.ActerHpSuck += equipActerHpSuck;//人物的固定生命偷取值
		thePlayer.ActerHpSuckPercent += equipActerHpSuckPercent;//根据所造成伤害的百分比生命吸取
		//法力吸取属性
		thePlayer.ActerSpSuck += equipActerSpSuck;//人物的固定的法力偷取
		thePlayer.ActerSpSuckPercent += equipActerSpSuckPercent;//根据所造成伤害的百分比法力偷取
		//额外战斗属性
		thePlayer.ActerDamageAdderPercent += equipActerDamageAdderPercent;//额外百分比伤害
		thePlayer.ActerDamageAdder += equipActerDamageAdder;//额外真实加成
		//上面这些全都要放在RPC方法里面各种更新
		//人物等级提升后加成
		thePlayer.ActerMoveSpeedPercent += equipActerMoveSpeedPercent;//移动速度百分比，在移动的时候会有这个速度百分比加成
		thePlayer.ActerAttackSpeedPercent += equipActerAttackSpeedPercent;//攻击速度百分比，所有的动作的速度会受到这个限制
		thePlayer.ActerShieldMaxPercent += equipActerShieldMaxPercent ;//护盾针对最大生命值的上限
		//接下来是一些私有的战斗属性备份，用于计算值（作为例如护甲值提升10%这种的参数值计算）
		//因为是私有方法，所以还要给出获取这个值和修改这个值的方法
		//总体上讲，这些值是战斗属性的备份值，当有特殊计算方法的时候作为参数计算更新战斗属性
		//例如 战斗属性 = 备份值 *1.1f
		//顺带一提，之所以使用私有方法是因为不想让共有属性表太长，此外这些私有只会在特殊情况之下服务器才会使用
		thePlayer.theAttackAreaLength += equiptheAttackAreaLength;//攻击范围（非常重要，同时这个是简化版本的每一种攻击招式分开计算范围的方式）
		thePlayer.theAttackAreaAngel += equiptheAttackAreaAngel;//攻击范围的角度，自身前方锥形范围内都是攻击范围
		thePlayer.theViewAreaLength += equiptheViewAreaLength;//视野长度，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
		thePlayer.theViewAreaAngel += equiptheViewAreaAngel;//视野的角度，同样，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
	    //---------------------------------------------------接下来是备份数据---------------------------------------------------------------------------------//
		//最基本的属性生命法力和名字
		thePlayer.CActerHpMax += equipActerHpMax;//这个人物的生命上限
		thePlayer.CActerSpMax += equipActerSpMax;//这个人物的法力上限
		thePlayer.CActerHpUp += equipActerHpUp;//人物生命恢复
		thePlayer.CActerSpUp += equipActerSpUp;//人物法力回复
		//特殊战斗属性
		thePlayer.CActerSuperBaldePercent += equipActerSuperBaldePercent;//这个人物的暴击率
		thePlayer.CActerSuperBaldeAdder += equipActerSuperBaldeAdder;//暴击时伤害的倍数
		//thePlayer.CActerAttackAtPercent += equipActerAttackAtPercent;//这个人物的命中率
		thePlayer.CActerMissPercent += equipActerMissPercent;//这个人物的闪避率
		thePlayer.CActerShielderPercent += equipActerShielderPercent;//这个人物的格挡率
		thePlayer.CActerShielderDamageMiuns += equipActerShielderDamageMiuns;//格挡住的伤害值
		thePlayer.CActerShielderDamageMiunsPercent += equipActerShielderDamageMiunsPercent;//格挡住的伤害百分比 (先计算固定格挡，然后计算百分比格挡)
		//物理战斗属性
		thePlayer.CActerWuliDamage += equipActerWuliDamage;//这个人物的物理攻击力
		thePlayer.CActerWuliReDamage += equipActerWuliReDamage;//这个人物的物理反伤
		thePlayer.CActerWuliIner += equipActerWuliIner;//这个人物的固定物理穿透
		thePlayer.CActerWuliInerPercent += equipActerWuliInerPercent;//这个人物百分比穿透  （先计算固定穿透，然后计算百分比穿透）
		thePlayer.CActerDamageMinusPercent += equipActerDamageMinusPercent;//一直存在的伤害减免百分比
		thePlayer.CActerDamageMinusValue += equipActerDamageMinusValue;//一直存在的伤害减免数值
		//物理防御属性
		thePlayer.CActerWuliShield += equipActerWuliShield;//这个人物的物理护甲
		//生命吸取属性
		thePlayer.CActerHpSuck += equipActerHpSuck;//人物的固定生命偷取值
		thePlayer.CActerHpSuckPercent += equipActerHpSuckPercent;//根据所造成伤害的百分比生命吸取
		//法力吸取属性
		thePlayer.CActerSpSuck += equipActerSpSuck;//人物的固定的法力偷取
		thePlayer.CActerSpSuckPercent += equipActerSpSuckPercent;//根据所造成伤害的百分比法力偷取
		//额外战斗属性
		thePlayer.CActerDamageAdderPercent += equipActerDamageAdderPercent;//额外百分比伤害
		thePlayer.CActerDamageAdder += equipActerDamageAdder;//额外真实加成
		//上面这些全都要放在RPC方法里面各种更新
		//人物等级提升后加成
		thePlayer.CActerMoveSpeedPercent += equipActerMoveSpeedPercent;//移动速度百分比，在移动的时候会有这个速度百分比加成
		thePlayer.CActerAttackSpeedPercent += equipActerAttackSpeedPercent;//攻击速度百分比，所有的动作的速度会受到这个限制
		thePlayer.CActerShieldMaxPercent += equipActerShieldMaxPercent ;//护盾针对最大生命值的上限
		//接下来是一些私有的战斗属性备份，用于计算值（作为例如护甲值提升10%这种的参数值计算）
		//因为是私有方法，所以还要给出获取这个值和修改这个值的方法
		//总体上讲，这些值是战斗属性的备份值，当有特殊计算方法的时候作为参数计算更新战斗属性
		//例如 战斗属性 = 备份值 *1.1f
		//顺带一提，之所以使用私有方法是因为不想让共有属性表太长，此外这些私有只会在特殊情况之下服务器才会使用
		thePlayer.CtheAttackAreaLength += equiptheAttackAreaLength;//攻击范围（非常重要，同时这个是简化版本的每一种攻击招式分开计算范围的方式）
		thePlayer.CtheAttackAreaAngel += equiptheAttackAreaAngel;//攻击范围的角度，自身前方锥形范围内都是攻击范围
		thePlayer.CtheViewAreaLength += equiptheViewAreaLength;//视野长度，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
		thePlayer.CtheViewAreaAngel += equiptheViewAreaAngel;//视野的角度，同样，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
		thePlayer.weightPercent += this.theEquipWeight;//装备重量，游戏角色负重

		//变动数据的百分比归位
		thePlayer.ActerHp = thePlayer.ActerHpMax * hpPercent;
		thePlayer.ActerSp = thePlayer.ActerSpMax * spPercent;

		addEffects (thePlayer);
		thePlayer.makeValueUpdate();//网络版本需要强制更新，不能等自动的更新数值
		isUsing = true;//这个装备是一个已经被装备的装备
	}

	//扔掉这个装备
	public void DropThisThing(PlayerBasic thePlayer)
	{
		//最基本的属性生命法力和名字
		thePlayer.ActerHpMax -= equipActerHpMax;//这个人物的生命上限
		thePlayer.ActerSpMax -= equipActerSpMax;//这个人物的法力上限
		thePlayer.ActerHpUp -= equipActerHpUp;//人物生命恢复
		thePlayer.ActerSpUp -= equipActerSpUp;//人物法力回复
		//特殊战斗属性
		thePlayer.ActerSuperBaldePercent -= equipActerSuperBaldePercent;//这个人物的暴击率
		thePlayer.ActerSuperBaldeAdder -= equipActerSuperBaldeAdder;//暴击时伤害的倍数
		//thePlayer.ActerAttackAtPercent -= equipActerAttackAtPercent;//这个人物的命中率
		thePlayer.ActerMissPercent -= equipActerMissPercent;//这个人物的闪避率
		thePlayer.ActerShielderPercent -= equipActerShielderPercent;//这个人物的格挡率
		thePlayer.ActerShielderDamageMiuns -= equipActerShielderDamageMiuns;//格挡住的伤害值
		thePlayer.ActerShielderDamageMiunsPercent -= equipActerShielderDamageMiunsPercent;//格挡住的伤害百分比 (先计算固定格挡，然后计算百分比格挡)
		//物理战斗属性
		thePlayer.ActerWuliDamage -= equipActerWuliDamage;//这个人物的物理攻击力
		thePlayer.ActerWuliReDamage -= equipActerWuliReDamage;//这个人物的物理反伤
		thePlayer.ActerWuliIner -= equipActerWuliIner;//这个人物的固定物理穿透
		thePlayer.ActerWuliInerPercent -= equipActerWuliInerPercent;//这个人物百分比穿透  （先计算固定穿透，然后计算百分比穿透）
		thePlayer.ActerDamageMinusPercent -= equipActerDamageMinusPercent;//一直存在的伤害减免百分比
		thePlayer.ActerDamageMinusValue -= equipActerDamageMinusValue;//一直存在的伤害减免数值
		//物理防御属性
		thePlayer.ActerWuliShield -= equipActerWuliShield;//这个人物的物理护甲
		//生命吸取属性
		thePlayer.ActerHpSuck -= equipActerHpSuck;//人物的固定生命偷取值
		thePlayer.ActerHpSuckPercent -= equipActerHpSuckPercent;//根据所造成伤害的百分比生命吸取
		//法力吸取属性
		thePlayer.ActerSpSuck -= equipActerSpSuck;//人物的固定的法力偷取
		thePlayer.ActerSpSuckPercent -= equipActerSpSuckPercent;//根据所造成伤害的百分比法力偷取
		//额外战斗属性
		thePlayer.ActerDamageAdderPercent -= equipActerDamageAdderPercent;//额外百分比伤害
		thePlayer.ActerDamageAdder -= equipActerDamageAdder;//额外真实加成
		//上面这些全都要放在RPC方法里面各种更新
		//人物等级提升后加成
		thePlayer.ActerMoveSpeedPercent -= equipActerMoveSpeedPercent;//移动速度百分比，在移动的时候会有这个速度百分比加成
		thePlayer.ActerAttackSpeedPercent -= equipActerAttackSpeedPercent;//攻击速度百分比，所有的动作的速度会受到这个限制
		thePlayer.ActerShieldMaxPercent -= equipActerShieldMaxPercent ;//护盾针对最大生命值的上限
		//接下来是一些私有的战斗属性备份，用于计算值（作为例如护甲值提升10%这种的参数值计算）
		//因为是私有方法，所以还要给出获取这个值和修改这个值的方法
		//总体上讲，这些值是战斗属性的备份值，当有特殊计算方法的时候作为参数计算更新战斗属性
		//例如 战斗属性 = 备份值 *1.1f
		//顺带一提，之所以使用私有方法是因为不想让共有属性表太长，此外这些私有只会在特殊情况之下服务器才会使用
		thePlayer.theAttackAreaLength -= equiptheAttackAreaLength;//攻击范围（非常重要，同时这个是简化版本的每一种攻击招式分开计算范围的方式）
		thePlayer.theAttackAreaAngel -= equiptheAttackAreaAngel;//攻击范围的角度，自身前方锥形范围内都是攻击范围
		thePlayer.theViewAreaLength -= equiptheViewAreaLength;//视野长度，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
		thePlayer.theViewAreaAngel -= equiptheViewAreaAngel;//视野的角度，同样，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
		//---------------------------------------------------接下来是备份数据---------------------------------------------------------------------------------//
		//最基本的属性生命法力和名字
		thePlayer.CActerHpMax -= equipActerHpMax;//这个人物的生命上限
		thePlayer.CActerSpMax -= equipActerSpMax;//这个人物的法力上限
		thePlayer.CActerHpUp -= equipActerHpUp;//人物生命恢复
		thePlayer.CActerSpUp -= equipActerSpUp;//人物法力回复
		//特殊战斗属性
		thePlayer.CActerSuperBaldePercent -= equipActerSuperBaldePercent;//这个人物的暴击率
		thePlayer.CActerSuperBaldeAdder -= equipActerSuperBaldeAdder;//暴击时伤害的倍数
		//thePlayer.CActerAttackAtPercent -= equipActerAttackAtPercent;//这个人物的命中率
		thePlayer.CActerMissPercent -= equipActerMissPercent;//这个人物的闪避率
		thePlayer.CActerShielderPercent -= equipActerShielderPercent;//这个人物的格挡率
		thePlayer.CActerShielderDamageMiuns -= equipActerShielderDamageMiuns;//格挡住的伤害值
		thePlayer.CActerShielderDamageMiunsPercent -= equipActerShielderDamageMiunsPercent;//格挡住的伤害百分比 (先计算固定格挡，然后计算百分比格挡)
		//物理战斗属性
		thePlayer.CActerWuliDamage -= equipActerWuliDamage;//这个人物的物理攻击力
		thePlayer.CActerWuliReDamage -= equipActerWuliReDamage;//这个人物的物理反伤
		thePlayer.CActerWuliIner -= equipActerWuliIner;//这个人物的固定物理穿透
		thePlayer.CActerWuliInerPercent -= equipActerWuliInerPercent;//这个人物百分比穿透  （先计算固定穿透，然后计算百分比穿透）
		thePlayer.CActerDamageMinusPercent -= equipActerDamageMinusPercent;//一直存在的伤害减免百分比
		thePlayer.CActerDamageMinusValue -= equipActerDamageMinusValue;//一直存在的伤害减免数值
		//物理防御属性
		thePlayer.CActerWuliShield -= equipActerWuliShield;//这个人物的物理护甲
		//生命吸取属性
		thePlayer.CActerHpSuck -= equipActerHpSuck;//人物的固定生命偷取值
		thePlayer.CActerHpSuckPercent -= equipActerHpSuckPercent;//根据所造成伤害的百分比生命吸取
		//法力吸取属性
		thePlayer.CActerSpSuck -= equipActerSpSuck;//人物的固定的法力偷取
		thePlayer.CActerSpSuckPercent -= equipActerSpSuckPercent;//根据所造成伤害的百分比法力偷取
		//额外战斗属性
		thePlayer.CActerDamageAdderPercent -= equipActerDamageAdderPercent;//额外百分比伤害
		thePlayer.CActerDamageAdder -= equipActerDamageAdder;//额外真实加成
		//上面这些全都要放在RPC方法里面各种更新
		//人物等级提升后加成
		thePlayer.CActerMoveSpeedPercent -= equipActerMoveSpeedPercent;//移动速度百分比，在移动的时候会有这个速度百分比加成
		thePlayer.CActerAttackSpeedPercent -= equipActerAttackSpeedPercent;//攻击速度百分比，所有的动作的速度会受到这个限制
		thePlayer.CActerShieldMaxPercent -= equipActerShieldMaxPercent ;//护盾针对最大生命值的上限
        //攻击范围和侦查范围
		thePlayer.CtheAttackAreaLength -= equiptheAttackAreaLength;//攻击范围（非常重要，同时这个是简化版本的每一种攻击招式分开计算范围的方式）
		thePlayer.CtheAttackAreaAngel -= equiptheAttackAreaAngel;//攻击范围的角度，自身前方锥形范围内都是攻击范围
		thePlayer.CtheViewAreaLength -= equiptheViewAreaLength;//视野长度，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
		thePlayer.CtheViewAreaAngel -= equiptheViewAreaAngel;//视野的角度，同样，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
		thePlayer.weightPercent -= this.theEquipWeight;//装备重量，游戏角色负重

		dropEffects (thePlayer);
		thePlayer.makeValueUpdate();//网络版本需要强制更新，不能等自动的更新数值
		isUsing = false;//这个装备不再是一个已经被装备的装备
	}

	//获得装备的加成信息
	//这里有一个强制性规定，所有加成没有负数
	//有负数的加成都是在effect里面额外添加的
	public string getEquipAdderInformation()
	{
		//长字符串拼接需要用这个，能减少大概一半的GC
		StringBuilder information = new StringBuilder ();

		//最基本的属性生命法力和名字
		if (equipActerHpMax > 0)
		{information.Append ("生命上限 + ");information.Append (equipActerHpMax.ToString("f0"));information.Append ("\n");}
		if (equipActerSpMax > 0)
		{information.Append ("斗气上限 + ");information.Append (equipActerSpMax.ToString("f0"));information.Append ("\n");}
		if (equipActerHpUp > 0)
		{information.Append ("生命回复 + ");information.Append (equipActerHpUp.ToString("f1"));information.Append ("/秒\n");}
		if (equipActerSpUp > 0)
		{information.Append ("斗气回复 + ");information.Append (equipActerSpUp.ToString("f1"));information.Append ("/秒\n");}

		//特殊战斗属性
		if (equipActerSuperBaldePercent > 0)
		{information.Append ("暴击率 + ");information.Append ((equipActerSuperBaldePercent * 100).ToString("f0"));information.Append ("%\n");}
		if (equipActerSuperBaldeAdder > 0)
		{information.Append ("暴击伤害百分比 + ");information.Append ((equipActerSuperBaldeAdder*100).ToString("f0"));information.Append ("%\n");}
		if (equipActerMissPercent > 0)
		{information.Append ("闪避率 + ");information.Append ((equipActerMissPercent*100).ToString("f0"));information.Append ("%\n");}
		if (equipActerShielderPercent > 0)
		{information.Append ("格挡率 + ");information.Append ((equipActerShielderPercent * 100).ToString("f0"));information.Append ("%\n");}
		if (equipActerShielderDamageMiuns > 0 || equipActerShielderDamageMiunsPercent >0) //共同作用的只有一个有效就行
		{information.Append ("格挡伤害 + (");information.Append (equipActerShielderDamageMiuns.ToString("f0"));
			information.Append (" + ");information.Append ((equipActerShielderDamageMiunsPercent*100).ToString("f0"));information.Append ("%)\n");}

		//物理战斗属性
		if (equipActerWuliDamage > 0)
		{information.Append ("攻击力 + ");information.Append ((equipActerWuliDamage).ToString("f0"));information.Append ("\n");}
		if (equipActerWuliReDamage > 0)
		{information.Append ("反伤 + ");information.Append ((equipActerWuliReDamage).ToString("f0"));information.Append ("\n");}

		if ( equipActerWuliIner > 0 || equipActerWuliInerPercent >0) //共同作用的只有一个有效就行
		{information.Append ("伤害穿透 + (");information.Append ( equipActerWuliIner.ToString("f0"));
			information.Append (" + ");information.Append ( (equipActerWuliInerPercent*100).ToString("f0"));information.Append ("%)\n");}
		
		if ( equipActerDamageMinusValue > 0 || equipActerDamageMinusPercent >0) //共同作用的只有一个有效就行
		{information.Append ("伤害减免 + (");information.Append ( equipActerDamageMinusValue .ToString("f0"));
			information.Append (" + ");information.Append ((equipActerDamageMinusPercent *100).ToString("f0"));information.Append ("%)\n");}

		//物理防御属性
		if (equipActerWuliShield > 0)
		{information.Append ("护甲 + ");information.Append ((equipActerWuliShield).ToString("f0"));information.Append ("\n");}

		//生命吸取属性
		if (equipActerHpSuck > 0 || equipActerHpSuckPercent >0) //共同作用的只有一个有效就行
		{information.Append ("生命偷取 + (");information.Append ( equipActerHpSuck.ToString("f0"));
			information.Append (" + ");information.Append ((equipActerHpSuckPercent *100).ToString("f0"));information.Append ("%)\n");}
		//法力吸取属性
		if (equipActerSpSuck > 0 || equipActerSpSuckPercent >0) //共同作用的只有一个有效就行
		{information.Append ("斗气偷取 + (");information.Append ( equipActerSpSuck.ToString("f0"));
			information.Append (" + ");information.Append (( equipActerSpSuckPercent*100).ToString("f0"));information.Append ("%)\n");}
		//额外战斗伤害
		if (equipActerDamageAdderPercent > 0 || equipActerDamageAdder >0) //共同作用的只有一个有效就行
		{information.Append ("额外伤害 + (");information.Append ( equipActerDamageAdder.ToString("f0"));
			information.Append (" + ");information.Append (( equipActerDamageAdderPercent*100).ToString("f0"));information.Append ("%)\n");}
		
		//移动速度百分比，在移动的时候会有这个速度百分比加成
		if ( equipActerMoveSpeedPercent > 0)
		{information.Append ("移动速度 + ");information.Append (( equipActerMoveSpeedPercent* 100).ToString("f0"));information.Append ("%\n");}
		//攻击速度百分比，所有的动作的速度会受到这个限制
		if ( equipActerAttackSpeedPercent > 0)
		{information.Append ("攻击速度 + ");information.Append (( equipActerAttackSpeedPercent * 100).ToString("f0"));information.Append ("%\n");}
		//护盾针对最大生命值的上限
		if ( equipActerShieldMaxPercent > 0)
		{information.Append ("护盾上限 + ");information.Append (( equipActerShieldMaxPercent* 100).ToString("f0"));information.Append ("%\n");}
			
		//攻击范围（非常重要，同时这个是简化版本的每一种攻击招式分开计算范围的方式）
		if (equiptheAttackAreaLength> 0)
		{information.Append ("攻击距离 + ");information.Append ((equiptheAttackAreaLength*100).ToString("f0"));information.Append ("%\n");}
		if (equiptheAttackAreaAngel> 0)
		{information.Append ("攻击广度 + ");information.Append ((equiptheAttackAreaAngel).ToString("f0"));information.Append ("\n");}
		if (equiptheViewAreaLength > 0)
		{information.Append ("侦查距离 + ");information.Append ((equiptheViewAreaLength).ToString("f0"));information.Append ("\n");}
		if (equiptheViewAreaAngel > 0)
		{information.Append ("侦查广度 + ");information.Append ((equiptheViewAreaAngel).ToString("f0"));information.Append ("\n");}
		if (theEquipWeight > 0) 
		{information.Append ("负重 + ");information.Append ((theEquipWeight*100).ToString("f0"));information.Append ("%\n");}
		return information.ToString ();
	}

	public static string equipTrast(equipBasics newOne , equipBasics oldOne = null)
	{
		//装备比较是用在已经装备的装备和没有装备的装备上的，两个已经装备上的装备就不用这样了
		if ( newOne!= null  &&  newOne.isUsing && oldOne.isUsing )
			return "";
		
		//生命法力数值
		float hpMaxAdder =  0f;
		float spMaxAdder =  0f;
		float equipActerHpUp =  0f;
		float equipActerSpUp =   0f;
		//物理战斗属性
		float equipActerWuliDamage =   0f;
		float equipActerWuliReDamage =   0f;
		float equipActerWuliIner =   0f;
		float equipActerWuliInerPercent =   0f;
		float equipActerDamageMinusPercent =   0f;
		float equipActerDamageMinusValue =   0f;
		//特殊战斗属性
		float equipActerSuperBaldePercent =   0f;
		float equipActerSuperBaldeAdder =   0f;
		float equipActerMissPercent =   0f;
		float equipActerShielderPercent =   0f;
		float equipActerShielderDamageMiuns =   0f;
		float equipActerShielderDamageMiunsPercent =   0f;
		//物理防御属性
		float equipActerWuliShield =   0f;
		float equipActerShieldMaxPercent =   0f;
		//生命吸取属性
		float equipActerHpSuck =  0f;
		float equipActerHpSuckPercent =   0f;
		//法力吸取属性
		float equipActerSpSuck =  0f;
		float equipActerSpSuckPercent =   0f;
		//额外战斗属性
		float equipActerDamageAdderPercent =   0f;
		float equipActerDamageAdder =   0f;
		//人物的一些范围属性
		float equipActerMoveSpeedPercent =   0f;
		float equipActerAttackSpeedPercent =   0f;
		float equiptheAttackAreaLength =  0f;
		float equiptheAttackAreaAngel =  0f;
		float equiptheViewAreaLength =   0f;
		float equiptheViewAreaAngel =   0f;
		float equipWeight = 0f;

		//一次性判断要比三目靠谱
		if (oldOne == null) 
		{
			//生命法力数值
			hpMaxAdder =  newOne.equipActerHpMax  ;
			spMaxAdder =  newOne.equipActerSpMax  ;
			equipActerHpUp =  newOne.equipActerHpUp  ;
		    equipActerSpUp =  newOne.equipActerSpUp   ;
			//物理战斗属性
			equipActerWuliDamage =  newOne.equipActerWuliDamage  ;
			equipActerWuliReDamage =  newOne.equipActerWuliReDamage   ;
			equipActerWuliIner =  newOne.equipActerWuliIner  ;
			equipActerWuliInerPercent =  newOne.equipActerWuliInerPercent   ;
			equipActerDamageMinusPercent =  newOne.equipActerDamageMinusPercent  ;
			equipActerDamageMinusValue =  newOne.equipActerDamageMinusValue ;
			//特殊战斗属性
			equipActerSuperBaldePercent =  newOne.equipActerSuperBaldePercent ;
			equipActerSuperBaldeAdder =  newOne.equipActerSuperBaldeAdder ;
			equipActerMissPercent =  newOne.equipActerMissPercent  ;
			equipActerShielderPercent =  newOne.equipActerShielderPercent  ;
			equipActerShielderDamageMiuns =  newOne.equipActerShielderDamageMiuns  ;
			equipActerShielderDamageMiunsPercent =  newOne.equipActerShielderDamageMiunsPercent  ;
			//物理防御属性
			equipActerWuliShield =  newOne.equipActerWuliShield  ;
			equipActerShieldMaxPercent =  newOne.equipActerShieldMaxPercent ;
			//生命吸取属性
			equipActerHpSuck =  newOne.equipActerHpSuck ;
			equipActerHpSuckPercent =  newOne.equipActerHpSuckPercent  ;
			//法力吸取属性
			equipActerSpSuck =  newOne.equipActerSpSuck  ;
			equipActerSpSuckPercent =  newOne.equipActerSpSuckPercent  ;
			//额外战斗属性
			equipActerDamageAdderPercent =  newOne.equipActerDamageAdderPercent  ;
			equipActerDamageAdder =  newOne.equipActerDamageAdder  ;
			//人物的一些范围属性
			equipActerMoveSpeedPercent =  newOne.equipActerMoveSpeedPercent  ;
			equipActerAttackSpeedPercent =  newOne.equipActerAttackSpeedPercent  ;
			equiptheAttackAreaLength =  newOne.equiptheAttackAreaLength  ;
		    equiptheAttackAreaAngel =  newOne.equiptheAttackAreaAngel  ;
			equiptheViewAreaLength =  newOne.equiptheViewAreaLength  ;
			equiptheViewAreaAngel =  newOne.equiptheViewAreaAngel  ;
			equipWeight = newOne.theEquipWeight;
		}
		else
		{
	        //生命法力数值
			hpMaxAdder =  newOne.equipActerHpMax  - oldOne.equipActerHpMax;
			spMaxAdder =  newOne.equipActerSpMax  - oldOne.equipActerSpMax;
			equipActerHpUp =  newOne.equipActerHpUp  - oldOne.equipActerHpUp;
			equipActerSpUp =  newOne.equipActerSpUp  - oldOne.equipActerSpUp;
			//物理战斗属性
			equipActerWuliDamage =  newOne.equipActerWuliDamage  - oldOne.equipActerWuliDamage;
			equipActerWuliReDamage =  newOne.equipActerWuliReDamage  - oldOne.equipActerWuliReDamage;
			equipActerWuliIner =  newOne.equipActerWuliIner  - oldOne.equipActerWuliIner;
			equipActerWuliInerPercent =  newOne.equipActerWuliInerPercent  - oldOne.equipActerWuliInerPercent;
			equipActerDamageMinusPercent =  newOne.equipActerDamageMinusPercent - oldOne.equipActerDamageMinusPercent;
			equipActerDamageMinusValue =  newOne.equipActerDamageMinusValue- oldOne.equipActerDamageMinusValue;
			//特殊战斗属性
			equipActerSuperBaldePercent =  newOne.equipActerSuperBaldePercent- oldOne.equipActerSuperBaldePercent;
			equipActerSuperBaldeAdder =  newOne.equipActerSuperBaldeAdder - oldOne.equipActerSuperBaldeAdder;
			equipActerMissPercent =  newOne.equipActerMissPercent - oldOne.equipActerMissPercent;
			equipActerShielderPercent =  newOne.equipActerShielderPercent - oldOne.equipActerShielderPercent;
			equipActerShielderDamageMiuns =  newOne.equipActerShielderDamageMiuns - oldOne.equipActerShielderDamageMiuns;
			equipActerShielderDamageMiunsPercent =  newOne.equipActerShielderDamageMiunsPercent - oldOne.equipActerShielderDamageMiunsPercent;
			//物理防御属性
			equipActerWuliShield =  newOne.equipActerWuliShield - oldOne.equipActerWuliShield;
			equipActerShieldMaxPercent =  newOne.equipActerShieldMaxPercent - oldOne.equipActerShieldMaxPercent;
			//生命吸取属性
			equipActerHpSuck =  newOne.equipActerHpSuck - oldOne.equipActerHpSuck;
			equipActerHpSuckPercent =  newOne.equipActerHpSuckPercent - oldOne.equipActerHpSuckPercent;
			//法力吸取属性
			equipActerSpSuck =  newOne.equipActerSpSuck - oldOne.equipActerSpSuck;
			equipActerSpSuckPercent =  newOne.equipActerSpSuckPercent - oldOne.equipActerSpSuckPercent;
			//额外战斗属性
			equipActerDamageAdderPercent =  newOne.equipActerDamageAdderPercent - oldOne.equipActerDamageAdderPercent;
			equipActerDamageAdder =  newOne.equipActerDamageAdder - oldOne.equipActerDamageAdder;
			//人物的一些范围属性
			equipActerMoveSpeedPercent =  newOne.equipActerMoveSpeedPercent  - oldOne.equipActerMoveSpeedPercent;
			equipActerAttackSpeedPercent =  newOne.equipActerAttackSpeedPercent  - oldOne.equipActerAttackSpeedPercent;
			equiptheAttackAreaLength =  newOne.equiptheAttackAreaLength  - oldOne.equiptheAttackAreaLength;
			equiptheAttackAreaAngel =  newOne.equiptheAttackAreaAngel  - oldOne.equiptheAttackAreaAngel;
			equiptheViewAreaLength =  newOne.equiptheViewAreaLength - oldOne.equiptheViewAreaLength;
			equiptheViewAreaAngel =  newOne.equiptheViewAreaAngel - oldOne.equiptheViewAreaAngel;
			equipWeight = newOne.theEquipWeight - oldOne.theEquipWeight;
		}

		StringBuilder theString = new StringBuilder ();
		if (newOne == oldOne)
			return "";
		theString.Append (newOne.equipName);
		if (oldOne != null) 
		{
			theString.Append ("  对比  ");
			theString.Append (oldOne.equipName);
		}
		theString.Append ("\n");
		//生命法力数值
		if (hpMaxAdder != 0) 
		{
			theString.Append ("生命上限 ");
			if (hpMaxAdder >= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (hpMaxAdder.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (spMaxAdder != 0) 
		{
			theString.Append ("斗气上限 ");
			if (spMaxAdder >= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (spMaxAdder.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerHpUp  != 0) 
		{
			theString.Append ("生命回复 ");
			if (equipActerHpUp >= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equipActerHpUp .ToString("f1"));
			theString.Append ("/秒\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerSpUp  != 0) 
		{
			theString.Append ("斗气回复 ");
			if (equipActerSpUp >= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equipActerSpUp.ToString("f1"));
			theString.Append ("/秒\n");
			theString.Append (systemValues.colorEnd);
		}
		//物理战斗属性
		if (equipActerWuliDamage  != 0) 
		{
			theString.Append ("攻击力 ");
			if (equipActerWuliDamage >= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equipActerWuliDamage.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}

		if (equipActerWuliReDamage != 0) 
		{
			theString.Append ("反伤 ");
			if (equipActerWuliReDamage >= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equipActerWuliReDamage.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerWuliIner != 0) 
		{
			theString.Append ("真实穿透 ");
			if (equipActerWuliIner>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equipActerWuliIner.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerWuliInerPercent != 0) 
		{
			theString.Append ("百分比穿透 ");
			if (equipActerWuliInerPercent>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerWuliInerPercent*100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerDamageMinusValue != 0) 
		{
			theString.Append ("真实减伤 ");
			if (equipActerDamageMinusValue>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equipActerDamageMinusValue.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}

		if (equipActerDamageMinusPercent != 0) 
		{
			theString.Append ("百分比减伤 ");
			if (equipActerDamageMinusPercent>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerDamageMinusPercent*100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		//特殊战斗属性
		if (equipActerSuperBaldePercent != 0) 
		{
			theString.Append ("暴击率 ");
			if (equipActerSuperBaldePercent>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerSuperBaldePercent*100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}

		if (equipActerSuperBaldeAdder != 0) 
		{
			theString.Append ("暴击伤害 ");
			if (equipActerSuperBaldeAdder>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerSuperBaldeAdder*100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerMissPercent  != 0) 
		{
			theString.Append ("闪避率 ");
			if (equipActerMissPercent >= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerMissPercent *100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerShielderPercent  != 0) 
		{
			theString.Append ("格挡率 ");
			if (equipActerShielderPercent>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerShielderPercent *100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerShielderDamageMiuns != 0) 
		{
			theString.Append ("格挡真实减伤 ");
			if (equipActerShielderDamageMiuns>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equipActerShielderDamageMiuns.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerShielderDamageMiunsPercent != 0) 
		{
			theString.Append ("格挡百分比减伤 ");
			if (equipActerShielderDamageMiunsPercent>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerShielderDamageMiunsPercent *100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		//物理防御属性
		if (equipActerWuliShield != 0) 
		{
			theString.Append ("护甲 ");
			if (equipActerWuliShield>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equipActerWuliShield.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerShieldMaxPercent != 0) 
		{
			theString.Append ("护盾上限 ");
			if (equipActerShieldMaxPercent>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerShieldMaxPercent*100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		//生命吸取属性
		if (equipActerHpSuck != 0) 
		{
			theString.Append ("真实生命偷取 ");
			if (equipActerHpSuck>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equipActerHpSuck.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerHpSuckPercent != 0) 
		{
			theString.Append ("百分比生命偷取 ");
			if (equipActerHpSuckPercent>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerHpSuckPercent*100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		//法力吸取属性
		if (equipActerSpSuck != 0) 
		{
			theString.Append ("真实斗气偷取 ");
			if (equipActerSpSuck>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equipActerSpSuck.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerSpSuckPercent != 0) 
		{
			theString.Append ("百分比斗气偷取 ");
			if (equipActerSpSuckPercent>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerSpSuckPercent*100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		//额外战斗属性
		if (equipActerDamageAdder != 0) 
		{
			theString.Append ("真实最终伤害增加 ");
			if (equipActerDamageAdder>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equipActerDamageAdder.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerDamageAdderPercent != 0) 
		{
			theString.Append ("百分比最终伤害增加 ");
			if (equipActerDamageAdderPercent>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerDamageAdderPercent*100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		//人物的速度属性
		if (equipActerMoveSpeedPercent != 0) 
		{
			theString.Append ("移动速度 ");
			if (equipActerMoveSpeedPercent>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerMoveSpeedPercent*100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipActerAttackSpeedPercent != 0) 
		{
			theString.Append ("攻击速度 ");
			if (equipActerAttackSpeedPercent>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equipActerAttackSpeedPercent*100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		//人物的一些范围属性
		if (equiptheAttackAreaLength != 0) 
		{
			theString.Append ("攻击距离 ");
			if (equiptheAttackAreaLength>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append ((equiptheAttackAreaLength*100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equiptheAttackAreaAngel != 0) 
		{
			theString.Append ("攻击广度 ");
			if (equiptheAttackAreaAngel>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equiptheAttackAreaAngel.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equiptheViewAreaLength != 0) 
		{
			theString.Append ("侦查距离 ");
			if (equiptheViewAreaLength>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equiptheViewAreaLength.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equiptheViewAreaAngel != 0) 
		{
			theString.Append ("侦查广度 ");
			if (equiptheViewAreaAngel>= 0){theString.Append ( systemValues.equipAddColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipMinusColor);}
			theString.Append (equiptheViewAreaAngel.ToString("f0"));
			theString.Append ("\n");
			theString.Append (systemValues.colorEnd);
		}
		if (equipWeight != 0)
		{
			theString.Append ("负重 ");
			if (equipWeight>= 0){theString.Append ( systemValues.equipMinusColor);theString.Append ( "+");} 
			else {theString.Append (systemValues.equipAddColor);}
			theString.Append ((equipWeight *100).ToString("f0"));
			theString.Append ("%\n");
			theString.Append (systemValues.colorEnd);
		}
		return theString.ToString();
	}
		
	//检查是不是可以升级装备
	public bool checkCanLvUp()
	{
		if (equipLvNow >= equipLvMax)
			return false;
		return true;
		
	}

	//装备注灵
	public void makeEquipAddSkill(equipBasics theEquip)
	{
		string theEffectName = theEquip.equipName;
		//如果已经装备上了，就需要先卸下来升级再装上去
		if (isUsing)
		{
			DropThisThing (systemValues.thePlayer);
			theEffectNames.Add (theEffectName);
			equipExtraName += "[" + systemValues.getEffectNameWithName (theEffectName) + "]";
			GetThisThing (systemValues.thePlayer);
		}
		else 
		{
			theEffectNames.Add (theEffectName);
			equipExtraName +="["+systemValues.getEffectNameWithName(theEffectName)+"]";
			//GetThisThing (systemValues.thePlayer);
			//DropThisThing (systemValues.thePlayer);
		}
		skillValueCount += theEquip.skillValueCountAll;
	}


	//装备升级
	public void makeEquipLvUp()
	{
		//如果已经装备上了，就需要先卸下来升级再装上去
		if (isUsing) 
		{
			DropThisThing (systemValues.thePlayer);
			makeValueAdd ();
			GetThisThing (systemValues.thePlayer);
		}
		else
			makeValueAdd ();
	}

	private void makeValueAdd()
	{
		float valueUse = 1 + equipLvUpRate;

		//最基本的属性生命法力和名字
		equipActerHpMax *= valueUse;
		equipActerSpMax *= valueUse;
		equipActerHpUp *= valueUse;
		equipActerSpUp *= valueUse;
		//特殊战斗属性
		equipActerSuperBaldePercent *= valueUse;
		equipActerSuperBaldeAdder *= valueUse;
		equipActerMissPercent *= valueUse;
		equipActerShielderPercent *= valueUse;
		equipActerShielderDamageMiuns*= valueUse;
		equipActerShielderDamageMiunsPercent*= valueUse;
		//物理战斗属性
		equipActerWuliDamage*= valueUse;
		equipActerWuliReDamage*= valueUse;
		equipActerWuliIner*= valueUse;
		equipActerWuliInerPercent*= valueUse;
		equipActerDamageMinusPercent*= valueUse;
		equipActerDamageMinusValue*= valueUse;
		//物理防御属性
		equipActerWuliShield*= valueUse;
		//生命吸取属性
		equipActerHpSuck*= valueUse;
		equipActerHpSuckPercent*= valueUse;
		//法力吸取属性
		equipActerSpSuck*= valueUse;
		equipActerSpSuckPercent*= valueUse;
		//额外战斗属性
		equipActerDamageAdderPercent*= valueUse;
		equipActerDamageAdder*= valueUse;
		//上面这些全都要放在RPC方法里面各种更新
		//人物等级提升后加成
		equipActerMoveSpeedPercent*= valueUse;
		equipActerAttackSpeedPercent*= valueUse;
		equipActerShieldMaxPercent*= valueUse;
		//范围属性
		equiptheAttackAreaLength*= valueUse;
		equiptheAttackAreaAngel*= valueUse;
		equiptheViewAreaLength*= valueUse;
		equiptheViewAreaAngel*= valueUse;

		equipLvNow++;
	}

	//添加特效
	private void addEffects(PlayerBasic thePlayer)
	{
		//所有脚本都确定只是唯一被动脚本存在一个
		//可叠加被动就用updateEffect来做了
		for (int i = 0; i < theEffectNames.Count; i++)
		{
			thePlayer.addEffectUpdate (theEffectNames[i]);
		}
	}

	//消除特效
	//实际上应该装备栏中所有的这个装备都不要了这个效果才会消失
	//但是因为装备已经有了明确的分类，这里就算是有一点简化
	//当然具体要做的时候还是应该参照thePlayer的装备容器，所以后面这种判断再行补上
	private void dropEffects (PlayerBasic thePlayer)
	{
		for (int i = 0; i < theEffectNames.Count; i++)
		{
			thePlayer.dropEffectUpdate (theEffectNames[i]);
		}
	}

	void Start()
	{
		if (this.theEquipType == equiptype.equipSkill) 
		{
			this.equipExtraName = systemValues.getEffectNameWithName (this.equipName,this.gameObject);
			this.theEquipStroy = systemValues.getEffectInfromationWithName (this.equipName , this.gameObject);
		}
	}

}
