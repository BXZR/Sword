using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectShowDamageOnBeAttack : effectBasic
{

	//受到攻击时候显示受到了多少伤害
	private GameObject theShowTextProfab;//预设物保存
	private GameObject theShowText;//生成的物体的保存
	private float damageAll = 0;//，记录下来收到的总伤害，用于更新
	private bool isOn = true;//是否开启显示，死了或者其他的时候不显示

	private float showTimer = 1f;//显示时间，多受到一次攻击就多显示一会
	private Vector3 theTextMoveAim ;//这个3dtext的移动目标，移动到某地方之后就不再移动了

	private void makeShow(float damage = 0)
	{
		if (theShowTextProfab == null)
		{
			theShowTextProfab = Resources.Load <GameObject>("effects/bloodText");
		}
		if (theShowText == null) 
		{
			theShowText = GameObject.Instantiate (theShowTextProfab);
			theShowText.transform.position = this.thePlayer.transform.position + new Vector3 (0,0.3f,0);
			//theShowText.transform.SetParent (thePlayer.transform);//作为可选选项先放在这里
			theTextMoveAim = this.thePlayer.transform.position + new Vector3 (0,1.5f,0);
			showTimer = 1.5f;
		}
		else
		{
			showTimer += 0.25f;
			theShowText.transform.Translate (new Vector3 (0,1,0) * -0.1f);
		}
		damageAll += damage;
		theShowText.GetComponentInChildren <TextMesh> ().text = damageAll.ToString ("f0");
	}

	private void makeFlash()
	{
		damageAll = 0;
		Destroy (theShowText);
	}

	public override void OnDead ()
	{
		isOn = false;
		if (theShowText)
			Destroy (theShowText);
	}

	public override bool isExtraUse ()
	{
		return true;
	}

	public override void OnBeAttack (float damage = 0)
	{
		if (isOn)
		{
			makeShow (damage);
		}
	}

	void Start ()
	{
		makeStart ();
	}
	void Update ()
	{
		if (theShowText != null) 
		{
			theShowText.transform.LookAt (Camera.main.transform);
			theShowText.transform.position = Vector3.Lerp (	theShowText.transform.position ,theTextMoveAim , Time.deltaTime/2 );
			showTimer -= Time.deltaTime;
			if (showTimer < 0)
			{
				makeFlash ();
			}
		}
	}
}
