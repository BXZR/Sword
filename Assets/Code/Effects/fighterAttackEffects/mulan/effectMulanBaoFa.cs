using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectMulanBaoFa : effectBasic
{
	public float superBladePercentAdd = 0.25f;
	public float effectTimer = 12f;//生效时间
	public float lifeTimer = 30f;//总持续时间，也是冷却时间
	public float hpDamagePercent = 0.20f;//暴击额外伤害转化生命
	public float DamageUseMax = 60;//伤害中生效的部分上限
	bool opened = true;//是否开启
	public float timeCoolingMinus =2f;//冷却时间使用就减少冷却时间
	public float spAdder = 10f;//冷却中使用时候返还的斗气
	GameObject theEffect;//特效

	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{
		theEffectName = "裂天";
		//注意的是，最大生命值每回合都会更新的，这个最大生命值的削弱仅仅限制于本回合(如果削减最大斗气值就太变态了)
		theEffectInformation = "额外获得" + superBladePercentAdd * 100 + "%的暴击率，持" +
		"续" + effectTimer + "秒\n并且，在暴击时造成额外伤害的" + hpDamagePercent * 100 + "%(最多" + DamageUseMax * hpDamagePercent + ")将转化为自身生命,冷却时间" + lifeTimer + "秒";
		theEffedctExtraInformation = "(特性：裂天奏，冷却中使用这个技能可以减少冷却时间"+ timeCoolingMinus+"秒,并返还"+ spAdder +"斗气)";
		makeStart ();
		if (this.thePlayer) 
		{
			theEffect = GameObject.Instantiate<GameObject> (Resources.Load<GameObject> ("effects/mulanBaoFA"));
			theEffect.transform.SetParent (this.thePlayer.transform);
			theEffect.transform.localPosition = new Vector3 (0, 1.25f, 0);

			this.thePlayer.ActerSuperBaldePercent += superBladePercentAdd;
			this.thePlayer.CActerSuperBaldePercent += superBladePercentAdd;
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
		effectTimer -= systemValues.updateTimeWait;
		if (opened && effectTimer < 0) 
		{
			opened = false;
			this.thePlayer.ActerSuperBaldePercent -= superBladePercentAdd;
			this.thePlayer.CActerSuperBaldePercent -= superBladePercentAdd;
			Destroy (theEffect);
		}
		lifeTimer -= systemValues.updateTimeWait;
		if (lifeTimer < 0)
		{
			Destroy (this);
		}
	}

	public override void updateEffect ()
	{
		lifeTimer -= 2f;
		thePlayer.ActerSp += spAdder;
	}
}
