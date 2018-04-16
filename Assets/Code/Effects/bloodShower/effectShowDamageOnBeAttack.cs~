using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectShowDamageOnBeAttack : effectBasic
{

	//受到攻击时候显示受到了多少伤害
	private GameObject theShowTextProfab;//预设物保存
	private GameObject theShowText;//生成的物体的保存
	private GameObject theShowTextForUp;//生成的物体的保存

	private bool isOn = true;//是否开启显示，死了或者其他的时候不显示
	private float showTimer =0.5f;//显示时间，多受到一次攻击就多显示一会

	public Color theShowColorForEMYamage = Color.yellow;//显示的颜色，因为打出的伤害和受到的伤害应该是不同的
	public Color theShowColorForSELFDamage = Color.red;//显示的颜色，自己受到伤害的时候用另一个颜色显示
	public Color theShowColorForSELFUp = Color.green;//显示的颜色，回血效果

	public  void makeShowForHpUp(float hpup = 0)
	{
		//建立对象
		if (theShowTextProfab == null)
			theShowTextProfab = Resources.Load <GameObject>("effects/bloodText");
		if (theShowTextForUp == null) 
		{
			theShowTextForUp = GameObject.Instantiate (theShowTextProfab);
			theShowTextForUp.transform.position = this.thePlayer.transform.position + new Vector3 (Random.Range (0f, 0.5f) - 0.25f, 0.75f, 0);
			//theShowText.transform.SetParent (thePlayer.transform);//作为可选选项先放在这里
			//初始化和重新构建
			Vector3 theTextMoveAim = this.thePlayer.transform.position + new Vector3 (0, 1.5f, 0);
			extraMoveUp theMoveEffect = theShowTextForUp.GetComponent<extraMoveUp> ();
			theMoveEffect.makeStart (theTextMoveAim, hpup, showTimer);
			theMoveEffect.makeColor (2);
		}
		else 
		{
			extraMoveUp theMoveEffect = theShowTextForUp.GetComponent<extraMoveUp> ();
			theMoveEffect.makeUpdate (hpup);
		}

	}

	//Color.yellow被认为是默认颜色，与null的功效一样
	public void makeShowTextExtra(string text , int ColorNumber = 0)
	{
		//建立对象
		if (theShowTextProfab == null)
			theShowTextProfab = Resources.Load <GameObject>("effects/bloodText");

	    GameObject 	extraShow = GameObject.Instantiate (theShowTextProfab);
	    extraShow .transform.position = this.thePlayer.transform.position + new Vector3 (Random.Range (0f, 0.5f) - 0.25f, 0.75f, 0);
		//theShowText.transform.SetParent (thePlayer.transform);//作为可选选项先放在这里
		//初始化和重新构建
		Vector3 theTextMoveAim = this.thePlayer.transform.position + new Vector3 (0, 1.5f, 0);
	    extraMoveUp theMoveEffect = extraShow.GetComponent<extraMoveUp> ();
	    theMoveEffect.makeStart (theTextMoveAim, text, showTimer);

		theMoveEffect.makeColor (ColorNumber);
	}

	public void makeShowForDamage(float damage = 0)
	{
		//建立对象
		if (theShowTextProfab == null)
			theShowTextProfab = Resources.Load <GameObject>("effects/bloodText");
		if (damage > 0) 
		{
			if (theShowText == null) 
			{
				theShowText = GameObject.Instantiate (theShowTextProfab);
				theShowText.transform.position = this.thePlayer.transform.position + new Vector3 (0, 0.75f, 0);
				//theShowText.transform.SetParent (thePlayer.transform);//作为可选选项先放在这里
				//初始化和重新构建
				Vector3 theTextMoveAim = this.thePlayer.transform.position + new Vector3 (Random.Range (0f, 0.5f) - 0.25f, 1.5f, 0);
				extraMoveUp theMoveEffect = theShowText.GetComponent<extraMoveUp> ();
				theMoveEffect.makeStart (theTextMoveAim, damage, showTimer);
				if (this.thePlayer == systemValues.thePlayer) 
				{
					//theShowText.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);//默认大小是0.5,0.5,0.5
					theMoveEffect.makeColor (1);
				} 
				else
					theMoveEffect.makeColor (0);
			}
			else
			{
				extraMoveUp theMoveEffect = theShowText.GetComponent<extraMoveUp> ();
				theMoveEffect.makeUpdate (damage);
			}
		}
	}

	public override void OnDead ()
	{
		isOn = false;
		if (theShowText)
			Destroy (theShowText);
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
		{
			makeShowForDamage(damage);
		}
	}


	public override void OnHpUp (float upValue = 0)
	{
		if (isOn)
		{
			//生命胡恢复效果的标识是需要门限的
			if(upValue > 5)
			makeShowForHpUp (upValue);
		}
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
		makeShowTextExtra("本次攻击未命中" , 3);
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
