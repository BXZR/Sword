using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectShowDamageOnBeAttack : effectBasic
{

	//受到攻击时候显示受到了多少伤害
	private extraMoveUp theShowText;//生成的物体的保存
	private extraMoveUp theShowTextForUp;//生成的物体的保存

	private bool isOn = true;//是否开启显示，死了或者其他的时候不显示
	private float showTimer =0.5f;//显示时间，多受到一次攻击就多显示一会

	public Color theShowColorForEMYamage = Color.yellow;//显示的颜色，因为打出的伤害和受到的伤害应该是不同的
	public Color theShowColorForSELFDamage = Color.red;//显示的颜色，自己受到伤害的时候用另一个颜色显示
	public Color theShowColorForSELFUp = Color.green;//显示的颜色，回血效果

	public  void makeShowForHpUp(float hpup = 0)
	{
		if (theShowTextForUp == null || !theShowTextForUp.gameObject.activeInHierarchy ) 
		{
			GameObject theOBJ = systemValues.getTextFromTextPool();
			theShowTextForUp = theOBJ.GetComponent<extraMoveUp> ();
			reSetText (theShowTextForUp  , hpup.ToString("f0") , 2);
		}
		else 
		{
			theShowTextForUp.makeUpdate (hpup);
		}
	}

	//Color.yellow被认为是默认颜色，与null的功效一样
	public void makeShowTextExtra(string text , int ColorNumber = 0)
	{
		GameObject 	extraShow = systemValues.getTextFromTextPool();
		reSetText (extraShow.GetComponent<extraMoveUp> () , text , ColorNumber);
	}

	public void makeShowForDamage(float damage = 0)
	{
		if (damage <= 0)
			return;

		if (theShowText == null || !theShowText.gameObject.activeInHierarchy) 
		{
			GameObject theOBJ = systemValues.getTextFromTextPool();
			theShowText = theOBJ.GetComponent<extraMoveUp> ();
			int colorNumber = this.thePlayer == systemValues.thePlayer ? 1 : 0;
			reSetText (theShowText,damage.ToString("f0"),colorNumber);
		}
		else
		{
			theShowText .makeUpdate (damage);
		}

	}


	public void reSetText(extraMoveUp theMoveEffect , string theText , int colorNumber = 0)
	{
		theMoveEffect.transform.position = this.thePlayer.transform.position + new Vector3 (0, 1f, 0);
		Vector3 theTextMoveAim = this.thePlayer.transform.position + new Vector3 (Random.Range (0f, 0.5f) - 0.25f, 1.75f, 0);
		theMoveEffect.makeStart (theTextMoveAim, theText, showTimer);
		theMoveEffect.makeColor (colorNumber);
	}

	public override void OnDead ()
	{
		isOn = false;
		//if (theShowText)
		//	Destroy (theShowText);
	}

	public override bool isShowing ()
	{
		return false;
	}

	public override bool isExtraUse ()
	{
		return true;
	}

	public override void OnBeAttack (float damage = 0)
	{
		if (isOn)
			makeShowForDamage(damage);
	}


	public override void OnHpUp (float upValue = 0)
	{
		//生命胡恢复效果的标识是需要门限的
		if(isOn && upValue > 5)
		    makeShowForHpUp (upValue);
	}

	public override void OnSuperBlade (PlayerBasic aim, float Damage = 0)
	{
		makeShowTextExtra("暴击"+thePlayer.ActerSuperBaldeAdder*100 +"%!");
	}

	public override void OnMiss (PlayerBasic attacker)
	{
		makeShowTextExtra("闪避!");
	}
	public override void OnShield (PlayerBasic attacker, float damageMinus = 0)
	{
		makeShowTextExtra("格挡" + damageMinus.ToString("f0") +"伤害!");
	}
	public override void OnDoNotAttackAt (PlayerBasic aim)
	{
		makeShowTextExtra("攻击未命中" , 3);
	}
	public override void OnAddSoul (int soulCount)
	{
		makeShowTextExtra("获得"+soulCount+"灵力" , 0);
	}

	void Start ()
	{
		makeStart ();
	}
}
