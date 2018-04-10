using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//物品装备的类别
public enum equiptype {head,body,weapon,shoe,extra  }
public class equipBasics : MonoBehaviour {

	//这个类是装备，物品等等相关东西的基类
	//实际上对于一般带有一些加成信息的物品，直接用就可以了
	//特殊性在于不得不给所有的playerBasic增加一个属性

	public string equipName = "" ;//装备的名字
	public float equipValue = 0;//装备的价值
	public equiptype theEquipType;//装备种类也是分类的标记
	//加成属性如下-------------------------------------------------------------------------------------------------------------------------
	//最基本的属性生命法力和名字
	public float equipActerHpMax=1000f;//这个人物的生命上限
	public float equipActerSpMax=500f;//这个人物的法力上限
	public float equipActerHpUp=0.5f;//人物生命恢复
	public float equipActerSpUp=0.5f;//人物法力回复
	//特殊战斗属性
	public float equipActerSuperBaldePercent=0f;//这个人物的暴击率
	public float equipActerSuperBaldeAdder=2f;//暴击时伤害的倍数
	public float equipActerAttackAtPercent = 1f;//这个人物的命中率
	public float equipActerMissPercent=0f;//这个人物的闪避率
	public float equipActerShielderPercent=0f;//这个人物的格挡率
	public float equipActerShielderDamageMiuns=1;//格挡住的伤害值
	public float equipActerShielderDamageMiunsPercent=0.1f;//格挡住的伤害百分比 (先计算固定格挡，然后计算百分比格挡)
	//物理战斗属性
	public float equipActerWuliDamage=25f;//这个人物的物理攻击力
	public float equipActerWuliReDamage=0f;//这个人物的物理反伤
	public float equipActerWuliIner=0f;//这个人物的固定物理穿透
	public float equipActerWuliInerPercent=0f;//这个人物百分比穿透  （先计算固定穿透，然后计算百分比穿透）
	public float equipActerDamageMinusPercent = 0.0f;//一直存在的伤害减免百分比
	public float equipActerDamageMinusValue = 0.0f;//一直存在的伤害减免数值
	//物理防御属性
	public float equipActerWuliShield=150f;//这个人物的物理护甲
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
	public float equipActerMoveSpeedPercent = 1f;//移动速度百分比，在移动的时候会有这个速度百分比加成
	public float equipActerAttackSpeedPercent = 1f;//攻击速度百分比，所有的动作的速度会受到这个限制
	public float equipActerShieldMaxPercent = 0.15f;//护盾针对最大生命值的上限
	//接下来是一些私有的战斗属性备份，用于计算值（作为例如护甲值提升10%这种的参数值计算）
	//因为是私有方法，所以还要给出获取这个值和修改这个值的方法
	//总体上讲，这些值是战斗属性的备份值，当有特殊计算方法的时候作为参数计算更新战斗属性
	//例如 战斗属性 = 备份值 *1.1f
	//顺带一提，之所以使用私有方法是因为不想让共有属性表太长，此外这些私有只会在特殊情况之下服务器才会使用
	public float equiptheAttackAreaLength;//攻击范围（非常重要，同时这个是简化版本的每一种攻击招式分开计算范围的方式）
	public float equiptheAttackAreaAngel = 20f;//攻击范围的角度，自身前方锥形范围内都是攻击范围
	public float equiptheViewAreaLength = 4f;//视野长度，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
	public float equiptheViewAreaAngel = 30f;//视野的角度，同样，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
	//加成属性-------------------------------------------------------------------------------------------------------------------------
	//这件装备能够带来的特效名字
	//这些特效也会是effectBasic的脚本
	//当然，可以有多个被动技能
	public string [] theEffectNames ;

	//获得装备
	public void GetThisThing(PlayerBasic thePlayer)
	{
		//最基本的属性生命法力和名字
		thePlayer.ActerHpMax += equipActerHpMax;//这个人物的生命上限
		thePlayer.ActerSpMax += equipActerSpMax;//这个人物的法力上限
		thePlayer.ActerHpUp += equipActerHpUp;//人物生命恢复
		thePlayer.ActerSpUp += equipActerSpUp;//人物法力回复
		//特殊战斗属性
		thePlayer.ActerSuperBaldePercent += equipActerSuperBaldePercent;//这个人物的暴击率
		thePlayer.ActerSuperBaldeAdder += equipActerSuperBaldeAdder;//暴击时伤害的倍数
		thePlayer.ActerAttackAtPercent += equipActerAttackAtPercent;//这个人物的命中率
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
		thePlayer.CActerAttackAtPercent += equipActerAttackAtPercent;//这个人物的命中率
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
		thePlayer.ActerAttackAtPercent -= equipActerAttackAtPercent;//这个人物的命中率
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
		thePlayer.CActerAttackAtPercent -= equipActerAttackAtPercent;//这个人物的命中率
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
		//接下来是一些私有的战斗属性备份，用于计算值（作为例如护甲值提升10%这种的参数值计算）
		//因为是私有方法，所以还要给出获取这个值和修改这个值的方法
		//总体上讲，这些值是战斗属性的备份值，当有特殊计算方法的时候作为参数计算更新战斗属性
		//例如 战斗属性 = 备份值 *1.1f
		//顺带一提，之所以使用私有方法是因为不想让共有属性表太长，此外这些私有只会在特殊情况之下服务器才会使用
		thePlayer.CtheAttackAreaLength -= equiptheAttackAreaLength;//攻击范围（非常重要，同时这个是简化版本的每一种攻击招式分开计算范围的方式）
		thePlayer.CtheAttackAreaAngel -= equiptheAttackAreaAngel;//攻击范围的角度，自身前方锥形范围内都是攻击范围
		thePlayer.CtheViewAreaLength -= equiptheViewAreaLength;//视野长度，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
		thePlayer.CtheViewAreaAngel -= equiptheViewAreaAngel;//视野的角度，同样，在不同的模式之下。例如暗夜模式，是很有需要实际的地方的
	}

	//添加特效
	private void addEffects(PlayerBasic thePlayer)
	{
		//所有脚本都确定只是唯一被动脚本存在一个
		//可叠加被动就用updateEffect来做了
		for (int i = 0; i < theEffectNames.Length; i++)
		{
			System.Type theType = System.Type.GetType (theEffectNames[i]);
			if (!thePlayer.gameObject.GetComponent ( theType))
				thePlayer.gameObject.AddComponent (theType);
			else
				((effectBasic)thePlayer.GetComponent (theType)).updateEffect ();
		}
	}

	//消除特效
	//实际上应该装备栏中所有的这个装备都不要了这个效果才会消失
	//但是因为装备已经有了明确的分类，这里就算是有一点简化
	//当然具体要做的时候还是应该参照thePlayer的装备容器，所以后面这种判断再行补上
	private void dropEffects(PlayerBasic thePlayer)
	{
		for(int i = 0 ; i < theEffectNames.Length ; i++)
		  Destroy (thePlayer.gameObject.GetComponent ( System.Type.GetType (theEffectNames[i])));
	}

}
