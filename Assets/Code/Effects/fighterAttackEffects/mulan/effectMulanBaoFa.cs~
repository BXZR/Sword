using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectMulanBaoFa : effectBasic
{
	public float superBladePercentAdd = 0.20f;
	public float attackSpeedAdd = 0.20f;
	public float hpDamagePercent = 0.35f;//暴击额外伤害转化生命
	public float DamageUseMax = 60;//伤害中生效的部分上限
	public float timeCoolingMinus =1.25f;//冷却时间使用就减少冷却时间
	public float spAdder = 10f;//冷却中使用时候返还的斗气
	GameObject theEffect;//特效
	private GameObject theEffectProfab;//预设物引用保存

	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{

		timerForEffect = 9f;//生效时间
		lifeTimerAll = 35f;//总持续时间，也是冷却时间
		theEffectName = "裂天";
		//注意的是，最大生命值每回合都会更新的，这个最大生命值的削弱仅仅限制于本回合(如果削减最大斗气值就太变态了)
		theEffectInformation = "获得" + superBladePercentAdd * 100 + "%暴击率和"+attackSpeedAdd*100+"%攻击速度，持" +
			"续" + timerForEffect + "秒\n暴击时造成额外伤害的" + hpDamagePercent * 100 + "%将转化为自身生命\n每一击最多额外恢复" + DamageUseMax * hpDamagePercent +"生命，冷却时间" + (lifeTimerAll-timerForEffect) + "秒";
		theEffedctExtraInformation = "特性：飞鸟，冷却中可以使用这个技能\n效果为减少冷却时间"+ timeCoolingMinus+"秒,并返还"+ spAdder +"斗气";
		makeStart ();
		if (this.thePlayer) 
		{
			if(!theEffectProfab)
				theEffectProfab = Resources.Load<GameObject> ("effects/mulanBaoFA");
			
			theEffect = GameObject.Instantiate<GameObject> (theEffectProfab);
			theEffect.transform.SetParent (this.thePlayer.transform);
			theEffect.transform.localPosition = new Vector3 (0, 1.25f, 0);

			this.thePlayer.ActerSuperBaldePercent += superBladePercentAdd;
			this.thePlayer.CActerSuperBaldePercent += superBladePercentAdd;
			this.thePlayer.ActerAttackSpeedPercent += attackSpeedAdd;
			this.thePlayer.CActerAttackSpeedPercent += attackSpeedAdd;
		}
	}

	public override void OnSuperBlade (PlayerBasic aim, float Damage = 0)
	{
		float hpup = Mathf.Clamp ((Damage - this.thePlayer.ActerWuliDamage),0,DamageUseMax)*hpDamagePercent ;
		this.thePlayer.ActerHp += hpup;
		//附加的各种效果
		effectBasic [] effects = this.thePlayer.GetComponents<effectBasic> ();
		foreach (effectBasic EF in effects)
			EF.OnHpUp (hpup);
	}

 
	public override void effectOnUpdateTime ()
	{
		addTimer ();
		if (isEffecting && timerForAdd > timerForEffect) 
		{
			isEffecting = false;
			this.thePlayer.ActerSuperBaldePercent -= superBladePercentAdd;
			this.thePlayer.CActerSuperBaldePercent -= superBladePercentAdd;
			this.thePlayer.ActerAttackSpeedPercent -= attackSpeedAdd;
			this.thePlayer.CActerAttackSpeedPercent -= attackSpeedAdd;
			Destroy (theEffect);
		}
		if(timerForAdd > lifeTimerAll)
		{
			Destroy (this);
		}
	}

	public override void updateEffect ()
	{
		timerForAdd += timeCoolingMinus;//减少冷却时间
		thePlayer.ActerSp += spAdder;
	}
}
